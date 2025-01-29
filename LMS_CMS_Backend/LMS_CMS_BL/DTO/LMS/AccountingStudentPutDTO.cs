using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class AccountingStudentPutDTO
    {
        public long ID { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; }
        public long? Nationality { get; set; }
        public string? NationalityName { get; set; }
        public long? AccountNumberID { get; set; }
        public string? AccountNumberName { get; set; }
    }
}
