using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LMS_CMS_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace LMS_CMS_BL.Repository
{
    public class GenericRepo<TEntity> where TEntity : class
    {
        LMS_CMS_Context db;


        public GenericRepo(LMS_CMS_Context db)
        {
            this.db = db;
        }

        public LMS_CMS_Context Database()
        {
            return this.db;
        }

        public List<TEntity> Select_All()
        {
            return db.Set<TEntity>().ToList();
        }

        public List<TEntity> Select_All_With_Includes(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = db.Set<TEntity>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.ToList();
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

        public void Delete(long id)
        {
            TEntity obj = db.Set<TEntity>().Find(id);
            db.Set<TEntity>().Remove(obj);
        }

        public TEntity First_Or_Default(Expression<Func<TEntity, bool>> predicate)
        {
            return db.Set<TEntity>().FirstOrDefault(predicate);
        }

        public async Task<TEntity> FindByIncludesAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = db.Set<TEntity>();

            foreach (var include in includes)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public List<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return db.Set<TEntity>().Where(predicate).ToList();
        }

        public async Task<List<TEntity>> Select_All_With_Includesbyid<TProperty>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TProperty>> include)
        {
            return await db.Set<TEntity>()
                .Where(predicate)
                .Include(include)
                .ToListAsync();
        }
    }
}
