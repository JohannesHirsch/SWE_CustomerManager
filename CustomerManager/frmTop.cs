using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using customerDLL;
using System.Diagnostics;

namespace CustomerManager
{
    public partial class FrmTop : Form
    {
        #region MemberVariables
        CSV csv;
        BindingSource bind;
        bool isDescending;
        string path = "Data/data.csv";
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new frmTop form which is the main form of the app and displays the cumstomers in a DataGridView
        /// </summary>
        public FrmTop()
        {
            InitializeComponent();
            csv = new CSV(path);
            bind = new BindingSource();
            bind.DataSource = csv.Customers;
            dgvCustomers.DataSource = bind;

            this.isDescending = false;
        }
        #endregion

        #region Events
        private void btnAdd_Click(object sender, EventArgs e)
        {

            FrmAdd add = new FrmAdd(csv.Customers.Count, this.csv.Customers);
              
            if (add.ShowDialog() == DialogResult.OK)
            {

                csv.Customers.Add(add.CNew);

                csv.WriteLastCustomerCSV();

                bind = new BindingSource();
                bind.DataSource = csv.Customers;

                dgvCustomers.DataSource = bind;
                dgvCustomers.Update();
                dgvCustomers.Show();
            }
        }

        private void frmTop_Load(object sender, EventArgs e)
        {
            dgvCustomers.Update();
            dgvCustomers.Show();
        }

        private void dgvCustomers_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            List<Customer> SortedList = this.SortCustomers(e.ColumnIndex,this.isDescending);
            this.isDescending = !this.isDescending;

            bind.DataSource = SortedList;
            dgvCustomers.Update();
            dgvCustomers.Show();
        }

        /// <summary>
        /// When the User double-clicks a cell the FrmEdit-Dialog opens, that allows the user to modify a Customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)//Beim Sortieren ist Index -1 (Header)
            {

                int idEdit = Convert.ToInt32(dgvCustomers.Rows[e.RowIndex].Cells[0].Value);

                FrmEdit add = new FrmEdit(csv.Customers[idEdit], this.csv.Customers);

                if (add.ShowDialog() == DialogResult.OK)
                {
                    csv.Customers[idEdit] = add.CNew;
                    csv.WriteCSV();
                    csv = new CSV(path);
                    bind.DataSource = csv.Customers;
                    dgvCustomers.DataSource = bind;
                    dgvCustomers.Update();
                    dgvCustomers.Show();
                }                
            }
        }

        private void chbxFilter_CheckedChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        private void tbxFilter_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }
        #endregion

        #region Membermethods

        /// <summary>
        /// This method does the filtering. If the Filter Checkbox is checked, this method looks if the Strings 
        /// (Firstname, Lastname and Email) contains the Word in the Filter Textbox
        /// </summary>
        private void Filter()
        {
            List<Customer> FilteredList = new List<Customer>();
            string text;

            if (chbxFilter.Checked)
            {
                text = tbxFilter.Text;

                for (int i = 0; i < this.csv.Customers.Count; i++)
                {
                    if (this.csv.Customers[i].FirstName.Contains(text))
                    {
                        FilteredList.Add(this.csv.Customers[i]);
                    }
                    else if (this.csv.Customers[i].LastName.Contains(text))
                    {
                        FilteredList.Add(this.csv.Customers[i]);
                    }
                    else if (this.csv.Customers[i].Email.Contains(text))
                    {
                        FilteredList.Add(this.csv.Customers[i]);
                    }
                }

                bind.DataSource = FilteredList;
                dgvCustomers.Update();
                dgvCustomers.Show();
            }
            else
            {
                bind.DataSource = this.csv.Customers;
                dgvCustomers.Update();
                dgvCustomers.Show();
            }
        }

        /// <summary>
        /// This method Sorts the List of Customers. The attribbute which has to be sorted
        /// is selexted by the index value. The List can be sorted ascending or descending.
        /// </summary>
        /// <param name="index">The index of the selected Column Header</param>
        /// <param name="descending"></param>
        /// <returns>The Sorted List of Customers</returns>
        private List<Customer> SortCustomers(int index, bool descending)
        {
            List<Customer> SortedList = null;

            switch (index)
            {
                case 0:

                    if (descending)
                    {
                        SortedList = this.csv.Customers.OrderBy(o => o.ID).ToList();
                    }
                    else
                    {
                        SortedList = this.csv.Customers.OrderByDescending(o => o.ID).ToList();
                    }

                    break;
                case 1:
                    if (descending)
                    {
                        SortedList = this.csv.Customers.OrderBy(o => o.FirstName).ToList();
                    }
                    else
                    {
                        SortedList = this.csv.Customers.OrderByDescending(o => o.FirstName).ToList();
                    }

                    break;
                case 2:

                    if (descending)
                    {
                        SortedList = this.csv.Customers.OrderBy(o => o.LastName).ToList();
                    }
                    else
                    {
                        SortedList = this.csv.Customers.OrderByDescending(o => o.LastName).ToList();
                    }

                    break;
                case 3:

                    if (descending)
                    {
                        SortedList = this.csv.Customers.OrderBy(o => o.Email).ToList();
                    }
                    else
                    {
                        SortedList = this.csv.Customers.OrderByDescending(o => o.Email).ToList();
                    }

                    break;
                case 4:

                    if (descending)
                    {
                        SortedList = this.csv.Customers.OrderBy(o => o.Balance).ToList();
                    }
                    else
                    {
                        SortedList = this.csv.Customers.OrderByDescending(o => o.Balance).ToList();
                    }

                    break;
                case 5:

                    if (descending)
                    {
                        SortedList = this.csv.Customers.OrderBy(o => o.LastChange).ToList();
                    }
                    else
                    {
                        SortedList = this.csv.Customers.OrderByDescending(o => o.LastChange).ToList();
                    }

                    break;
                default:
                    throw new IndexOutOfRangeException("Ausgewählte Spalte nicht zulässig (Hofer)");

            }
            return SortedList;
        }
        #endregion
    }
}
