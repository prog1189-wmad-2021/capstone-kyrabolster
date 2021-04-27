using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VastVoyages.Service;
using VastVoyages.Model;

namespace VastVoyages.WinFrontEnd
{
    public partial class Login : Form
    {
        internal LoginDTO loginInfo;
        public Login()
        {
            InitializeComponent();
            loginInfo = new LoginDTO();
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                LoginService service = new LoginService();

                loginInfo.EmployeeId = txtEmpId.Text.Trim() != string.Empty ? Convert.ToInt32(txtEmpId.Text.Trim()) : 0;
                loginInfo.Password = txtPassword.Text.Trim();

                if (service.AttemptLogin(loginInfo))
                {
                    DialogResult = DialogResult.OK;
                }

                else
                {
                    string errorMsg = "";

                    foreach(ValidationError error in loginInfo.Errors)
                    {
                        errorMsg += error.Description + Environment.NewLine;
                    }

                    MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
