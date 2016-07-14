using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FP.CORE.Permissions;
using FP.CORE.Utilities;

namespace FP.Attributes
{
    public class NaviAttribute : FilterAttribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            FPUser user = new FPUser();
            filterContext.Controller.ViewBag.UserName = !string.IsNullOrEmpty(user.UserName) ? user.UserName : "";
            filterContext.Controller.ViewBag.NaviMenu = NaviBuilder.NaviMenu; // 選單
        }
    }
}