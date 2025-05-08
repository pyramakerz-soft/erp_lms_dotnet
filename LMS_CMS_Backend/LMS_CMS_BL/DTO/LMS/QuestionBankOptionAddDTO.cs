using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class QuestionBankOptionAddDTO
    {
        public string Option { get; set; }
        public int? Order { get; set; }
        public long? QuestionBankID { get; set; }
    }
}
