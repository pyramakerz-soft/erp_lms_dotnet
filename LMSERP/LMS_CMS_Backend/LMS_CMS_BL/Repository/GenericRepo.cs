using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_CMS_DAL.Models;

namespace LMS_CMS_BL.Repository
{
    public class GenericRepo<TEntity> where TEntity : class
    {
        LMS_CMS_Context DB;

        public GenericRepo(LMS_CMS_Context db)
        {
            this.DB = db;
        }
    }
}
