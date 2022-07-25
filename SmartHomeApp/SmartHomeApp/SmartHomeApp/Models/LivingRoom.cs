using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHomeApp.Models
{
    public class LivingRoom
    {
        public string _id { get; set; }
        public string homeId { get; set; }
        public Boolean lamp { get; set; }
        public string airConditioningId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
