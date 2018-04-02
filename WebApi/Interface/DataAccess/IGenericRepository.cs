using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.DataAccess
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// generic Get method untuk entities
        IEnumerable<TEntity> Get();

        /// Generic get method untuk Entities berdasarkan ID
        TEntity GetByID(object id);

        /// Generic Insert Method untuk Entities
        void Insert(TEntity entity);

        /// Generic Delete method untuk Entities berdasarkan ID
        void Delete(object id);

        /// Generic Delete method untuk Entities berdasarkan Object TEntity
        void Delete(TEntity entityToDelete);

        /// Generic Update Method untuk Entities berdasarkan TEntity
        void Update(TEntity entityToUpdate);

        /// generic method Get banyak record berdasarkan kondisi
        IEnumerable<TEntity> GetMany(Func<TEntity, bool> where);

        /// generic method Get banyak record berdasarkan kondisi dan bisa query
        IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where);

        /// generic get method untuk entities berdasarkan kondisi
        TEntity Get(Func<TEntity, Boolean> where);

        /// generic delete method berdasarkan kondisi
        void Delete(Func<TEntity, Boolean> where);

        /// generic method untuk mendapatkan semua data dari db
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetRawQuery(string sql, params object[] parameters);
        /// Inclue multiple
        IQueryable<TEntity> GetWithInclude(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate,
            params string[] include);

        /// Generic method untuk check jika entities Exists
        bool Exists(object primaryKey);

        /// Gets a single data by specified criteria
        TEntity GetSingle(Func<TEntity, bool> predicate);

        /// Mengambil Data Single atau Default dari data yang sesuai dengan kondisi yang di tentukan
        TEntity GetSingleOrDefault(Func<TEntity, bool> predicate);

        /// Get Data pertama dari data yang match atas kondisi yang ditentukan
        TEntity GetFirst(Func<TEntity, bool> predicate);

        /// Mengambil Data Pertama atau Default dari data yang sesuai dengan kondisi yang di tentukan
        TEntity GetFirstOrDefault(Func<TEntity, bool> predicate);
    }
}
