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
    public partial class frmCreateDepartment : Form
    {

        public frmCreateDepartment()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Create Department button click
        /// Creates a new department
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                DepartmentService departmentService = new DepartmentService();

                Department department = PopulateDepartmentObject();

                if (departmentService.AddDepartment(department))
                {
                    MessageBox.Show("Success!\n" +
                        "New Department Id is: " + department.DepartmentId.ToString());
                    ClearForm();
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

        /// <summary>
        /// Populate a department object with form data
        /// </summary>
        /// <returns></returns>
        private Department PopulateDepartmentObject()
        {
            return new Department()
            {
                DepartmentName = txtDepartmentName.Text.Trim(),
                DepartmentDescription = txtDepartmentDescription.Text.Trim(),
                InvocationDate = dtpInvocationDate.Value,
            };
        }

        /// <summary>
        /// Reset all input fields
        /// </summary>
        private void ClearForm()
        {
            txtDepartmentName.Text = "";
            txtDepartmentDescription.Text = "";
            dtpInvocationDate.Value = DateTime.Now;
        }

        #endregion

        private void frmCreateDepartment_Load(object sender, EventArgs e)
        {
            LoadLoginInfo();
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
    }
}
