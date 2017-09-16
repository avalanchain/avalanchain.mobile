#r @"System.dll"
#r @"System.Core.dll"
#r @"..\packages\Newtonsoft.Json.9.0.1\lib\net45\Newtonsoft.Json.dll"

#load "FSharpLu.Json.Helpers.fs"
#load "FSharpLu.Json.WithFunctor.fs"
#load "FSharpLu.Json.Default.fs"
#load "FSharpLu.Json.Compact.fs"
#load "FSharpLu.Json.BackwardCompatible.fs"

#load "MatchingEngine.fs"

open System
open FSharpLu.Json

open avalanchain.Common.MatchingEngine

let toBid p q = { orderData with OrderType = Limit p; Quantity = q }
let toAsk p q = { aorderData with OrderType = Limit p; Quantity = q }
let toPQ o = o.Price, o.Quantity
let toLPQ = List.map toPQ

let orders (bm, bc) (am, ac) = 
    [for i in bm .. 1M .. bc -> toBid (100M<price> - i * 5M<price>) 10M<qty> ]
    @ [for i in am .. 1M .. ac -> toAsk (100M<price> + i * 5M<price>) 10M<qty> ]

let test ords (expBid, expAsk) =
    let sym = Symbol "AVC"
    let ms = Facade.MatchingService(5M<price>, false)
    ords
    |> List.map OrderCommand.Create
    |> List.iter ms.SubmitOrder

    //ms.OrderStack(sym)
    let stackView = ms.OrderStackView sym 10
    let bids = stackView.BidOrders |> toLPQ
    let asks = stackView.AskOrders |> toLPQ
    let retb, reta = bids = expBid, asks = expAsk
    if not retb then 
        printfn "Bid View: %A" bids
        printfn "Bid Det View: %A" stackView.BidOrders
        printfn "Bid Orders: %A" ((ms.OrderStack sym).BidOrders)
    if not reta then 
        printfn "Ask View: %A" bids
        printfn "Ask Det View: %A" stackView.AskOrders
        printfn "Ask Orders: %A" ((ms.OrderStack sym).AskOrders)
    retb, reta

let blankB1 = [ 95M<price>, 10M<qty> ]

let blankA1 = [ 105M<price>, 10M<qty> ]

let blankB2 = [ 95M<price>, 10M<qty>
                90M<price>, 10M<qty> ]

let blankA2 = [ 105M<price>, 10M<qty>
                110M<price>, 10M<qty> ]

let blankB5 = [ 95M<price>, 10M<qty>
                90M<price>, 10M<qty>
                85M<price>, 10M<qty>
                80M<price>, 10M<qty>
                75M<price>, 10M<qty> ]

let blankA5 = [ 105M<price>, 10M<qty>
                110M<price>, 10M<qty>
                115M<price>, 10M<qty>
                120M<price>, 10M<qty>
                125M<price>, 10M<qty> ]

let test0 = test [] ([], [])
let test5 = test (orders (1M, 5M) (1M, 5M)) (blankB5, blankA5) 

let testBidEmpty = test (orders (0M, -1M) (1M, 5M)) ([], blankA5) 
let testAskEmpty = test (orders (1M, 5M) (0M, -1M)) (blankB5, []) 

let testAskFull = test [toBid 105M<price> 10M<qty>
                        toAsk 100M<price> 10M<qty> ] ([], []) 

let testAskFull2 = test [   toBid 105M<price> 10M<qty>
                            toAsk 105M<price> 10M<qty> ] ([], []) 

let testAskOver = test [toBid 105M<price> 10M<qty>
                        toAsk 100M<price> 20M<qty> ] ([], [100M<price>, 10M<qty>]) 

let testAskOver2 = test [   toBid 105M<price> 10M<qty>
                            toAsk 110M<price> 20M<qty> 
                            toAsk 100M<price> 15M<qty> ] ([], [ 100M<price>, 5M<qty>
                                                                110M<price>, 20M<qty>])                         

let testAskTopup = test [   toBid 100M<price> 10M<qty> 
                            toAsk 105M<price> 20M<qty>
                            toAsk 105M<price> 20M<qty> ] ([100M<price>, 10M<qty>], [105M<price>, 40M<qty>]) 

