using LMS_CMS_DAL.Models.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Octa
{
    public class DomainGetDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string? InsertedAt { get; set; }
        public long[]? Pages { get; set; }
    }
}
