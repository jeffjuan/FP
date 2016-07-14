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

namespace FP.Areas.FPDepartment.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DepartmentService _deptService = null;
        private readonly PermissionService _permService = null;
        private readonly IPermissionProvider _departmentPermission = null;

        // 權限
        public IPermissionProvider DeptPermission
        {
            get
            {
                return _departmentPermission ?? DepartmentPermission.GetInstance;
            }
        }

        public DepartmentService DeptService
        {
            get
            {
                return _deptService ?? new DepartmentService();
            }
        }

        public PermissionService PermService
        {
            get
            {
                return _permService ?? PermissionService.GetInstance;
            }
        }


        [Navi]
        public ActionResult Index(int page = 1)
        {
            // 驗證權限
            bool isAuthorized = PermService.IsAuthorized(DepartmentPermission.View, DeptPermission);

            if (!isAuthorized)
                return RedirectToAction("Index", "Home", new { Area = "" });

            return View(DeptService.GetAll(page));
        }

        public ActionResult Delete(Guid id)
        {
            if (DeptService.Delete(id))
            {
                TempData["message"] = "刪除成功~";
            }
            else
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

        public ActionResult CreatePost(FP_DEPARTMENT model)
        {
            if (DeptService.Create(model))
            {
                TempData["message"] = "新增成功~";
            }
            else
            {
                TempData["error_message"] = "新增失敗!!";
            }

            return RedirectToAction("index");
        }


        [Navi]
        public ActionResult Edit(Guid id)
        {
            return View(DeptService.Edit(id));
        }

        [HttpPost]
        public ActionResult EditPost(FP_DEPARTMENT model)
        {
            if (DeptService.EditPost(model))
            {
                TempData["message"] = "編輯成功~";
            }
            else
            {
                TempData["error_message"] = "編輯失敗!!";
            }
                
            return RedirectToAction("index");
        }


        


    }
}