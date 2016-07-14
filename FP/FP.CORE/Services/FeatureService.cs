using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FP.CORE.Models;
using FP.CORE.Repositories;
using PagedList;

namespace FP.CORE.Services
{
    public class FeatureService
    {
        private FeatureRepository _repository = new FeatureRepository();

        public FeatureRepository Repository
        {
            get { return _repository; }
        }

        public IPagedList<FP_FEATURE> GetAll(int page = 1)
        {
            int pageSize = 25;
            return Repository.GetAll(pageSize,page);
        }

        public FP_FEATURE GetSingle(Guid id)
        {
            return Repository.GetSingle(id);
        }

        public bool CreateFeature(FP_FEATURE data)
        {
           return Repository.Create(data);
        }

        public bool UpdateFeature(FP_FEATURE data)
        {
            return Repository.Update(data);
        }

        public bool Delete(Guid id)
        {
            return Repository.Delete(id);
        }




    }
}
