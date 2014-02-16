using InceptionDemo.Models.RenderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace InceptionDemo.Controllers
{
    //Umbraco route hijacking: http://our.umbraco.org/documentation/Reference/Mvc/custom-controllers
    public class PeopleController : InceptionControllerBase
    {
        public override ActionResult Index(RenderModel model)
        {
            base.HandleLanguage();
            return View(new PeopleRenderModel(model.Content,base._language));
        }
    }
}