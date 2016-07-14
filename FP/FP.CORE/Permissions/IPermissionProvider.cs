using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.CORE.Permissions
{
    public interface IPermissionProvider
    {
        /// <summary>
        /// 公用方法
        /// </summary>
        /// <returns></returns>
        IEnumerable<PermissionStereotype> GetDefaultPermissionStereotype();

        string FeatureCode { get; }
    }

    public class PermissionStereotype
    {
        public string RoleName { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
    }
}
