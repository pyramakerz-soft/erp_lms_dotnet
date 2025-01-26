using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Accounting
{
    public class AccountingTreeChartGetDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public long SubTypeID { get; set; }
        public string SubTypeName { get; set; }
        public long EndTypeID { get; set; }
        public string EndTypeName { get; set; }
        public long MainAccountNumberID { get; set; }
        public string MainAccountNumberName { get; set; }
        public long LinkFileID { get; set; }
        public string LinkFileName { get; set; }
        public long MotionTypeID { get; set; }
        public string MotionTypeName { get; set; }
        public long InsertedByUserId { get; set; }

        public List<AccountingTreeChartGetDTO> Children { get; set; } = new List<AccountingTreeChartGetDTO>();
    }
}
