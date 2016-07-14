using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FP.CORE.Services;
using FP.CORE.Models;
using FP.Attributes;
using FP.CORE.Utilities;

namespace FP.Areas.FPUser.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _service = null;

        public UserService Service
        {
            get
            {
                return _service ?? new UserService();
            }
        }

        [Navi]
        public ActionResult Index(int page = 1)
        {
            return View(Service.GetAll(page));
        }

        [Navi]
        public ActionResult Register()
        {         
            ViewBag.Department = Service.GetDepartmentDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult RegisterPost(FP_USER model)
        {
            if (Service.Register(model))
            {
                TempData["message"] = "註冊成功";
            }
            else
            {
                TempData["error_message"] = "註冊失敗";
            }

            return RedirectToAction("Index");
        }


        public ActionResult Login()
        {
            //CORE.Permissions.FPUser user = new CORE.Permissions.FPUser();
            //if (user.IsLoggedIn)
            //{
            //    ViewBag.UserName = user.UserName;
            //    return RedirectToAction("Index", "Home", new { area = "" });
            //}
            return View();
        }

        [HttpPost]
        public ActionResult LogInPost(FP_USER model)
        {
            if(!Service.Login(model))
            {
                return RedirectToAction("Login");
            }

            ViewBag.UserName = model.CNAME;
            return RedirectToAction("Index","Home",new { area=""});
        }

        public ActionResult LogOut()
        {
            UserCookie cookieTool = new UserCookie();
            cookieTool.DeleteUserCookie();
            Session["NaviMenu"] = null; // Clear Navigation Session.
            return RedirectToAction("Login");
        }

        [Navi]
        public ActionResult Permission(Guid id)
        {
            return View(Service.GetUserAllPermission(id));
        }


        [HttpPost]
        public ActionResult PermissionPost(string Feature_Role, Guid userID)
        {
            if (Service.ExeSetUserFeatureRole(Feature_Role, userID))
            {
                TempData["message"] = "編輯成功~";
            }
            else
            {
                TempData["error_message"] = "編輯失敗!!";
            }

            return RedirectToAction("Index");
        }


        [Navi]
        public ActionResult Edit(Guid id)
        {    
            return View(Service.Edit(id));
        }


        public ActionResult EditPost(UserView model)
        {
            if(Service.EditPost(model))
            {
                TempData["message"] = "編輯成功~";
                return RedirectToAction("index");
            }

            TempData["error_message"] = "編輯失敗!!";
            return RedirectToAction("Edit",new { id = model.User.ID});
        }


        public ActionResult Delete(Guid id)
        {
            if (Service.Delete(id))
            {
                TempData["message"] = "刪除成功~";
            }else
            {
                TempData["error_message"] = "刪除失敗!!";
            }      
         
            return RedirectToAction("index");
        }

        [Navi]
        public ActionResult Detail(Guid id)
        {
            return View(Service.GetDetail(id));
        }


    }
}