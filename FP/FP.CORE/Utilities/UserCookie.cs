using System;
using System.Web;

namespace FP.CORE.Utilities
{
    public class UserCookie
    {
        /// <summary>
        /// Set User Cookie
        /// </summary>
        public void SetCUserookie(string no)
        {
            try
            {
                no = StringExtensions.ToBase64(no); // 加密
                HttpCookie cookie = new HttpCookie("User");
                cookie["No"] = no;
                cookie.Expires = DateTime.Now.AddDays(1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception ex)
            {
            }
        }


        /// <summary>
        /// Return User NO
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public string ReadUserCookie()
        {
            string no = string.Empty;
            if (HttpContext.Current.Request.Cookies["User"] != null)
            {
                if (HttpContext.Current.Request.Cookies["User"]["No"] != null)
                {
                    no = HttpContext.Current.Request.Cookies["User"]["No"].ToString();
                    no = StringExtensions.Base64Decode(no); // 解密
                }
            }
            return no;
        }


        /// <summary>
        /// Delete User Cookie
        /// </summary>
        /// <returns></returns>
        public void DeleteUserCookie()
        {
            if (HttpContext.Current.Request.Cookies["User"] != null)
            {
                if (HttpContext.Current.Request.Cookies["User"]["No"] != null)
                {
                    HttpCookie cookie = new HttpCookie("User");
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
            }
        }

        /// <summary>
        /// 導至首頁
        /// </summary>
        public void DirectToWelcome()
        {
            string virtualPath = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath;
            HttpContext.Current.Response.Redirect(virtualPath + "Home/Index", true);
        }
    }
}
