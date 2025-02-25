using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectStorage
{
    [Table("PHANQUYEN")]
    public class Permission
    {
        [Write(false)]
        public string MaCT { get; set; }
        [Write(false)]
        public string TenCT { get; set; }
        [Key]
        public int id { get; set; }
        public string taikhoan { get; set; }
        public bool add { get; set; }
        public bool edit { get; set; }
        public bool delete { get; set; }
    }
}
