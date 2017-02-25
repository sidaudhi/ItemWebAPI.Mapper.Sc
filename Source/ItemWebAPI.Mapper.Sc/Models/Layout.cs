using System.Collections.Generic;
using System.Xml.Serialization;

namespace ItemWebAPI.Mapper.Sc.Models
{
    [XmlRoot("d")]
    public class Layout
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("r")]
        public List<Rendering> Renderings { get; set; }
    }
}
