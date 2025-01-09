using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class CategoryFieldGetDTO
    {
        public long ID { get; set; }
        public string EnName { get; set; }
        public string ArName { get; set; }
        public int OrderInForm { get; set; }
        public bool IsMandatory { get; set; }
        public long? InsertedByUserId { get; set; }
        public long FieldTypeID { get; set; }
        public string FieldTypeName { get; set; }
        public long RegistrationCategoryID { get; set; }
        public string RegistrationCategoryName { get; set; }
        public List<FieldOptionGetDTO> Options { get; set; }
    }
}
