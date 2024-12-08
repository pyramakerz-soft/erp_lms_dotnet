using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class School_GetDTO
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public List<Grade_GetDTO> Grades { get; set; } = new List<Grade_GetDTO>();
    }
}
