using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Web;
using StaffRating.Domain.Repository.Interfaces;
using System.Web.Mvc;
using StaffRating.WebUI.Models;
using StaffRating.Domain.Entities;

namespace StaffRating.WebUI.Controllers.Services
{
    public class CategoryServiceController : Controller
    {
        private IDBRepository db;
        public CategoryServiceController(IDBRepository _db)
        {
            db = _db;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();

            base.Dispose(disposing);
        }

        //Read
        public ActionResult ReadForGrid([DataSourceRequest] DataSourceRequest request)
        {
            var categories = db.CATEGORIES.Get().ToDataSourceResult(request, c => new CategoryViewModel
            {
                id = c.ID,
                name = c.NAME
            });

            return Json(categories);
        }

        //Create
        [HttpPost]
        public ActionResult CreateForGrid([DataSourceRequest]DataSourceRequest request, CategoryViewModel category)
        {
            
            if (ModelState.IsValid)
            {
                CATEGORY entity = category.ToEntity(new CATEGORY());
                try
                {
                    db.CATEGORIES.Create(entity);
                    category.id = entity.ID;

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("CATEGORY", ex.Message);
                }
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState));

        }

        //Update
        [HttpPost]
        public ActionResult UpdateForGrid([DataSourceRequest]DataSourceRequest request, CategoryViewModel category)
        {
            
                CATEGORY entity = db.CATEGORIES.Get().FirstOrDefault(c => c.ID == category.id);

                if (entity == null)
                {
                    ModelState.AddModelError("CATEGORY", String.Format("Категория '{0}' не обнаружена в базе данных!", category.name));
                }
                else
                {
                    //TODO Validate not found
                    entity = category.ToEntity(entity);
                }

            if (ModelState.IsValid)
            {
                try
                {
                    db.CATEGORIES.Update(entity);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("CATEGORY", ex.Message);
                }
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState));

        }

        //Delete
        [HttpPost]
        public ActionResult DestroyForGrid([DataSourceRequest]DataSourceRequest request, CategoryViewModel category)
        {
           
                CATEGORY entity = db.CATEGORIES.Get().FirstOrDefault(c => c.ID == category.id);
                if (entity == null)
                {
                    ModelState.AddModelError("CATEGORY", String.Format("Категория '{0}' не обнаружена в базе данных!", category.name));
                }

                //Used  in test 
                if (db.TESTS.Get().Any(p => p.CATEGORYID == category.id))
                {
                    ModelState.AddModelError("CATEGORY","Невозможно удалить данную запись!<br>" + String.Format("К категории '{0}' относятся следующие тесты: <ul><li> {1} </li></ul>", category.name, String.Join("</li><li>", db.TESTS.Get().Where(p => p.CATEGORYID == entity.ID).Select(s => s.NAME))));
                }

            if (ModelState.IsValid)
            {
                try
                {
                    db.CATEGORIES.Delete(entity);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("CATEGORY", ex.Message);
                }
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState));

        }

    }
}