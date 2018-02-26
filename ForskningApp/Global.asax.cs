﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ForskningApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

           // Directory.SetCurrentDirectory(System.Web.HttpContext.Current.Server.MapPath("~/bin/"));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}