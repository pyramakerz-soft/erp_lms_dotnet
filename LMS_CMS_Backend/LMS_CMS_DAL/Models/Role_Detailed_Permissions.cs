using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public partial class Role_Detailed_Permissions
    {
        // NOTE ==> Both said to be ForeignKey and PrimaryKey in context
        public int Role_ID { get; set; }
        public int Detailed_Permissions_ID { get; set; }

        public Role Role { get; set; }
        public Detailed_Permissions Detailed_Permissions { get; set; }
    }
}
