using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public class Domain_Modules
    {
        public int ID { get; set; }
        public int Domain_Id { get; set; }
        public int Module_Id { get; set; }
        public Domain Domain { get; set; }
        public Modules Module { get; set; }
    }
}
