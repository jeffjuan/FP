using FP.CORE.Services;
using FP.CORE.Utilities;
using FP.CORE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.CORE.Permissions
{
    public class PermissionService
    {
        // Singleton
        private static readonly Lazy<PermissionService> LazyInstance = new Lazy<PermissionService>(() => new PermissionService());
        private PermissionService() { }
        public static PermissionService GetInstance { get { return LazyInstance.Value; } }


        /// <summary>
        /// 要求驗證，permissions 模組提供要求的權限
        /// </summary>
        /// <returns></returns>
        public bool IsAuthorized(Permission requiredPermission, IPermissionProvider feature = null)
        {
            return CheckAccess(requiredPermission, feature);
        }


        /// <summary>
        /// 檢查權限
        /// </summary>
        /// <returns></returns>
        protected bool CheckAccess(Permission requiredPermission, IPermissionProvider feature = null)
        {
            bool msg = false;
            try
            {
                UserCookie cookieFunc = new UserCookie();
                string userNo = cookieFunc.ReadUserCookie(); // Get Login User No
                if (string.IsNullOrEmpty(userNo)) //萬一沒登入，回傳False
                    return msg;

                UserService userService = new UserService();

                // 取得登入者在這支作業設定的角色。
                var userRole = userService.GetUserRoleNameByFeatureCode(userNo, feature.FeatureCode);
                if (userRole == null) // 未設定任何角色，回傳False
                    return msg;

                if (userRole.ROLECODE == "0") //SuperAdmin
                    return msg = true;

                // 調用feature的公用方法，取得這支程式預設的各種權限角色 / Admin,Manager,Editor,General /
                var defaultPermissions = feature.GetDefaultPermissionStereotype();

                //  依登入者角色，找出他在此程式的權限集合 / manage,create,edit,delete,view,void /
                var permissions = defaultPermissions.Where(a => a.RoleName == userRole.NAME).Select(a => a.Permissions).FirstOrDefault();

                //  比對登入者是否有需要的權限
                string auth = permissions.Where(a => a.Name == requiredPermission.Name).Select(a => a.Name).FirstOrDefault();

                msg = string.IsNullOrEmpty(auth) ? false : true;
            }
            catch (Exception ex)
            {
            }
            return msg;
        }


        /// <summary>
        /// 取得使用者在 X 功能的所有權限
        /// </summary>
        /// <returns></returns>
        //public IEnumerable<Permission> GetUserAuthHere(IPermissionProvider feature)
        //{
        //    IEnumerable<Permission> userPermissions = null;
        //    UserCookie cookieFunc = new UserCookie();
        //    string userNo = cookieFunc.ReadUserCookie(); // Get Login User No
        //    UserService userService = new UserService();
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(userNo))
        //        {
        //            // 取得登入者在這一支程式的角色。
        //            string roleName = userService.GetUserRoleNameByFeatureCode(userNo, feature.FeatureCode);
        //            if (string.IsNullOrEmpty(roleName))
        //            {
        //               // 調用feature的公用方法，取得這支程式預設的各種權限角色 / Admin,Manager,Editor,General /
        //                var defaultPermissions = feature.GetDefaultPermissionStereotype();

        //                // 依登入者角色，找出他在此程式的權限集合 / manage,create,edit,delete,view,void /
        //                userPermissions = defaultPermissions.Where(a => a.RoleName == roleName).Select(a => a.Permissions).FirstOrDefault();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return userPermissions;
        //}


        /// <summary>
        /// 依Controller Name, 取得程式功能代碼
        /// </summary>
        /// <returns></returns>
        public string GetFeatureCode(string controllerName)
        {
            if(string.IsNullOrEmpty(controllerName))
                return "";

            FeatureRepository repo = new FeatureRepository();
            return repo.GetFeatureCode(controllerName);
        }
    }
}
