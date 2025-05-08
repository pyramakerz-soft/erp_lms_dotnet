using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LMS_CMS_DAL.Models.Domains;
using LMS_CMS_DAL.Models.Domains.AccountingModule;
using LMS_CMS_DAL.Models.Octa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace LMS_CMS_BL.Repository
{
    public class GenericRepo<TEntity> where TEntity : class
    {
        LMS_CMS_Context? db;
        Octa_DbContext? octa_db;

        public GenericRepo(LMS_CMS_Context db)
        {
            this.db = db;
        }

        public GenericRepo(Octa_DbContext octaDb)
        {
            this.octa_db = octaDb;
        }

        public LMS_CMS_Context Database()
        {
            return this.db;
        }
        
        public Octa_DbContext OctaDatabase()
        {
            return this.octa_db;
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
        public async Task<TEntity> Select_By_IdAsync(params object[] keyValues)
        {
            return await db.Set<TEntity>().FindAsync(keyValues);
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await db.Set<TEntity>().CountAsync(predicate);
        }

        public void Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            db.Set<TEntity>().AddRange(entities);
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            db.Set<TEntity>().RemoveRange(entities);
        }
        public async Task AddAsync(TEntity entity)
        {
            await db.Set<TEntity>().AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
        }


        public void Delete(long id)
        {
            TEntity obj = db.Set<TEntity>().Find(id);
            db.Set<TEntity>().Remove(obj);
        }
        public async Task DeleteAsync(long id)
        {
            TEntity obj = await db.Set<TEntity>().FindAsync(id); 
            if (obj != null)
            {
                db.Set<TEntity>().Remove(obj); 
            }
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

        public IQueryable<T> SelectQuery<T>(Expression<Func<T, bool>> filter) where T : class
        {
            return db.Set<T>().Where(filter);
        }

        public async Task<List<TEntity>> Select_All_With_IncludesById<TProperty>(
            Expression<Func<TEntity, bool>> predicate,
            params Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = db.Set<TEntity>();

            foreach (var include in includes)
            {
                query = include(query);
            }

            return await query.Where(predicate).ToListAsync();
        }

        public IQueryable<TEntity> Select_All_With_IncludesById_Pagination<TEntity>(
            Expression<Func<TEntity, bool>> filter,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes) where TEntity : class
        {
            IQueryable<TEntity> query = db.Set<TEntity>().Where(filter);

            foreach (var include in includes)
            {
                query = include(query);
            }

            return query; // Return IQueryable to support Skip & Take
        }

        public async Task<List<T>> FindByAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await db.Set<T>().Where(predicate).ToListAsync();
        }
        ///////////////////////////////////////////////////////////////////// Octa /////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Add_Octa(TEntity entity)
        {
            octa_db.Set<TEntity>().Add(entity);
        }

        public List<TEntity> Select_All_Octa()
        {
            return octa_db.Set<TEntity>().ToList();
        }

        public TEntity Select_By_Id_Octa(params object[] keyValues)
        {
            return octa_db.Set<TEntity>().Find(keyValues);
        }

        public void Update_Octa(TEntity entity)
        {
            octa_db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Delete_Octa(long id)
        {
            TEntity obj = octa_db.Set<TEntity>().Find(id);
            octa_db.Set<TEntity>().Remove(obj);
        }
        public TEntity First_Or_Default_Octa(Expression<Func<TEntity, bool>> predicate)
        {
            return octa_db.Set<TEntity>().FirstOrDefault(predicate);
        }

        public List<TEntity> FindBy_Octa(Expression<Func<TEntity, bool>> predicate)
        {
            return octa_db.Set<TEntity>().Where(predicate).ToList();
        }
    }
}
