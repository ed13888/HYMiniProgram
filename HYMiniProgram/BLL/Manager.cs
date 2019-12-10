using HYMiniProgram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public ReturnValue<string> ValidateUserPrivilege(string wcid)
        {
            try
            {
                var m = SqlHelper.QuerySingle<string>(new { wcid = wcid }, "select wcid from userprivilege where wcid=@wcid");
                return new ReturnValue<string>(!string.IsNullOrEmpty(m), m, "登录成功");
            }
            catch
            {
                return new ReturnValue<string>(false, null, "该账号未授权");
            }
        }

        public ReturnValue<Order> SearchOrder(string orderNo)
        {
            try
            {
                var m = SqlHelper.QuerySingle<Order>(new { OrderNo = orderNo }, "select * from `order` where orderno=@OrderNo");
                return new ReturnValue<Order>(true, m, "操作失败");
            }
            catch
            {
                return new ReturnValue<Order>(false, null, "订单不存在");
            }
        }

        public ReturnValue<int> InsertOrder(Order m)
        {
            try
            {
                m = new Order
                {
                    OrderNo =m.OrderNo,
                    State = 1,
                };
                var sql = $"insert into `order` (orderno,state) values (@OrderNo,@State)";
                var val = SqlHelper.Execute(sql.ToString(), m);
                return new ReturnValue<int>(val > 0, val, "操作失败");
            }
            catch
            {
                return new ReturnValue<int>(false, 0, "操作失败");
            }
        }

        public ReturnValue<int> UpdateOrderState(string beginTime, string endTime, string state)
        {
            try
            {
                var sql = new StringBuilder($"update `order` set state=@state");
                switch (state)
                {
                    case "2":
                        sql.Append($",ZGTime=@time");
                        break;
                    case "3":
                        sql.Append($",CGTime=@time");
                        break;
                    case "4":
                        sql.Append($",QGTime=@time");
                        break;
                    case "5":
                        sql.Append($",DDTime=@time");
                        break;
                    default:
                        break;
                }
                sql.Append($" where createtime>= @begintime and createtime <= @endtime ");
                var val = SqlHelper.Execute(sql.ToString(), new { state = state, begintime = beginTime, endtime = endTime, time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
                return new ReturnValue<int>(val > 0, val, "操作失败");
            }
            catch
            {
                return new ReturnValue<int>(false, 0, "操作失败");
            }
        }

    }
}