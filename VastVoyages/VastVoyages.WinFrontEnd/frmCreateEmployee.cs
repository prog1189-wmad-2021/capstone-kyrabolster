using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using VastVoyages.Model;
using VastVoyages.Model.DTO;
using VastVoyages.Model.Entities;
using VastVoyages.Service;

namespace VastVoyages.WinFrontEnd
{
    public partial class frmCreateEmployee : Form
    {
        public frmCreateEmployee()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Populate comboboxes when form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCreateEmployee_Load(object sender, EventArgs e)
        {
            try
            {
                LoadLoginInfo();
                LoadCountries();
                LoadProvinceStates();
                LoadJobAssignments();
                LoadDepartments();
                LoadSupervisors();
                lblSupervisorMsg.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Populate provinces/states when country changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCountry_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadProvinceStates();
        }

        /// <summary>
        /// Populate supervisors when selected department changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDepartment_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbSupervisor.SelectedIndex = -1;
            LoadSupervisors();
        }

        /// <summary>
        /// Populate CEO as supervisor when checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsSupervisor_CheckedChanged(object sender, EventArgs e)
        {
            LoadSupervisors();
        }

        /// <summary>
        /// Create employee button
        /// Create employee from form data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeService employeeService = new EmployeeService();

                Employee employee = PopulateEmployeeObject();

