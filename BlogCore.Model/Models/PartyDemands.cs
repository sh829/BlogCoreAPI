using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Model.Models
{
    /// <summary>
    /// 派对客户需求的物品
    /// </summary>
    public class PartyDemands : RootEntity
    {
        /// <summary>
        /// 派对信息表
        /// </summary>
        public int BookInfoId { get; set; }
        /// <summary>
        /// 物品种类编码
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string ItemCode { get; set; }
        /// <summary>
        /// 物品信息名
        /// </summary>
        public int ItemName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string  Size { get; set; }
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
