using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjMvcDemo.Models
{
    public class CCustomer
    {
        public int fId { get; set; }//加上getset可轉為屬性
        public string fName { get; set; }
        public string fPhone { get; set; }
        public string fEmail { get; set; }
        public string fAddress { get; set; }
        public string fPassword { get; set; }  
    }
}