using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Octa
{
    public class OctaGetDTO
    {
        public long ID { get; set; }
        public string User_Name { get; set; } 
        public string Arabic_Name { get; set; } 
        public long? InsertedByUserId { get; set; }
        public DateTime? InsertedAt { get; set; }
        public long? UpdatedByUserId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long? DeletedByUserId { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