                if (employeeService.AddEmployee(employee))
                {
                    MessageBox.Show("Success!\n" + 
                                    "New Employee Id is: " + employee.EmployeeId.ToString() +
                                    "\nUsername: " + employee.UserName.ToString()
                                    );
                    LoadSupervisors();
                    ClearForm();
                }
                else
                {
                    string msg = "";
                    foreach (ValidationError error in employee.Errors)
                    {
                        msg += error.Description + Environment.NewLine;
                    }
                    MessageBox.Show("Please address the following issues:\n\n" + msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #region Helpers

        /// <summary>
        /// Populate job assignments
        /// </summary>
        private void LoadJobAssignments()
        {
            LookupsService service = new LookupsService();
            List<JobAssignmentsLookupsDTO> jobAssignments = service.GetJobAssignments();
            cmbJobAssignment.DataSource = jobAssignments;
            cmbJobAssignment.DisplayMember = "JobAssignment";
            cmbJobAssignment.ValueMember = "JobAssignmentId";
        }

        /// <summary>
        /// Populate Departments
        /// </summary>
        private void LoadDepartments()
        {
            LookupsService service = new LookupsService();
            List<DepartmentLookupsDTO> departments = service.GetDepartments();
            cmbDepartment.DataSource = departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "DepartmentId";
        }

        /// <summary>
        /// Populate supervisors
        /// Set supervisor as CEO if supervisor checkbox is checked
        /// </summary>
        private void LoadSupervisors()
        {
            btnCreateEmployee.Enabled = true;

            LookupsService service = new LookupsService();
            List<SupervisorLookupsDTO> supervisors;

            if (chkIsSupervisor.Checked == true)
            {
                supervisors = service.GetCEO();
                cmbSupervisor.Enabled = false;
                lblSupervisorMsg.Text = "All supervisors are supervised by the CEO";
            }
            else
            {
                supervisors = service.GetSupervisors(Convert.ToInt32(cmbDepartment.SelectedValue));
                cmbSupervisor.Enabled = true;
                lblSupervisorMsg.Text = "";
            }

            if(supervisors.Count < 1)
            {
                cmbSupervisor.Enabled = false;
                lblSupervisorMsg.Text = "Regular employees cannot be added\n to departments without supervisors";
                btnCreateEmployee.Enabled = false;
            }

            cmbSupervisor.DataSource = supervisors;
            cmbSupervisor.DisplayMember = "SupervisorName";
            cmbSupervisor.ValueMember = "SupervisorId";
        }

        /// <summary>
        /// Load countries
        /// </summary>
        private void LoadCountries()
        {
            Dictionary<string, string> countries = new Dictionary<string, string>();
            countries.Add("Canada", "Canada");
            countries.Add("United States Of America", "United States Of America");
           
            cmbCountry.DataSource = new BindingSource(countries, null);
            cmbCountry.DisplayMember = "Value";
            cmbCountry.ValueMember = "Key";
        }

        /// <summary>
        /// Load provinces and states
        /// </summary>
        private void LoadProvinceStates()
        {
            Dictionary<string, string> provinces = new Dictionary<string, string>();

            if (cmbCountry.SelectedValue.Equals("Canada"))
            {
                provinces.Add("AB", "Alberta");
                provinces.Add("BC", "British Columbia");
                provinces.Add("MB", "Manitoba");
                provinces.Add("NL", "Newfoundland and Labrador");
                provinces.Add("NB", "New Brunswick");
                provinces.Add("NT", "Northwest Territories");
                provinces.Add("NS", "Nova Scotia");
                provinces.Add("NU", "Nunavut");
                provinces.Add("ON", "Ontario");
                provinces.Add("PE", "Prince Edward Island");
                provinces.Add("QC", "Quebec");
                provinces.Add("SK", "Saskatchewan");
                provinces.Add("YT", "Yukon");
            } 
            else
            {
                provinces.Add("AL", "Alabama");
                provinces.Add("AK", "Alaska");
                provinces.Add("AZ", "Arizona");
                provinces.Add("AR", "Arkansas");
                provinces.Add("CA", "California");
                provinces.Add("CO", "Colorado");
                provinces.Add("CT", "Connecticut");
                provinces.Add("DE", "Delaware");
                provinces.Add("DC", "District of Columbia");
                provinces.Add("FL", "Florida");
                provinces.Add("GA", "Georgia");
                provinces.Add("HI", "Hawaii");
                provinces.Add("ID", "Idaho");
                provinces.Add("IL", "Illinois");
                provinces.Add("IN", "Indiana");
                provinces.Add("IA", "Iowa");
                provinces.Add("KS", "Kansas");
                provinces.Add("KY", "Kentucky");
                provinces.Add("LA", "Louisiana");
                provinces.Add("ME", "Maine");
                provinces.Add("MD", "Maryland");
                provinces.Add("MA", "Massachusetts");
                provinces.Add("MI", "Michigan");
                provinces.Add("MN", "Minnesota");
                provinces.Add("MS", "Mississippi");
                provinces.Add("MO", "Missouri");
                provinces.Add("MT", "Montana");
                provinces.Add("NE", "Nebraska");
                provinces.Add("NV", "Nevada");
                provinces.Add("NH", "New Hampshire");
                provinces.Add("NJ", "New Jersey");
                provinces.Add("NM", "New Mexico");
                provinces.Add("NY", "New York");
                provinces.Add("NC", "North Carolina");
                provinces.Add("ND", "North Dakota");
                provinces.Add("OH", "Ohio");
                provinces.Add("OK", "Oklahoma");
                provinces.Add("OR", "Oregon");
                provinces.Add("PA", "Pennsylvania");
                provinces.Add("RI", "Rhode Island");
                provinces.Add("SC", "South Carolina");
                provinces.Add("SD", "South Dakota");
                provinces.Add("TN", "Tennessee");
                provinces.Add("TX", "Texas");
                provinces.Add("UT", "Utah");
                provinces.Add("VT", "Vermont");
                provinces.Add("VA", "Virginia");
                provinces.Add("WA", "Washington");
                provinces.Add("WV", "West Virginia");
                provinces.Add("WI", "Wisconsin");
                provinces.Add("WY", "Wyoming");
            }
            
            cmbProvince.DataSource = new BindingSource(provinces, null);
            cmbProvince.DisplayMember = "Value";
            cmbProvince.ValueMember = "Key";
        }

        /// <summary>
        /// Populate employee object from form data
        /// </summary>
        /// <returns></returns>
        private Employee PopulateEmployeeObject()
        {
            return new Employee()
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                MiddleInitial = txtMiddleInit.Text.Trim(),
                DateOfBirth = dtpDOB.Value.Date,
                Street = txtStreet.Text.Trim(),
                City = txtCity.Text.Trim(),
                Province = cmbProvince.SelectedValue.ToString(),
                Country = cmbCountry.SelectedValue.ToString(),
                PostalCode = txtPostalCode.Text.Trim(),
                WorkPhone = txtWorkPhone.Text.Trim(),
                CellPhone = txtCellPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                JobStartDate = dtpJobStartDate.Value.Date,
                SeniorityDate = dtpSeniorityDate.Value.Date,
                SIN = txtSIN.Text.Trim(),
                SupervisorId = Convert.ToInt32(cmbSupervisor.SelectedValue),
                DepartmentId = Convert.ToInt32(cmbDepartment.SelectedValue),
                EmployeeStatusId = 1,
                JobAssignmentId = Convert.ToInt32(cmbJobAssignment.SelectedValue)
            };
        }

        private void LoadLoginInfo()
        {
            lbEmpName.Text = ((MainForm)this.MdiParent).loginInfo.FullName;
            lbUserName.Text = ((MainForm)this.MdiParent).loginInfo.UserName;
            lbJob.Text = ((MainForm)this.MdiParent).loginInfo.Job;
            lbDepartment.Text = ((MainForm)this.MdiParent).loginInfo.Department;
            lbSupervisor.Text = ((MainForm)this.MdiParent).loginInfo.Supervisor;
            lbCurrentDate.Text = DateTime.Now.ToShortDateString();
        }

        /// <summary>
        /// Clear all input fields
        /// </summary>
        private void ClearForm()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtMiddleInit.Text = "";
            dtpDOB.Value = DateTime.Now;
            txtStreet.Text = "";
            txtCity.Text = "";
            LoadProvinceStates();
            LoadCountries();
            txtPostalCode.Text = "";
            txtWorkPhone.Text = "";
            txtCellPhone.Text = "";
            txtEmail.Text = "";
            dtpJobStartDate.Value = DateTime.Now;
            dtpSeniorityDate.Value = DateTime.Now;
            txtSIN.Text = "";
            LoadJobAssignments();
            LoadDepartments();
            LoadSupervisors();
        }
        #endregion
    }
}
