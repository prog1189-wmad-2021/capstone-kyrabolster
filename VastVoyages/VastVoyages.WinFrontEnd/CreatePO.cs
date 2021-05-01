using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VastVoyages.WinFrontEnd
{
    public partial class CreatePO : Form
    {
        public CreatePO()
        {
            InitializeComponent();
        }

        private void CreatePO_Load(object sender, EventArgs e)
        {
            if (((MainForm)this.MdiParent).emp.MiddleInit != null)
            {
                lbEmpName.Text = ((MainForm)this.MdiParent).emp.FirstName + ' ' + ((MainForm)this.MdiParent).emp.MiddleInit + ' ' + ((MainForm)this.MdiParent).emp.LastName;
            }
            else
            {
                lbEmpName.Text = ((MainForm)this.MdiParent).emp.FirstName + ' ' + ((MainForm)this.MdiParent).emp.LastName;
            }

            lbJob.Text = ((MainForm)this.MdiParent).emp.Job;
            lbDepartment.Text = ((MainForm)this.MdiParent).emp.Department;
            lbSupervisor.Text = ((MainForm)this.MdiParent).emp.Supervisor;
            lbCurrentDate.Text = DateTime.Now.ToShortDateString();
        }

        private void CreatePO_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((MainForm)this.MdiParent).RefreshParent();
        }
    }
}
