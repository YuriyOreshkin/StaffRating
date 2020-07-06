using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using StaffRating.Domain.Repository.Interfaces;
using StaffRating.WebUI.Models;
using System.Collections.Generic;
using StaffRating.Domain.Entities;

namespace StaffRating.WebUI.Controllers.Services
{
    public class TestRatingsServiceController : Controller
    {
        private IDBRepository db;
        public TestRatingsServiceController(IDBRepository _db)
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
            var testsdates = db.TESTRATINGS.Get().Where(d=>d.TESTID == _testid).ToDataSourceResult(request, t => new TestRatingViewModel
            {
                id = t.ID,
                rating=t.RATING,
                fact = t.FACT,
                maximum = db.QUESTIONS.Get().Where(q=>q.TESTID == _testid && q.RATING == t.RATING).Count(),
                testid = t.TESTID
            });

            return Json(testsdates);
        }

      

        //Update
        [HttpPost]
        public ActionResult UpdateForGrid([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<TestRatingViewModel> ratings)
        {

            if (ratings != null && ModelState.IsValid)
            {
                foreach (var rating in ratings)
                {
                    
                    TESTRATINGS entity = db.TESTRATINGS.Get().FirstOrDefault(r => r.ID == rating.id);
                    if (entity != null)
                    {
                        entity = rating.ToEntity(entity);

                        try
                        {
                            db.TESTRATINGS.Update(entity);

                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("TESTRATINGS", ex.Message);
                        }
                    }
                    else {

                        ModelState.AddModelError("TESTRATINGS", String.Format("Рейтинг теста '{0}'  не обнаружена в базе данных!", rating.rating));
                    }
                }
            }

            return Json(ratings.ToDataSourceResult(request, ModelState));

        }

       

    }
}