using BlogCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Model.Model
{
    public class Party:RootEntity
    {
        public string Topic { get; set; }
        public string TopicColor { get; set; }
        public decimal Price { get; set; }

    }
}
