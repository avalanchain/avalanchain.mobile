using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using avalanchain.Web.Services;
using static avalanchain.Common.MatchingEngine;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace avalanchain.Web
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ExchangeController : Controller
    {
        private Facade.MatchingService ExchangeService = Facade.MatchingService.Instance; //ExchangeService.Instance;

        [HttpGet("[action]")]
        public IEnumerable<OrderCommand> OrderCommands(UInt64 startIndex, uint pageSize)
        {
            return ExchangeService.OrderCommands(startIndex, pageSize > 0 ? pageSize : 50);
        }

        [HttpGet("[action]")]
        public IEnumerable<OrderEvent> OrderEvents(UInt64 startIndex, uint pageSize)
        {
            return ExchangeService.OrderEvents(startIndex, pageSize > 0 ? pageSize : 50);
        }

        [HttpGet("[action]")]
        public IEnumerable<Order> FullOrders(UInt64 startIndex, uint pageSize)
        {
            return ExchangeService.FullOrders(startIndex, pageSize > 0 ? pageSize : 50);
        }

        [HttpGet("[action]")]
        public OrderStack OrderStack()
        {
            return ExchangeService.OrderStack;
        }

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
