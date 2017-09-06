using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static avalanchain.Common.MatchingEngine;
using avalanchain.Common;

namespace avalanchain.Web.Services
{
    /// <summary>
    /// NOT USED. See Facade.MatchingService in F# project
    /// </summary>
    public class ExchangeService
    {
        private OrderStack orderStack;
        private List<OrderCommand> orderCommands;

        public ExchangeService()
        {
            orderStack = OrderStack.Create(1);
            var mockCommand = OrderCommand.NewCreate(orderData);
            orderCommands = new List<OrderCommand>
            {
                mockCommand
            };
            ProcessOrderCommand(mockCommand);
        }

        private void ProcessOrderCommand(OrderCommand orderCommand)
        {
            //var processResult = orderStack.AddOrder(OrderCommand.NewCreate(orderCommand));
        }

        public IEnumerable<OrderCommand> OrderCommands(UInt64 startIndex, uint pageSize)
        {
            //orderCommands.AddFirst
            return orderCommands.Skip((int) startIndex).Take((int) pageSize);
        }

        public void SubmitOrder(OrderCommand orderCommand)
        {
            orderCommands.Add(orderCommand);
            ProcessOrderCommand(orderCommand); // Add error handling
        }



        /// <summary>
        /// /////
        /// </summary>

        public static ExchangeService Instance = new ExchangeService();
    }
}
