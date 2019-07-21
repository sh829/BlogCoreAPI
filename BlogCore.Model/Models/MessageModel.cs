using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Model.Models
{
    public class MessageModel<T>
    {
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool success { get; set; } = false;
        public string msg { get; set; } = "服务器异常";
        public T response { get; set; }
    }
}
