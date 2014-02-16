using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web;
using Umbraco.Inception.Extensions;

namespace InceptionDemo.Models.RenderModels
{
    public class PeopleRenderModel:InceptionRenderModel
    {

        public IEnumerable<Person> Persons { get; set; }

        public PeopleRenderModel(IPublishedContent content, string language):base(content, language)
        {
            Persons = content.Children.Select(x => x.ConvertToModel<Person>()).ToArray();
            foreach (Person item in Persons)
            {
                item.SetCorrectUrl(Language);
            }
        }
    }
}