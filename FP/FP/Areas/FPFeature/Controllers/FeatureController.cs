using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FP.CORE.Models;
using FP.CORE.Services;
using FP.Attributes;

namespace FP.Areas.FPFeature.Controllers
{
    public class FeatureController : Controller
    {
        private FeatureService _service = new FeatureService();

        public FeatureService Service
        {
            get { return _service; }
        }

        [Navi]
        public ActionResult Index(int page = 1)
        {         
            return View(Service.GetAll(page));
        }

        [Navi]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(FP_FEATURE model)
        {
            if(!Service.CreateFeature(model))
            {
                TempData["error_message"] = "新增失敗";
                return View("Create");
            }
            TempData["message"] = "新增成功";
            return RedirectToAction("Index");
        }

        [Navi]
        public ActionResult Edit(Guid id)
        {         
            return View(Service.GetSingle(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(FP_FEATURE model)
        {
            if (!Service.UpdateFeature(model))
            {
                TempData["error_message"] = "修改失敗";
                return RedirectToAction("Edit",new { id = model.ID});
            }
            TempData["message"] = "修改成功";
            return RedirectToAction("Index");
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



    }
}