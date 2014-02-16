using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Inception.Extensions;


namespace InceptionDemo.Models.RenderModels
{
    //Umbraco route hijacking: http://our.umbraco.org/documentation/Reference/Mvc/custom-controllers
    public class PersonRenderModel:InceptionRenderModel
    {
        public Person Person { get; set; }
        public string Description { get; set; }

        public PersonRenderModel(IPublishedContent content, string language):base(content,language)
        {
            Person = content.ConvertToModel<InceptionDemo.Models.Person>();
            Description = (language.ToLower() == InceptionConstants.English.ToLower() ? Person.EnglishProperties.Description : Person.DutchProperties.Description);
        }
    }
}