using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PatientManager.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public string ToXML()
        {
            using (var stringwriter = new StringWriter())
            {
                var serializer = new XmlSerializer(this.GetType());

                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                var xmlWriter = XmlWriter.Create(stringwriter, new XmlWriterSettings() { OmitXmlDeclaration = true, Encoding = Encoding.ASCII });

                serializer.Serialize(xmlWriter, this, ns);
                return stringwriter.ToString();
            }
        }
    }
}