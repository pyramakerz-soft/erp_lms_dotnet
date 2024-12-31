using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class SubjectGetDTO
    {
        public long ID { get; set; }
        public string en_name { get; set; }
        public string ar_name { get; set; }
        public int OrderInCertificate { get; set; }
        public double CreditHours { get; set; }
        public string SubjectCode { get; set; }
        public int PassByDegree { get; set; }
        public int TotalMark { get; set; }
        public bool HideFromGradeReport { get; set; }
        public string IconLink { get; set; }
        public int NumberOfSessionPerWeek { get; set; }
        public long GradeID { get; set; }
        public string GradeName { get; set; }
        public long SubjectCategoryID { get; set; }
        public string SubjectCategoryName { get; set; }
        public string InsertedAt { get; set; }
    }
}
