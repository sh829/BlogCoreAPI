using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Model.Models
{

    /// <summary>
    /// 派对预定信息表
    /// </summary>
    public class PartyBookInfo : RootEntity
    {
        /// <summary>
        /// 顾客信息ID
        /// </summary>
        public int CustomId { get; set; }
        /// <summary>
        /// 派对ID
        /// </summary>
        public int PartyId { get; set; }
        /// <summary>
        /// 实到人数
        /// </summary>
        public string ActualNumber { get; set; }
        /// <summary>
        /// 派对名
        /// </summary>
        [SugarColumn(Length = 200, IsNullable = true)]
        public string TopicName { get; set; }
        /// <summary>
        /// 派对编码
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string TopicCode { get; set; }
        /// <summary>
        /// 派对色系
        /// </summary>
        [SugarColumn(Length = 100, IsNullable = true)]
        public string TopicColor { get; set; }
        /// <summary>
        /// 派对套系
        /// </summary>
        public decimal Price { get; set; }
      
        /// <summary>
        /// 自助餐花费
        /// </summary>
        public decimal BuffetCost { get; set; }
        
        /// <summary>
        /// 餐饮消费
        /// </summary>
        public decimal FoodCost { get; set; }
        /// <summary>
        /// 总消费金额
        /// </summary>
        public decimal TotalCost { get; set; }
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
