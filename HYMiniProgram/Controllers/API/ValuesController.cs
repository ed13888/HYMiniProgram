using HYMiniProgram.Models;
using Newtonsoft.Json;
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
        public JsonResult<ReturnValue<string>> ValidateUserPrivilege(string code)
        {
            string appid = "wxda26ff1bff212af3", secret = "73fac3abf65ec86423961d00d8be9eb8";
            var url = $"https://api.weixin.qq.com/sns/jscode2session?appid={appid}&secret={secret}&js_code={code}&grant_type=authorization_code";
            var result = HttpHelper.HttpGet(url);
            var d = JsonConvert.DeserializeAnonymousType(result, new
            {
                session_key = "",
                openid = "",
            });
            var res = Manager.Instance.ValidateUserPrivilege(d.openid);
            return Json(res);
        }

        //[HttpGet]
        //[Route("ValidateUserPrivilege")]
        //public JsonResult<ReturnValue<string>> ValidateUserPrivilege(string wcid)
        //{
        //    var result = Manager.Instance.ValidateUserPrivilege(wcid);
        //    return Json(result);
        //}

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
