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
    public class QuestionServiceController : Controller
    {
        private IDBRepository db;
        public QuestionServiceController(IDBRepository _db)
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
            var questions = db.QUESTIONS.Get().ToDataSourceResult(request, q => new QuestionViewModel
            {
                id = q.ID,
                text = q.TEXT,
                rating = q.RATING,
                testid = q.TESTID,
            });

            return Json(questions);
        }

        /*/Create
        [HttpPost]
        public ActionResult CreateForGrid([DataSourceRequest]DataSourceRequest request, TestViewModel test)
        {
            
            if (ModelState.IsValid)
            {
                TEST entity = test.ToEntity(new TEST());
                try
                {
                    db.TESTS.Create(entity);
                    test.id = entity.ID;

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("TEST", ex.Message);
                }
            }

            return Json(new[] { test }.ToDataSourceResult(request, ModelState));

        }

        //Update
        [HttpPost]
        public ActionResult UpdateForGrid([DataSourceRequest]DataSourceRequest request, TestViewModel test)
        {
            if (ModelState.IsValid)
            {
                TEST entity = db.TESTS.Get().FirstOrDefault(c => c.ID == test.id);

                if (entity == null)
                {
                    ModelState.AddModelError("TEST", String.Format("Тест '{0}' не обнаружен в базе данных!", test.name));
                }
                else
                {
                    //TODO Validate not found
                    entity = test.ToEntity(entity);
                }
                
                try
                {
                    db.TESTS.Update(entity);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("TEST", ex.Message);
                }
            }

            return Json(new[] { test }.ToDataSourceResult(request, ModelState));

        }

        //Delete
        [HttpPost]
        public ActionResult DestroyForGrid([DataSourceRequest]DataSourceRequest request, CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                CATEGORY entity = db.CATEGORIES.Get().FirstOrDefault(c => c.ID == category.id);
                if (entity == null)
                {
                    ModelState.AddModelError("CATEGORY", String.Format("Категория '{0}' не обнаружена в базе данных!", category.name));
                }

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
        */
    }
}