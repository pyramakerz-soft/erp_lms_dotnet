using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public class Master_Detailes_Permissions
    {
        public int ID { get; set; }
        public int Details_Id { get; set; }
        public int Master_Id { get; set; }

        public Detailed_Permissions Detailed_Permission { get; set; }
        public Master_Permissions Master_Permission { get; set; }

        public ICollection<Role_Permissions> Role_Permissions { get; set; } = new HashSet<Role_Permissions>(); // Navigation property

    }
}
