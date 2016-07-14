using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FP.CORE.Utilities;
using System.Web;

namespace FP.CORE.Permissions
{
    public class FPUser : IUser
    {
        private UserCookie _cookie = new UserCookie();
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public bool IsLoggedIn { get; set; }

        public FPUser()
        {
            UserNo = _cookie.ReadUserCookie();

            // no cookie, redirect to login page
            if (string.IsNullOrEmpty(UserNo))
                RedirectToLgoin();

            this.UserName = GetUserName(UserNo);           
            this.IsLoggedIn = !string.IsNullOrEmpty(UserNo) ? true : false;
        }

        public string GetUserName(string no)
        {
            Services.UserService service = new Services.UserService();
            return service.GetUserName(no);
        }

        public void RedirectToLgoin()
        {
            string virtualPath = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath;
            HttpContext.Current.Response.Redirect(virtualPath + "FPUser/User/Login", true);
        }
    }
}
