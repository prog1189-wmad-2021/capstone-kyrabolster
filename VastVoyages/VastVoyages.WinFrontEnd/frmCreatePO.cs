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
        private PurchaseOrder _purchaseOrder;
        private decimal _subTotal = 0;
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
            lbEmpName.Text = ((MainForm)this.MdiParent).loginInfo.FullName;
            lbUserName.Text = ((MainForm)this.MdiParent).loginInfo.UserName;
            lbJob.Text = ((MainForm)this.MdiParent).loginInfo.Job;
            lbDepartment.Text = ((MainForm)this.MdiParent).loginInfo.Department;
            lbSupervisor.Text = ((MainForm)this.MdiParent).loginInfo.Supervisor;
            lbCurrentDate.Text = DateTime.Now.ToShortDateString();
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
                PurchaseOrderService POservice = new PurchaseOrderService();
                ItemService itemService = new ItemService();
                Item item = PopulateItemObject();

                const decimal TAX_RATE = 0.15m;

                if(item.Errors.Count == 0)
                {
                    // When first item added, insert new purchase order
                    if (dgvItem.Rows.Count == 0)
                    {
                        _purchaseOrder = new PurchaseOrder();
                        _purchaseOrder.employeeId = Convert.ToInt32(((MainForm)this.MdiParent).loginInfo.EmployeeId);
                        _purchaseOrder = POservice.InsertPurchaseOrder(_purchaseOrder, item);
                        if (item.Errors.Count == 0)
                        {
                            lbPONumber.Text = _purchaseOrder.PONumber;
                        }
                        else
                        {
                            ItemValidationErrorMsg(item);
                        }
                    }

                    // If there is already item, insert new item to item table
                    else
                    {
                        itemService.InsertItem(item, _purchaseOrder);
                    }

                    // Refresh data grive view

                    dgvItem.DataSource = null;
                    List<ItemDTO> items = itemService.GetItemList(Convert.ToInt32(_purchaseOrder.PONumber));
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
                    dgvItem.Columns[8].HeaderText = "Status";
                    dgvItem.Columns[9].Visible = false; //Decision Reason

                    dgvItem.Columns[5].DefaultCellStyle.Format = "C";
                    dgvItem.AutoResizeColumns();

                    _subTotal = items.Sum(i => i.Price * i.Quantity);
                    lbSubTotal.Text = _subTotal.ToString("C");
                    lbTax.Text = (_subTotal * TAX_RATE).ToString("C");
                    lbTotal.Text = (_subTotal * (1 + TAX_RATE)).ToString("C");

                    ClearForm();
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
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                PurchaseOrderService POservice = new PurchaseOrderService();

                _purchaseOrder.SubTotal = _subTotal;
                _purchaseOrder = POservice.UpdatePurcaseOrder(_purchaseOrder);

                if(_purchaseOrder.Errors.Count > 0)
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
                    _purchaseOrder = new PurchaseOrder();
                    dgvItem.DataSource = null;
                    lbSubTotal.Text = "";
                    lbTax.Text = "";
                    lbTotal.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                ItemName = txtItemName.Text,
                ItemDescription = txtDescription.Text,
                Justification = txtJustification.Text,
                Location = txtLocation.Text,
                Price = txtPrice.Text != "" ? Convert.ToDecimal(txtPrice.Text) : 0,
                Quantity = Convert.ToInt32(numQty.Value)
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
        /// Clear form after add item
        /// </summary>
        private void ClearForm()
        {
            txtItemName.Text = "";
            txtDescription.Text = "";
            txtJustification.Text = "";
            txtLocation.Text = "";
            txtPrice.Text = "";
            numQty.Value = 0;
        }

    }
}
