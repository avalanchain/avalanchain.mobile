﻿namespace avalanchain.Common

module MatchingEngine = 

    open System
    //open System.Collections.Generic
    //open FSharpx.Collections

    type MatchType = | Partial | Full
    type MarketSide = | Bid | Ask
    with member __.Opposite = if __ = Bid then Ask else Bid
  

    type OrderID = Guid //uint64
    let newOrderID = Guid.NewGuid

    type ClOrdID = ClOrdID of string
    type Price = decimal<price>
    and [<Measure>] price
    type Quantity = decimal<qty>
    and [<Measure>] qty

    type Symbol = Symbol of string
    type TradingAccount = TradingAccount of string

    type OrderType = 
        // | Market 
        | Limit of Price
        // | Stop 
        // | StopLimit

    [<RequireQualifiedAccess>]
    type OrderCommand = 
    | Create of OrderData
    | Cancel of OrderID

    and OrderEvent =
    | Created of Order
    | CreationFailed of OrderData * Reason: string
    | Matched of Allocation * MatchType
    | Cancelled of OrderID
    | CancellationFailed of OrderID * Reason: string

    and OrderData = {
        OrderType: OrderType
        Symbol: Symbol 
        MarketSide: MarketSide
        Quantity: Quantity
        ClOrdID: ClOrdID
        Account: TradingAccount
        CreatedTime: DateTimeOffset // TODO: Change to vector clock
    }

    and Allocation = {
        From: OrderID
        To: OrderID
        Time: DateTimeOffset // TODO: Change to vector clock
        Quantity: Quantity
        Price: Price
    }
    and Order = {
        ID: OrderID
        OrderData: OrderData
        RemainingQuantity: Quantity
        AcceptenceTime: DateTimeOffset
        Allocations: Allocation list
    } with 
        member __.OrderType = __.OrderData.OrderType
        member __.Symbol = __.OrderData.Symbol
        member __.MarketSide = __.OrderData.MarketSide
        member __.ClOrdID = __.OrderData.ClOrdID
        member __.Account = __.OrderData.Account
        member __.OriginalQuantity = __.OrderData.Quantity
        member __.Price = match __.OrderData.OrderType with | Limit price -> price 
        member __.LastUpdateTime = if __.Allocations |> List.isEmpty then __.AcceptenceTime else __.Allocations.Head.Time
        member __.FilledQuantity = __.OriginalQuantity - __.RemainingQuantity
        member __.MatchType = if __.RemainingQuantity <= 0M<qty> then Full else Partial
        member __.FullyAllocated = __.MatchType = Full
        static member Create orderData = {  ID = newOrderID()
                                            OrderData = orderData
                                            RemainingQuantity = orderData.Quantity
                                            AcceptenceTime = DateTimeOffset.UtcNow
                                            Allocations = [] }
        static member Allocate order1 order2 quantity = 
            let time = DateTimeOffset.UtcNow
            let allocate o1 o2 price =
                let allocation = { From = o1.ID; To = o2.ID; Time = time; Quantity = quantity; Price = price }
                let newOrder = { o1 with    RemainingQuantity = o1.RemainingQuantity - quantity
                                            Allocations = allocation :: o1.Allocations }
                let event = Matched (allocation, newOrder.MatchType)
                newOrder, event
            (allocate order1 order2 order1.Price), (allocate order2 order1 order1.Price)

        member __.HasBetterPriceThan (y: Order) = 
            if __.MarketSide <> y.MarketSide then failwith "Trying to compare prices for orders on different market sides"
            if __.MarketSide = Ask then __.Price < y.Price else __.Price > y.Price


    // type OrderMatch = {
    //     OrderID: OrderID
    //     Price: Price
    //     OriginalOrderQuantity: Quantity
    //     MatchedQuantity: Quantity
    //     RemainingQuantity: Quantity
    //     Symbol: Symbol
    //     MatchType: MatchType
    //     ClOrdID: ClOrdID
    //     MarketSide: MarketSide
    //     Account: TradingAccount
    // }

    type PriceBucket = {
        Price: Price
        Orders: Map<OrderID, Order>
        OrderQueue: OrderID list
    }
    with 
        member __.Update order = { __ with Orders = __.Orders |> Map.add order.ID order }
        member __.PopHead() = 
            let head = __.OrderQueue.Head
            { __ with   Orders = __.Orders.Remove head
                        OrderQueue = __.OrderQueue.Tail  }
        member __.MatchFIFO order = 
            let rec matchFIFO o bucket events fullOrders =
                if bucket.OrderQueue.IsEmpty || o.RemainingQuantity <= 0M<qty> then
                    o, bucket, o.RemainingQuantity, events, fullOrders
                else 
                    let head = bucket.Orders.[bucket.OrderQueue.Head]
                    let remaining = min o.RemainingQuantity head.RemainingQuantity
                    let (head, e1), (o, e2) = Order.Allocate head o remaining
                    let bucket = bucket.Update head
                    let events = e1 :: e2 :: events
                    if o.FullyAllocated then 
                        if head.FullyAllocated then 
                            o, (bucket.PopHead()), 0M<qty>, events, head :: fullOrders
                        else o, bucket, 0M<qty>, events, fullOrders
                    else
                        matchFIFO o (bucket.PopHead()) events (head :: fullOrders)
            matchFIFO order __ [] []

        static member Create (order: Order) = 
            {   PriceBucket.Price = order.Price 
                Orders = [ order.ID, order ] |> Map.ofList
                OrderQueue = [ order.ID ] }

    let rec private insertOrder (op: Price -> Price -> bool) order skipped remaining = 
        match remaining with
        | [] -> 
            let pb = [PriceBucket.Create order]
            let sk = skipped |> List.rev
            sk @ pb
        | pb :: xs ->
            if pb.Price = order.Price then  [{ pb with  Orders = pb.Orders.Add(order.ID, order)
                                                        OrderQueue = pb.OrderQueue @ [ order.ID ] }]
            elif (op) pb.Price order.Price then insertOrder op order (pb :: skipped) xs
            else (skipped |> List.rev) @ ( (PriceBucket.Create order) :: remaining)

    let rec private matchOrder (op: Price -> Price -> bool) (order: Order) (matched: PriceBucket list) events fullOrders (remaining: PriceBucket list) = 
        match remaining with
        | [] -> order, matched, events, fullOrders, remaining
        | pb :: xs ->
            if op pb.Price order.Price then 
                let order, bucket, remainingQuantity, ets, fOrders = pb.MatchFIFO order
                let events = ets @ events
                let fullOrders = fOrders @ fullOrders
                let matched = bucket :: matched
                let remaining = xs
                if remainingQuantity > 0M<qty> then matchOrder op order matched events fullOrders remaining
                else order, matched, events, fullOrders, remaining
            else 
                order, matched, events, fullOrders, remaining


    type OrderStack = {
        BidOrders: PriceBucket list
        AskOrders: PriceBucket list
        PriceStep: Price
    }
    with 
        member __.AddOrder (order: Order) =
                match order.MarketSide with
                | Bid -> 
                    let order, matched, events, fullOrders, remaining = matchOrder (<=) order [] [] [] __.AskOrders
                    let askOrders = matched (*???*) @ remaining |> List.filter (fun pb -> pb.OrderQueue |> List.isEmpty |> not)
                    if order.FullyAllocated then { __ with AskOrders = askOrders }, events, fullOrders
                    else 
                        let bidOrders = insertOrder (>) order [] __.BidOrders
                        { __ with AskOrders = askOrders; BidOrders = bidOrders }, Created order :: events, fullOrders 
                | Ask -> 
                    let order, matched, events, fullOrders, remaining = matchOrder (>=) order [] [] [] __.BidOrders
                    let bidOrders = matched (*???*) @ remaining |> List.filter (fun pb -> pb.OrderQueue |> List.isEmpty |> not)
                    if order.FullyAllocated then { __ with BidOrders = bidOrders }, events, fullOrders
                    else 
                        let askOrders = insertOrder (<) order [] __.AskOrders
                        { __ with AskOrders = askOrders; BidOrders = bidOrders }, Created order :: events, fullOrders 
        static member Create priceStep = { PriceStep = priceStep; BidOrders = []; AskOrders = [] }

    let compactUnionJsonConverter =  new FSharpLu.Json.CompactUnionJsonConverter()


    let orderData = {
        OrderType = Limit 5M<price>
        Symbol = Symbol "AVC"
        MarketSide = Bid
        Quantity = 10M<qty>
        ClOrdID = ClOrdID "1"
        Account = TradingAccount "TRA1"
        CreatedTime = DateTimeOffset.UtcNow
    }

    let orderData2 = {
        OrderType = Limit 10M<price>
        Symbol = Symbol "AVC"
        MarketSide = Bid
        Quantity = 10M<qty>
        ClOrdID = ClOrdID "2"
        Account = TradingAccount "TRA1"
        CreatedTime = DateTimeOffset.UtcNow
    }

    let aorderData = {
        OrderType = Limit 15M<price>
        Symbol = Symbol "AVC"
        MarketSide = Ask
        Quantity = 15M<qty>
        ClOrdID = ClOrdID "3"
        Account = TradingAccount "TRA1"
        CreatedTime = DateTimeOffset.UtcNow
    }

    let aorderData2 = {
        OrderType = Limit 10M<price>
        Symbol = Symbol "AVC"
        MarketSide = Ask
        Quantity = 5M<qty>
        ClOrdID = ClOrdID "4"
        Account = TradingAccount "TRA1"
        CreatedTime = DateTimeOffset.UtcNow
    }

    let aorderData3 = {
        OrderType = Limit 2M<price>
        Symbol = Symbol "AVC"
        MarketSide = Ask
        Quantity = 27M<qty>
        ClOrdID = ClOrdID "4"
        Account = TradingAccount "TRA1"
        CreatedTime = DateTimeOffset.UtcNow
    }


    module Facade = 
        open System.Linq

        type SymbolStack = {
            Symbol: Symbol
            OrderStack: OrderStack
            Commands: ResizeArray<OrderCommand>
            Events: ResizeArray<OrderEvent>
            FullOrders: ResizeArray<Order>
        }
        with static member Create symbol priceStep = {  Symbol = symbol
                                                        OrderStack = OrderStack.Create priceStep
                                                        Commands = ResizeArray<_>() 
                                                        Events = ResizeArray<_>() 
                                                        FullOrders = ResizeArray<_>() }

        type MatchingService(priceStep) =
            let orderCommands = ResizeArray<OrderCommand>()
            let mutable symbolStackMap = Map.empty<Symbol, SymbolStack>
            let findSymbolStack symbol = match symbolStackMap.TryFind symbol with
                                            | Some ss -> ss
                                            | None -> SymbolStack.Create symbol priceStep

            //orderStack = OrderStack.Create priceStep
            let fullOrders = ResizeArray<Order>()
            let events = ResizeArray<OrderEvent>()
            let processCommand command = 
                match command with
                | OrderCommand.Create order -> 
                    orderCommands.Add command
                    let symbolStack = findSymbolStack order.Symbol
                    let newOrderStack, evts, orders = order |> Order.Create |> symbolStack.OrderStack.AddOrder 
                    let newSymbolStack = { symbolStack with OrderStack = newOrderStack }
                    newSymbolStack.Commands.Add command
                    newSymbolStack.Events.AddRange evts
                    newSymbolStack.FullOrders.AddRange orders
                    events.AddRange evts
                    fullOrders.AddRange orders
                    symbolStackMap <- symbolStackMap.Add (newSymbolStack.Symbol, newSymbolStack)
                | OrderCommand.Cancel oid -> failwith "Not supported yet"
            
            // TODO: Remove fakes:
            do for o in [orderData; orderData2; aorderData; aorderData2; aorderData3] do 
                for sym in ["AVC"; "USD"; "EUR"; "GBP"; "ICODAO"; "V1"; "V2" ] do 
                    { o with Symbol = Symbol sym } |> OrderCommand.Create |> processCommand 
            ///

            let getPage (lst: ResizeArray<_>) (startIndex: uint64) (pageSize: uint32) = lst.Skip(startIndex |> int).Take(pageSize |> int).ToArray()
            let getLastPage (lst: ResizeArray<_>) (pageSize: uint32) = 
                let pageSizeI = pageSize |> int
                if pageSizeI > lst.Count then lst.ToArray()
                else lst.Skip(lst.Count - pageSizeI - 1).Take(pageSizeI).ToArray()
            
            member __.SubmitOrder orderCommand = processCommand orderCommand

            member __.Symbols with get() = symbolStackMap |> Map.toSeq |> Seq.map fst
            member __.SymbolStrings = __.Symbols |> Seq.map(fun (Symbol s) -> s) |> Seq.toArray
            member __.OrderStack symbol = (findSymbolStack symbol).OrderStack

            member __.OrderCommands startIndex pageSize = getPage orderCommands startIndex pageSize
            member __.OrderEvents startIndex pageSize = getPage events startIndex pageSize
            member __.FullOrders startIndex pageSize = getPage fullOrders startIndex pageSize

            member __.LastOrderCommands pageSize = getLastPage orderCommands pageSize
            member __.LastOrderEvents pageSize = getLastPage events pageSize
            member __.LastFullOrders pageSize = getLastPage fullOrders pageSize

            member __.OrderCommandsCount with get() = orderCommands.LongCount() |> uint64
            member __.OrderEventsCount with get() = events.LongCount() |> uint64
            member __.FullOrdersCount with get() = fullOrders.LongCount() |> uint64

            member __.SymbolOrderCommands symbol startIndex pageSize = getPage (symbol |> findSymbolStack).Commands startIndex pageSize
            member __.SymbolOrderEvents symbol startIndex pageSize = getPage (symbol |> findSymbolStack).Events startIndex pageSize
            member __.SymbolFullOrders symbol startIndex pageSize = getPage (symbol |> findSymbolStack).FullOrders startIndex pageSize

            member __.SymbolLastOrderCommands symbol pageSize = getLastPage (symbol |> findSymbolStack).Commands pageSize
            member __.SymbolLastOrderEvents symbol pageSize = getLastPage (symbol |> findSymbolStack).Events pageSize
            member __.SymbolLastFullOrders symbol pageSize = getLastPage (symbol |> findSymbolStack).FullOrders pageSize

            member __.SymbolOrderCommandsCount symbol = (symbol |> findSymbolStack).Commands.LongCount() |> uint64 
            member __.SymbolOrderEventsCount symbol = (symbol |> findSymbolStack).Events.LongCount() |> uint64 
            member __.SymbolFullOrdersCount symbol = (symbol |> findSymbolStack).FullOrders.LongCount() |> uint64

            static member Instance = new MatchingService 1M<price>
