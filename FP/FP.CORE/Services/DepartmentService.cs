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
    public class DepartmentService
    {
        private IRepository<FP_DEPARTMENT> _repository = null;

        public IRepository<FP_DEPARTMENT> Repository
        {
            get
            {
                return _repository ?? new DepartmentRepository();
            }
        }

        public IPagedList<FP_DEPARTMENT> GetAll(int page = 1)
        {
            int pageSize = 25;
            return Repository.GetAll(pageSize, page);
        }

        public bool Delete(Guid id)
        {
            return Repository.Delete(id);
        }


        public bool Create(FP_DEPARTMENT model)
        {
            return Repository.Create(model);
        }

        public FP_DEPARTMENT Edit(Guid id)
        {
            return Repository.GetSingle(id);
        }


        public bool EditPost(FP_DEPARTMENT model)
        {
            return Repository.Update(model);
        }


    }
}
