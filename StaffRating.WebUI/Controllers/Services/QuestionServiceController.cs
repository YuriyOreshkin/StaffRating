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
        public ActionResult ReadForGrid([DataSourceRequest] DataSourceRequest request,long _testid)
        {
            var questions = db.QUESTIONS.Get().Where(t=>t.TESTID == _testid).ToDataSourceResult(request, q => new QuestionViewModel
            {
                id = q.ID,
                ordernum=q.ORDERNUM,
                text = q.TEXT,
                rating = q.RATING,
                testid = q.TESTID,
            });

            return Json(questions);
        }

        //Create
        [HttpPost]
        public ActionResult CreateForGrid([DataSourceRequest]DataSourceRequest request,QuestionViewModel question, long _testid)
        {
            if (!db.TESTS.Get().Any(t => t.ID == _testid))
            {
                ModelState.AddModelError("QUESTION", "Невозможно добавить данный вопрос!<br> Ошибка: Тест не обнаружен в базе данных!");
            }

            if (ModelState.IsValid)
            {
                question.testid = _testid;
                question.ordernum = (short)(db.QUESTIONS.Get().Where(q => q.TESTID == _testid).Count() + 1);

                QUESTION entity = question.ToEntity(new QUESTION());
                try
                {
                    db.QUESTIONS.Create(entity);
                    question.id = entity.ID;

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("QUESTION", ex.Message);
                }
            }

            return Json(new[] { question }.ToDataSourceResult(request, ModelState));

        }

        //Update
        [HttpPost]
        public ActionResult UpdateForGrid([DataSourceRequest]DataSourceRequest request, QuestionViewModel question)
        {
          
             if (!db.QUESTIONS.Get().Any(c => c.ID == question.id))
             {
                    ModelState.AddModelError("QUESTION", "Невозможно редактировать данный вопрос!< br > Ошибка: Вопрос не обнаружен в базе данных!");
             }
                
            if (ModelState.IsValid)
            {
                QUESTION entity = db.QUESTIONS.Get().FirstOrDefault(c => c.ID == question.id);
                entity = question.ToEntity(entity);

                try
                {
                    db.QUESTIONS.Update(entity);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("QUESTION", ex.Message);
                }
            }

            return Json(new[] { question }.ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public JsonResult SetOrder(long id, short step)
        {
            QUESTION entityFrom = db.QUESTIONS.Get().FirstOrDefault(q => q.ID == id);

            if (entityFrom != null)
            {

                QUESTION entityTo = db.QUESTIONS.Get().FirstOrDefault(q => q.TESTID == entityFrom.TESTID && q.ORDERNUM == entityFrom.ORDERNUM + step);

                if (entityTo != null)
                {
                    try
                    {
                        entityFrom.ORDERNUM = (short)(entityFrom.ORDERNUM + step);
                        db.QUESTIONS.Update(entityFrom);
                        entityTo.ORDERNUM = (short)(entityTo.ORDERNUM - step);
                        db.QUESTIONS.Update(entityTo);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { result = "errors", errors = "Ошибка: " + ex.Message }, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json(new { result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = "errors", errors = "Ошибка: Вопрос не обнаружен в базе данных!" }, JsonRequestBehavior.AllowGet);
            }


        }

        /*/Delete
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