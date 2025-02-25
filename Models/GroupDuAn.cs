﻿using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectStorage.Models
{
    [Table("NHOMCONGTRINH")]
    public class GroupDuAn
    {
        [Key]
        public int Id { get; set; }
        public string TenNhom { get; set; } 
    }
}
