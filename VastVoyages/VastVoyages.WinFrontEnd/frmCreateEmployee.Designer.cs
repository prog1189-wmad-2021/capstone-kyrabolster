namespace VastVoyages.WinFrontEnd
{
    partial class frmCreateEmployee
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreateEmployee));
            this.dtpDOB = new System.Windows.Forms.DateTimePicker();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.btnCreateEmployee = new System.Windows.Forms.Button();
            this.lblDOB = new System.Windows.Forms.Label();
            this.lblMiddleInit = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.grpCreateEmployee = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSIN = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMiddleInit = new System.Windows.Forms.TextBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStreet = new System.Windows.Forms.TextBox();
            this.txtPostalCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtWorkPhone = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCellPhone = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.grpEmploymentDetails = new System.Windows.Forms.GroupBox();
            this.lblHeadSupervisor = new System.Windows.Forms.Label();
            this.chkHeadSupervisor = new System.Windows.Forms.CheckBox();
            this.lblSupervisorMsg = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.chkIsSupervisor = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cmbSupervisor = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cmbJobAssignment = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.dtpSeniorityDate = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.dtpJobStartDate = new System.Windows.Forms.DateTimePicker();
            this.grpContactInfo = new System.Windows.Forms.GroupBox();
            this.cmbProvince = new System.Windows.Forms.ComboBox();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lbUserName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbSupervisor = new System.Windows.Forms.Label();
            this.lbJob = new System.Windows.Forms.Label();
            this.lbCurrentDate = new System.Windows.Forms.Label();
            this.lbDepartment = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbEmpName = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.grpCreateEmployee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.grpEmploymentDetails.SuspendLayout();
            this.grpContactInfo.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpDOB
            // 
            this.dtpDOB.Location = new System.Drawing.Point(182, 129);
            this.dtpDOB.Name = "dtpDOB";
            this.dtpDOB.Size = new System.Drawing.Size(340, 22);
            this.dtpDOB.TabIndex = 3;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(182, 40);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(340, 22);
            this.txtFirstName.TabIndex = 0;
            // 
            // btnCreateEmployee
            // 
            this.btnCreateEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateEmployee.Location = new System.Drawing.Point(578, 684);
            this.btnCreateEmployee.Name = "btnCreateEmployee";
            this.btnCreateEmployee.Size = new System.Drawing.Size(111, 39);
            this.btnCreateEmployee.TabIndex = 19;
            this.btnCreateEmployee.Text = "Create";
            this.btnCreateEmployee.UseVisualStyleBackColor = true;
            this.btnCreateEmployee.Click += new System.EventHandler(this.btnCreateEmployee_Click);
            // 
            // lblDOB
            // 
            this.lblDOB.AutoSize = true;
            this.lblDOB.Location = new System.Drawing.Point(64, 129);
            this.lblDOB.Name = "lblDOB";
            this.lblDOB.Size = new System.Drawing.Size(91, 17);
            this.lblDOB.TabIndex = 5;
            this.lblDOB.Text = "Date of Birth:";
            // 
            // lblMiddleInit
            // 
            this.lblMiddleInit.AutoSize = true;
            this.lblMiddleInit.Location = new System.Drawing.Point(64, 68);
            this.lblMiddleInit.Name = "lblMiddleInit";
            this.lblMiddleInit.Size = new System.Drawing.Size(89, 17);
            this.lblMiddleInit.TabIndex = 4;
            this.lblMiddleInit.Text = "Middle Initial:";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(64, 40);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(80, 17);
            this.lblFirstName.TabIndex = 3;
            this.lblFirstName.Text = "First Name:";
            // 
            // grpCreateEmployee
            // 
            this.grpCreateEmployee.Controls.Add(this.label11);
            this.grpCreateEmployee.Controls.Add(this.txtSIN);
            this.grpCreateEmployee.Controls.Add(this.txtLastName);
            this.grpCreateEmployee.Controls.Add(this.label1);
            this.grpCreateEmployee.Controls.Add(this.txtMiddleInit);
            this.grpCreateEmployee.Controls.Add(this.lblDOB);
            this.grpCreateEmployee.Controls.Add(this.lblMiddleInit);
            this.grpCreateEmployee.Controls.Add(this.lblFirstName);
            this.grpCreateEmployee.Controls.Add(this.dtpDOB);
            this.grpCreateEmployee.Controls.Add(this.txtFirstName);
            this.grpCreateEmployee.Location = new System.Drawing.Point(13, 161);
            this.grpCreateEmployee.Name = "grpCreateEmployee";
            this.grpCreateEmployee.Size = new System.Drawing.Size(600, 316);
            this.grpCreateEmployee.TabIndex = 0;
            this.grpCreateEmployee.TabStop = false;
            this.grpCreateEmployee.Text = "Personal Information";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(64, 157);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 17);
            this.label11.TabIndex = 22;
            this.label11.Text = "SIN:";
            // 
            // txtSIN
            // 
            this.txtSIN.Location = new System.Drawing.Point(182, 157);
            this.txtSIN.Name = "txtSIN";
            this.txtSIN.Size = new System.Drawing.Size(340, 22);
            this.txtSIN.TabIndex = 4;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(182, 96);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(340, 22);
            this.txtLastName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "LastName:";
            // 
            // txtMiddleInit
            // 
            this.txtMiddleInit.Location = new System.Drawing.Point(182, 68);
            this.txtMiddleInit.Name = "txtMiddleInit";
            this.txtMiddleInit.Size = new System.Drawing.Size(126, 22);
            this.txtMiddleInit.TabIndex = 1;
            // 
            // pbLogo
            // 
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(13, 13);
            this.pbLogo.Margin = new System.Windows.Forms.Padding(4);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(499, 140);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLogo.TabIndex = 34;
            this.pbLogo.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Province:";
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(186, 68);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(340, 22);
            this.txtCity.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "City:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(70, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Street:";
            // 
            // txtStreet
            // 
            this.txtStreet.Location = new System.Drawing.Point(186, 40);
            this.txtStreet.Name = "txtStreet";
            this.txtStreet.Size = new System.Drawing.Size(340, 22);
            this.txtStreet.TabIndex = 5;
            // 
            // txtPostalCode
            // 
            this.txtPostalCode.Location = new System.Drawing.Point(186, 154);
            this.txtPostalCode.Name = "txtPostalCode";
            this.txtPostalCode.Size = new System.Drawing.Size(340, 22);
            this.txtPostalCode.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 17);
            this.label5.TabIndex = 19;
            this.label5.Text = "Postal Code:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(70, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "Country:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(70, 210);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 17);
            this.label13.TabIndex = 24;
            this.label13.Text = "Work Phone:";
            // 
            // txtWorkPhone
            // 
            this.txtWorkPhone.Location = new System.Drawing.Point(186, 207);
            this.txtWorkPhone.Name = "txtWorkPhone";
            this.txtWorkPhone.Size = new System.Drawing.Size(340, 22);
            this.txtWorkPhone.TabIndex = 10;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(70, 238);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 17);
            this.label14.TabIndex = 26;
            this.label14.Text = "Cell Phone:";
            // 
            // txtCellPhone
            // 
            this.txtCellPhone.Location = new System.Drawing.Point(186, 235);
            this.txtCellPhone.Name = "txtCellPhone";
            this.txtCellPhone.Size = new System.Drawing.Size(340, 22);
            this.txtCellPhone.TabIndex = 11;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(70, 266);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(46, 17);
            this.label15.TabIndex = 28;
            this.label15.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(186, 263);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(340, 22);
            this.txtEmail.TabIndex = 12;
            // 
            // grpEmploymentDetails
            // 
            this.grpEmploymentDetails.Controls.Add(this.lblHeadSupervisor);
            this.grpEmploymentDetails.Controls.Add(this.chkHeadSupervisor);
            this.grpEmploymentDetails.Controls.Add(this.lblSupervisorMsg);
            this.grpEmploymentDetails.Controls.Add(this.label21);
            this.grpEmploymentDetails.Controls.Add(this.chkIsSupervisor);
            this.grpEmploymentDetails.Controls.Add(this.label20);
            this.grpEmploymentDetails.Controls.Add(this.cmbDepartment);
            this.grpEmploymentDetails.Controls.Add(this.label19);
            this.grpEmploymentDetails.Controls.Add(this.cmbSupervisor);
            this.grpEmploymentDetails.Controls.Add(this.label18);
            this.grpEmploymentDetails.Controls.Add(this.cmbJobAssignment);
            this.grpEmploymentDetails.Controls.Add(this.label17);
            this.grpEmploymentDetails.Controls.Add(this.dtpSeniorityDate);
            this.grpEmploymentDetails.Controls.Add(this.label16);
            this.grpEmploymentDetails.Controls.Add(this.dtpJobStartDate);
            this.grpEmploymentDetails.Location = new System.Drawing.Point(13, 483);
            this.grpEmploymentDetails.Name = "grpEmploymentDetails";
            this.grpEmploymentDetails.Size = new System.Drawing.Size(1226, 185);
            this.grpEmploymentDetails.TabIndex = 13;
            this.grpEmploymentDetails.TabStop = false;
            this.grpEmploymentDetails.Text = "Employment Information";
            // 
            // lblHeadSupervisor
            // 
            this.lblHeadSupervisor.AutoSize = true;
            this.lblHeadSupervisor.Location = new System.Drawing.Point(870, 74);
            this.lblHeadSupervisor.Name = "lblHeadSupervisor";
            this.lblHeadSupervisor.Size = new System.Drawing.Size(136, 17);
            this.lblHeadSupervisor.TabIndex = 19;
            this.lblHeadSupervisor.Text = "Is Head Supervisor?";
            // 
            // chkHeadSupervisor
            // 
            this.chkHeadSupervisor.AutoSize = true;
            this.chkHeadSupervisor.Location = new System.Drawing.Point(1012, 75);
            this.chkHeadSupervisor.Name = "chkHeadSupervisor";
            this.chkHeadSupervisor.Size = new System.Drawing.Size(18, 17);
            this.chkHeadSupervisor.TabIndex = 20;
            this.chkHeadSupervisor.UseVisualStyleBackColor = true;
            // 
            // lblSupervisorMsg
            // 
            this.lblSupervisorMsg.AutoSize = true;
            this.lblSupervisorMsg.Location = new System.Drawing.Point(809, 124);
            this.lblSupervisorMsg.Name = "lblSupervisorMsg";
            this.lblSupervisorMsg.Size = new System.Drawing.Size(116, 17);
            this.lblSupervisorMsg.TabIndex = 18;
            this.lblSupervisorMsg.Text = "lblSupervisorMsg";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(696, 73);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(110, 17);
            this.label21.TabIndex = 17;
            this.label21.Text = "Is a Supervisor?";
            // 
            // chkIsSupervisor
            // 
            this.chkIsSupervisor.AutoSize = true;
            this.chkIsSupervisor.Location = new System.Drawing.Point(812, 74);
            this.chkIsSupervisor.Name = "chkIsSupervisor";
            this.chkIsSupervisor.Size = new System.Drawing.Size(18, 17);
            this.chkIsSupervisor.TabIndex = 17;
            this.chkIsSupervisor.UseVisualStyleBackColor = true;
            this.chkIsSupervisor.CheckedChanged += new System.EventHandler(this.chkIsSupervisor_CheckedChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(696, 47);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(86, 17);
            this.label20.TabIndex = 15;
            this.label20.Text = "Department:";
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(812, 44);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(340, 24);
            this.cmbDepartment.TabIndex = 16;
            this.cmbDepartment.SelectionChangeCommitted += new System.EventHandler(this.cmbDepartment_SelectionChangeCommitted);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(696, 97);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(80, 17);
            this.label19.TabIndex = 13;
            this.label19.Text = "Supervisor:";
            // 
            // cmbSupervisor
            // 
            this.cmbSupervisor.FormattingEnabled = true;
            this.cmbSupervisor.Location = new System.Drawing.Point(812, 97);
            this.cmbSupervisor.Name = "cmbSupervisor";
            this.cmbSupervisor.Size = new System.Drawing.Size(340, 24);
            this.cmbSupervisor.TabIndex = 18;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(64, 105);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(112, 17);
            this.label18.TabIndex = 11;
            this.label18.Text = "Job Assignment:";
            // 
            // cmbJobAssignment
            // 
            this.cmbJobAssignment.FormattingEnabled = true;
            this.cmbJobAssignment.Location = new System.Drawing.Point(182, 102);
            this.cmbJobAssignment.Name = "cmbJobAssignment";
            this.cmbJobAssignment.Size = new System.Drawing.Size(340, 24);
            this.cmbJobAssignment.TabIndex = 15;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(64, 46);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(105, 17);
            this.label17.TabIndex = 9;
            this.label17.Text = "Seniority  Date:";
            // 
            // dtpSeniorityDate
            // 
            this.dtpSeniorityDate.Location = new System.Drawing.Point(182, 46);
            this.dtpSeniorityDate.Name = "dtpSeniorityDate";
            this.dtpSeniorityDate.Size = new System.Drawing.Size(340, 22);
            this.dtpSeniorityDate.TabIndex = 13;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(64, 74);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(103, 17);
            this.label16.TabIndex = 7;
            this.label16.Text = "Job Start Date:";
            // 
            // dtpJobStartDate
            // 
            this.dtpJobStartDate.Location = new System.Drawing.Point(182, 74);
            this.dtpJobStartDate.Name = "dtpJobStartDate";
            this.dtpJobStartDate.Size = new System.Drawing.Size(340, 22);
            this.dtpJobStartDate.TabIndex = 14;
            // 
            // grpContactInfo
            // 
            this.grpContactInfo.Controls.Add(this.cmbProvince);
            this.grpContactInfo.Controls.Add(this.cmbCountry);
            this.grpContactInfo.Controls.Add(this.label15);
            this.grpContactInfo.Controls.Add(this.txtEmail);
            this.grpContactInfo.Controls.Add(this.label14);
            this.grpContactInfo.Controls.Add(this.txtStreet);
            this.grpContactInfo.Controls.Add(this.txtCellPhone);
            this.grpContactInfo.Controls.Add(this.label4);
            this.grpContactInfo.Controls.Add(this.label13);
            this.grpContactInfo.Controls.Add(this.label3);
            this.grpContactInfo.Controls.Add(this.txtWorkPhone);
            this.grpContactInfo.Controls.Add(this.txtCity);
            this.grpContactInfo.Controls.Add(this.label2);
            this.grpContactInfo.Controls.Add(this.label6);
            this.grpContactInfo.Controls.Add(this.txtPostalCode);
            this.grpContactInfo.Controls.Add(this.label5);
            this.grpContactInfo.Location = new System.Drawing.Point(639, 161);
            this.grpContactInfo.Name = "grpContactInfo";
            this.grpContactInfo.Size = new System.Drawing.Size(600, 316);
            this.grpContactInfo.TabIndex = 5;
            this.grpContactInfo.TabStop = false;
            this.grpContactInfo.Text = "Contact Information";
            // 
            // cmbProvince
            // 
            this.cmbProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvince.FormattingEnabled = true;
            this.cmbProvince.Location = new System.Drawing.Point(186, 96);
            this.cmbProvince.Name = "cmbProvince";
            this.cmbProvince.Size = new System.Drawing.Size(340, 24);
            this.cmbProvince.TabIndex = 30;
            // 
            // cmbCountry
            // 
            this.cmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Location = new System.Drawing.Point(186, 124);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(340, 24);
            this.cmbCountry.TabIndex = 29;
            this.cmbCountry.SelectionChangeCommitted += new System.EventHandler(this.cmbCountry_SelectionChangeCommitted);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lbUserName);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.lbSupervisor);
            this.groupBox4.Controls.Add(this.lbJob);
            this.groupBox4.Controls.Add(this.lbCurrentDate);
            this.groupBox4.Controls.Add(this.lbDepartment);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.lbEmpName);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(538, 14);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(701, 140);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "User Information";
            // 
            // lbUserName
            // 
            this.lbUserName.AutoSize = true;
            this.lbUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUserName.Location = new System.Drawing.Point(131, 68);
            this.lbUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbUserName.Name = "lbUserName";
            this.lbUserName.Size = new System.Drawing.Size(94, 20);
            this.lbUserName.TabIndex = 27;
            this.lbUserName.Text = "User Name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 68);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 20);
            this.label7.TabIndex = 26;
            this.label7.Text = "User Name:";
            // 
            // lbSupervisor
            // 
            this.lbSupervisor.AutoSize = true;
            this.lbSupervisor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSupervisor.Location = new System.Drawing.Point(483, 68);
            this.lbSupervisor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSupervisor.Name = "lbSupervisor";
            this.lbSupervisor.Size = new System.Drawing.Size(113, 20);
            this.lbSupervisor.TabIndex = 12;
            this.lbSupervisor.Text = "Sophia Brown";
            // 
            // lbJob
            // 
            this.lbJob.AutoSize = true;
            this.lbJob.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbJob.Location = new System.Drawing.Point(483, 38);
            this.lbJob.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbJob.Name = "lbJob";
            this.lbJob.Size = new System.Drawing.Size(89, 20);
            this.lbJob.TabIndex = 11;
            this.lbJob.Text = "Supervisor";
            // 
            // lbCurrentDate
            // 
            this.lbCurrentDate.AutoSize = true;
            this.lbCurrentDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurrentDate.Location = new System.Drawing.Point(483, 100);
            this.lbCurrentDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCurrentDate.Name = "lbCurrentDate";
            this.lbCurrentDate.Size = new System.Drawing.Size(180, 20);
            this.lbCurrentDate.TabIndex = 25;
            this.lbCurrentDate.Text = "June 1, 2020 04:22 PM";
            // 
            // lbDepartment
            // 
            this.lbDepartment.AutoSize = true;
            this.lbDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDepartment.Location = new System.Drawing.Point(131, 100);
            this.lbDepartment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDepartment.Name = "lbDepartment";
            this.lbDepartment.Size = new System.Drawing.Size(82, 20);
            this.lbDepartment.TabIndex = 10;
            this.lbDepartment.Text = "Marketing";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(419, 100);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 20);
            this.label10.TabIndex = 8;
            this.label10.Text = "Date:";
            // 
            // lbEmpName
            // 
            this.lbEmpName.AutoSize = true;
            this.lbEmpName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEmpName.Location = new System.Drawing.Point(131, 38);
            this.lbEmpName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbEmpName.Name = "lbEmpName";
            this.lbEmpName.Size = new System.Drawing.Size(120, 20);
            this.lbEmpName.TabIndex = 9;
            this.lbEmpName.Text = "Ashely Presely";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(387, 38);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 20);
            this.label12.TabIndex = 8;
            this.label12.Text = "Job Title:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(368, 68);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 20);
            this.label9.TabIndex = 7;
            this.label9.Text = "Supervisor:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 100);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 20);
            this.label8.TabIndex = 6;
            this.label8.Text = "Department:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(53, 37);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(58, 20);
            this.label22.TabIndex = 5;
            this.label22.Text = "Name:";
            // 
            // frmCreateEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 735);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.grpContactInfo);
            this.Controls.Add(this.grpEmploymentDetails);
            this.Controls.Add(this.grpCreateEmployee);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.btnCreateEmployee);
            this.Name = "frmCreateEmployee";
            this.Text = "Create Employee";
            this.Load += new System.EventHandler(this.frmCreateEmployee_Load);
            this.grpCreateEmployee.ResumeLayout(false);
            this.grpCreateEmployee.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.grpEmploymentDetails.ResumeLayout(false);
            this.grpEmploymentDetails.PerformLayout();
            this.grpContactInfo.ResumeLayout(false);
            this.grpContactInfo.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDOB;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Button btnCreateEmployee;
        private System.Windows.Forms.Label lblDOB;
        private System.Windows.Forms.Label lblMiddleInit;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.GroupBox grpCreateEmployee;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMiddleInit;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSIN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtStreet;
        private System.Windows.Forms.TextBox txtPostalCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtWorkPhone;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtCellPhone;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.GroupBox grpEmploymentDetails;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker dtpJobStartDate;
        private System.Windows.Forms.GroupBox grpContactInfo;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DateTimePicker dtpSeniorityDate;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cmbSupervisor;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbJobAssignment;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox chkIsSupervisor;
        private System.Windows.Forms.Label lblSupervisorMsg;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lbUserName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbSupervisor;
        private System.Windows.Forms.Label lbJob;
        private System.Windows.Forms.Label lbCurrentDate;
        private System.Windows.Forms.Label lbDepartment;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbEmpName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.ComboBox cmbProvince;
        private System.Windows.Forms.Label lblHeadSupervisor;
        private System.Windows.Forms.CheckBox chkHeadSupervisor;
    }
}