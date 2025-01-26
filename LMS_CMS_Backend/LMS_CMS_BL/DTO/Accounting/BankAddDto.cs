using LMS_CMS_DAL.Models.Domains.AccountingModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class BankAddDto
    {

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        public string BankAccountName { get; set; }
        public string BankName { get; set; }
        public string IBAN { get; set; }
        public int BankAccountNumber { get; set; }
        public string AccountOpeningDate { get; set; }
        public string AccountClosingDate { get; set; }
        public long AccountNumberID { get; set; }

    }
}
