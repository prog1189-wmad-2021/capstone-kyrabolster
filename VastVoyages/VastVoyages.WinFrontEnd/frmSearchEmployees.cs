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
using VastVoyages.Service;

namespace VastVoyages.WinFrontEnd
{
    public partial class frmSearchEmployees : Form
    {
        public frmSearchEmployees()
        {
            InitializeComponent();
        }

        private void frmSearchEmployees_Load(object sender, EventArgs e)
        {
            LoadLoginInfo();
            PopulateSearchOptions();
            HideEmployeeDetails();
        }

        private void btnSearchEmployees_Click(object sender, EventArgs e)
        {
            try
            {
                dgvEmployees.DataSource = null;
                txtSearchCriteria.Enabled = true;
                HideEmployeeDetails();
                bool isValid = true;


                if (cmbSearchEmployees.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a field to search by");
                }

                EmployeeService employeeService = new EmployeeService();

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Helpers
        private void PopulateEmployeeDetails()
        {
            int employeeId = Convert.ToInt32(dgvEmployees.CurrentRow.Cells["Id"].Value);

            EmployeeService employeeService = new EmployeeService();
            List<EmployeeDTO> employee = employeeService.SearchEmployeesById(employeeId);

            txtEmployeeId.Text = (employee[0].EmpId).ToString();
            txtFirstName.Text = (employee[0].FirstName).ToString();
            txtMiddleInit.Text = (employee[0].MiddleInitial).ToString();
            txtLastName.Text = (employee[0].LastName).ToString();

            txtStreet.Text = (employee[0].Street).ToString();
            txtCity.Text = (employee[0].City).ToString();
            txtProvince.Text = (employee[0].Province).ToString();
            txtCountry.Text = (employee[0].Country).ToString();
            txtPostalCode.Text = (employee[0].PostalCode).ToString();

            txtWorkPhone.Text = (employee[0].WorkPhone).ToString();
            txtCellPhone.Text = (employee[0].CellPhone).ToString();
            txtEmail.Text = (employee[0].Email).ToString();

        }

        private void PopulateSearchOptions()
        {
            //cmbSearchEmployees.Items.Add("View All Employees");
            cmbSearchEmployees.Items.Add("Employee Id");
            cmbSearchEmployees.Items.Add("Last Name");
            cmbSearchEmployees.SelectedIndex = 0;
        }

        private void HideEmployeeDetails()
        {
            grpEmployeeDetails.Visible = false;
        }

        private void ShowEmployeeDetails()
        {
            grpEmployeeDetails.Visible = true;
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

        #endregion

    }
}
