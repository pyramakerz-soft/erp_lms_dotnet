using System.ComponentModel.DataAnnotations;

namespace LMS_CMS_BL.DTO.Clinic
{
    public class HygieneFormPutDTO
    {
        public long ID { get; set; }

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
    }
}
