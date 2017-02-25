using System.Xml.Serialization;

namespace ItemWebAPI.Mapper.Sc.Models
{
    [XmlRoot("r")]
    public class Rendering
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("ph")]
        public string Placeholder { get; set; }

        [XmlAttribute("ds")]
        public string DataSource { get; set; }
    }
}
