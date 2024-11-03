using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public class Modules_Master_permissions
    {
        // NOTE ==> Both said to be ForeignKey and PrimaryKey in context
        public int ID { get; set; }
        public int Module_Id { get; set; }
        public int Master_Id { get; set; }

        public Modules Module { get; set; }
        public Master_Permissions Master_Permission { get; set; }
    }
}
