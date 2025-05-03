using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class LessonResourceTypeGetDTo
    {
        public long ID { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }

    }
}
