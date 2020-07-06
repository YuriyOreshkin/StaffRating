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
    public class AnswerServiceController : Controller
    {
        private IDBRepository db;
        public AnswerServiceController(IDBRepository _db)
        {
            db = _db;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();

            base.Dispose(disposing);
        }

        //Read
        public ActionResult ReadForGrid([DataSourceRequest] DataSourceRequest request,long questionid)
        {
            var answers = db.ANSWERS.Get().Where(q=>q.QUESTIONID == questionid).OrderBy(o=>o.ORDERNUM).ToDataSourceResult(request, a => new AnswerViewModel
            {
                id = a.ID,
                ordernum = a.ORDERNUM,
                text = a.TEXT,
                good = a.GOOD,
                questionid = a.QUESTIONID,
            });

            return Json(answers);
        }

        //Create
        [HttpPost]
        public ActionResult CreateForGrid([DataSourceRequest]DataSourceRequest request, AnswerViewModel answer,long _questionid)
        {
            if (!db.QUESTIONS.Get().Any(q=>q.ID == _questionid))
            {
                ModelState.AddModelError("ANSWER", "Невозможно добавить данный ответ!<br> Ошибка: Вопрос не обнаружен в базе данных!");
            }
            if (ModelState.IsValid)
            {
                answer.questionid = _questionid;
                answer.ordernum = (short)(db.ANSWERS.Get().Where(q => q.QUESTIONID == _questionid).Count()+1);

                ANSWER entity = answer.ToEntity(new ANSWER());
                try
                {
                    db.ANSWERS.Create(entity);
                    answer.id = entity.ID;

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ANSWER", ex.Message);
                }
            }

            return Json(new[] { answer }.ToDataSourceResult(request, ModelState));

        }

        //Update
        [HttpPost]
        public ActionResult UpdateForGrid([DataSourceRequest]DataSourceRequest request, AnswerViewModel answer)
        {
            ANSWER entity = db.ANSWERS.Get().FirstOrDefault(c => c.ID == answer.id);

            if (entity == null)
            {
                ModelState.AddModelError("ANSWER", "Невозможно редактировать данный ответ!< br > Ошибка: Ответ не обнаружен в базе данных!");
            }
              
            if (ModelState.IsValid)
            {
                entity = answer.ToEntity(entity);

                try
                {
                    db.ANSWERS.Update(entity);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ANSWER", ex.Message);
                }
            }

            return Json(new[] { answer }.ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public JsonResult SetOrder(long id, short step)
        {
           ANSWER entityFrom = db.ANSWERS.Get().FirstOrDefault(a => a.ID == id);

            if (entityFrom != null)
            {

                ANSWER entityTo = db.ANSWERS.Get().FirstOrDefault(a => a.QUESTIONID == entityFrom.QUESTIONID && a.ORDERNUM == entityFrom.ORDERNUM + step);

                if (entityTo != null)
                {
                    try
                    {
                        entityFrom.ORDERNUM = (short)(entityFrom.ORDERNUM + step);
                        db.ANSWERS.Update(entityFrom);
                        entityTo.ORDERNUM = (short)(entityTo.ORDERNUM - step);
                        db.ANSWERS.Update(entityTo);
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
                return Json(new { result = "errors", errors = "Ошибка: Ответ не обнаружен в базе данных!" }, JsonRequestBehavior.AllowGet);
            }

           
        }


        //Delete
        [HttpPost]
        public ActionResult DestroyForGrid([DataSourceRequest]DataSourceRequest request, AnswerViewModel answer)
        {
            ANSWER entity = db.ANSWERS.Get().FirstOrDefault(a => a.ID == answer.id);

            if (entity == null)
            {
                ModelState.AddModelError("ANSWER", "Ответ не обнаружен в базе данных!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.ANSWERS.Delete(entity);
                    //Recalculate ordernum
                    db.ANSWERS.Get().Where(a => a.QUESTIONID == answer.questionid && a.ORDERNUM > answer.ordernum).ToList().ForEach(an =>
                    {
                        an.ORDERNUM -= 1;
                        db.ANSWERS.Update(an);
                    });

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ANSWER", ex.Message);
                }
            }

            return Json(new[] { answer }.ToDataSourceResult(request, ModelState));

        }
        
    }
}