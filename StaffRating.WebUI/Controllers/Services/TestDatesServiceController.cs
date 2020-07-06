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
    public class TestDatesServiceController : Controller
    {
        private IDBRepository db;
        public TestDatesServiceController(IDBRepository _db)
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
            var testsdates = db.TESTSDATES.Get().Where(d=>d.TESTID == _testid).ToDataSourceResult(request, t => new TestDatesViewModel
            {
                id = t.ID,
                begin = t.BEGIN,
                end= t.END,
                testid = t.TESTID
            });

            return Json(testsdates);
        }

        private void CheckingErrors(TestDatesViewModel testsdates)
        {
            if (testsdates.end < testsdates.begin)
            {
                ModelState.AddModelError("TESTDATES", String.Format("Дата начала периода '{0}' должна быть меньше даты его окончания '{1}' !", testsdates.begin.ToString("dd.MM.yyyy H:mm"), testsdates.end.ToString("dd.MM.yyyy H:mm")));
            }

            /*if (db.REPORTPERIOD.Any(r => r.PERIODNAME_ID == reportPeriod.periodName.id && r.PERIODYEAR == reportPeriod.periodYear))
            {
                var uniq = db.REPORTPERIOD.FirstOrDefault(r => r.PERIODNAME_ID == reportPeriod.periodName.id && r.PERIODYEAR == reportPeriod.periodYear);
                if (uniq != null)
                {
                    if (uniq.ID != reportPeriod.id)
                    {
                        ModelState.AddModelError("REPORTPERIOD", String.Format("Отчетный период '{0}' за '{1}' год уже заведён!", reportPeriod.periodName.Name, reportPeriod.periodYear));
                    }
                }

            }*/

        }


        //Create
        [HttpPost]
        public ActionResult CreateForGrid([DataSourceRequest]DataSourceRequest request, TestDatesViewModel testsdates, long _testid)
        { 

            if (!db.TESTS.Get().Any(t => t.ID == _testid))
            {
                ModelState.AddModelError("TESTDATES", "Невозможно добавить данный период!<br> Ошибка: Тест не обнаружен в базе данных!");
            }

            CheckingErrors(testsdates);
            
            if (ModelState.IsValid)
            {
                testsdates.testid = _testid;

                TESTDATES entity = testsdates.ToEntity(new TESTDATES());
                try
                {
                    db.TESTSDATES.Create(entity);
                    testsdates.id = entity.ID;

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("TESTDATES", ex.Message);
                }
            }

            return Json(new[] { testsdates }.ToDataSourceResult(request, ModelState));

        }

        //Update
        [HttpPost]
        public ActionResult UpdateForGrid([DataSourceRequest]DataSourceRequest request, TestDatesViewModel testsdates)
        {
            
                TESTDATES entity = db.TESTSDATES.Get().FirstOrDefault(t => t.ID ==testsdates.id);

                if (entity == null)
                {
                    ModelState.AddModelError("TESTDATES", String.Format("Период не обнаружен в базе данных!"));
                }

            CheckingErrors(testsdates);

            if (ModelState.IsValid)
            {
                entity = testsdates.ToEntity(entity);

                try
                {
                    db.TESTSDATES.Update(entity);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("TESTDATES", ex.Message);
                }
            }

            return Json(new[] { testsdates }.ToDataSourceResult(request, ModelState));

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

        }*/

    }
}