﻿using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;

namespace StaffRating.WebUI.IoC
{
    public class NinjectControllerFactory: DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = NinjectIoC.Initialize();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
       
    }
}