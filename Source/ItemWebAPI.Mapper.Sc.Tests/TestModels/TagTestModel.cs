using ItemWebAPI.Mapper.Sc.Attributes;
using ItemWebAPI.Mapper.Sc.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemWebAPI.Mapper.Sc.Tests.TestModels
{
    internal class TagTestModel
    {
        [SitecoreField("Id", FieldType.Id)]
        public string Id { get; set; }

        [SitecoreField("Name", FieldType.SingleLineText)]
        public string Name { get; set;  }
   
        [SitecoreField("Description", FieldType.SingleLineText)]
        public string Description { get; set; }
    }
}
