using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public class Domain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<School> Schools { get; set; } = new HashSet<School>();
    }
}
