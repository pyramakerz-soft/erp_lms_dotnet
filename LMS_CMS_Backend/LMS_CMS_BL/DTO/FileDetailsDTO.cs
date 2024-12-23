using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.DTO
{
    public class FileDetailsDTO
    {
        public string? FolderName { get; set; }
        public string FileName { get; set; }
        public string DownloadUrl { get; set; }
    }
}
