using System.Collections.Generic;
using System.Xml.Serialization;

namespace ItemWebAPI.Mapper.Sc.Models
{
    public class Item
    {
        public string Id { get; set; }

        public Dictionary<string,Field> Fields { get; set; }
    }
}
