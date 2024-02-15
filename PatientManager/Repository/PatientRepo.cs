using PatientManager.Models;
using PatientManager.Utils;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PatientManager.Repository
{
    public class PatientRepo
    {
        string xmlFilePath = HttpContext.Current.Server.MapPath("~/App_Data/patients.xml");
        const string PATIENT = "Patient";
        const string PATIENTS = "Patients";

        public Patient[] GetAll()
        {
            XDocument xmlDoc = BuildXmlDoc();

            var patients = xmlDoc.Descendants(PATIENT).Where(o => bool.Parse(o.Element("IsDeleted").Value) == false)
                .Select(p => BuildPatient(p));
            return patients.ToArray();
        }

        public Patient Get(int id)
        {
            XDocument xmlDoc = BuildXmlDoc();

            var patients = xmlDoc.Descendants(PATIENT).Where(o => o.Element("Id").Value == id.ToString().Trim())
                .Select(p => BuildPatient(p));
            return patients.FirstOrDefault();
        }

        public void Save(Patient patient)
        {
            XDocument xmlDoc = BuildXmlDoc();

            if (patient.Id == -1) //INSERT
            {
                patient.Id = GenerateNextId(xmlDoc);
                patient.FirstName = Encryption.Encrypt(patient.FirstName);
                patient.LastName = Encryption.Encrypt(patient.LastName);
                patient.Phone = Encryption.Encrypt(patient.Phone);
                patient.Email = Encryption.Encrypt(patient.Email);
                var patientXml = patient.ToXML();
                XElement newPatient = XElement.Parse(patientXml);
                xmlDoc.Element(PATIENTS).Add(newPatient);
            }
            else //UPDATE
            {
                var existingPatient = xmlDoc.Element(PATIENTS).Elements(PATIENT).Where(e => e.Element("Id").Value == patient.Id.ToString().Trim()).Single();
                existingPatient.Element("FirstName").Value = Encryption.Encrypt(patient.FirstName);
                existingPatient.Element("LastName").Value = Encryption.Encrypt(patient.LastName);
                existingPatient.Element("Gender").Value = patient.Gender;
                existingPatient.Element("Phone").Value = Encryption.Encrypt(patient.Phone);
                existingPatient.Element("Email").Value = Encryption.Encrypt(patient.Email);
                existingPatient.Element("Notes").Value = patient.Notes;
                existingPatient.Element("LastUpdatedDate").Value = DateTime.Now.ToString();
                existingPatient.Element("IsDeleted").Value = patient.IsDeleted.ToString().ToLower();
            }
            xmlDoc.Save(xmlFilePath);
        }

        public void Delete(int id)
        {
            var patient = this.Get(id);

            if (patient != null)
            {
                patient.IsDeleted = true;
                Save(patient);
            }
        }

        private static Patient BuildPatient(XElement patient)
        {
            return new Patient
            {
                Id = int.Parse(patient.Element("Id").Value),
                FirstName = Encryption.Decrypt(patient.Element("FirstName").Value),
                LastName = Encryption.Decrypt(patient.Element("LastName").Value),
                Gender = patient.Element("Gender").Value,
                Phone = Encryption.Decrypt(patient.Element("Phone").Value),
                Email = Encryption.Decrypt(patient.Element("Email").Value),
                Notes = patient.Element("Notes").Value
            };
        }

        private XDocument BuildXmlDoc()
        {
            XDocument xmlDoc;
            if (File.Exists(xmlFilePath))
                xmlDoc = XDocument.Load(xmlFilePath);
            else
                xmlDoc = new XDocument(new XElement(PATIENTS));
            return xmlDoc;
        }

        private int GenerateNextId(XDocument xmlDoc)
        {
            int nextId = 0;
            if (xmlDoc.Root.Elements().Count() > 0)
                nextId = xmlDoc.Root.Elements().Max(x => (int)x.Element("Id"));

            nextId = nextId + 1;
            return nextId;
        }
    }
}