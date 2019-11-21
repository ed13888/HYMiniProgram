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

        // GET api/values/5
        public string Get()
        {
            return "value";
        }

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
        public JsonResult<bool> UpdateOrderState(string beginTime, string endTime, string state)
        {
            var result = OrderManager.Instance.UpdateOrderState(beginTime, endTime, state);
            return Json(result);
        }

    }
}
