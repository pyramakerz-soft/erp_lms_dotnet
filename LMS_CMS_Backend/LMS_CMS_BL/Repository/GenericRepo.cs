using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LMS_CMS_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS_CMS_BL.Repository
{
    public class GenericRepo<TEntity> where TEntity : class
    {
        LMS_CMS_Context db;


        public GenericRepo(LMS_CMS_Context db)
        {
            this.db = db;
        }

        public List<TEntity> Select_All()
        {
            return db.Set<TEntity>().ToList();
        }



        public TEntity Select_By_Id(params object[] keyValues)
        {
            return db.Set<TEntity>().Find(keyValues);
        }

        public void Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);

        }

        public void Update(TEntity entity)
        {
            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Delete(int id)
        {
            TEntity obj = db.Set<TEntity>().Find(id);
            db.Set<TEntity>().Remove(obj);
        }

        public TEntity First_Or_Default(Expression<Func<TEntity, bool>> predicate)
        {
            return db.Set<TEntity>().FirstOrDefault(predicate);
        }


    }
}
