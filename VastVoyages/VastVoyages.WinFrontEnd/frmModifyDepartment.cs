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
        byte[] _recordVersion;

        private void frmModifyDepartment_Load(object sender, EventArgs e)
        {
            LoadLoginInfo();
            LoadDepartments();
            grpModifyDepartment.Visible = false;
            btnDeleteDepartment.Visible = false;
        }

        private void dgvDepartments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                grpModifyDepartment.Visible = true;
                PopulateDepartmentDetails();
                string role = ((MainForm)this.MdiParent).loginInfo.Role;

                if (role == "CEO" || role == "HR Supervisor" || role == "HR Employee")
                    btnDeleteDepartment.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                Department department = PopulateDepartmentObject();

                if (department.DepartmentId <= 0)
                {
                    MessageBox.Show("Please select a department to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (departmentService.DeleteDepartment(department))
                    {
                        LoadDepartments();
                        MessageBox.Show(department.DepartmentName + " department successfully deleted.", "Success", MessageBoxButtons.OK);
                        grpModifyDepartment.Visible = false;
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
                string role = ((MainForm)this.MdiParent).loginInfo.Role;

                Department department = PopulateDepartmentObject();

                departmentService.UpdateDepartment(department, role);

                if (department.Errors.Count <= 0)
                {
                    MessageBox.Show("Success!\n" +
                        department.DepartmentName.ToString() + " department successfully updated");
                    grpModifyDepartment.Visible = false;
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
            string role  = ((MainForm)this.MdiParent).loginInfo.Role;
            int departmentId = ((MainForm)this.MdiParent).loginInfo.DepartmentId;

            //dgvDepartments.DataSource = departmentService.GetDepartments(role, departmentId).Select(o => new
            //{ DepartmentId = o.DepartmentId, Name = o.DepartmentName, Date = o.InvocationDate }).ToList();

            dgvDepartments.DataSource = departmentService.GetDepartments(role, departmentId);
            dgvDepartments.Columns["RecordVersion"].Visible = false;

        }

        private void PopulateDepartmentDetails()
        {
            string role = ((MainForm)this.MdiParent).loginInfo.Role;
            
            int departmentId = Convert.ToInt32(dgvDepartments.CurrentRow.Cells["DepartmentId"].Value);

            Department department = departmentService.GetDepartmentById(departmentId);

            txtDepartmentName.Text = (department.DepartmentName).ToString();
            txtDepartmentDescription.Text = (department.DepartmentDescription).ToString();
            dtpInvocationDate.Text = (department.InvocationDate).ToString();
            _recordVersion = (byte[])(department.RecordVersion);

            if (role == "Supervisor")
            {
                txtDepartmentName.ReadOnly = true;
                dtpInvocationDate.Enabled = false;
            }
        }

        private Department PopulateDepartmentObject()
        {
            return new Department()
            {
                DepartmentId = Convert.ToInt32(dgvDepartments.CurrentRow.Cells["DepartmentId"].Value),
                DepartmentName = txtDepartmentName.Text.Trim(),
                DepartmentDescription = txtDepartmentDescription.Text.Trim(),
                InvocationDate = dtpInvocationDate.Value,
                RecordVersion = _recordVersion
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
