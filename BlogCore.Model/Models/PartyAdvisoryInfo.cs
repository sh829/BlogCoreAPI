using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Model.Models
{
    /// <summary>
    /// 派对咨询表
    /// </summary>
    public class PartyAdvisoryInfo: RootEntity
    {
        /// <summary>
        /// 顾客信息ID
        /// </summary>
        public int CustomInfo { get; set; }
        /// <summary>
        /// 定金
        /// </summary>
        public decimal Deposit { get; set; }
        /// <summary>
        /// 交定金日期
        /// </summary>
        public DateTime DepositTime { get; set; }
        /// <summary>
        /// 预计人数
        /// </summary>
        public int PredictNumber { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string Source { get; set; }
        /// <summary>
        /// 第一次跟进情况
        /// </summary>
        [SugarColumn(Length = 500, IsNullable = true)]
        public string FirstInfo { get; set; }
        /// <summary>
        /// 第二次跟进情况
        /// </summary>
        [SugarColumn(Length = 500, IsNullable = true)]
        public string SecondInfo { get; set; }
        /// <summary>
        /// 第三次跟进情况
        /// </summary>
        [SugarColumn(Length = 500, IsNullable = true)]
        public string ThirdInfo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = 500, IsNullable = true)]
        public string Remark { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary> 
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(Length = 60, IsNullable = true)]
        public string CreatedBy { get; set; }
        public int CreateById { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [SugarColumn(Length = 60, IsNullable = true)]
        public string UpdatedBy { get; set; }
        public int UpdateById { get; set; }
    }
}
