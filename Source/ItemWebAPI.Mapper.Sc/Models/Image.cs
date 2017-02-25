using System.Xml.Serialization;

namespace ItemWebAPI.Mapper.Sc.Models
{
    [XmlRoot("image")]
    public class Image
    {
        [XmlAttribute("src")]
        public string Source { get; set; }
    }
}
