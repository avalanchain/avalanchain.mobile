using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using avalanchain.Web.Services;
using static avalanchain.Common.MatchingEngine;
using Microsoft.FSharp.Core;
using Microsoft.FSharp.Collections;

namespace avalanchain.Web
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ExchangeController : Controller
    {
        const uint maxPageSize = 50;

        private Symbol ToSymbol(string symbol) => string.IsNullOrWhiteSpace(symbol) ? throw new ArgumentException("Invalid Symbol string") : Symbol.NewSymbol(symbol);

        private static Facade.MatchingService ExchangeService = Facade.MatchingService.Instance; //ExchangeService.Instance;

        [HttpPost("[action]")]
        //[ProducesResponseType(typeof(TodoItem), 201)]
        //[ProducesResponseType(typeof(TodoItem), 400)]
        public IActionResult SubmitOrder([FromBody] OrderCommand orderCommand)
        {
            if (orderCommand == null)
            {
                return BadRequest();
            }
            ExchangeService.SubmitOrder(orderCommand);
            return Ok();
        }

        // ---

        [HttpGet("[action]")]
        public OrderStack OrderStack(string symbol)
        {
            return ExchangeService.OrderStack(ToSymbol(symbol));
        }

        [HttpGet("[action]")]
        public OrderStackView OrderStackView(string symbol, int maxDepth)
        {
            return ExchangeService.OrderStackView(ToSymbol(symbol), maxDepth > 0 ? maxDepth : 10);
        }

        [HttpGet("[action]")]
        public string MainSymbol()
        {
            return ExchangeService.MainSymbol.Item;
        }

        [HttpGet("[action]")]
        public string[] Symbols()
        {
            return ExchangeService.SymbolStrings;
        }

        [HttpGet("[action]")]
        public FSharpOption<Order> GetOrder(Guid orderID)
        {
            return ExchangeService.OrderById(orderID);
        }

        [HttpGet("[action]")]
        public string GetOrder2(string orderID)
        {
            return ExchangeService.OrderById2(orderID);
        }

        [HttpGet("[action]")]
        public FSharpMap<Guid, Order> GetOrders()
        {
            return ExchangeService.Orders();
        }

        // ---

        [HttpGet("[action]")]
        public IEnumerable<OrderCommand> OrderCommands(UInt64 startIndex, uint pageSize) => ExchangeService.OrderCommands(startIndex, pageSize > 0 && pageSize <= maxPageSize ? pageSize : maxPageSize);

        [HttpGet("[action]")]
        public IEnumerable<OrderEvent> OrderEvents(UInt64 startIndex, uint pageSize) => ExchangeService.OrderEvents(startIndex, pageSize > 0 && pageSize <= maxPageSize ? pageSize : maxPageSize);

        [HttpGet("[action]")]
        public IEnumerable<Order> FullOrders(UInt64 startIndex, uint pageSize) => ExchangeService.FullOrders(startIndex, pageSize > 0 && pageSize <= maxPageSize ? pageSize : maxPageSize);

        // ---

        [HttpGet("[action]")]
        public UInt64 OrderCommandsCount() => ExchangeService.OrderCommandsCount;

        [HttpGet("[action]")]
        public UInt64 OrderEventsCount() => ExchangeService.OrderEventsCount;

        [HttpGet("[action]")]
        public UInt64 FullOrdersCount() => ExchangeService.FullOrdersCount;

        // ---

        [HttpGet("[action]")]
        public IEnumerable<OrderCommand> LastOrderCommands(uint pageSize) => ExchangeService.LastOrderCommands(pageSize > 0 && pageSize <= maxPageSize ? pageSize : maxPageSize);

        [HttpGet("[action]")]
        public IEnumerable<OrderEvent> LastOrderEvents(uint pageSize) => ExchangeService.LastOrderEvents(pageSize > 0 && pageSize <= maxPageSize ? pageSize : maxPageSize);

        [HttpGet("[action]")]
        public IEnumerable<Order> LastFullOrders(uint pageSize) => ExchangeService.LastFullOrders(pageSize > 0 && pageSize <= maxPageSize ? pageSize : maxPageSize);

        // ---

        [HttpGet("[action]")]
        public IEnumerable<OrderCommand> SymbolOrderCommands(string symbol, UInt64 startIndex, uint pageSize) => ExchangeService.SymbolOrderCommands(ToSymbol(symbol), startIndex, pageSize > 0 && pageSize <= maxPageSize ? pageSize : maxPageSize);

        [HttpGet("[action]")]
        public IEnumerable<OrderEvent> SymbolOrderEvents(string symbol, UInt64 startIndex, uint pageSize) => ExchangeService.SymbolOrderEvents(ToSymbol(symbol), startIndex, pageSize > 0 && pageSize <= maxPageSize ? pageSize : maxPageSize);

        [HttpGet("[action]")]
        public IEnumerable<Order> SymbolFullOrders(string symbol, UInt64 startIndex, uint pageSize) => ExchangeService.SymbolFullOrders(ToSymbol(symbol), startIndex, pageSize > 0 && pageSize <= maxPageSize ? pageSize : maxPageSize);

        // ---

        [HttpGet("[action]")]
        public UInt64 SymbolOrderCommandsCount(string symbol) => ExchangeService.SymbolOrderCommandsCount(ToSymbol(symbol));

        [HttpGet("[action]")]
        public UInt64 SymbolOrderEventsCount(string symbol) => ExchangeService.SymbolOrderEventsCount(ToSymbol(symbol));

        [HttpGet("[action]")]
        public UInt64 SymbolFullOrdersCount(string symbol) => ExchangeService.SymbolFullOrdersCount(ToSymbol(symbol));

        // ---

        [HttpGet("[action]")]
        public IEnumerable<OrderCommand> SymbolLastOrderCommands(string symbol, uint pageSize) => 
            ExchangeService.SymbolLastOrderCommands(ToSymbol(symbol), pageSize > 0 && pageSize <= maxPageSize ? pageSize : maxPageSize);

        [HttpGet("[action]")]
        public IEnumerable<OrderEvent> SymbolLastOrderEvents(string symbol, uint pageSize) => 
            ExchangeService.SymbolLastOrderEvents(ToSymbol(symbol), pageSize > 0 && pageSize <= maxPageSize ? pageSize : maxPageSize);

        [HttpGet("[action]")]
        public IEnumerable<Order> SymbolLastFullOrders(string symbol, uint pageSize) => 
            ExchangeService.SymbolLastFullOrders(ToSymbol(symbol), pageSize > 0 && pageSize <= maxPageSize ? pageSize : maxPageSize);



        //[HttpPost]
        //[ProducesResponseType(typeof(TodoItem), 201)]
        ////[ProducesResponseType(typeof(TodoItem), 400)]
        //public IActionResult Create([FromBody] TodoItem item)
        //{
        //    if (item == null)
        //    {
        //        return BadRequest();
        //    }

        //    _context.TodoItems.Add(item);
        //    _context.SaveChanges();

        //    return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        //}

        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
