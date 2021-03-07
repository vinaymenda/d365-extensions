using DynamicsExtensions.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicsExtensions.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (context.HttpContext.Request.Form["EndPointUrl"] != null)
            {
                var authCredentials = new AuthCredentials()
                {
                    EndPointUrl = context.HttpContext.Request["EndPointUrl"],
                    UserName = context.HttpContext.Request["UserName"],
                    Password = context.HttpContext.Request["Password"]
                };
                HttpContext.Session.Add("AuthCredentials", authCredentials);
            }
        }

    }
}