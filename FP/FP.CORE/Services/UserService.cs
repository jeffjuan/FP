using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FP.CORE.Models;
using FP.CORE.Repositories;
using System.Web.Mvc;
using PagedList;
using FP.CORE.Utilities;
using System.Text.RegularExpressions;

namespace FP.CORE.Services
{
    public class UserService
    {
        private UserRepository _repository = null;

        public UserRepository Repository
        {
            get
            {
                return _repository ?? new UserRepository();
            }
        }

        /// <summary>
        /// 取得登入者在某功能的角色物件
        /// </summary>
        /// <param name="userName"></param>
        public FP_ROLE GetUserRoleNameByFeatureCode(string userNo, string featureCode)
        {
           return Repository.GetUserRoleNameByFeatureCode(userNo, featureCode);
        }

        //public FP_ROLE GetUserRole(string userNo)
        //{
        //    return Repository.GetUserRoleData(userNo);
        //}

        /// <summary>
        /// 取得部門下拉選單
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetDepartmentDropDownList()
        {
            DepartmentService deptService = new DepartmentService();
            List<SelectListItem> departmentList = new List<SelectListItem>();

            var deptData = deptService.GetAll();
            foreach (var item in deptData)
            {
                departmentList.Add(new SelectListItem()
                {
                    Text = item.NAME,
                    Value = item.CODE.ToString(),
                    Selected = false
                });
            }

            return departmentList;
        }


        public bool Register(FP_USER data)
        {
            data.USERNO = "UA" + DateTime.Now.ToString("MMddHHmm");
            data.CreDateTime = DateTime.Now;
            return Repository.Create(data);
        }

        public IPagedList<FP_USER> GetAll(int page = 1)
        {
            int pageSize = 25;
            return Repository.GetAll(pageSize,page);
        }

        public bool Login(FP_USER model)
        {
            bool rs = false;
            var user = Repository.GetOne(model);
            if(user == null)
            {
                return rs;
            }

            // 記入COOKIE
            UserCookie cookie = new UserCookie();
            cookie.SetCUserookie(user.USERNO);
            rs = true;
            return rs;
        }

        public UserView Edit(Guid id)
        {           
            UserView data = new UserView();
            data.User = Repository.GetSingle(id);
            data.Department = GetDepartmentDropDownList();
            return data;
        }

        //EditPost
        public bool EditPost(UserView data)
        {
            return Repository.Update(data.User);
        }

        public bool Delete(Guid id)
        {
            return Repository.Delete(id);
        }

        public FP_USER GetDetail(Guid id)
        {
            return Repository.GetSingle(id);
        }

        public string GetUserName(string userNo)
        {
            return Repository.GetNameByUserNo(userNo);
        }

        public IEnumerable<FP_USER_FEATURE_ROLE> GetUserFeatureRole(Guid id)
        {
           return Repository.GetUserFeatureRoleByUserID(id);
        }

        public UserPermissionView GetUserAllPermission(Guid id)
        {
            UserPermissionView data = new UserPermissionView();
            RoleService roleService = new RoleService();
            FeatureService featureService = new FeatureService();
            UserService userService = new UserService();
            data.UserID = id;
            data.Roles = roleService.GetAll();
            data.Features = featureService.GetAll();
            data.User_Feature_Role = userService.GetUserFeatureRole(id); // 取得使用者各項作業的權限角色

            return data;
        }


        /// <summary>
        /// 執行設定使用者各項作業的權限角色
        /// </summary>
        /// <param name="featureRole"></param>
        /// <param name="userNo"></param>
        /// <returns></returns>
        public bool ExeSetUserFeatureRole(string featureRole, Guid userID)
        {
            bool rs = false;
            try
            {
                // [後台資料]
                var userFeatureRole = Repository.GetUserFeatureRoleByUserID(userID);

                // [前台資料]
                // 取出字串分割為陣列。字串格式："100_1,110_2,120_2"
                string[] charA = featureRole.Split(',');

                // 刪除[前台資料]無，但[後台資料]有的值
                if(userFeatureRole.Count() > 0)
                {
                    DeleteFeatureCodeUserRemoved(userFeatureRole, charA);
                }
                
                Regex regexForCode = new Regex(@"\d+");
                Regex regexForRole = new Regex(@"\d+$");

                List<FP_USER_FEATURE_ROLE> userFeatureRoleList_AddNew = new List<FP_USER_FEATURE_ROLE>();
                List<FP_USER_FEATURE_ROLE> userFeatureRoleList_Modify = new List<FP_USER_FEATURE_ROLE>();

                // 從前台資料取出資料，寫入設定的程式與角色對應關係
                foreach (var item in charA)
                {
                    Match matchFeature = regexForCode.Match(item); // 程式代碼
                    Match matchRole = regexForRole.Match(item);// 角色 CODE
                                                               
                    var f = userFeatureRole.FirstOrDefault(a => a.FEATURE_CODE == matchFeature.Value.ToString());
                    if (f != null)
                    {
                        f.ROLE_CODE = matchRole.Value.ToString();

                        userFeatureRoleList_Modify.Add(f);
                    }
                    else
                    {
                        FP_USER_FEATURE_ROLE u = new FP_USER_FEATURE_ROLE();
                        u.FEATURE_CODE = matchFeature.Value.ToString();
                        u.ROLE_CODE = matchRole.Value.ToString();
                        u.USER_ID = userID;

                        userFeatureRoleList_AddNew.Add(u);
                    }
                }
                // 寫入 DB
                rs = Repository.SetUserFeatureRole(userFeatureRoleList_AddNew, userFeatureRoleList_Modify);
            }
            catch (Exception ex)
            {

            }
            return rs;
        }


        /// <summary>
        /// 刪除[前台資料]無，但[後台資料]有的程式代碼
        /// </summary>
        /// <param name="fr">資料庫資料</param>
        /// <param name="charA">前台資料</param>
        private void DeleteFeatureCodeUserRemoved(IEnumerable<FP_USER_FEATURE_ROLE> fr, string[] charA)
        {
            List<Guid> GuidList = new List<Guid>();
            // 取出程式代碼放在List
            List<string> fcode = new List<string>();
            Regex regexForCode = new Regex(@"\d+");

            foreach (var item in charA)
            {
                Match matchFeatureCode = regexForCode.Match(item);
                fcode.Add(matchFeatureCode.Value.ToString());// 程式代碼
            }

            try
            {
                foreach (var dbItem in fr)
                {
                    if (!fcode.Contains(dbItem.FEATURE_CODE))
                    {
                        GuidList.Add(dbItem.ID); // 找出需移除的                  
                    }
                }

                if(GuidList.Count() > 0)
                    Repository.RemoveUserFeatureRole(GuidList);
            }
            catch (Exception ex)
            {
                //logger.Error(ex.ToString());
            }
        }


        public List<FeatureRoleView> GetUserAllRolesInAllFeature(string userNo)
        {
            if(string.IsNullOrEmpty(userNo))
            {
                return null;
            }
            return Repository.GetUserAllRolesInAllFeature(userNo);
        }





    }
}
