using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Model.ViewModels
{
    public class PartyAdvisoryViewModel
    {
        /// <summary>
        /// 顾客ID
        /// </summary>
        public int CustomId { get; set; }
        /// <summary>
        /// 咨询信息ID
        /// </summary>
        public int? AdvisoryId { get; set; }
        /// <summary>
        /// 顾客姓名
        /// </summary>
        public string Name { get; set; }
        public string Age { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 咨询日期
        /// </summary>
        public DateTime AdvisoryTime { get; set; }
        /// <summary>
        /// 预计日期
        /// </summary>
        public DateTime? ScheduledDate { get; set; }
        /// <summary>
        /// 预计人数
        /// </summary>
        public int? PredictNumber { get; set; }
        /// <summary>
        /// 定金
        /// </summary>
        public decimal? Deposit { get; set; }
        /// <summary>
        /// 交定金日期
        /// </summary>
        public DateTime? DepositTime { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 第一次跟进情况
        /// </summary>
        public string FirstInfo { get; set; }
        /// <summary>
        /// 第二次跟进情况
        /// </summary>
        public string SecondInfo { get; set; }
        /// <summary>
        /// 第三次跟进情况
        /// </summary>
        public string ThirdInfo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool? IsDelete { get; set; }


    }
}
