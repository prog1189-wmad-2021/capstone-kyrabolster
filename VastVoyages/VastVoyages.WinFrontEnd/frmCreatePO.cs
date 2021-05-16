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
    public partial class frmCreatePO : Form
    {
        ItemService itemService = new ItemService();
        PurchaseOrderService POservice = new PurchaseOrderService();

        internal PurchaseOrder _purchaseOrder;
        internal bool edit = false;
        private decimal _subTotal = 0;
        const decimal TAX_RATE = 0.15m;
        private byte[] recordVersion;

        public frmCreatePO()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load child form with data from parent form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatePO_Load(object sender, EventArgs e)
        {
            LoadLoginInfo();

            if (_purchaseOrder == null)
            {
                _purchaseOrder = new PurchaseOrder();
                _purchaseOrder.items = new List<Item>();
            }

            if(edit)
            {
                this.Text = "Edit Purchase Order";
                btnSave.Visible = true;
                btnSave.Enabled = false;
                lbItemIDValue.Visible = true;
                GenerateItemDataGridView(_purchaseOrder.PONumber);
                lbPONumber.Text = _purchaseOrder.PONumber;
                if(_purchaseOrder.SubmissionDate != null)
                {
                    btnSubmit.Visible = false;
                }
            }
        }

        /// <summary>
        /// Form close and load praent form again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatePO_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((MainForm)this.MdiParent).RefreshParent();
        }

        /// <summary>
        /// Add item to purchas order.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                Item item = PopulateItemObject();                

                if(item.Errors.Count == 0)
                {
                    // When first item added, insert new purchase order
                    if (dgvItem.Rows.Count == 0)
                    {
                        _purchaseOrder = new PurchaseOrder();
                        _purchaseOrder.employeeId = Convert.ToInt32(((MainForm)this.MdiParent).loginInfo.EmployeeId);
                        _purchaseOrder = POservice.AddPurchaseOrder(_purchaseOrder, item);
                        if (item.Errors.Count == 0)
                        {
                            lbPONumber.Text = _purchaseOrder.PONumber;
                            ClearForm();
                        }
                        else
                        {
                            ItemValidationErrorMsg(item);
                            return;
                        }
                    }

                    // If there is already item, insert new item to item table
                    else
                    {
                        item = itemService.AddItem(item, _purchaseOrder);

                        if(item.Errors.Count > 0)
                        {
                            string errorMsg = "";

                            foreach (ValidationError error in item.Errors)
                            {
                                errorMsg += error.Description + Environment.NewLine;
                            }

                            MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            _purchaseOrder.RecordVersion = item.PORecordVersion;
                            ClearForm();
                        }
                    }

                    // Refresh data grive view
                    GenerateItemDataGridView(_purchaseOrder.PONumber);                    
                }

                else
                {
                    ItemValidationErrorMsg(item);
                }              

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Final submit button. Update purchase order
        /// If the user didn't submit when they create purchase order first, they can submit in modify PO page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (edit)
                {
                    if (_purchaseOrder.items == null)
                    {
                        _purchaseOrder.items = new List<Item>();
                    }

                    List<ItemDTO> items = itemService.GetItemListByPO(Convert.ToInt32(_purchaseOrder.PONumber), false);

                    foreach (ItemDTO i in items)
                    {
                        _purchaseOrder.items.Add(new Item
                        {
                            ItemId = i.ItemId,
                            ItemName = i.ItemName,
                            ItemDescription = i.ItemDescription,
                            Justification = i.Justification,
                            Location = i.Location,
                            Price = i.Price,
                            Quantity = i.Quantity,
                            ItemStatusId = i.ItemStatusId
                        });
                    }
                }                 

                _purchaseOrder = POservice.UpdatePurcaseOrder(_purchaseOrder);

                if (_purchaseOrder.Errors.Count > 0)
                {
                    string errorMsg = "";

                    foreach (ValidationError error in _purchaseOrder.Errors)
                    {
                        errorMsg += error.Description + Environment.NewLine;
                    }

                    MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Purchase order has been submitted successful!");
                    ((MainForm)this.MdiParent).StatusLabel.Text = "Purchase order create Successful!";
                    dgvItem.DataSource = null;
                    lbSubTotal.Text = "";
                    lbTax.Text = "";
                    lbTotal.Text = "";
                    lbPONumber.Text = "";

                    if(edit)
                    {
                        this.Close();
                    }
                }

                _purchaseOrder = new PurchaseOrder();
                _purchaseOrder.items = new List<Item>();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Save modified item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbItemIDValue.Text != "")
                {
                    Item item = PopulateItemObject();
                    item.ItemId = Convert.ToInt32(lbItemIDValue.Text);
                    item.PONumber = Convert.ToInt32(_purchaseOrder.PONumber);
                    item.RecordVersion = recordVersion;
                    item.PORecordVersion = _purchaseOrder.RecordVersion;

                    item = itemService.UpdateItem(item, chkNoNeed.Checked);

                    if (item.Errors.Count > 0)
                    {
                        string errorMsg = "";

                        foreach (ValidationError error in item.Errors)
                        {
                            errorMsg += error.Description + Environment.NewLine;
                        }

                        MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    else
                    {
                        MessageBox.Show($"Item Id: {lbItemIDValue.Text} has been updated successful!");
                        ((MainForm)this.MdiParent).StatusLabel.Text = $"Item Id: {lbItemIDValue.Text} has been updated!";
                        _purchaseOrder.RecordVersion = item.PORecordVersion;
                        ClearForm();

                        List<ItemDTO> items = itemService.GetItemListByPO(Convert.ToInt32(_purchaseOrder.PONumber), false);

                        if (items.Where(i => i.ItemStatusId != 1).ToList().Count == items.Count)
                        {
                            _purchaseOrder.items = new List<Item>();
                            
                            foreach (ItemDTO i in items)
                            {
                                _purchaseOrder.items.Add(new Item
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
                            POservice.UpdatePurcaseOrder(_purchaseOrder);
                        }

                        GenerateItemDataGridView(_purchaseOrder.PONumber);
                        btnAddItem.Enabled = true;
                        _purchaseOrder.RecordVersion = item.PORecordVersion;
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
        /// Retrive item informatin when a user click cell in item list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (edit)
            {
                try
                {
                    if (e.RowIndex > -1 && e.ColumnIndex > -1)
                    {
                        ClearForm();
                        lbItemID.Visible = true;
                        btnSave.Enabled = true;
                        btnAddItem.Enabled = false;
                        chkNoNeed.Visible = true;
                        int itemId = Convert.ToInt32(dgvItem.Rows[e.RowIndex].Cells[0].Value.ToString());

                        ItemDTO ItemDTO = itemService.GetItemByItemId(itemId, Convert.ToInt32(((MainForm)this.MdiParent).loginInfo.EmployeeId), null);

                        recordVersion = ItemDTO.RecordVersion;
                        lbItemIDValue.Text = ItemDTO.ItemId.ToString();
                        txtItemName.Text = ItemDTO.ItemName;
                        txtDescription.Text = ItemDTO.ItemDescription;
                        txtJustification.Text = ItemDTO.Justification;
                        txtLocation.Text = ItemDTO.Location;
                        txtPrice.Text = ItemDTO.Price.ToString();
                        numQty.Value = ItemDTO.Quantity;
                        
                        if (ItemDTO.ItemStatus != "Pending")
                        {
                            foreach (Control ctl in this.grpItemDetails.Controls)
                            {
                                if (ctl is TextBox)
                                {
                                    ctl.Enabled = false;
                                }
                            }
                            chkNoNeed.Visible = false;
                            numQty.Enabled = false;
                            chkNoNeed.Enabled = false;
                            btnSave.Enabled = false;
                            btnAddItem.Enabled = false;
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }


        /// <summary>
        /// Generate item object from form
        /// </summary>
        /// <returns></returns>
        private Item PopulateItemObject()
        {
            return new Item()
            {
                ItemName = txtItemName.Text.Trim(),
                ItemDescription = txtDescription.Text.Trim(),
                Justification = txtJustification.Text.Trim(),
                Location = txtLocation.Text.Trim(),
                Price = txtPrice.Text == "" || !decimal.TryParse(txtPrice.Text, out decimal priceParam) ? 0 : Convert.ToDecimal(txtPrice.Text.Trim()),
                Quantity = Convert.ToInt32(numQty.Value),
                ItemStatusId = 1,
                DecisionReason = "",
                PONumber = _purchaseOrder == null ? 0 : Convert.ToInt32(_purchaseOrder.PONumber),
                PORecordVersion = _purchaseOrder == null ? null : _purchaseOrder.RecordVersion
            };
        }


        /// <summary>
        /// Display error message from item entity
        /// </summary>
        /// <param name="item"></param>
        private void ItemValidationErrorMsg(Item item)
        {
            string errorMsg = "";

            foreach (ValidationError error in item.Errors)
            {
                errorMsg += error.Description + Environment.NewLine;
            }

            MessageBox.Show(errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Generate Item grid view by Purchase order number
        /// </summary>
        /// <param name="PONumber"></param>
        private void GenerateItemDataGridView(string PONumber)
        {
            dgvItem.DataSource = null;
            List<ItemDTO> items = itemService.GetItemListByPO(Convert.ToInt32(PONumber), false);
            dgvItem.DataSource = items;

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
            dgvItem.Columns[13].Visible = false; // Record version        
            dgvItem.Columns[14].Visible = false; // Record version        
            dgvItem.Columns[15].Visible = false; // Record version        

            dgvItem.Columns[5].DefaultCellStyle.Format = "C";
            dgvItem.AutoResizeColumns();

            _subTotal = items.Sum(i => i.Price * i.Quantity);
            lbSubTotal.Text = _subTotal.ToString("C");
            lbTax.Text = (_subTotal * TAX_RATE).ToString("C");
            lbTotal.Text = (_subTotal * (1 + TAX_RATE)).ToString("C");

        }

        /// <summary>
        /// Clear form after add item
        /// </summary>
        private void ClearForm()
        {
            foreach (Control ctl in this.grpItemDetails.Controls)
            {
                if(ctl is TextBox)
                {
                    ctl.ResetText();
                    ctl.Enabled = true;
                }
            }

            chkNoNeed.Checked = false;
            chkNoNeed.Visible = false;
            lbItemID.Visible = false;
            lbItemIDValue.Text = "";
            numQty.Value = 0;
            numQty.Enabled = true;
            chkNoNeed.Enabled = true;
            btnSave.Enabled = false;            
            btnAddItem.Enabled = true;
        }

        /// <summary>
        /// Load user information
        /// </summary>
        private void LoadLoginInfo()
        {
            lbEmpName.Text = ((MainForm)this.MdiParent).loginInfo.FullName;
            lbUserName.Text = ((MainForm)this.MdiParent).loginInfo.UserName;
            lbJob.Text = ((MainForm)this.MdiParent).loginInfo.Job;
            lbDepartment.Text = ((MainForm)this.MdiParent).loginInfo.Department;
            lbSupervisor.Text = ((MainForm)this.MdiParent).loginInfo.Supervisor;
            lbCurrentDate.Text = DateTime.Now.ToShortDateString();
        }

        /// <summary>
        /// Reset all form data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

    }
}
