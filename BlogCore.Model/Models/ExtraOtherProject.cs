using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Model.Models
{
    public class ExtraOtherProject:RootEntity
    {
        /// <summary>
        /// 预定信息表ID
        /// </summary>
        public int BookInfoId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string Name { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string Code { get; set; }
        /// <summary>
        /// 项目花费
        /// </summary>
        public decimal Cost { get; set; }
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
