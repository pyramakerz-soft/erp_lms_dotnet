using LMS_CMS_DAL.Models.Domains.BusModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Octa
{
    public class Country
    {
        [Key]
        public long ID { get; set; }
        public string Name { get; set; }
        public string ArName { get; set; }
        public string Iso { get; set; }
        public string Iso3 { get; set; }
        public int Numcode { get; set; }
        public int PhoneCode { get; set; }

        public ICollection<Nationality> Nationalities { get; set; } = new HashSet<Nationality>();
    }
}
