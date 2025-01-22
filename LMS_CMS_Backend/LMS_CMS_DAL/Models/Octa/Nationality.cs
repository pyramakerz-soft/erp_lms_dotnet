using LMS_CMS_DAL.Models.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models.Octa
{
    public class Nationality
    {
        [Key]
        public long ID { get; set; }
        public string Name { get; set; }
        public string ArName { get; set; }

        [ForeignKey("Country")]
        public long CountryID { get; set; }
        public Country Country { get; set; }
    }
}
