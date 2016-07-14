using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FP.CORE.DAL;
using FP.CORE.Models;
using PagedList;

namespace FP.CORE.Repositories
{
    public class RoleRepository : IRepository<FP_ROLE>
    {
        private readonly FP_EFContext _db = new FP_EFContext();

        public FP_EFContext Db
        {
            get
            {
                return _db;
            }
        }

        public bool Create(FP_ROLE instance)
        {
            bool rs = false;
            Db.ROLE.Add(instance);
            rs = Db.SaveChanges() > 0 ? true : false;
            return rs;
        }

        public bool Delete(Guid primaryID)
        {
            bool rs = false;
            var instance = Db.ROLE.Find(primaryID);
            Db.ROLE.Remove(instance);
            rs = Db.SaveChanges() > 0 ? true : false;
            return rs;
        }

        public IPagedList<FP_ROLE> GetAll(int pageSize, int page)
        {
            return Db.ROLE.OrderBy(a => a.ROLECODE).ToPagedList(page, pageSize);
        }

        public FP_ROLE GetSingle(Guid primaryID)
        {
            return Db.ROLE.Find(primaryID);
        }

        public bool Update(FP_ROLE instance)
        {
            bool rs = false;
            Db.ROLE.Attach(instance);
            Db.Entry(instance).Property(x => x.NAME).IsModified = true;
            Db.Entry(instance).Property(x => x.ROLECODE).IsModified = true;
            Db.Entry(instance).Property(x => x.CNAME).IsModified = true;
            rs = Db.SaveChanges() > 0 ? true : false;
            return rs;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RoleRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
