using System;
using System.Collections.Generic;

namespace FP.CORE.Permissions.ControllerPermissions
{
    public class DepartmentPermission : IPermissionProvider
    {
        private static readonly Lazy<DepartmentPermission> LazyInstance = new Lazy<DepartmentPermission>(() => new DepartmentPermission());
        private DepartmentPermission() { }
        public static DepartmentPermission GetInstance { get { return LazyInstance.Value; } }

        private readonly string _featureCode = null;

        // 定義權限
        //public static readonly Permission Manage = new Permission { Description = "Manage Order", Name = "Manage" };
        public static readonly Permission Create = new Permission { Description = "Create Order", Name = "Create" };
        public static readonly Permission Edit = new Permission { Description = "Edit Order", Name = "Edit" };
        public static readonly Permission Delete = new Permission { Description = "Delete Order", Name = "Delete" };
        public static readonly Permission Void = new Permission { Description = "Void Order", Name = "Void" }; //作廢
        public static readonly Permission View = new Permission { Description = "View Order", Name = "View" };

        /// <summary>
        /// 作業代碼
        /// </summary>
        public string FeatureCode
        {
            get
            {
                return _featureCode?? PermissionService.GetInstance.GetFeatureCode("Department");
            }
        }


        /// <summary>
        /// 公用方法 -- 定義角色與所屬權限
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PermissionStereotype> GetDefaultPermissionStereotype()
        {
            return new[] {
                new PermissionStereotype {
                    RoleName = "SuperAdmin",
                    Permissions = new[] { Create,Edit, Delete, Void,View }
                },
                new PermissionStereotype {
                    RoleName = "Admin",
                    Permissions = new[] { Create,Edit, Delete, Void }
                },
                new PermissionStereotype {
                    RoleName = "Manager",
                    Permissions = new[] { Create,Edit, Delete, Void,View }
                },
                new PermissionStereotype {
                    RoleName = "Editor",
                    Permissions = new[] { View , Edit, Void }
                },
                new PermissionStereotype {
                    RoleName = "General",
                    Permissions = new[] { View }
                },
            };
        }
    }
}
