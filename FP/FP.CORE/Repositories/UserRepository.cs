using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FP.CORE.Models;
using FP.CORE.DAL;
using PagedList;
using System.Transactions;

namespace FP.CORE.Repositories
{
    public class UserRepository : IRepository<FP_USER>
    {
        private readonly FP_EFContext _db = new FP_EFContext();

        public FP_EFContext Db
        {
            get
            {
                return _db;
            }
        }


        public bool Create(FP_USER instance)
        {
            bool rs = false;
            try
            {
                Db.USER.Add(instance);
                rs = Db.SaveChanges() > 0 ? true : false;
            }
            catch(Exception ex)
            {

            }
            
            return rs;
        }

        public bool Delete(Guid primaryID)
        {
            bool rs = false;
            try
            {
                var data = Db.USER.Find(primaryID);
                Db.USER.Remove(data);
                rs = Db.SaveChanges() > 0 ? true : false;
            }
            catch (Exception ex)
            {

            }

            return rs;
        }

        public IPagedList<FP_USER> GetAll(int pageSize,int page = 1)
        {
            return Db.USER.OrderBy(a=>a.CreDateTime).ToPagedList(page, pageSize);
        }

        public FP_USER GetSingle(Guid primaryID)
        {
            return Db.USER.Find(primaryID);
        }

        public FP_USER GetOne(FP_USER instance)
        {
            return Db.USER.FirstOrDefault(a => a.ACCOUNT == instance.ACCOUNT && a.PW == instance.PW);
        }

        public string GetNameByUserNo(string userNo)
        {
            if (string.IsNullOrEmpty(userNo))
                return "";

            string name = string.Empty;
            try
            {
                name = Db.USER.FirstOrDefault(a => a.USERNO == userNo).CNAME;
            }
            catch(Exception ex)
            {

            }
            return name;
        }

        public bool Update(FP_USER instance)
        {
            bool rs = false;
            try
            {
                Db.USER.Attach(instance);
                Db.Entry(instance).Property(x => x.CNAME).IsModified = true;
                Db.Entry(instance).Property(x => x.DEPARTMENTCODE).IsModified = true;
                Db.Entry(instance).Property(x => x.EMAIL).IsModified = true;
                Db.Entry(instance).Property(x => x.ENABLE).IsModified = true;
                Db.Entry(instance).Property(x => x.ENAME).IsModified = true;
                Db.Entry(instance).Property(x => x.USERNO).IsModified = false;
                rs = Db.SaveChanges() > 0 ? true : false;
            }
            catch(Exception ex)
            {

            }
           
            return rs;

            // 寫法二
            //Db.Entry(instance).State = System.Data.Entity.EntityState.Modified;
            //rs = Db.SaveChanges() > 0 ? true : false;

        }

        public FP_ROLE GetUserRoleNameByFeatureCode(string userNo, string featureCode)
        {
            FP_ROLE rs = null;
            try
            {
                rs = (from a in Db.USER
                      join b in Db.USER_FEATURE_ROLE
                      on a.ID equals b.USER_ID
                      join c in Db.ROLE
                      on b.ROLE_CODE equals c.ROLECODE
                      where a.USERNO == userNo & b.FEATURE_CODE == featureCode
                      select c).FirstOrDefault();
            }
            catch (Exception ex)
            {
            }
            return rs;
        }

        /// <summary>
        /// 依據使用者 no 取得完整角色物件資料
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        //public FP_ROLE GetUserRoleData(string userNo)
        //{
        //    var data = (from a in Db.USER
        //               join b in Db.ROLE
        //               on a.ROLECODE equals b.ROLECODE
        //               where a.USERNO == userNo
        //               select b).FirstOrDefault();

        //    return data;
        //}

        public IEnumerable<FP_USER_FEATURE_ROLE> GetUserFeatureRoleByUserID(Guid id)
        {
           return Db.USER_FEATURE_ROLE.Where(a => a.USER_ID == id).ToList();
        }

        /// <summary>
        /// 寫入使用者在各作業項目的權限角色
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool SetUserFeatureRole(IEnumerable<FP_USER_FEATURE_ROLE> addList, IEnumerable<FP_USER_FEATURE_ROLE> modifyList)
        {
            bool rs = false;
            using (TransactionScope ts = new TransactionScope())
            {
                // 寫入新增
                if(addList.Count() > 0)
                {
                    foreach (var item in addList)
                    {
                        Db.USER_FEATURE_ROLE.Add(item);
                    }
                    Db.SaveChanges();
                }

                // 寫入修改
                if (modifyList.Count() > 0)
                {
                    foreach (var item in modifyList)
                    {
                        Db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }
                    Db.SaveChanges();
                }           

                ts.Complete();
                rs = true;
            }
                
            return rs;
        }

        /// <summary>
        /// 移除使用者在各作業項目的權限角色
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool RemoveUserFeatureRole(IEnumerable<Guid> list)
        {
            bool rs = false;
            foreach (var item in list)
            {
               var data = Db.USER_FEATURE_ROLE.Find(item);
               Db.USER_FEATURE_ROLE.Remove(data);
            }

            rs = Db.SaveChanges() > 0 ? true : false;
            return rs;
        }


        public List<FeatureRoleView> GetUserAllRolesInAllFeature(string userNo)
        {
            var userId = Db.USER.FirstOrDefault(a => a.USERNO == userNo).ID;
            var data = (from a in Db.USER_FEATURE_ROLE
                        join b in Db.ROLE
                        on a.ROLE_CODE equals b.ROLECODE
                        where a.USER_ID == userId
                        select new FeatureRoleView
                        {
                            FeatureCode = a.FEATURE_CODE,
                            RoleName = b.NAME
                        }).ToList();
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
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UserRepository() {
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
