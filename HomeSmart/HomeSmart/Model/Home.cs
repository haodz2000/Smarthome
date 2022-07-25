using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSmart.Model
{
    public class Home
    {
        public string _id { get; set; }
        public string userId { get; set; }
        public int temperature { get; set; }
        public string weather { get; set; }
        public int humidity { get; set; }
        public Boolean door { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
