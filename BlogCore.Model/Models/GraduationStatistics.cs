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
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime Birthday{ get; set; }
        /// <summary>
        /// 入学年份
        /// </summary>
        public int StartYear { get; set; }
        /// <summary>
        /// 所在班级
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 辅导员
        /// </summary>
        public string Teacher { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string Birthplace { get; set; }
        /// <summary>
        /// 当前所在城市
        /// </summary>
        public string CurrentCity { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 职务/职称
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 业务范围
        /// </summary>
        public string BusinessScope { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 是否参加校庆
        /// </summary>
        public bool IsCelebration { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsDelete { get; set; }
    }
}
