using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Model.Models
{
    public class Dictionary:RootEntity
    {

        /// <summary>
        /// 种类
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string Category { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string Code { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string Remark { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        [SugarColumn(Length = 500, IsNullable = true)]
        public string Value { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public int ParentId { get; set; }
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
