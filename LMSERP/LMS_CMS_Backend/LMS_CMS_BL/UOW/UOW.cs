using LMS_CMS_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_CMS_BL.UOW
{
    public class UOW
    {
        LMS_CMS_Context DB;

        public UOW(LMS_CMS_Context db)
        {
            DB = db;
        }

        public void SaveChanges()
        {
            DB.SaveChanges();
        }
    }
}
