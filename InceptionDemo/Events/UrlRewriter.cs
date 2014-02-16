using InceptionDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace InceptionDemo.Events
{
    public static class UrlRewriter
    {
        public static void RewriteUrl(Umbraco.Core.Models.IContent entity, IContentService contentService)
        {
            switch (entity.ContentType.Alias)
            {
                case "People":
                    RewritePeople(entity, contentService);
                    break;
                case "Person":
                    RewritePerson(entity, contentService);
                    break;
            }
        }

        private static void RewritePerson(Umbraco.Core.Models.IContent entity, IContentService contentService)
        {
            string characterName = entity.GetValue<string>("characterName");
            string newAlias = ParseUrl(InceptionConstants.English + "/" + characterName) + "," + ParseUrl(InceptionConstants.Dutch + "/" + characterName);
            //the additional if is because of this: http://issues.umbraco.org/issue/U4-4147
            if(newAlias != entity.GetUmbracoUrlAlias())
            {
                entity.SetUmbracoUrlAlias(newAlias);
                contentService.SaveAndPublishWithStatus(entity, 0, false);
            }
        }

        private static void RewritePeople(Umbraco.Core.Models.IContent entity, IContentService contentService)
        {
            string newAlias = ParseUrl(InceptionConstants.English) + "," + ParseUrl(InceptionConstants.Dutch);
            //the additional if is because of this: http://issues.umbraco.org/issue/U4-4147
            if(newAlias != entity.GetUmbracoUrlAlias())
            {
                entity.SetUmbracoUrlAlias(newAlias);
                contentService.SaveAndPublishWithStatus(entity, 0, false);
            }
        }

        public static string GetUmbracoUrlAlias(this IContent content)
        {
            return content.GetValue<string>(InceptionConstants.UmbracoUrlAliasProperty);
        }

        public static void SetUmbracoUrlAlias(this IContent content, string newUmbracoUrlAlias)
        {
            content.SetValue(InceptionConstants.UmbracoUrlAliasProperty, newUmbracoUrlAlias);
        }

        public static string ParseUrl(string input, bool toLowerCase = true, bool allowComas = false)
        {
            input = input.Replace(" ", "-");
            input = input.Replace("é", "e");
            input = input.Replace("è", "e");
            input = input.Replace("à", "a");
            input = input.Replace("â", "a");
            input = input.Replace("ç", "c");
            input = input.Replace("î", "i");
            input = input.Replace("ï", "i");
            input = input.Replace("ë", "e");
            input = input.Replace("Ë", "e");
            input = input.Replace("ô", "o");
            input = input.Replace("ù", "u");
            string returnValue = "";

            for (int i = 0; i < input.Length; i++)
            {
                string s = input.Substring(i, 1);
                string regexString = "";

                if (allowComas)
                {
                    regexString = "[A-z0-9,/\\-\\(\\)_]";
                }
                else
                {
                    regexString = "[A-z0-9/\\-\\(\\)_]";
                }

                Regex reg = new Regex(regexString);
                if (reg.IsMatch(s))
                    returnValue += s;
            }

            // If multiple spaces (-), clear to 1
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"[\\-]{2,}", options);
            returnValue = regex.Replace(returnValue, @"-");

            // In case of all bad characters
            if (returnValue == "")
            {
                returnValue = "empty";
            }

            // Lowercase urls better for SEO
            if (toLowerCase)
            {
                return returnValue.ToLower();
            }

            return returnValue;
        }
    }
}