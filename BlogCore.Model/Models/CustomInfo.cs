using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Model.Models
{
    public class CustomInfo:RootEntity
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [SugarColumn(Length = 64, IsNullable = true)]
        public string Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [SugarColumn(Length = 64, IsNullable = true)]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 咨询日期
        /// </summary>
        public DateTime AdvisoryTime { get; set; }
        /// <summary>
        /// 预定日期
        /// </summary>
        public DateTime ScheduledDate { get; set; }

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
