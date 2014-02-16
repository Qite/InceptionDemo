using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Inception.Attributes;
using Umbraco.Inception.BL;

namespace InceptionDemo.Models
{
    [UmbracoContentType("People", "People", new Type[] {typeof(Person)},true, BuiltInUmbracoContentTypeIcons.IconPeople, "_Layout", true, true)]
    public class People : UmbracoGeneratedBase
    {
        [UmbracoProperty("Umbraco url alias", "umbracoUrlAlias", BuiltInUmbracoDataTypes.NoEdit)]
        public string Url { get; set; }
    }
}