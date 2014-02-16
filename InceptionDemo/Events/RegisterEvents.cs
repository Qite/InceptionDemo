using InceptionDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;
using Umbraco.Inception.CodeFirst;

namespace InceptionDemo.Events
{
    /// <summary>
    /// Umbraco Register Events on Application Startup
    /// 
    /// Documentation
    /// http://our.umbraco.org/documentation/Reference/Events/application-startup
    /// http://our.umbraco.org/documentation/reference/events-v6/
    /// </summary>
    public class RegisterEvents : ApplicationEventHandler
    {

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //Once the content types are generated you don't need this to run every time
            //unless you did some changes to the models
            RegisterModels();

            ContentService.Published += ContentService_Published;
        }

        //every time we save a document, we rewrite the urls
        void ContentService_Published(IPublishingStrategy sender, PublishEventArgs<IContent> e)
        {
            IContentService contentService = ApplicationContext.Current.Services.ContentService;
            foreach(var entity in e.PublishedEntities)
            {
                UrlRewriter.RewriteUrl(entity, contentService);
            }
        }


        private void RegisterModels()
        {
            UmbracoCodeFirstInitializer.CreateOrUpdateEntity(typeof(People));
            UmbracoCodeFirstInitializer.CreateOrUpdateEntity(typeof(Person));
        }

    }
}
