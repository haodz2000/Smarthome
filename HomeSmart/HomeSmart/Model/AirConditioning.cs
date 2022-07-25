﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSmart.Model
{
    public class AirConditioning
    {
        public string _id { get; set; }
        public string name { get; set; }
        public Boolean active { get; set; }
        public int temperature { get; set; }
        public int mode { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
