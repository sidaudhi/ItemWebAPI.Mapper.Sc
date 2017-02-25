using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ItemWebAPI.Mapper.Sc.Models
{
    [XmlRoot("link")]
    public class Link
    {
        [XmlAttribute("text")]
        public string Text { get; set; }

        [XmlAttribute("linkType")]
        public string LinkType { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("anchor")]
        public string Anchor { get; set; }

        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlAttribute("target")]
        public string Target { get; set; }
    }
}
