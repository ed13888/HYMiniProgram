using HYMiniProgram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HYMiniProgram
{
    public class OrderManager
    {
        public static readonly OrderManager Instance = null;
        static OrderManager()
        {
            Instance = new OrderManager();
        }

        public Order SearchOrder(string orderNo)
        {
            return new Order();
        }

        public bool InsertOrder(Order m)
        {
            return true;
        }

        public bool UpdateOrderState(string beginTime, string endTime, string state)
        {
            return true;
        }

    }
}