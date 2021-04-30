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
                Province = txtProvince.Text.Trim(),
                Country = txtCountry.Text.Trim(),
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
            txtProvince.Text = "";
            txtCountry.Text = "";
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
