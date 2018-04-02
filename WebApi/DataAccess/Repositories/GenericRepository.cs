using InterfaceApi.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        #region Private member variables...
        internal OctopusDBEntities Context;
        internal DbSet<TEntity> DbSet;
        #endregion

        #region Public Constructor...
        public GenericRepository(OctopusDBEntities context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
        }
        #endregion

        #region Public member methods...

        /// generic Get method untuk entities
        public virtual IEnumerable<TEntity> Get()
        {
            IQueryable<TEntity> query = DbSet;
            return query.AsNoTracking().ToList();
        }

        /// Generic get method untuk Entities berdasarkan ID
        public virtual TEntity GetByID(object id)
        {
            return DbSet.Find(id);
        }

        /// Generic Insert Method untuk Entities
        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }
        /// Generic Delete method untuk Entities berdasarkan ID
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        /// Generic Delete method untuk Entities berdasarkan Object TEntity
        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        /// Generic Update Method untuk Entities berdasarkan TEntity
        public virtual void Update(TEntity entityToUpdate)
        {
            if (Context.Entry(entityToUpdate).State == EntityState.Detached)
            {
                DbSet.Attach(entityToUpdate);
            }
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        /// generic method Get banyak record berdasarkan kondisi
        public virtual IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
        {
            return DbSet.AsNoTracking().Where(where).ToList();
        }

        /// generic method Get banyak record berdasarkan kondisi dan bisa query
        public virtual IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where)
        {
            return DbSet.AsNoTracking().Where(where).AsQueryable();
        }

        /// generic get method untuk entities berdasarkan kondisi
        public TEntity Get(Func<TEntity, Boolean> where)
        {
            return DbSet.AsNoTracking().Where(where).FirstOrDefault<TEntity>();
        }

        /// generic delete method berdasarkan kondisi
        public void Delete(Func<TEntity, Boolean> where)
        {
            IQueryable<TEntity> objects = DbSet.Where<TEntity>(where).AsQueryable();
            foreach (TEntity obj in objects)
                DbSet.Remove(obj);
        }

        /// generic method untuk mendapatkan semua data dari db
        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking().ToList();
        }

        // generic method to execute raw query
        public virtual IEnumerable<TEntity> GetRawQuery(string sql, params object[] parameters)
        {
            return Context.Database.SqlQuery<TEntity>(sql, parameters);
        }

        /// Inclue multiple
        public IQueryable<TEntity> GetWithInclude(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = this.DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }

        /// Generic method untuk check jika entities Exists
        public bool Exists(object primaryKey)
        {
            return DbSet.Find(primaryKey) != null;
        }

        /// Gets a single data by specified criteria
        public TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return DbSet.AsNoTracking().Single<TEntity>(predicate);
        }
        /// Mengambil Data Single atau Default dari data yang sesuai dengan kondisi yang di tentukan
        public TEntity GetSingleOrDefault(Func<TEntity, bool> predicate)
        {
            return DbSet.AsNoTracking().SingleOrDefault<TEntity>(predicate);
        }

        /// Get Data pertama dari data yang match atas kondisi yang ditentukan
        public TEntity GetFirst(Func<TEntity, bool> predicate)
        {
            return DbSet.AsNoTracking().First<TEntity>(predicate);
        }

        /// Mengambil Data Pertama atau Default dari data yang sesuai dengan kondisi yang di tentukan
        public TEntity GetFirstOrDefault(Func<TEntity, bool> predicate)
        {
            return DbSet.AsNoTracking().FirstOrDefault<TEntity>(predicate);
        }
        #endregion
    }
}
