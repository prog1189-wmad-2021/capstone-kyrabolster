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
        internal bool edit;

        public frmViewPO()
        {
            InitializeComponent();
        }

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

                LoadStatus();
                GetPurchaseOrderList(Convert.ToInt32(((MainForm)this.MdiParent).loginInfo.EmployeeId));
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GetPurchaseOrderList(int employeeId)
        {
            try
            {
                PurchaseOrderService service = new PurchaseOrderService();
                List<PurchaseOrderDTO> purchaseOrders = new List<PurchaseOrderDTO>();

                purchaseOrders = service.GetPurchaseOrderList(employeeId);

                dgvPO.DataSource = purchaseOrders;
                dgvPO.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgvPO.Columns[0].HeaderText = "Purchase Order #";
                dgvPO.Columns[1].HeaderText = "Submission Date";
                dgvPO.Columns[2].HeaderText = "Sub Total";
                dgvPO.Columns[3].HeaderText = "Tax";
                dgvPO.Columns[4].HeaderText = "Total";
                dgvPO.Columns[5].HeaderText = "Employee";
                dgvPO.Columns[6].HeaderText = "Supervisor";
                dgvPO.Columns[7].HeaderText = "Status";

                dgvPO.Columns[2].DefaultCellStyle.Format = "C";
                dgvPO.Columns[3].DefaultCellStyle.Format = "C";
                dgvPO.Columns[4].DefaultCellStyle.Format = "C";

                // don't need to show employee's name and supervisor's name in this form
                dgvPO.Columns[5].Visible = false;
                dgvPO.Columns[6].Visible = false;
                dgvPO.Columns[8].Visible = false;

                dgvPO.AutoResizeColumns();

                DataGridViewButtonColumn dgvBtnDetail = new DataGridViewButtonColumn();
                dgvPO.Columns.Insert(dgvPO.Columns.Count, dgvBtnDetail);
                dgvBtnDetail.HeaderText = "Edit";
                dgvBtnDetail.Text = "Edit";
                dgvBtnDetail.Name = "btnEdit";
                dgvBtnDetail.UseColumnTextForButtonValue = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewPO_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((MainForm)this.MdiParent).RefreshParent();
        }

        private void dgvPO_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int PONumber = Convert.ToInt32(dgvPO.Rows[e.RowIndex].Cells[1].Value.ToString());

                if (e.ColumnIndex > -1 && dgvPO.Columns[e.ColumnIndex].Name == "btnEdit")
                {
                    // only pending PO can be edited
                    if (e.RowIndex >= 0 && dgvPO.Rows[e.RowIndex].Cells[8].Value.ToString() == "Pending")
                    {
                        PurchaseOrderService service = new PurchaseOrderService();

                        PurchaseOrderDTO purchaseOrderDTO = service.GetPurchaseOrderByPONumber(PONumber, null);

                        frmCreatePO createForm = new frmCreatePO();
                        
                        createForm.edit = true;
                        
                        createForm._purchaseOrder = new PurchaseOrder{
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
                else
                {
                    ItemService service = new ItemService();

                    dgvItem.DataSource = service.GetItemList(PONumber);
                    dgvItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    dgvItem.Columns[0].HeaderText = "Item ID";
                    dgvItem.Columns[1].HeaderText = "Item Name";
                    dgvItem.Columns[2].HeaderText = "Description";
                    dgvItem.Columns[3].HeaderText = "Justificaton";
                    dgvItem.Columns[4].HeaderText = "Location";
                    dgvItem.Columns[5].HeaderText = "Price";
                    dgvItem.Columns[6].HeaderText = "Qty";
                    dgvItem.Columns[7].Visible = false; //Status Id
                    dgvItem.Columns[8].Visible = false; //PO Number
                    dgvItem.Columns[9].HeaderText = "Status";
                    dgvItem.Columns[10].HeaderText = "Decision Reason";
                    dgvItem.Columns[11].Visible = false; // Record version                    

                    dgvItem.Columns[5].DefaultCellStyle.Format = "C";
                    dgvItem.AutoResizeColumns();
                }
            }
        }

        private void LoadStatus()
        {
            POLookUpsService service = new POLookUpsService();
            List<ItemStatusLookUpsDTO> itemStatus = service.GetItemStatus();

            itemStatus.Insert(0, new ItemStatusLookUpsDTO { ItemStatusId = 0, ItemStatus = "--Select a status--" });

            cmbPOStatus.DataSource = itemStatus;
            cmbPOStatus.DisplayMember = "ItemStatus";
            cmbPOStatus.ValueMember = "ItemStatusId";
            cmbPOStatus.SelectedIndex = 0;
        }

    }
}
