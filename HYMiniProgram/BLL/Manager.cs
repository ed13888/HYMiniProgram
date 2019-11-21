using HYMiniProgram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HYMiniProgram
{
    public class Manager
    {
        public static readonly Manager Instance = null;
        static Manager()
        {
            Instance = new Manager();
        }

        public bool ValidateUserPrivilege(string wcid)
        {
            return true;
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