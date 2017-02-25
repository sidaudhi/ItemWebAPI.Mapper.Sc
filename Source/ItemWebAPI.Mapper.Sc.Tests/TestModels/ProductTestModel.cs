using ItemWebAPI.Mapper.Sc.Attributes;
using ItemWebAPI.Mapper.Sc.Enums;
using ItemWebAPI.Mapper.Sc.Models;
using System.Collections.Generic;

namespace ItemWebAPI.Mapper.Sc.Tests.TestModels
{
    internal class ProductTestModel
    {
        [SitecoreField("Id", FieldType.Id)]
        public string Id { get; set; }

        [SitecoreField("Name", FieldType.SingleLineText)]
        public string Name { get; set; }

        [SitecoreField("Description", FieldType.SingleLineText)]
        public string Description { get; set; }

        [SitecoreField("IsFeatured", FieldType.Checkbox)]
        public bool IsFeatured { get; set; }

        [SitecoreField("ProductLink", FieldType.GeneralLink)]
        public Link ProductLink { get; set; }

        [SitecoreField("Image", FieldType.Image)]
        public Image Image { get; set; }

        [SitecoreField("Tags", FieldType.Multilist)]
        public IEnumerable<TagTestModel> Tags { get; set; }
    }
}
