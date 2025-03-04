using LMS_CMS_DAL.Models.Domains.ClinicModule;
using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class HygieneFormAddDTO
    {
        [Required(ErrorMessage = "School ID is required")]
        public long SchoolId { get; set; }

        [Required(ErrorMessage = "Grade ID is required")]
        public long GradeId { get; set; }

        [Required(ErrorMessage = "Classroom ID is required")]
        public long ClassRoomID { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }
       
        public long StudentId { get; set; }
        public long HygieneTypeId { get; set; }

        //public List<StudentHygieneTypes> StudentHygieneTypes { get; set; } 
    }
}
