using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FP.CORE.Models;

namespace FP.CORE.Models
{
    public class UserPermissionView
    {
        public Guid UserID { get; set; }
        public IEnumerable<FP_ROLE> Roles { get; set; }
        //public FP_USER User { get; set; }
        public IEnumerable<FP_FEATURE> Features { get; set; }
        public IEnumerable<FP_USER_FEATURE_ROLE> User_Feature_Role { get; set; }
    }
}
