using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHomeApp.Models
{
    public class Kitchen
    {
        public string _id { get; set; }
        public string homeId { get; set; }
        public Boolean lamp { get; set; }
        public Boolean waterPump { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
