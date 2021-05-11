using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VastVoyages.Model;
using VastVoyages.Model.DTO;
using VastVoyages.Model.Entities;
using VastVoyages.Service;

namespace VastVoyages.WinFrontEnd
{
    public partial class frmSearchEmployees : Form
    {
        public frmSearchEmployees()
        {
            InitializeComponent();
        }

        EmployeeService employeeService = new EmployeeService();
        private Employee _employee = new Employee();

        private void frmSearchEmployees_Load(object sender, EventArgs e)
        {
            try
            {
                LoadLoginInfo();
                PopulateSearchOptions();
                HideEmployeeDetails();
                PopulateEmployeeInfoCategories();
                lblSupervisorMsg.Text = "";
                cmbSelectInfoCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCountry_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                LoadProvinceStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbDepartment_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbSupervisor.SelectedIndex = -1;
            LoadSupervisors();
        }

        private void chkIsSupervisor_CheckedChanged(object sender, EventArgs e)
        {
            LoadSupervisors();
        }

        private void btnSearchEmployees_Click(object sender, EventArgs e)
        {
            try
            {
                dgvEmployees.DataSource = null;
                txtSearchCriteria.Enabled = true;
                HideEmployeeDetails();
                grpEditEmployeeInfo.Visible = false;
                bool isValid = true;


                if (cmbSearchEmployees.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a field to search by");
                }


                switch (cmbSearchEmployees.SelectedItem.ToString())
                {
                    //case "View All Employees":
                    //    dgvEmployees.DataSource = employeeService.GetAllEmployees().Select(o => new
                    //    { Id = o.EmpId, Name =  o.FullName }).ToList();
                    //    break;
                    case "Employee Id":
                        string empSearch = txtSearchCriteria.Text.Trim();
                        if (!int.TryParse(empSearch, out int empId))
                        {
                            MessageBox.Show("Please enter an 8 digit employee Id.");
                            return;
                        }
                        else
                        {
                            int employeeId = Convert.ToInt32(txtSearchCriteria.Text.Trim());
                            dgvEmployees.DataSource = employeeService.SearchEmployeesById(employeeId).Select(o => new
                            { Id = o.EmpId, Name = o.FullName }).ToList();
                        }
                        break;
                    case "Last Name":
                        string lastName = txtSearchCriteria.Text.Trim();

                        if (string.IsNullOrEmpty(lastName))
                        {
                            MessageBox.Show("Please enter an employee last name or partial last name.");
                            return;
                        }
                        dgvEmployees.DataSource = employeeService.SearchEmployeesByLastName(lastName).Select(o => new
                        { Id = o.EmpId, Name = o.FullName }).ToList();
                        break;
                }

                if (dgvEmployees.Rows.Count == 0 && isValid == true)
                {
                    dgvEmployees.DataSource = null;
                    MessageBox.Show("No employees found matching this search criteria");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ShowEmployeeDetails();
                PopulateEmployeeDetails();
                grpEditEmployeeInfo.Visible = true;
                HideEditEmployeeCategories();
                btnSave.Visible = false;
                _employee = employeeService.GetEmployeeToModifyById(Convert.ToInt32(dgvEmployees.CurrentRow.Cells["Id"].Value));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectInfoCategory_Click(object sender, EventArgs e)
        {
            try
            {
                string category = cmbSelectInfoCategory.SelectedItem.ToString();

                HideEditEmployeeCategories();
                btnSave.Visible = true;
                grpJobInfo.Enabled = true;
                grpEmploymentStatus.Enabled = true;
                cmbEmploymentStatus.Enabled = true;
                dtpEndDate.Value = DateTime.Now;

                switch (category)
                {
                    case ("Personal Information"):
                        grpPersonalInfo.Visible = true;
                        PopulatePersonalInformation();
                        break;
                    case ("Job Information"):
                        grpJobInfo.Visible = true;
                        PopulateJobInformation();
                        break;
                    case ("Employment Status"):
                        grpEmploymentStatus.Visible = true;
                        PopulateEmploymentStatus();
                        break;
                    default:
                        MessageBox.Show("Please select a category of information to modify.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                int employeeId = Convert.ToInt32(dgvEmployees.CurrentRow.Cells["Id"].Value);
                int loggedInEmployee = Convert.ToInt32(((MainForm)this.MdiParent).loginInfo.EmployeeId);
                if (employeeId == loggedInEmployee)
                {
                    //cmbJobAssignment.Enabled = false;
                    //cmbEmploymentStatus.Enabled = false;
                    grpJobInfo.Enabled = false;
                    grpEmploymentStatus.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbEmploymentStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int selectedStatus = Convert.ToInt32(cmbEmploymentStatus.SelectedValue);
            if (selectedStatus == 2 || selectedStatus == 3)
            {
                lblEndDate.Visible = true;
                dtpEndDate.Visible = true;
                dtpEndDate.Enabled = true;
            }
            else
            {
                lblEndDate.Visible = false;
                dtpEndDate.Visible = false;
            }
        }

        private void cmbJobAssignment_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(Convert.ToInt32(cmbJobAssignment.SelectedValue) != _employee.JobAssignmentId)
                dtpJobStartDate.Value = DateTime.Now;
            else
                dtpJobStartDate.Value = _employee.JobStartDate;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string category = cmbSelectInfoCategory.SelectedItem.ToString();

                int employeeId = Convert.ToInt32(dgvEmployees.CurrentRow.Cells["Id"].Value);
                Employee employee = employeeService.GetEmployeeToModifyById(employeeId);

                switch (category)
                {
                    case ("Personal Information"):
                        grpPersonalInfo.Visible = true;
                        _employee = ModifyPersonalInformation(_employee);
                        break;
                    case ("Job Information"):
                        grpJobInfo.Visible = true;
                        _employee = ModifyJobInformation(_employee);
                        break;
                    case ("Employment Status"):
                        grpEmploymentStatus.Visible = true;
                        _employee = ModifyEmploymentStatus(_employee);
                        break;
                }

                _employee = employeeService.UpdateEmployee(_employee);
                PopulateEmploymentStatus();
                PopulateEmployeeDetails();

                if (_employee.Errors.Count <= 0)
                {
                    MessageBox.Show("Success!Employee Id: " +
                        _employee.EmployeeId.ToString() + " successfully updated");
                }
                else
                {
                    string msg = "";
                    foreach (ValidationError error in _employee.Errors)
                    {
                        msg += error.Description + Environment.NewLine;
                    }
                    MessageBox.Show("Please address the following issues:\n\n" + msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _employee = employee;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Modify Methods

        private Employee ModifyPersonalInformation(Employee employee)
        {
            employee.FirstName = txtFirstNameMod.Text;
            employee.MiddleInitial = txtMiddleInitMod.Text;
            employee.LastName = txtLastNameMod.Text;
            employee.DateOfBirth = dtpDOBMod.Value;

            employee.Street = txtStreetMod.Text;
            employee.City = txtCityMod.Text;
            employee.Province = cmbProvince.SelectedValue.ToString();
            employee.Country = cmbCountry.SelectedValue.ToString();
            employee.PostalCode = txtPostalCodeMod.Text;

            return employee;
        }

        private Employee ModifyJobInformation(Employee employee)
        {

            employee.SIN = txtSIN.Text;
            employee.JobAssignmentId = Convert.ToInt32(cmbJobAssignment.SelectedValue);
            employee.JobStartDate = Convert.ToDateTime(dtpJobStartDate.Value);
            employee.DepartmentId = Convert.ToInt32(cmbDepartment.SelectedValue);
            employee.SupervisorId = Convert.ToInt32(cmbSupervisor.SelectedValue);
            employee.IsHeadSupervisor = chkHeadSupervisor.Checked;

            return employee;
        }

        private Employee ModifyEmploymentStatus(Employee employee)
        {
            employee.SIN = txtSIN2.Text;
            employee.SeniorityDate = Convert.ToDateTime(dtpSeniorityDate.Value);
            employee.EmployeeStatusId = Convert.ToInt32(cmbEmploymentStatus.SelectedValue);

            if(employee.EmployeeStatusId == 2 || employee.EmployeeStatusId == 3)
                employee.EndDate = Convert.ToDateTime(dtpEndDate.Value);
            else
                employee.EndDate = null;


            PopulateEmploymentStatus();

            return employee;
        }

        #endregion


        #region Helpers
        private void PopulateEmployeeDetails()
        {
            int employeeId = Convert.ToInt32(dgvEmployees.CurrentRow.Cells["Id"].Value);

            //List<EmployeeDTO> employee = employeeService.SearchEmployeesById(employeeId);
            Employee employee = employeeService.GetEmployeeToModifyById(employeeId);

            txtEmployeeId.Text = (employee.EmployeeId).ToString();
            txtFirstName.Text = (employee.FirstName).ToString();
            txtMiddleInit.Text = (employee.MiddleInitial).ToString();
            txtLastName.Text = (employee.LastName).ToString();

            txtStreet.Text = (employee.Street).ToString();
            txtCity.Text = (employee.City).ToString();
            txtProvince.Text = (employee.Province).ToString();
            txtCountry.Text = (employee.Country).ToString();
            txtPostalCode.Text = (employee.PostalCode).ToString();

            txtWorkPhone.Text = (employee.WorkPhone).ToString();
            txtCellPhone.Text = (employee.CellPhone).ToString();
            txtEmail.Text = (employee.Email).ToString();
        }

        private void PopulatePersonalInformation()
        {
            LoadCountries();

            int employeeId = Convert.ToInt32(dgvEmployees.CurrentRow.Cells["Id"].Value);

            Employee employee = employeeService.GetEmployeeToModifyById(employeeId);

            txtFirstNameMod.Text = (employee.FirstName).ToString();
            txtMiddleInitMod.Text = employee.MiddleInitial == null ? "" : (employee.MiddleInitial).ToString();
            txtLastNameMod.Text = (employee.LastName).ToString();
            dtpDOBMod.Value = Convert.ToDateTime(employee.DateOfBirth);

            txtStreetMod.Text = (employee.Street).ToString();
            txtCityMod.Text = (employee.City).ToString();
            cmbCountry.SelectedValue = (employee.Country).ToString();
            LoadProvinceStates();
            cmbProvince.SelectedValue = (employee.Province).ToString();
            txtPostalCodeMod.Text = (employee.PostalCode).ToString();
        }

        private void PopulateJobInformation()
        {
            LoadJobAssignments();
            LoadDepartments();

            int employeeId = Convert.ToInt32(dgvEmployees.CurrentRow.Cells["Id"].Value);

            Employee employee = employeeService.GetEmployeeToModifyById(employeeId);

            txtSIN.Text = (employee.SIN).ToString();
            txtSIN.ReadOnly = true;
            cmbJobAssignment.SelectedValue = Convert.ToInt32(employee.JobAssignmentId);
            dtpJobStartDate.Value = Convert.ToDateTime(employee.JobStartDate);
            
            if(employee.SupervisorId == employeeService.GetCEO()[0].SupervisorId)
                chkIsSupervisor.Checked = true;
            else 
                chkIsSupervisor.Checked = false;

            //make sure isheadsupervisor implemented
            if (employee.IsHeadSupervisor == true)
            {
                chkHeadSupervisor.Enabled = true;
                chkHeadSupervisor.Checked = true;
            }
            else
            {
                chkHeadSupervisor.Checked = false;
            }

            //validate for CEO **?
            if (employee.DepartmentId == 1)
            {
                grpJobInfo.Visible = false;
                btnSave.Visible = false;
                MessageBox.Show("The CEO's job information cannot be modified at this time.", "No Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cmbDepartment.SelectedValue = Convert.ToInt32(employee.DepartmentId);

            LoadSupervisors();
        }

        private void PopulateEmploymentStatus()
        {
            LoadEmployeeStatus();
            lblEndDate.Visible = false;
            dtpEndDate.Visible = false;

            int employeeId = Convert.ToInt32(dgvEmployees.CurrentRow.Cells["Id"].Value);

            Employee employee = employeeService.GetEmployeeToModifyById(employeeId);

            txtSIN2.Text = (employee.SIN).ToString();
            dtpSeniorityDate.Value = Convert.ToDateTime(employee.SeniorityDate);
            cmbEmploymentStatus.SelectedValue = Convert.ToInt32(employee.EmployeeStatusId);

            if (employee.EmployeeStatusId == 2 || employee.EmployeeStatusId == 3)
            {
                lblEndDate.Visible = true;
                dtpEndDate.Visible = true;
                dtpEndDate.Enabled = false;
                dtpEndDate.Value = Convert.ToDateTime(employee.EndDate);
                if (employee.EmployeeStatusId == 3)
                    cmbEmploymentStatus.Enabled = false;
            }
        }

        private void PopulateSearchOptions()
        {
            //cmbSearchEmployees.Items.Add("View All Employees");
            cmbSearchEmployees.Items.Add("Employee Id");
            cmbSearchEmployees.Items.Add("Last Name");
            cmbSearchEmployees.SelectedIndex = 0;
        }

        private void PopulateEmployeeInfoCategories()
        {
            cmbSelectInfoCategory.Items.Add("--Select a category--");
            cmbSelectInfoCategory.Items.Add("Personal Information");
            cmbSelectInfoCategory.Items.Add("Job Information");
            cmbSelectInfoCategory.Items.Add("Employment Status");
        }

        private void HideEmployeeDetails()
        {
            grpEmployeeDetails.Visible = false;
        }

        private void ShowEmployeeDetails()
        {
            grpEmployeeDetails.Visible = true;
        }

        private void HideEditEmployeeCategories()
        {
            grpPersonalInfo.Visible = false;
            grpJobInfo.Visible = false;
            grpEmploymentStatus.Visible = false;
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

        private void LoadJobAssignments()
        {
            LookupsService service = new LookupsService();
            List<JobAssignmentsLookupsDTO> jobAssignments = service.GetJobAssignments();
            cmbJobAssignment.DataSource = jobAssignments;
            cmbJobAssignment.DisplayMember = "JobAssignment";
            cmbJobAssignment.ValueMember = "JobAssignmentId";
        }

        private void LoadDepartments()
        {
            LookupsService service = new LookupsService();
            List<DepartmentLookupsDTO> departments = service.GetDepartments();
            cmbDepartment.DataSource = departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "DepartmentId";
        }

        private void LoadSupervisors()
        {
            btnSave.Enabled = true;

            chkHeadSupervisor.Checked = false;
            chkHeadSupervisor.Enabled = false;

            ToolTip toolTip = new ToolTip();

            LookupsService service = new LookupsService();
            List<SupervisorLookupsDTO> supervisors;

            if (chkIsSupervisor.Checked == true)
            {
                supervisors = service.GetCEO();
                cmbSupervisor.Enabled = false;
                lblSupervisorMsg.Text = "All supervisors are supervised by the CEO";

                List<SupervisorLookupsDTO> headSupervisor = service.GetHeadSupervisor(Convert.ToInt32(cmbDepartment.SelectedValue));
                if (headSupervisor.Count > 0)
                {
                    chkHeadSupervisor.Enabled = false;

                    toolTip.SetToolTip(lblHeadSupervisor, "Only one head supervisor per department");
                }
                else
                {
                    chkHeadSupervisor.Enabled = true;
                }
            }
            else
            {
                supervisors = service.GetSupervisors(Convert.ToInt32(cmbDepartment.SelectedValue));
                cmbSupervisor.Enabled = true;
                lblSupervisorMsg.Text = "";
            }

            if (supervisors.Count < 1)
            {
                cmbSupervisor.Enabled = false;
                cmbSupervisor.SelectedIndex = -1;
                lblSupervisorMsg.Text = "Regular employees cannot be added\n to departments without supervisors";
                btnSave.Enabled = false;
            }

            cmbSupervisor.DataSource = supervisors;
            cmbSupervisor.DisplayMember = "SupervisorName";
            cmbSupervisor.ValueMember = "SupervisorId";
        }

        private void LoadEmployeeStatus()
        {
            LookupsService service = new LookupsService();
            List<EmployeeStatusLookupsDTO> status = service.GetEmployeeStatus();
            cmbEmploymentStatus.DataSource = status;
            cmbEmploymentStatus.DisplayMember = "EmployeeStatus";
            cmbEmploymentStatus.ValueMember = "EmployeeStatusId";
        }

        private void LoadCountries()
        {
            Dictionary<string, string> countries = new Dictionary<string, string>();
            countries.Add("Canada", "Canada");
            countries.Add("United States Of America", "United States Of America");

            cmbCountry.DataSource = new BindingSource(countries, null);
            cmbCountry.DisplayMember = "Value";
            cmbCountry.ValueMember = "Key";
        }

        private void LoadProvinceStates()
        {
            Dictionary<string, string> provinces = new Dictionary<string, string>();

            if (cmbCountry.SelectedValue.Equals("Canada"))
            {
                lblPostalCode.Text = "Postal Code:";

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
                lblPostalCode.Text = "Zip Code:";

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

        #endregion
    }
}
