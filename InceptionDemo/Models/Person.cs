using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Inception.Attributes;
using Umbraco.Inception.BL;
using Umbraco.Inception.Converters;

namespace InceptionDemo.Models
{
    [UmbracoContentType("Person", "Person", null, true, BuiltInUmbracoContentTypeIcons.IconPeopleAlt, "_Layout")]
    public class Person : People
    {
        [UmbracoProperty("Real name", "realName", BuiltInUmbracoDataTypes.Textbox)]
        public string RealName { get; set; }

        [UmbracoProperty("Character name", "characterName", BuiltInUmbracoDataTypes.Textbox)]
        public string CharacterName { get; set; }

        //set the forth param null if you are using a built in dataType
        [UmbracoProperty("Image", "image", BuiltInUmbracoDataTypes.MediaPicker, null, typeof(MediaIdConverter))]
        public string Image { get; set; }

        //separate tabs
        [UmbracoTab("Dutch")]
        public PersonLanguageTab DutchProperties { get; set; }

        [UmbracoTab("English")]
        public PersonLanguageTab EnglishProperties { get; set; }



        public void SetCorrectUrl(string language)
        {
            if (string.IsNullOrEmpty(Url)) return;
            string[] aliasses = Url.Split(',');
            string url = aliasses.FirstOrDefault(x => x.Contains(language.ToLower()));
            Url = url;
        }
    }

    public class PersonLanguageTab : TabBase
    {
        [UmbracoProperty("Description","description",BuiltInUmbracoDataTypes.TinyMce)]
        public string Description { get; set; }
    }
}