using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web;

namespace InceptionDemo.Models.RenderModels
{
    public class InceptionRenderModel:RenderModel
    {
        public string Language { get; set; }
        public InceptionRenderModel(IPublishedContent content, string language): base(content)
        {
            Language = language;
            SetUrls();
        }

        private void SetUrls()
        {
            string[] aliasses = Content.GetPropertyValue<string>(InceptionConstants.UmbracoUrlAliasProperty).Split(',');
            DutchUrl = aliasses.FirstOrDefault(x => x.StartsWith(InceptionConstants.Dutch.ToLower()));
            EnglishUrl = aliasses.FirstOrDefault(x => x.StartsWith(InceptionConstants.English.ToLower()));
        }

        public string DutchUrl { get; set; }
        public string EnglishUrl { get; set; }
    }
}