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
using VastVoyages.Model.Entities;

namespace VastVoyages.WinFrontEnd
{
    public partial class LoginForm : Form
    {
        internal Login loginInfo;
        public LoginForm()
        {
            InitializeComponent();
            loginInfo = new Login();
            txtPassword.UseSystemPasswordChar = true;
        }

        EmployeeService employeeService = new EmployeeService();
        ReviewService reviewService = new ReviewService();
        EmailService emailService = new EmailService();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                LoginService service = new LoginService();
                loginInfo = new Login();

                loginInfo.EmployeeId = txtEmpId.Text.Trim();
                loginInfo.Password = txtPassword.Text.Trim();

                if (service.AttemptLogin(loginInfo))
                {
                    DialogResult = DialogResult.OK;

                    //Send review reminders when HR employee logs in
                    int employeeId = Convert.ToInt32(loginInfo.EmployeeId);
                    Employee employee = employeeService.GetEmployeeToModifyById(employeeId);
                    if (employee.DepartmentId == 2)
                    {
                        //check if reminders already sent
                        if(!reviewService.HaveReviewEmailsBeenSentToday())
                            try
                            {
                                SendReviewReminders();
                            }
                            catch (Exception ex)
                            {
                                //email logged in hr employee when error sending email
                                Email email = new Email();
                                email.mailTo = employee.Email;
                                email.mailFrom = "Admin@VastVoyages.ca";
                                email.subject = "Employee Review Reminder - Error";
                                email.body = $"Unble to send today's employee review reminders. Please contact your administrator.";

                                emailService.SendReviewReminders(email);

                                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                    }
                }

                else
                {
                    string errorMsg = "";

                    foreach (ValidationError error in loginInfo.Errors)
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

        public void SendReviewReminders()
        {

            Email email = new Email();
            List<EmployeeDTO> employees = new List<EmployeeDTO>();
            employees = employeeService.GetAllEmployees();

            //get all supervisors
            List<EmployeeDTO> supervisors = new List<EmployeeDTO>();
            supervisors = employees.Where(x => x.SupervisorId.Equals((employeeService.GetCEO()[0]).SupervisorId)).ToList();

            //employees due
            foreach (EmployeeDTO supervisor in supervisors)
            {
                employees = employeeService.GetAllEmployees().Where(x => x.SupervisorId.Equals(supervisor.EmpId)).ToList();
                employees = reviewService.GetEmployeesDueFoReviewThisQuarter(employees);
                //employees = reviewService.GetEmployeesDueFoReviewLastQuarter(employees);

                if (employees.Count() > 0)
                {
                    //send mail
                    email.mailTo = supervisor.Email;
                    email.mailFrom = "Admin@VastVoyages.ca";
                    email.subject = "Employee Review Reminder";
                    email.body = $"<h2>The following employees are due for review this quarter</h2><ul>";

                    foreach (EmployeeDTO employee in employees)
                    {
                        email.body += $"<li> {employee.FullName}</li>";
                    }

                    //check overdue
                    if (reviewService.Is30DaysPastQuarter())
                    {
                        //hr staff
                        List<EmployeeDTO> hrStaff = employeeService.GetAllEmployees().Where(x => x.DepartmentId.Equals(2)).ToList();
                        List<string> hrStaffList = new List<string>();

                        foreach (EmployeeDTO staff in hrStaff)
                        {
                            if (staff.EmpId != supervisor.EmpId)
                                hrStaffList.Add(staff.Email);
                        }
                        email.cc = hrStaffList;
                        //email.cc = String.Join(";", hrStaff);
                        //email.cc = hrStaff.Select(r => String.Join(";", r.Split(',', ';'))).ToList();
                    }else
                    {
                        email.cc = null;
                    }

                    email.body += $"</ul><br><i>HR staff is notified when reviews are due 30 days past the start of the quarter.</i><br>";
                    email.body += $"<br><a href='https://localhost:44370/Reviews'>Create Review</a>";

                    emailService.SendReviewReminders(email);
                }
            }
            reviewService.TrackReviewReminderSent();
        }
    }
}
