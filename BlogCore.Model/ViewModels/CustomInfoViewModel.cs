using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Model.ViewModels
{
    public class CustomInfoViewModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        //public string Age { get; set; }
        public DateTime AdvisoryTime { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int PredictNumber { get; set; }
    }
}
