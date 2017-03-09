using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ItemWebAPI.Mapper.Sc.Models
{
    public class Item
    {
        public Guid Id { get; set; }

        public string Category { get; set; }

        public string Database { get; set; }

        public string DisplayName { get; set; }

        public string Icon { get; set; }

        public string Language { get; set; }
        public string LongID { get; set; }
        public string MediaUrl { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public string Template { get; set; }

        public Guid TemplateId { get; set; }

        public string TemplateName { get; set; }

        public string Url { get; set; }

        public int Version { get; set; }
        
        public Dictionary<string,Field> Fields { get; set; }
    }
}
