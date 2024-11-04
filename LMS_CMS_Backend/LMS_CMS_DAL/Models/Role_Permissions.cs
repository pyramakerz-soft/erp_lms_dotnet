using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public partial class Role_Permissions
    {
        public int Id { get; set; }
        public int Role_ID { get; set; }
        public int Master_Detailed_Permissions_ID { get; set; }

        public Role Role { get; set; }
        public Master_Detailes_Permissions Master_Detailes_Permissions { get; set; }
    }
}
