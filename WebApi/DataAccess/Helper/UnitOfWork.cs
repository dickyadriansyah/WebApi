using DataAccess.Repositories;
using Entites.Data;
using InterfaceApi.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.Entity.Validation;

namespace DataAccess.Helper
{
    public class UnitOfWork :IUnitOfWork
    {

        private readonly OctopusDBEntities _context = null;
        public GenericRepository<siswa> _siswaDataRepository { get; set; }

        public IGenericRepository<siswa> SiswaRepository
        {
            get
            {
                if (_siswaDataRepository == null)
                    _siswaDataRepository = new GenericRepository<siswa>(_context);
                return _siswaDataRepository;
            }
        }

        public UnitOfWork()
        {
            _context = new OctopusDBEntities();
            _context.Configuration.LazyLoadingEnabled = true;
            _context.Configuration.AutoDetectChangesEnabled = false;
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(System.Web.HttpContext.Current.Server.MapPath("~/App_Log/errors.txt"), outputLines);

                throw e;
            }
        }

        public IGenericRepository<T> GetGenericRepository<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return _context.Database.ExecuteSqlCommand(sql, parameters);
        }

        public IEnumerable SqlQuery(Type elementType, string sql, params object[] parameters)
        {
            return _context.Database.SqlQuery(elementType, sql, parameters);
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string query, params object[] parameters)
        {
            return _context.Database.SqlQuery<TElement>(query, parameters);
        }

        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
    }
}
