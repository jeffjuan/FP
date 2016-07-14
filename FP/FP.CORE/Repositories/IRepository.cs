using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace FP.CORE.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        bool Create(TEntity instance);

        bool Update(TEntity instance);

        bool Delete(Guid primaryID);

        TEntity GetSingle(Guid primaryID);

        //IEnumerable<TEntity> GetAll();
        IPagedList<TEntity> GetAll(int pageSize, int page);

        //int SaveChanges();
    }
}
