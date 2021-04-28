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

        private void btnCreateDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                //validate form ?

                DepartmentService departmentService = new DepartmentService();

                //statusStrip1.Visible = true;

                Department department = PopulateDepartmentObject();

                if (departmentService.AddDepartment(department))
                {
                    MessageBox.Show("New Department Id is: " + department.DepartmentId.ToString());
                    //statusStrip1.Items[0].Text = "Insert Successful!";
                }
                else
                {
                    string msg = "";
                    foreach (ValidationError error in department.Errors)
                    {
                        msg += error.Description + Environment.NewLine;
                    }
                    MessageBox.Show(msg);
                    //statusStrip1.Items[0].Text = "Insert Failed!";
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Helpers
        private Department PopulateDepartmentObject()
        {
            return new Department()
            {
                DepartmentName = txtDepartmentName.Text.Trim(),
                DepartmentDescription = txtDepartmentDescription.Text.Trim(),
                InvocationDate = dtpInvocationDate.Value,
            };
        }

        #endregion
    }
}
