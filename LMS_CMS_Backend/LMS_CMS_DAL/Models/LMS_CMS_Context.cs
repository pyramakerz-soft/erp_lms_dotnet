using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_DAL.Models
{
    public partial class LMS_CMS_Context : DbContext
    {
        public LMS_CMS_Context()
        {
        }

        public LMS_CMS_Context(DbContextOptions<LMS_CMS_Context> options)
            : base(options)
        {
        }
    }
}
