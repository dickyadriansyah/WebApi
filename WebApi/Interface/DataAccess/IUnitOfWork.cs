using DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {

        IGenericRepository<siswa> SiswaRepository { get; }

        void Save();

        IGenericRepository<T> GetGenericRepository<T>() where T : class;

        int ExecuteSqlCommand(string sql, params object[] parameters);

        IEnumerable SqlQuery(Type elementType, string sql, params object[] parameters);

        IEnumerable<TElement> SqlQuery<TElement>(string query, params object[] parameters);
    }
}
