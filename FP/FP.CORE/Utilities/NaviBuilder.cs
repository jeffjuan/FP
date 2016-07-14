using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FP.CORE.Services;
using FP.CORE.Utilities;
using FP.CORE.Models;

namespace FP.CORE.Utilities
{
    public class NaviBuilder
    {
        
        public static string NaviMenu
        {
            get
            {
                return CreateNavi();
            }
        }


        /// <summary>
        /// (讀DB)依據登入者的角色建立主選單
        /// </summary>
        /// <returns></returns>
        public static string CreateNavi()
        {
            if (HttpContext.Current.Session["NaviMenu"] != null)
                return HttpContext.Current.Session["NaviMenu"].ToString();

            // Get User Name
            UserCookie cookieTool = new UserCookie();
            string userNo = cookieTool.ReadUserCookie();

            if (string.IsNullOrEmpty(userNo))
                return "";

            StringBuilder html = new StringBuilder();
            HttpContext.Current.Session["NaviMenu"] = null;
            List<FeatureRoleView> userAllRoles = GetUserAllRolesInAllFeature(userNo);
            bool isAuthorized1 = false;
            bool isAuthorized2 = false;
            try
            {
                FeatureService service = new FeatureService();
                var data = service.GetAll();
                foreach (var feature in data)
                {
                    string[] permissions1 = null;
                    if (feature.PERMISSION != null)
                    {
                        //這程式接受的角色群
                        permissions1 = feature.PERMISSION.Split(',');
                        //檢查user在這個程式的角色
                        var userRole1 = userAllRoles.FirstOrDefault(a => a.FeatureCode == feature.CODE.ToString());
                        if (userRole1 == null) continue;
                        isAuthorized1 = permissions1.Contains(userRole1.RoleName);
                    }

                    if (isAuthorized1)
                    {
                        // 第一層 (no parent)
                        if (string.IsNullOrEmpty(feature.PARENT))
                        {
                            html.Append("<li><a href=\"#\"><i class=\"fa fa-cog fa-fw\"></i>" + feature.NAME.ToString()
                               + "<span class=\"fa arrow\"></span></a>");
                            // 找孩子
                            var child = data.Where(a => a.PARENT == feature.CODE).Count();
                            if (child > 0) // 有孩子
                            {
                                html.Append("<ul class=\"nav nav-second-level\">");
                                foreach (var feature2 in data)
                                {
                                    // 第二層
                                    if (feature2.PARENT == feature.CODE)
                                    {
                                        string[] permissions2 = null;
                                        if (feature2.PERMISSION != null)
                                        {
                                            permissions2 = feature2.PERMISSION.Split(',');
                                            //user在這個程式的角色
                                            var userRole2 = userAllRoles.FirstOrDefault(a => a.FeatureCode == feature2.CODE.ToString());
                                            if (userRole2 == null) continue;
                                            // 檢查權限
                                            isAuthorized2 = permissions2.Contains(userRole2.RoleName);
                                            if (isAuthorized2)
                                            {
                                                html.Append("<li><a href=\"" + feature2.URL + "\">" + feature2.NAME + "</a></li>");
                                                isAuthorized2 = false;
                                            }
                                        }
                                    }
                                }
                                html.Append("</ul>");
                            }
                            html.Append("</li>");
                        }

                        isAuthorized1 = false;
                    }
                    else
                    {
                        continue;
                    }
                }
                HttpContext.Current.Session["NaviMenu"] = html.ToString();
            }
            catch (Exception ex)
            {
            }
            return HttpContext.Current.Session["NaviMenu"].ToString();
        }

        /// <summary>
        /// 取得User在所有作業程式的角色
        /// </summary>
        /// <param name="userNo"></param>
        /// <returns></returns>
        private static List<FeatureRoleView> GetUserAllRolesInAllFeature(string userNo)
        {
            UserService service = new UserService();
            return service.GetUserAllRolesInAllFeature(userNo);
        }

    }

    
}
