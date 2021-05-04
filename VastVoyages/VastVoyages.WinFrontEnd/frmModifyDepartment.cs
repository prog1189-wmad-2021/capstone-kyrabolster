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
    public partial class frmModifyDepartment : Form
    {
        public frmModifyDepartment()
        {
            InitializeComponent();
        }

        DepartmentService departmentService = new DepartmentService();

        private void frmModifyDepartment_Load(object sender, EventArgs e)
        {
            LoadLoginInfo();
            LoadDepartments();
            grpModifyDepartment.Visible = false;
        }

        private void dgvDepartments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                grpModifyDepartment.Visible = true;
                PopulateDepartmentDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModifyDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                Department department = PopulateDepartmentObject();

                departmentService.UpdateDepartment(department);

                if (department.Errors.Count <= 0)
                {
                    MessageBox.Show("Success!\n" +
                        department.DepartmentName.ToString() + " department successfully updated");
                    LoadDepartments();
                }
                else
                {
                    string msg = "";
                    foreach (ValidationError error in department.Errors)
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

        private void LoadDepartments()
        {
            dgvDepartments.DataSource = departmentService.GetDepartments();
        }

        private void PopulateDepartmentDetails()
        {
            int departmentId = Convert.ToInt32(dgvDepartments.CurrentRow.Cells["DepartmentId"].Value);

            Department department = departmentService.GetDepartmentById(departmentId);

            txtDepartmentName.Text = (department.DepartmentName).ToString();
            txtDepartmentDescription.Text = (department.DepartmentDescription).ToString();
            dtpInvocationDate.Text = (department.InvocationDate).ToString();
        }

        private Department PopulateDepartmentObject()
        {
            return new Department()
            {
                DepartmentId = Convert.ToInt32(dgvDepartments.CurrentRow.Cells["DepartmentId"].Value),
                DepartmentName = txtDepartmentName.Text.Trim(),
                DepartmentDescription = txtDepartmentDescription.Text.Trim(),
                InvocationDate = dtpInvocationDate.Value,
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

        #endregion


    }
}
