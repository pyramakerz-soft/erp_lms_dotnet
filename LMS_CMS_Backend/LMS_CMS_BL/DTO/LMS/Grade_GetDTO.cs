using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class Grade_GetDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public List<Class_GetDTO> Classes { get; set; } = new List<Class_GetDTO>();
    }
}
