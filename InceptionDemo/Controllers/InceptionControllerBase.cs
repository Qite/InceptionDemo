using InceptionDemo.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using Umbraco.Web.Mvc;

namespace InceptionDemo.Controllers
{
    public class InceptionControllerBase:RenderMvcController
    {
        protected string _language;

        protected void HandleLanguage()
        {
            string url = (Request.RawUrl.Length > 1 ? Request.RawUrl.Substring(1, Request.RawUrl.Length - 1) : Request.RawUrl);
            if (url.StartsWith(InceptionConstants.Dutch))
            {
                _language = InceptionConstants.Dutch;
            }
            else
            {
                _language = InceptionConstants.English;
            }
 
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(_language);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(_language);
        }
    }
}