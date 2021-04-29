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
    public partial class MainForm : Form
    {
        private int childFormNumber = 0;

        internal LoginDTO loginInfo;

        public MainForm()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            try
            {
                Form childForm = null;
                ToolStripMenuItem m = (ToolStripMenuItem)sender;

                switch (m.Tag)
                {
                    case "ViewPO":
                        childForm = new frmViewPO();
                        break;
                    case "CreatePO":
                        childForm = new frmCreatePO();
                        break;
                    case "ProcessPO":
                        childForm = new frmProcessPO();
                        break;

                        //case "Book":
                        //    childForm = new frmMaintenanceBook();
                        //    break;
                        //case "Author":
                        //    childForm = new frmMaintenanceAuthor();
                        //    break;

                }

                if (childForm != null)
                {
                    foreach (Form f in this.MdiChildren)
                    {
                        if (f.GetType() == childForm.GetType())
                        {
                            f.Activate();
                            return;
                        }
                    }
                }

                childForm.MdiParent = this;
                groupBox4.Visible = false;
                pictureBox1.Visible = false;
                btnEmp.Visible = false;
                btnSupervisor.Visible = false;
                childForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoginSplashLoad();
        }

        private void LoginSplashLoad()
        {
            Splash mySplash = new Splash();
            LoginForm myLogin = new LoginForm();

            if (loginInfo == null)
            {
                mySplash.ShowDialog();

                if (mySplash.DialogResult != DialogResult.OK)
                {
                    this.Close();
                }

                else
                {
                    myLogin.ShowDialog();
                }
            }

            else
            {
                myLogin.ShowDialog();
            }                     

            if (myLogin.DialogResult == DialogResult.OK)
            {
                try
                {
                    if(loginInfo != null)
                    {
                        this.Controls.Clear();
                        this.InitializeComponent();
                    }

                    this.Show();

                    LoginService service = new LoginService();

                    loginInfo = service.GetEmpInfo(myLogin.loginInfo.EmployeeId);

                    lbEmpName.Text = loginInfo.FullName;
                    lbUserName.Text = loginInfo.UserName;
                    lbJob.Text = loginInfo.Job;
                    lbDepartment.Text = loginInfo.Department;
                    lbSupervisor.Text = loginInfo.Supervisor;
                    lbCurrentDate.Text = DateTime.Now.ToShortDateString();

                    if (loginInfo.Role == "CEO" || loginInfo.Role == "Supervisor" || loginInfo.Role == "HR Supervisor")
                    {
                        btnSupervisor.Visible = true;
                        processPOToolStripMenuItem.Visible = true;
                    }

                    toolStripStatusLabel.Text = "Ready...";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                if (loginInfo == null)
                {
                    this.Close();

                }
            }
        }

        private void logOff_Click(object sender, EventArgs e)
        {
            LoginSplashLoad();
        }

        internal void RefreshParent()
        {
            int openChildFormCount = Application.OpenForms.Cast<Form>().Count(openForm => openForm.IsMdiChild);
            if (openChildFormCount == 0)
            {
                groupBox4.Visible = true;
                pictureBox1.Visible = true;
                btnEmp.Visible = true;

                if (loginInfo.Role == "CEO" || loginInfo.Role == "Supervisor" || loginInfo.Role == "HR Supervisor")
                {
                    btnSupervisor.Visible = true;
                    processPOToolStripMenuItem.Visible = true;
                }
            }
        }
    }
}
