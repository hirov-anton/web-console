﻿// WebConsole (https://github.com/hirov-anton/web-console)
// See LICENSE file in the solution root for full license information
// Copyright (c) 2018 Anton Hirov

using System.Web.Mvc;

namespace WebConsole.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}