using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projet3.Controllers
{
    [Authorize]
    public class RedirectionController : Controller
    {
        // GET: Redirection
        
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Administrateur")]

        public ActionResult IndexAdmin()
        {
            return View();
        }
        [Authorize(Roles = "User")]

        public ActionResult IndexUser()
        {
            return View();
        }
    }
}