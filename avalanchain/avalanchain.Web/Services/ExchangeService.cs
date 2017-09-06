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

    public class ExchangeService
    {
        //private object ExchangeState;
        private LinkedList<OrderCommand> orderCommands;

        public ExchangeService()
        {
            orderCommands = new LinkedList<OrderCommand>();
            orderCommands.AddFirst(OrderCommand.NewCreate(orderData));
        }

        private void ProcessOrderCommand(OrderCommand orderCommand)
        {

        }

        public IEnumerable<OrderCommand> OrderCommands(int pageSize)
        {
            //orderCommands.AddFirst
            return orderCommands.Take(pageSize);
        }

        public void SubmitOrder(OrderCommand orderCommand)
        {
            orderCommands.AddFirst(orderCommand);
            ProcessOrderCommand(orderCommand); // Add error handling
        }



        /// <summary>
        /// /////
        /// </summary>

        public static ExchangeService Instance = new ExchangeService();
    }
}
