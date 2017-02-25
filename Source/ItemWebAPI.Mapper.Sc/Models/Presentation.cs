using System.Xml.Serialization;

namespace ItemWebAPI.Mapper.Sc.Models
{
    [XmlRoot("r")]
    public class Presentation
    {
        [XmlElement("d")]
        public Layout Layout { get; set; }
    }
}
