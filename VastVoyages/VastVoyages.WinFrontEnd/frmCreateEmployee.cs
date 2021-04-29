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

        private void frmCreateEmployee_Load(object sender, EventArgs e)
        {
            try
            {
                LoadJobAssignments();
                LoadDepartments();
                LoadSupervisors();
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
        private void btnCreateEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                //validate form ?

                EmployeeService employeeService = new EmployeeService();

                Employee employee = PopulateEmployeeObject();

                if (employeeService.AddEmployee(employee))
                {
                    MessageBox.Show("New Employee Id is: " + employee.EmployeeId.ToString() +
                                    "\nUsername: " + employee.UserName.ToString()
                                    );
                    LoadSupervisors();
                }
                else
                {
                    string msg = "";
                    foreach (ValidationError error in employee.Errors)
                    {
                        msg += error.Description + Environment.NewLine;
                    }
                    MessageBox.Show(msg);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #region Helpers

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
            LookupsService service = new LookupsService();
            List<SupervisorLookupsDTO> supervisors = service.GetSupervisors(Convert.ToInt32(cmbDepartment.SelectedValue));
            cmbSupervisor.DataSource = supervisors;
            cmbSupervisor.DisplayMember = "SupervisorName";
            cmbSupervisor.ValueMember = "SupervisorId";
        }

        private Employee PopulateEmployeeObject()
        {
            return new Employee()
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                MiddleInitial = txtMiddleInit.Text.Trim(),
                DateOfBirth = dtpDOB.Value,
                Street = txtStreet.Text.Trim(),
                City = txtCity.Text.Trim(),
                Province = txtProvince.Text.Trim(),
                Country = txtCountry.Text.Trim(),
                PostalCode = txtPostalCode.Text.Trim(),
                WorkPhone = txtWorkPhone.Text.Trim(),
                CellPhone = txtCellPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                JobStartDate = dtpJobStartDate.Value,
                SeniorityDate = dtpSeniorityDate.Value,
                SIN = txtSIN.Text.Trim(),
                SupervisorId = Convert.ToInt32(cmbSupervisor.SelectedValue),
                DepartmentId = Convert.ToInt32(cmbDepartment.SelectedValue),
                EmployeeStatusId = 1,
                JobAssignmentId = Convert.ToInt32(cmbJobAssignment.SelectedValue)
            };
        }
        #endregion
    }
}
