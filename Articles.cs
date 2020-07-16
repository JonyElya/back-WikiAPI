using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_Wiki
{
    public class Articles
    {
        public long Id { get; set; }
        public string title { get; set; }
        public string snippet { get; set; }
        public int pageId { get; set; }
        public DateTime timestamp { get; set; }
        public Articles()
        {
            this.timestamp = DateTime.UtcNow;
        }  
    }

}
