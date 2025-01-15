using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO.Registration
{
    public class RegisterationFormSubmittionForFiles
    {
        [FromForm]
        public long CategoryFieldID { get; set; }
        [FromForm]
        public IFormFile? SelectedFile { get; set; }
    }
}
