using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FP.CORE.Repositories;
using FP.CORE.Models;
using PagedList;

namespace FP.CORE.Services
{
    public class RoleService
    {
        private IRepository<FP_ROLE> _repository = null;

        public IRepository<FP_ROLE> Repository
        {
            get
            {
                return _repository ?? new RoleRepository();
            }
        }

        public IPagedList<FP_ROLE> GetAll(int page = 1)
        {
            int pageSize = 25;
            return Repository.GetAll(pageSize, page);
        }

        public bool Delete(Guid id)
        {
            return Repository.Delete(id);
        }

        public bool Create(FP_ROLE model)
        {
            return Repository.Create(model);
        }

        public FP_ROLE Edit(Guid id)
        {
            return Repository.GetSingle(id);
        }


        public bool EditPost(FP_ROLE model)
        {
            return Repository.Update(model);
        }



    }
}
