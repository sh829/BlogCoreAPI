using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Model.Models
{
    public class ExcelMapToModel:RootEntity
    {
        /// <summary>
        /// 对应的model名称  或者是表名  这里是codefirst 所以是model
        /// </summary>
        public string ModelName { get; set; }
        /// <summary>
        /// excel列名
        /// </summary>
        public string ExcelColumn { get; set; }
        /// <summary>
        /// 对应的model属性
        /// </summary>
        public string ModelProperty { get; set; }
    }
}
