namespace VastVoyages.WinFrontEnd
{
    partial class frmModifyDepartment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModifyDepartment));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lbUserName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbSupervisor = new System.Windows.Forms.Label();
            this.lbJob = new System.Windows.Forms.Label();
            this.lbCurrentDate = new System.Windows.Forms.Label();
            this.lbDepartment = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbEmpName = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvDepartments = new System.Windows.Forms.DataGridView();
            this.grpModifyDepartment = new System.Windows.Forms.GroupBox();
            this.btnModifyDepartment = new System.Windows.Forms.Button();
            this.lblInvocationDate = new System.Windows.Forms.Label();
            this.lblDepartmentDescription = new System.Windows.Forms.Label();
            this.lblDepartmentName = new System.Windows.Forms.Label();
            this.dtpInvocationDate = new System.Windows.Forms.DateTimePicker();
            this.txtDepartmentDescription = new System.Windows.Forms.TextBox();
            this.txtDepartmentName = new System.Windows.Forms.TextBox();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartments)).BeginInit();
            this.grpModifyDepartment.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lbUserName);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.lbSupervisor);
            this.groupBox4.Controls.Add(this.lbJob);
            this.groupBox4.Controls.Add(this.lbCurrentDate);
            this.groupBox4.Controls.Add(this.lbDepartment);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.lbEmpName);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(538, 14);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(701, 140);
            this.groupBox4.TabIndex = 32;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 68);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 20);
            this.label2.TabIndex = 26;
            this.label2.Text = "User Name:";
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(53, 37);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 20);
            this.label7.TabIndex = 5;
            this.label7.Text = "Name:";
            // 
            // pbLogo
            // 
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(13, 13);
            this.pbLogo.Margin = new System.Windows.Forms.Padding(4);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(499, 140);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLogo.TabIndex = 33;
            this.pbLogo.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvDepartments);
            this.groupBox1.Location = new System.Drawing.Point(13, 180);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1226, 191);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select a Department to Modify";
            // 
            // dgvDepartments
            // 
            this.dgvDepartments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDepartments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDepartments.Location = new System.Drawing.Point(266, 21);
            this.dgvDepartments.Name = "dgvDepartments";
            this.dgvDepartments.RowHeadersWidth = 51;
            this.dgvDepartments.RowTemplate.Height = 24;
            this.dgvDepartments.Size = new System.Drawing.Size(750, 150);
            this.dgvDepartments.TabIndex = 0;
            this.dgvDepartments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDepartments_CellClick);
            // 
            // grpModifyDepartment
            // 
            this.grpModifyDepartment.Controls.Add(this.btnModifyDepartment);
            this.grpModifyDepartment.Controls.Add(this.lblInvocationDate);
            this.grpModifyDepartment.Controls.Add(this.lblDepartmentDescription);
            this.grpModifyDepartment.Controls.Add(this.lblDepartmentName);
            this.grpModifyDepartment.Controls.Add(this.dtpInvocationDate);
            this.grpModifyDepartment.Controls.Add(this.txtDepartmentDescription);
            this.grpModifyDepartment.Controls.Add(this.txtDepartmentName);
            this.grpModifyDepartment.Location = new System.Drawing.Point(13, 390);
            this.grpModifyDepartment.Name = "grpModifyDepartment";
            this.grpModifyDepartment.Size = new System.Drawing.Size(1226, 297);
            this.grpModifyDepartment.TabIndex = 35;
            this.grpModifyDepartment.TabStop = false;
            this.grpModifyDepartment.Tag = "ModifyDepartment";
            this.grpModifyDepartment.Text = "Select a Department to Modify";
            // 
            // btnModifyDepartment
            // 
            this.btnModifyDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModifyDepartment.Location = new System.Drawing.Point(575, 238);
            this.btnModifyDepartment.Name = "btnModifyDepartment";
            this.btnModifyDepartment.Size = new System.Drawing.Size(78, 39);
            this.btnModifyDepartment.TabIndex = 9;
            this.btnModifyDepartment.Text = "Save";
            this.btnModifyDepartment.UseVisualStyleBackColor = true;
            this.btnModifyDepartment.Click += new System.EventHandler(this.btnModifyDepartment_Click);
            // 
            // lblInvocationDate
            // 
            this.lblInvocationDate.AutoSize = true;
            this.lblInvocationDate.Location = new System.Drawing.Point(327, 198);
            this.lblInvocationDate.Name = "lblInvocationDate";
            this.lblInvocationDate.Size = new System.Drawing.Size(110, 17);
            this.lblInvocationDate.TabIndex = 12;
            this.lblInvocationDate.Text = "Invocation Date:";
            // 
            // lblDepartmentDescription
            // 
            this.lblDepartmentDescription.AutoSize = true;
            this.lblDepartmentDescription.Location = new System.Drawing.Point(354, 77);
            this.lblDepartmentDescription.Name = "lblDepartmentDescription";
            this.lblDepartmentDescription.Size = new System.Drawing.Size(83, 17);
            this.lblDepartmentDescription.TabIndex = 11;
            this.lblDepartmentDescription.Text = "Description:";
            // 
            // lblDepartmentName
            // 
            this.lblDepartmentName.AutoSize = true;
            this.lblDepartmentName.Location = new System.Drawing.Point(388, 34);
            this.lblDepartmentName.Name = "lblDepartmentName";
            this.lblDepartmentName.Size = new System.Drawing.Size(49, 17);
            this.lblDepartmentName.TabIndex = 10;
            this.lblDepartmentName.Text = "Name:";
            // 
            // dtpInvocationDate
            // 
            this.dtpInvocationDate.Location = new System.Drawing.Point(443, 198);
            this.dtpInvocationDate.Name = "dtpInvocationDate";
            this.dtpInvocationDate.Size = new System.Drawing.Size(360, 22);
            this.dtpInvocationDate.TabIndex = 8;
            // 
            // txtDepartmentDescription
            // 
            this.txtDepartmentDescription.Location = new System.Drawing.Point(443, 74);
            this.txtDepartmentDescription.Multiline = true;
            this.txtDepartmentDescription.Name = "txtDepartmentDescription";
            this.txtDepartmentDescription.Size = new System.Drawing.Size(360, 104);
            this.txtDepartmentDescription.TabIndex = 7;
            // 
            // txtDepartmentName
            // 
            this.txtDepartmentName.Location = new System.Drawing.Point(443, 34);
            this.txtDepartmentName.Name = "txtDepartmentName";
            this.txtDepartmentName.Size = new System.Drawing.Size(360, 22);
            this.txtDepartmentName.TabIndex = 6;
            // 
            // frmModifyDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 699);
            this.Controls.Add(this.grpModifyDepartment);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.pbLogo);
            this.Name = "frmModifyDepartment";
            this.Text = "frmModifyDepartment";
            this.Load += new System.EventHandler(this.frmModifyDepartment_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartments)).EndInit();
            this.grpModifyDepartment.ResumeLayout(false);
            this.grpModifyDepartment.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lbUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbSupervisor;
        private System.Windows.Forms.Label lbJob;
        private System.Windows.Forms.Label lbCurrentDate;
        private System.Windows.Forms.Label lbDepartment;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbEmpName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvDepartments;
        private System.Windows.Forms.GroupBox grpModifyDepartment;
        private System.Windows.Forms.Button btnModifyDepartment;
        private System.Windows.Forms.Label lblInvocationDate;
        private System.Windows.Forms.Label lblDepartmentDescription;
        private System.Windows.Forms.Label lblDepartmentName;
        private System.Windows.Forms.DateTimePicker dtpInvocationDate;
        private System.Windows.Forms.TextBox txtDepartmentDescription;
        private System.Windows.Forms.TextBox txtDepartmentName;
    }
}