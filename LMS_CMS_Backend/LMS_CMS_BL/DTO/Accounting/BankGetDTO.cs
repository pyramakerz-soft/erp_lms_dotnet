using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class BankGetDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string BankAccountName { get; set; }
        public string BankName { get; set; }
        public string IBAN { get; set; }
        public int BankAccountNumber { get; set; }
        public string AccountOpeningDate { get; set; }
        public string AccountClosingDate { get; set; }
        public long AccountNumberID { get; set; }
        public string? AccountNumberName { get; set; }

    }
}
