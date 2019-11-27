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
        /// 出港
        /// 清关
        /// 到达
        /// </summary>
        public string State { get; set; }

        public string CreateTime { get; set; }

        public string SHTime { get; set; }

        public string ZGTime { get; set; }

        public string CGTime { get; set; }

        public string QGTime { get; set; }

        public string DDTime { get; set; }
    }
}