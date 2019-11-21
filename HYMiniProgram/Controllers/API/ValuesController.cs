using HYMiniProgram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace HYMiniProgram.Controllers
{
    [RoutePrefix("Values")]
    public class ValuesController : ApiController
    {

        [HttpGet]
        [Route("SearchOrder")]
        public JsonResult<Order> SearchOrder(string orderNo)
        {
            var m = OrderManager.Instance.SearchOrder(orderNo);
            return Json(m);
        }

        [HttpPost]
        [Route("InsertOrder")]
        public JsonResult<bool> InsertOrder(Order m)
        {
            var result = OrderManager.Instance.InsertOrder(m);
            return Json(result);
        }


        [HttpPost]
        [Route("UpdateOrderState")]
        public JsonResult<bool> UpdateOrderState(dynamic obj)
        {
            string beginTime = obj.BeginTime; string endTime = obj.EndTime; string state = obj.State;
            var result = OrderManager.Instance.UpdateOrderState(beginTime, endTime, state);
            return Json(result);
        }

    }
}
