using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FP.CORE.Models;
using FP.CORE.DAL;
using PagedList;

namespace FP.CORE.Repositories
{
    public class FeatureRepository : IRepository<FP_FEATURE>
    {
        private readonly FP_EFContext _db =  new FP_EFContext();

        public FP_EFContext Db
        {
            get
            {
                return _db;
            }
        }

        public bool Create(FP_FEATURE instance)
        {
            bool rs = false;
            Db.FEATURE.Add(instance);
            int x = Db.SaveChanges();
            rs = x > 0 ? true : false;
            return rs;
        }

        public bool Delete(Guid primaryID)
        {
            bool rs = false;
            var instance = Db.FEATURE.Find(primaryID);
            Db.FEATURE.Remove(instance);
            rs = Db.SaveChanges() > 0 ? true : false;
            return rs;
        }

        public IPagedList<FP_FEATURE> GetAll(int pageSize,int page=1)
        {
           return Db.FEATURE.OrderBy(a => a.CODE).ToPagedList(page, pageSize);
        }

        public FP_FEATURE GetSingle(Guid primaryID)
        {
            return Db.FEATURE.Find(primaryID);
        }

        public bool Update(FP_FEATURE instance)
        {
            bool rs = false;
            Db.FEATURE.Attach(instance);
            Db.Entry(instance).Property(x => x.NAME).IsModified = true;
            Db.Entry(instance).Property(x => x.CODE).IsModified = true;
            Db.Entry(instance).Property(x => x.CONTROLLER).IsModified = true;
            Db.Entry(instance).Property(x => x.PERMISSION).IsModified = true;
            Db.Entry(instance).Property(x => x.PARENT).IsModified = true;
            Db.Entry(instance).Property(x => x.ISMENU).IsModified = true;
            Db.Entry(instance).Property(x => x.URL).IsModified = true;
            rs = Db.SaveChanges() > 0 ? true : false;
            return rs;
        }

        public string GetFeatureCode(string controllerName)
        {
            string data = string.Empty;
            try
            {
                data = Db.FEATURE.FirstOrDefault(a => a.CONTROLLER == controllerName).CODE;
            }catch(Exception ex)
            {
            }         
            return data;
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
                    _db.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~FeatureRepository() {
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
