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
    public partial class frmProcessPO : Form
    {
        private bool startDateFlag = false;
        private byte[] recordVersion;
        private PurchaseOrderDTO _purchaseOrder;

        private ItemService itemService = new ItemService();
        private PurchaseOrderService POService = new PurchaseOrderService();

        public frmProcessPO()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When load form first, retrieve employee's information and generate pending purchase order list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessPO_Load(object sender, EventArgs e)
        {
            lbEmpName.Text = ((MainForm)this.MdiParent).loginInfo.FullName;
            lbUserName.Text = ((MainForm)this.MdiParent).loginInfo.UserName;
            lbJob.Text = ((MainForm)this.MdiParent).loginInfo.Job;
            lbDepartment.Text = ((MainForm)this.MdiParent).loginInfo.Department;
            lbSupervisor.Text = ((MainForm)this.MdiParent).loginInfo.Supervisor;
            lbCurrentDate.Text = DateTime.Now.ToShortDateString();

            dtpStart.CustomFormat = " ";
            dtpStart.Format = DateTimePickerFormat.Custom;

            LoadPOStatus();
            LoadItemStatus();
            GetPurchaseOrderList(Convert.ToInt32(((MainForm)this.MdiParent).loginInfo.EmployeeId));
            if (!((MainForm)this.MdiParent).loginInfo.IsHeadSupervisor)
            {
                txtLocation.Enabled = false;
                txtPrice.Enabled = false;
                cmbItemStatus.Enabled = false;
                txtReason.Enabled = false;
                numQty.Enabled = false;
                btnSave.Visible = false;
            }
        }

        /// <summary>
        /// Generate pending purchase order list for department
        /// </summary>
        /// <param name="supervisorId"></param>
        private void GetPurchaseOrderList(int supervisorId)
        {
            try
            {
                List<PurchaseOrderDTO> purchaseOrders = new List<PurchaseOrderDTO>();

                purchaseOrders = POService.GetPurchaseOrderListBySupervisor(supervisorId, 1);
                if (purchaseOrders.Count > 0)
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
        /// When user click purchase order, retrieve item list and display in data grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int PONumber = Convert.ToInt32(dgvPO.Rows[e.RowIndex].Cells["Purchase Order #"].Value.ToString());

                _purchaseOrder = POService.GetPurchaseOrderByPONumber(PONumber, null, null);

                if (e.ColumnIndex > -1 && dgvPO.Columns[e.ColumnIndex].Name == "btnClose")
                {
                    if (e.RowIndex >= 0 && dgvPO.Rows[e.RowIndex].Cells["Status"].Value.ToString() == "Closed")
                    {
                        MessageBox.Show("This purchase order is already closed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        ClosePurchaseOrder(GeneratePurchaseOrderObject(_purchaseOrder));                       
                    }
                }
                else
                {                   
                    GenerateItemGridView(PONumber);
                    ClearItemForm();
                }
                   
            }
        }
     

        /// <summary>
        /// When user click specific item, display details in detail area
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    int itemId = Convert.ToInt32(dgvItem.Rows[e.RowIndex].Cells[0].Value.ToString());

                    ItemDTO ItemDTO = itemService.GetItemByItemId(itemId, null, Convert.ToInt32(((MainForm)this.MdiParent).loginInfo.EmployeeId));

                    btnSave.Enabled = true;
                    recordVersion = ItemDTO.RecordVersion;
                    lbItemId.Text = ItemDTO.ItemId.ToString();
                    lbItemName.Text = ItemDTO.ItemName;
                    lbItemDescription.Text = ItemDTO.ItemDescription;
                    lbJustification.Text = ItemDTO.Justification;
                    txtLocation.Text = ItemDTO.Location;
                    txtPrice.Text = ItemDTO.Price.ToString();
                    numQty.Value = ItemDTO.Quantity;
                    lbSubTotal.Text = (ItemDTO.Quantity * ItemDTO.Price).ToString("C");
                    cmbItemStatus.SelectedIndex = ItemDTO.ItemStatusId;
                    txtReason.Text = ItemDTO.DecisionReason;

                    if (itemService.GetItemListByPO(Convert.ToInt32(ItemDTO.PONumber), true).FirstOrDefault().POStatusId == 3)
                    {
                        btnSave.Enabled = false;
                    }
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
                List<PurchaseOrderDTO> purchaseOrders = new List<PurchaseOrderDTO>();

                int supervisorId = Convert.ToInt32(((MainForm)this.MdiParent).loginInfo.EmployeeId);
                DateTime? start = null;
                DateTime? end = dtpEnd.Value.Date;
                int? POStatus = null;
                string empName = null;
                List<string> errorMsg = new List<string>();

                if (dtpEnd.Value.Date > DateTime.Now.Date)
                {
                    errorMsg.Add("End date can not be future");
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

                // If user enter employee name
                if (!string.IsNullOrEmpty(txtEmpName.Text.Trim()))
                    empName = txtEmpName.Text.Trim();

                // If user select po status
                if (cmbPOStatus.SelectedIndex > 0)
                {
                    POStatus = Convert.ToInt32(cmbPOStatus.SelectedValue);
                }

                // if no search criteria error
                if (errorMsg.Count > 0)
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
                    purchaseOrders = POService.GetPurchaseOrderListBySupervisor(supervisorId, POStatus, empName, start, end);

                    if (purchaseOrders.Count > 0)
                    {
                        GeneratePurchaseOrderDataGridView(purchaseOrders);
                        ClearItemForm();
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

        private void btnSearchCriteriaReset_Click(object sender, EventArgs e)
        {
            dtpStart.CustomFormat = " ";
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpEnd.Value = DateTime.Now;
            startDateFlag = false;
            txtEmpName.Text = "";
            cmbPOStatus.SelectedIndex = 0;
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
            }
        }

        /// <summary>
        /// When item saved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbItemId.Text != "")
                {
                    Item item = new Item {
                        ItemId = Convert.ToInt32(lbItemId.Text),
                        ItemName = lbItemName.Text,
                        ItemDescription = lbItemDescription.Text,
                        Justification = lbJustification.Text,
                        Location = txtLocation.Text.Trim(),
                        Price = Convert.ToDecimal(txtPrice.Text.Trim()),
                        Quantity = Convert.ToInt32(numQty.Value),
                        DecisionReason = txtReason.Text.Trim(),
                        PONumber = Convert.ToInt32(_purchaseOrder.PONumber),
                        ItemStatusId = Convert.ToInt32(cmbItemStatus.SelectedValue)
                    };

                    item.RecordVersion = recordVersion;

                    itemService.ApproveOrDenyItem(item, Convert.ToInt32(((MainForm)this.MdiParent).loginInfo.EmployeeId));

                    // if There is error when save item
                    if (item.Errors.Count > 0)
                    {
                        string errorMsg = "";

                        foreach (ValidationError error in item.Errors)
                        {
                            errorMsg += error.Description + Environment.NewLine;
                        }

                        MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // if no error
                    else
                    {
                        MessageBox.Show($"Item Id: {item.ItemId} has been updated successful!");
                        ClearItemForm();

                        PurchaseOrder purchaseOrder = GeneratePurchaseOrderObject(_purchaseOrder);

                        POService.UpdatePurcaseOrder(purchaseOrder);
                        GenerateItemGridView(Convert.ToInt32(_purchaseOrder.PONumber));
                        _purchaseOrder.RecordVersion = item.PORecordVersion;

                        //if all items are not pending, propmpt message to change status of purchase order
                        if(purchaseOrder.items.Where(i => i.ItemStatusId != 1).ToList().Count == purchaseOrder.items.Count()){
                            ClosePurchaseOrder(purchaseOrder);
                        }

                        GetPurchaseOrderList(Convert.ToInt32(((MainForm)this.MdiParent).loginInfo.EmployeeId));
                    }
                }
                else
                {
                    MessageBox.Show("You must select item to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Close form event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessPO_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((MainForm)this.MdiParent).RefreshParent();
        }


        #region Helper methods

        /// <summary>
        /// Generate purchase order status drop down list
        /// </summary>
        private void LoadPOStatus()
        {
            POLookUpsService service = new POLookUpsService();
            List<POStatusLookUpsDTO> poStatus = service.GetPOStatus();

            poStatus.Insert(0, new POStatusLookUpsDTO { POStatusId = 0, POStatus = "All" });
            poStatus.RemoveAt(2);

            cmbPOStatus.DataSource = poStatus;
            cmbPOStatus.DisplayMember = "POStatus";
            cmbPOStatus.ValueMember = "POStatusId";
            cmbPOStatus.SelectedIndex = 0;
        }

        /// <summary>
        /// Generate item status drop down list
        /// </summary>
        private void LoadItemStatus()
        {
            POLookUpsService service = new POLookUpsService();
            List<ItemStatusLookUpsDTO> itemStatus = service.GetItemStatus();

            itemStatus.Insert(0, new ItemStatusLookUpsDTO { ItemStatusId = 0, ItemStatus = "--Select a status--" });

            cmbItemStatus.DataSource = itemStatus;
            cmbItemStatus.DisplayMember = "ItemStatus";
            cmbItemStatus.ValueMember = "ItemStatusId";
            cmbItemStatus.SelectedIndex = 0;
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
            dgvPO.Columns[0].HeaderText = "PO #";
            dgvPO.Columns[0].Name = "Purchase Order #";
            dgvPO.Columns[1].HeaderText = "Submission Date";
            dgvPO.Columns[1].HeaderText = "Submission Date";
            dgvPO.Columns[2].HeaderText = "Sub Total";
            dgvPO.Columns[3].HeaderText = "Tax";
            dgvPO.Columns[4].HeaderText = "Total";
            dgvPO.Columns[5].Visible = false; //Employee Id
            dgvPO.Columns[6].HeaderText = "Employee";
            dgvPO.Columns[7].HeaderText = "Supervisor";
            dgvPO.Columns[8].HeaderText = "Status";
            dgvPO.Columns[8].Name = "Status";

            dgvPO.Columns[2].DefaultCellStyle.Format = "C";
            dgvPO.Columns[3].DefaultCellStyle.Format = "C";
            dgvPO.Columns[4].DefaultCellStyle.Format = "C";

            // don't need to show record version
            dgvPO.Columns[9].Visible = false;

            dgvPO.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvPO.AutoResizeColumns();

            if (((MainForm)this.MdiParent).loginInfo.IsHeadSupervisor || Convert.ToInt32(((MainForm)this.MdiParent).loginInfo.DepartmentId) == 1)
            {
                DataGridViewButtonColumn dgvBtnClose = new DataGridViewButtonColumn();
                dgvBtnClose.HeaderText = "Close";
                dgvBtnClose.Text = "Close";
                dgvBtnClose.Name = "btnClose";
                dgvPO.Columns.Insert(dgvPO.Columns.Count, dgvBtnClose);
                dgvBtnClose.UseColumnTextForButtonValue = true;
            }           
        }

        /// <summary>
        /// Generate items in data grid view
        /// </summary>
        /// <param name="PONumber"></param>
        private void GenerateItemGridView(int PONumber)
        {
            dgvItem.DataSource = null;

            dgvItem.DataSource = itemService.GetItemListByPO(PONumber, true);
            dgvItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvItem.Columns[0].HeaderText = "Item ID";
            dgvItem.Columns[1].HeaderText = "Item Name";
            dgvItem.Columns[2].HeaderText = "Description";
            dgvItem.Columns[2].Visible = false; //Description -> item details
            dgvItem.Columns[3].HeaderText = "Justificaton";
            dgvItem.Columns[3].Visible = false; //Justificaton -> item details
            dgvItem.Columns[4].HeaderText = "Location";
            dgvItem.Columns[4].Visible = false; //Location -> item details
            dgvItem.Columns[5].HeaderText = "Price";
            dgvItem.Columns[6].HeaderText = "Qty";
            dgvItem.Columns[7].Visible = false; //Status Id
            dgvItem.Columns[8].Visible = false; //PO Number
            dgvItem.Columns[9].Visible = false; //PO Status
            dgvItem.Columns[10].HeaderText = "Status";
            dgvItem.Columns[11].HeaderText = "Decision Reason";
            dgvItem.Columns[12].Visible = false; // Record version                    

            dgvItem.Columns[5].DefaultCellStyle.Format = "C";
            dgvItem.AutoResizeColumns();
        }


        /// <summary>
        /// Send final decision notification to employee
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="purchaseOrder"></param>
        private void SendEmail(EmployeeDTO emp, PurchaseOrder purchaseOrder)
        {
            EmailService emailService = new EmailService();

            PurchaseOrderDTO purchaseOrderDTO = POService.GetPurchaseOrderByPONumber(Convert.ToInt32(purchaseOrder.PONumber), emp.EmpId, null);
            purchaseOrderDTO.items = itemService.GetItemListByPO(Convert.ToInt32(purchaseOrderDTO.PONumber), false);

            Email email = new Email();

            email.mailTo = emp.Email;
            email.mailFrom = "Admin@VastVoyages.ca";
            email.subject = "Purchase order final decision notification";
            email.body = $"<h2>Your purchase order is closed.</h2>" +
                          $"<p>Purchase Order Number: {purchaseOrderDTO.PONumber}</p>" +
                          $"<p>Submission Date: {purchaseOrderDTO.SubmissionDate}</p>" +
                          $"<p>Total cost: {purchaseOrderDTO.Total.ToString("C")}</p>" +
                          "<hr><table style='border:1px solid #dddddd; border-collapse:collapse; width: 60%;'><tr><th style='border:1px solid #dddddd; text-align:left;'>Item Name</th><th style='text-align:left;'>Item Status</th></tr>";

            foreach (ItemDTO item in purchaseOrderDTO.items)
            {
                email.body += $"<tr><td style='border:1px solid #dddddd;'> {item.ItemName}</td>" +
                        $"<td style='border:1px solid #dddddd;'>{item.ItemStatus}</td></tr>";
            }

            email.body += $"</table>";

            emailService.SendNotificationEmail(email);

            MessageBox.Show("Notification Email sent successful!");
        }


        /// <summary>
        /// Close selected purchase order
        /// </summary>
        /// <param name="purchaseOrder"></param>
        private void ClosePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to close this purchase order?", "Close Purchase Order", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    purchaseOrder.POstatusId = 3;
                    POService.UpdatePurcaseOrder(purchaseOrder);

                    if (purchaseOrder.Errors.Count > 0)
                    {
                        string errorMsg = "";

                        foreach (ValidationError error in purchaseOrder.Errors)
                        {
                            errorMsg += error.Description + Environment.NewLine;
                        }

                        MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        EmployeeService employeeService = new EmployeeService();
                        EmployeeDTO emp = employeeService.GetAllEmployees().Where(employee => employee.EmpId == purchaseOrder.employeeId).FirstOrDefault();

                        SendEmail(emp, purchaseOrder);
                        GetPurchaseOrderList(Convert.ToInt32(((MainForm)this.MdiParent).loginInfo.EmployeeId));
                        dgvItem.DataSource = null;
                        ClearItemForm();
                    }
                }
                else
                {
                    purchaseOrder.POstatusId = 2;
                    POService.UpdatePurcaseOrder(purchaseOrder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Generate purchase order object using by DTO object(for update)
        /// </summary>
        /// <param name="_purchaseOrder"></param>
        /// <returns></returns>
        private PurchaseOrder GeneratePurchaseOrderObject(PurchaseOrderDTO _purchaseOrder)
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder();

            purchaseOrder.PONumber = _purchaseOrder.PONumber;
            purchaseOrder.SubmissionDate = _purchaseOrder.SubmissionDate;
            purchaseOrder.RecordVersion = _purchaseOrder.RecordVersion;
            purchaseOrder.employeeId = _purchaseOrder.EmployeeId;
            purchaseOrder.items = new List<Item>();


            List<ItemDTO> items = itemService.GetItemListByPO(Convert.ToInt32(_purchaseOrder.PONumber), true);

            foreach (ItemDTO i in items)
            {
                purchaseOrder.items.Add(new Item
                {
                    ItemId = i.ItemId,
                    ItemName = i.ItemName,
                    ItemDescription = i.ItemDescription,
                    Justification = i.Justification,
                    Location = i.Location,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    DecisionReason = i.DecisionReason,
                    ItemStatusId = i.ItemStatusId,
                    RecordVersion = i.RecordVersion
                });
            }

            return purchaseOrder;
        }

        /// <summary>
        /// Clear item details form
        /// </summary>
        private void ClearItemForm()
        {
            lbItemId.Text = "";
            lbItemName.Text = "";
            lbItemDescription.Text = "";
            lbJustification.Text = "";
            txtLocation.Text = "";
            txtPrice.Text = "";
            numQty.Value = 0;
            lbSubTotal.Text = "";
            cmbItemStatus.SelectedIndex = 0;
            txtReason.Text = "";
        }
        #endregion

    }
}