let testAskTopup2 = test [  toBid 95M<price> 10M<qty> 
                            toAsk 100M<price> 20M<qty>
                            toAsk 110M<price> 20M<qty> 
                            toAsk 105M<price> 10M<qty> ] ([95M<price>, 10M<qty>], [ 100M<price>, 20M<qty>
                                                                                    105M<price>, 10M<qty>
                                                                                    110M<price>, 20M<qty>
                                                                                    ]) 

let testBidFull = test [toAsk 100M<price> 10M<qty> 
                        toBid 105M<price> 10M<qty> ] ([], []) 

let testBidFull2 = test [   toAsk 105M<price> 10M<qty>
                            toBid 105M<price> 10M<qty> ] ([], []) 

let testBidOver = test [toAsk 100M<price> 10M<qty> 
                        toBid 105M<price> 20M<qty> ] ([105M<price>, 10M<qty>], []) 

let testBidTopup = test [   toAsk 105M<price> 10M<qty> 
                            toBid 100M<price> 20M<qty>
                            toBid 100M<price> 20M<qty> ] ([100M<price>, 40M<qty>], [105M<price>, 10M<qty>]) 

let testBidTopup2 = test [  toAsk 105M<price> 10M<qty> 
                            toBid 100M<price> 20M<qty>
                            toBid 90M<price> 20M<qty> 
                            toBid 95M<price> 10M<qty> ] ([  100M<price>, 20M<qty>
                                                            95M<price>, 10M<qty>
                                                            90M<price>, 20M<qty>
                                                            ], [105M<price>, 10M<qty>]) 
                            
let testBidIssue = test [   toBid 570M<price> 27M<qty> 
                            toBid 580M<price> 45M<qty>
                            toBid 570M<price> 57M<qty> ] ([ 580M<price>, 45M<qty>
                                                            570M<price>, 84M<qty>
                                                            ], [])

let ms = Facade.MatchingService(5M<price>, false)
let rnd = Random()
//for i in 0 .. rnd.Next(200, 2000) do
let step() =
    for i in 0 .. 10000 do
        for sym in [ "AVC" ] do 
            let sym = Symbol sym
            let side = if rnd.Next(10) < 5 then MarketSide.Bid else MarketSide.Ask
            let quantity = decimal(rnd.Next(2, 100)) * 1M<qty>
            let st = ms.OrderStack(sym)
            let p = match side, st.BidOrders, st.AskOrders with
                        | _, [], [] -> (decimal(rnd.Next(75, 200)) * st.PriceStep)
                        | MarketSide.Bid, b, [] -> (b.Head.Price + decimal(rnd.Next(6) - 3) * st.PriceStep)
                        | MarketSide.Bid, _, a -> 
                            let r = rnd.Next(6) 
                            if r < 2 then a.Head.Price else a.Head.Price + decimal(r) * st.PriceStep
                        | MarketSide.Ask, [], a -> (a.Head.Price + decimal(rnd.Next(6) - 3) * st.PriceStep)
                        | MarketSide.Ask, b, _ -> 
                            let r = rnd.Next(6) 
                            if r < 2 then b.Head.Price else b.Head.Price - decimal(r) * st.PriceStep

            printfn "s p q: %A %A %A" side p quantity
            { orderData with    Symbol = sym 
                                MarketSide = side
                                OrderType = Limit (p)
                                Quantity = quantity
                } |> OrderCommand.Create |> ms.SubmitOrder 

step()
let st = ms.OrderStackView(Symbol "AVC") 100
st.BidOrders.Length
st.AskOrders.Length

let stt = ms.OrderStack(Symbol "AVC") 



let ms = Facade.MatchingService(5M<price>, false)
let rnd = Random()
for o in [orderData; orderData2; aorderData; aorderData2] do 
    //for i in 0 .. rnd.Next(200, 2000) do
    for i in 0 .. 100 do
        for sym in ["AVC" ] do 
            { o with    Symbol = Symbol sym 
                        OrderType = Limit (decimal(rnd.Next(100, 400)) * 1M<price>)
                        Quantity = decimal(rnd.Next(2, 100)) * 1M<qty>
                } |> OrderCommand.Create |> ms.SubmitOrder 

let st = ms.OrderStackView(Symbol "AVC") 100
st.BidOrders.Length