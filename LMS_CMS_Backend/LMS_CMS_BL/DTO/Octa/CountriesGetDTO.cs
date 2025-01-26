using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Octa
{
    public class CountriesGetDTO
    {
        [Key]
        public long ID { get; set; }
        public string Name { get; set; }
        public string ArName { get; set; }
        public string Iso { get; set; }
        public string Iso3 { get; set; }
        public int Numcode { get; set; }
        public int PhoneCode { get; set; }
    }
}
