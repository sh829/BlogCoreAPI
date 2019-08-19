using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Model.Models
{
    /// <summary>
    /// 安全学院毕业生统计
    /// </summary>
    public class GraduationStatistics:RootEntity
    {
        public GraduationStatistics()
        {
            CreateTime = DateTime.Now;

        }
        /// <summary>
        /// 姓名
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Sex { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime Birthday{ get; set; }
        /// <summary>
        /// 入学年份
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int StartYear { get; set; }
        /// <summary>
        /// 所在班级
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Class { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string ClassName { get; set; }
        /// <summary>
        /// 辅导员
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Teacher { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Birthplace { get; set; }
        /// <summary>
        /// 当前所在城市
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string CurrentCity { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string CompanyName { get; set; }
        /// <summary>
        /// 职务/职称
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Position { get; set; }
        /// <summary>
        /// 业务范围
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string BusinessScope { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Email { get; set; }
        /// <summary>
        /// 是否参加校庆
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsCelebration { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsDelete { get; set; }
    }
}
