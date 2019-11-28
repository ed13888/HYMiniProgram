using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HYMiniProgram.Models
{
    /// <summary>
    /// 订单
    /// </summary>
    [Table("`Order`")]
    public class Order
    {
        public string OrderNo { get; set; }

        /// <summary>
        /// 1:已收货
        /// 2:装柜
        /// 3:出港
        /// 4:清关
        /// 5:到达
        /// </summary>
        public int State { get; set; }

        public string CreateTime { get; set; }

        public string SHTime { get; set; }

        public string ZGTime { get; set; }

        public string CGTime { get; set; }

        public string QGTime { get; set; }

        public string DDTime { get; set; }
    }
}