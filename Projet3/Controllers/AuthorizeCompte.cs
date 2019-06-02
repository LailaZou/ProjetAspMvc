using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace projet1ASPNET.Controllers
{
    public class AuthorizeCompte : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);
            if (httpContext.Session["compte1"] != null) return true;
            else return false;
        }
    }
}