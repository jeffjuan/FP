using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FP.CORE.Models;
using FP.CORE.Services;
using FP.CORE.Permissions;
using FP.CORE.Permissions.ControllerPermissions;
using FP.Attributes;

namespace FP.Areas.FPRole.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleService _service = new RoleService();

        public RoleService Service
        {
            get { return _service; }
        }

        [Navi]
        public ActionResult Index(int page = 1)
        {
            return View(Service.GetAll(page));
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
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreatePost(FP_ROLE model)
        {
            if (Service.Create(model))
            {
                TempData["message"] = "新增成功~";
            }else
            {
                TempData["error_message"] = "新增失敗!!";
            }
               
            return RedirectToAction("index");
        }

        [Navi]
        public ActionResult Edit(Guid id)
        {
            return View(Service.Edit(id));
        }

        public ActionResult EditPost(FP_ROLE model)
        {
            if (Service.EditPost(model))
            {
                TempData["message"] = "編輯成功~";
            }else
            {
                TempData["error_message"] = "編輯失敗!!";
            }
                
            return RedirectToAction("index");
        }


    }
}