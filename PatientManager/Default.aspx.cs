using PatientManager.Models;
using PatientManager.Repository;
using System;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace PatientManager
{
    public partial class _Default : System.Web.UI.Page
    {
        static PatientRepo patientRepo = new PatientRepo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }

        private void LoadData()
        {
            var data = patientRepo.GetAll();
            patientGridView.DataSource = data;
            patientGridView.DataBind();
        }

        [System.Web.Services.WebMethod]
        public static void SavePatient(int Id, string FirstName, string LastName, string Phone, string Email, string Gender, string Notes, bool IsDeleted)
        {
            Debug.WriteLine($"Received data: {Id}, {FirstName}, {LastName}, {Phone}, {Email}, {Gender}, {Notes}, {IsDeleted}");

            var patient = new Patient
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Phone = Phone,
                Email = Email,
                Gender = Gender,
                Notes = Notes,
                CreatedDate = DateTime.Now,
                LastUpdatedDate = DateTime.Now,
                IsDeleted = false
            };

            patientRepo.Save(patient);
        }

        protected void patientGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            TableCell cell = patientGridView.Rows[e.RowIndex].Cells[1];
            int id = -1;
            if (int.TryParse(cell.Text, out id))
            {
                patientRepo.Delete(id);
            }
            LoadData();
        }

        protected void uplPanel_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                LoadData();
        }

    }
}