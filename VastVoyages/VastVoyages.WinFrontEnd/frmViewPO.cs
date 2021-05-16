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
    public partial class frmViewPO : Form
    {
        private bool startDateFlag = false;
        private bool endDateFlag = false;

        public frmViewPO()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load user information, Generate status drop down list, Retrieve purchase order associated with the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewPO_Load(object sender, EventArgs e)
        {   
            try
            {
                lbEmpName.Text = ((MainForm)this.MdiParent).loginInfo.FullName;
                lbUserName.Text = ((MainForm)this.MdiParent).loginInfo.UserName;
                lbJob.Text = ((MainForm)this.MdiParent).loginInfo.Job;
                lbDepartment.Text = ((MainForm)this.MdiParent).loginInfo.Department;
                lbSupervisor.Text = ((MainForm)this.MdiParent).loginInfo.Supervisor;
                lbCurrentDate.Text = DateTime.Now.ToShortDateString();

                dtpStart.CustomFormat = " ";
                dtpStart.Format = DateTimePickerFormat.Custom;

                LoadStatus();
                GetPurchaseOrderList(Convert.ToInt32(((MainForm)this.MdiParent).loginInfo.EmployeeId));
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Retrieve purchase order list by employee Id
        /// </summary>
        /// <param name="employeeId"></param>
        private void GetPurchaseOrderList(int employeeId)
        {
            try
            {
                PurchaseOrderService service = new PurchaseOrderService();
                List<PurchaseOrderDTO> purchaseOrders = new List<PurchaseOrderDTO>();
                
                purchaseOrders = service.GetPurchaseOrderList(employeeId, null, null, null, 1);
                if(purchaseOrders.Count > 0)
                {
                    GeneratePurchaseOrderDataGridView(purchaseOrders);
                }
                else
                {
                    MessageBox.Show("No purchase order", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Search purchse order list by keyword
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                PurchaseOrderService service = new PurchaseOrderService();
                List<PurchaseOrderDTO> purchaseOrders = new List<PurchaseOrderDTO>();

                int employeeId = Convert.ToInt32(((MainForm)this.MdiParent).loginInfo.EmployeeId);
                DateTime? start = null;
                DateTime? end = null;
                int? POStatus = 1;
                int? PONumber = null;
                List<string> errorMsg = new List<string>();

                if (dtpEnd.Value.Date > DateTime.Now.Date)
                {
                    errorMsg.Add("End date can not be future");
                }
                // If user enter date in search area, set date to end parameter
                else if(endDateFlag)
                {
                    end = dtpEnd.Value.Date;
                }

                // If user enter date in search area
                if (startDateFlag)
                {
                    //If start date is provided(user changed the start data time picker)
                    if (startDateFlag && dtpStart.Value > DateTime.Now)
                    {
                        errorMsg.Add("Start date can not be future");
                    }
                    else
                    {
                        start = dtpStart.Value;
                    }

                    //if start date and end date are provided(user changed the start date and end date time picker)
                    if (startDateFlag && dtpStart.Value > dtpEnd.Value)
                    {
                        errorMsg.Add("End Date cannot be prior to start date");
                    }
                    else
                    {
                        start = dtpStart.Value;
                    }
                }

                // If user enter po number
                if(!string.IsNullOrEmpty(txtPONumber.Text.Trim())) {
                    if(!int.TryParse(txtPONumber.Text.Trim(), out int PONumParam))
                    {
                        errorMsg.Add("Invalid format. Purchase order number must be 8 digit.");
                    }
                    else
                    {
                        PONumber = PONumParam;
                    }
                }

                // If user select po status
                if(cmbPOStatus.SelectedIndex > 0)
                {
                    int? selectedStatusId = Convert.ToInt32(cmbPOStatus.SelectedValue);
                    POStatus = selectedStatusId == 1 ? 1 : selectedStatusId == 0 ? null : selectedStatusId;
                }
                
                // if no search criteria error
                if(errorMsg.Count > 0)
                {
                    string msg = "";

                    foreach (string error in errorMsg)
                    {
                        msg += error + Environment.NewLine;
                    }

                    MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    purchaseOrders = service.GetPurchaseOrderList(employeeId, PONumber, start, end, POStatus);

                    if (purchaseOrders.Count > 0)
                    {
                        GeneratePurchaseOrderDataGridView(purchaseOrders);
                    }
                    else
                    {
                        MessageBox.Show("No purchase order found matching this search criteria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event when user click edit button on each row, retrieve item list or fire edit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int PONumber = Convert.ToInt32(dgvPO.Rows[e.RowIndex].Cells["Purchase Order #"].Value.ToString());

                if (e.ColumnIndex > -1 && dgvPO.Columns[e.ColumnIndex].Name == "btnEdit")
                {
                    // only pending PO can be edited
                    if (e.RowIndex >= 0)
                    {
                        PurchaseOrderService service = new PurchaseOrderService();

                        PurchaseOrderDTO purchaseOrderDTO = service.GetPurchaseOrderByPONumber(PONumber, null, null, false);

                        if(purchaseOrderDTO.POStatus == "Pending")
                        {
                            frmCreatePO createForm = new frmCreatePO();

                            createForm.edit = true;

                            createForm._purchaseOrder = new PurchaseOrder
                            {
                                PONumber = purchaseOrderDTO.PONumber,
                                SubmissionDate = purchaseOrderDTO.SubmissionDate,
                                RecordVersion = purchaseOrderDTO.RecordVersion
                            };

                            createForm.MdiParent = this.MdiParent;
                            createForm.Show();
                        }
                        else
                        {
                            MessageBox.Show("You cannot modify this purchase order", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                   
                }
                else
                {
                    ItemService service = new ItemService();

                    dgvItem.DataSource = service.GetItemListByPO(PONumber, false);
                    dgvItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    dgvItem.Columns[0].HeaderText = "Item ID";
                    dgvItem.Columns[1].HeaderText = "Item Name";
                    dgvItem.Columns[2].HeaderText = "Description";
                    dgvItem.Columns[3].HeaderText = "Justificaton";
                    dgvItem.Columns[4].HeaderText = "Location";
                    dgvItem.Columns[5].HeaderText = "Price";
                    dgvItem.Columns[6].HeaderText = "Qty";
                    dgvItem.Columns[7].Visible = false; //PO Number
                    dgvItem.Columns[8].Visible = false; //PO Status Id                  
                    dgvItem.Columns[9].Visible = false; //Item Status Id
                    dgvItem.Columns[10].HeaderText = "Status";
                    dgvItem.Columns[11].HeaderText = "Decision Reason";
                    dgvItem.Columns[12].Visible = false; // Record version      
                    dgvItem.Columns[13].Visible = false;
                    dgvItem.Columns[14].Visible = false;
                    dgvItem.Columns[15].Visible = false;

                    dgvItem.Columns[5].DefaultCellStyle.Format = "C";
                    dgvItem.AutoResizeColumns();
                }
            }
        }

        /// <summary>
        /// When user select date, set date from empty field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            
            switch (dtp.Name)
            {
                case "dtpStart":
                    startDateFlag = true;
                    dtp.CustomFormat = "       MMM        d, yyyy";
                    break;
                case "dtpEnd":
                    endDateFlag = true;
                    break;
            }
        }

        /// <summary>
        /// Reset search criteria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchCriteriaReset_Click(object sender, EventArgs e)
        {
            dtpStart.CustomFormat = " ";
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpEnd.Value = DateTime.Now;
            startDateFlag = false;
            endDateFlag = false;
            txtPONumber.Text = "";
            cmbPOStatus.SelectedIndex = 0;
        }

        /// <summary>
        /// form close event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewPO_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((MainForm)this.MdiParent).RefreshParent();
        }

        #region Helper methods

        /// <summary>
        /// Generate purchase order status drop down list
        /// </summary>
        private void LoadStatus()
        {
            POLookUpsService service = new POLookUpsService();
            List<POStatusLookUpsDTO> poStatus = service.GetPOStatus();

            poStatus.Insert(0, new POStatusLookUpsDTO { POStatusId = -1, POStatus = "--Select a status--" });
            poStatus.Insert(1, new POStatusLookUpsDTO { POStatusId = 0, POStatus = "All POs" });
            poStatus.RemoveAt(3);

            cmbPOStatus.DataSource = poStatus;
            cmbPOStatus.DisplayMember = "POStatus";
            cmbPOStatus.ValueMember = "POStatusId";
            cmbPOStatus.SelectedIndex = 0;            
        }

        /// <summary>
        /// Generate purchase order DTO in Data grid view
        /// </summary>
        /// <param name="purchaseOrderDTO"></param>
        private void GeneratePurchaseOrderDataGridView(List<PurchaseOrderDTO> purchaseOrders)
        {
            dgvItem.DataSource = null;
            dgvPO.DataSource = null;
            dgvPO.Rows.Clear();
            dgvPO.Columns.Clear();
            dgvPO.Refresh();

            dgvPO.DataSource = purchaseOrders;
            dgvPO.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvPO.Columns[0].HeaderText = "Purchase Order #";
            dgvPO.Columns[0].Name = "Purchase Order #";
            dgvPO.Columns[1].HeaderText = "Submission Date";            
            dgvPO.Columns[1].HeaderText = "Submission Date";
            dgvPO.Columns[2].HeaderText = "Sub Total";
            dgvPO.Columns[3].HeaderText = "Tax";
            dgvPO.Columns[4].HeaderText = "Total";
            dgvPO.Columns[6].HeaderText = "Employee";
            dgvPO.Columns[7].HeaderText = "Supervisor";
            dgvPO.Columns[10].HeaderText = "Status";
            dgvPO.Columns[10].Name = "Status";

            dgvPO.Columns[2].DefaultCellStyle.Format = "C";
            dgvPO.Columns[3].DefaultCellStyle.Format = "C";
            dgvPO.Columns[4].DefaultCellStyle.Format = "C";

            // don't need to show employee's name and supervisor's name in this form
            dgvPO.Columns[5].Visible = false;
            dgvPO.Columns[6].Visible = false;
            dgvPO.Columns[8].Visible = false;
            dgvPO.Columns[9].Visible = false;
            dgvPO.Columns[11].Visible = false;

            dgvPO.AutoResizeColumns();

            DataGridViewButtonColumn dgvBtnDetail = new DataGridViewButtonColumn();
            dgvPO.Columns.Insert(dgvPO.Columns.Count, dgvBtnDetail);
            dgvBtnDetail.HeaderText = "Edit";
            dgvBtnDetail.Text = "Edit";
            dgvBtnDetail.Name = "btnEdit";
            dgvBtnDetail.UseColumnTextForButtonValue = true;
        }

        #endregion

    }
}
