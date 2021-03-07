using DynamicsExtensions.Dynamics;
using DynamicsExtensions.Extensions;
using DynamicsExtensions.Models.Core;
using DynamicsExtensions.Models.View;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicsExtensions.Controllers
{
    public class InQueryController : BaseController
    {
        private readonly IDynamicsOperations _dynamicsOperations;

        public InQueryController()
        {
            _dynamicsOperations = new DynamicsOperations((AuthCredentials)System.Web.HttpContext.Current.Session["AuthCredentials"]);
        }

        public ActionResult Index()
        {
            var entitiesMetaData = _dynamicsOperations.GetEntities()
                .Where(x => x.DisplayName.UserLocalizedLabel != null)
                .OrderBy(x => x.DisplayName.UserLocalizedLabel.Label);
            var model = new InQueryModel() { 
                Entities = entitiesMetaData.Select(x => new SelectListItem() { Text = x.DisplayName.UserLocalizedLabel.Label, Value = x.LogicalName }),
                EntitiesMetaData = entitiesMetaData
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(InQueryModel model)
        {
            var qe = new QueryExpression(model.EntityName);
            qe.ColumnSet.AddColumn(model.AttributeName);
            qe.Criteria = new FilterExpression(LogicalOperator.Or);
            var attributeValues = model.InputFile.InputStream.ReadAllLines();
            foreach (var value in attributeValues)
            {
                qe.Criteria.AddCondition(model.AttributeName, ConditionOperator.Equal, value);
            }
            _dynamicsOperations.CreatePersonalView($"In Query - {DateTime.Now:yyyy-MM-dd}", qe);
            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }

        public JsonResult GetAttributes(string entity)
        {
            var attributesMetaData = _dynamicsOperations.GetAttributes(entity)
                .Where(x => x.AttributeOf == null)
                 .Where(x => x.DisplayName.UserLocalizedLabel != null)
                 .OrderBy(x => x.DisplayName.UserLocalizedLabel.Label);
            return Json(attributesMetaData.Select(x => new { DisplayName = x.DisplayName.UserLocalizedLabel.Label, LogicalName = x.LogicalName }), 
                JsonRequestBehavior.AllowGet);
        }
    }
}