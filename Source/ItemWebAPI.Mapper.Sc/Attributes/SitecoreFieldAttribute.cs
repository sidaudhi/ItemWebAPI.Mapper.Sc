using ItemWebAPI.Mapper.Sc.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemWebAPI.Mapper.Sc.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SitecoreFieldAttribute : Attribute
    {
        public SitecoreFieldAttribute(string name, FieldType type)
        {
            this.Name = name;
            this.Type = type;
        }

        public string Name { get; set; }

        public FieldType Type { get; set; }
    }
}
