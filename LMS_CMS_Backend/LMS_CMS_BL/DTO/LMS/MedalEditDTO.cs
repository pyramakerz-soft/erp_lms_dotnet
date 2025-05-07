using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.LMS
{
    public class MedalEditDTO
    {
        public long ID { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string? ImageLink { get; set; }
        public IFormFile? ImageForm { get; set; }
    }
}
