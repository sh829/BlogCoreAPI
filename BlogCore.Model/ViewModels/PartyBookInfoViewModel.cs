using BlogCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Model.ViewModels
{
    public class PartyBookInfoViewModel
    {
        public int CustomId { get; set; }
        public int BookInfoId { get; set; }
        public DateTime AdvisoryTime { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public string Age { get; set; }

        public DateTime ScheduledDate { get; set; }

        public string ActualNumber { get; set; }

        public string TopicName { get; set; }

        public string TopicColor { get; set; }

        public decimal Price { get; set; }
        public decimal BuffetCost { get; set; }

        public decimal FoodCost { get; set; }

        public decimal TotalCost { get; set; }
        public List<ExtraProjectInfo> ExtraProjectInfo { get; set; }
        public List<ExtraOtherProject> ExtraOtherProject { get; set; }

    }
}
