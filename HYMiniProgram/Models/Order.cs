using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HYMiniProgram.Models
{
    /// <summary>
    /// 订单
    /// </summary>
    public class Order
    {
        public string OrderNo { get; set; }

        /// <summary>
        /// 已收货
        /// 装柜
        /// 发船
        /// 清关
        /// 到达
        /// </summary>
        public string State { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime SHTime { get; set; }

        public DateTime ZGTime { get; set; }

        public DateTime FCTime { get; set; }

        public DateTime QGTime { get; set; }

        public DateTime DDTime { get; set; }
    }
}