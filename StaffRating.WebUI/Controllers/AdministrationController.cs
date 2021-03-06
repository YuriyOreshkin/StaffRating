﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StaffRating.WebUI.Controllers
{
    public class AdministrationController : Controller
    {
        // GET: Administration
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Categories()
        {
            return View();
        }

        public ActionResult Tests()
        {
            return View();
        }

        public ActionResult TestsDates(long testid)
        {
            return PartialView(testid);
        }

        public ActionResult Questions(long testid)
        {
            return PartialView(testid);
        }

        public ActionResult TestRatings(long testid)
        {
            return PartialView(testid);
        }
    }
}