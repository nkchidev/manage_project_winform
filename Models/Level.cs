using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectStorage
{
    public class Level
    {
        public int id_level { get; set; }
        public string status { get; set; }
        public string description { get; set; }

        public string level_name { get; set; }
        public string username { get; set; }

        public int icon_index { get; set; }
        public string fullname { get; set; }
        public DateTime created_date { get; set; }
        public string tab_recently { get; set; }
        public string form_open_recently { get; set; }

    }
}
