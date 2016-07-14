using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FP.CORE.Permissions
{
    public interface IUser
    {
        string UserNo { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        void RedirectToLgoin();
    }
}
