using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace thuctaptotnghiep.Models
{
    public class JobCategory
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int Count { get; set; }
    }
    public class JobCity
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int Count { get; set; }
    }
    public class JobSkill
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}