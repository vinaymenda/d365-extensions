using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace DynamicsExtensions.Models.View
{
    public class InQueryModel
    {
        [Display(Name = "Entity")]
        public string EntityName { get; set; }

        public IEnumerable<SelectListItem> Entities { get; set; }

        [Display(Name = "Attribute")]
        public string AttributeName { get; set; }

        [Display(Name = "Values")]
        public HttpPostedFileBase InputFile { get; set; }

        public IEnumerable<EntityMetadata> EntitiesMetaData { get; set; }
    }
}