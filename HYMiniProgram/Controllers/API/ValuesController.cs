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
        [Route("ValidateUserPrivilege")]
        public JsonResult<ReturnValue<string>> ValidateUserPrivilege(string wcid)
        {
            var result = Manager.Instance.ValidateUserPrivilege(wcid);
            return Json(result);
        }

        [HttpGet]
        [Route("SearchOrder")]
        public JsonResult<ReturnValue<Order>> SearchOrder(string orderNo)
        {
            var m = Manager.Instance.SearchOrder(orderNo);
            return Json(m);
        }

        [HttpPost]
        [Route("InsertOrder")]
        public JsonResult<ReturnValue<int>> InsertOrder(Order m)
        {
            var result = Manager.Instance.InsertOrder(m);
            return Json(result);
        }


        [HttpPost]
        [Route("UpdateOrderState")]
        public JsonResult<ReturnValue<int>> UpdateOrderState(dynamic obj)
        {
            string beginTime = obj.BeginTime; string endTime = obj.EndTime; string state = obj.State;
            var result = Manager.Instance.UpdateOrderState(beginTime, endTime, state);
            return Json(result);
        }

    }
}
