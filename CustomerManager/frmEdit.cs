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

namespace CustomerManager
{
    public partial class FrmEdit : FrmUser
    {

        #region MemberVariables
        private Customer cOld = null;
        private Customer cNew = null;
        private List<Customer> customers;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new frmEdit form.
        /// </summary>
        /// <param name="cOld"></param>
        /// <param name="customers"></param>
        public FrmEdit(Customer cOld, List<Customer> customers)
        {
            InitializeComponent();

            this.cOld = cOld;
            this.customers = customers;
            this.tbxEmail.Text = cOld.Email;
            this.tbxFirstName.Text = cOld.FirstName;
            this.tbxLastName.Text = cOld.LastName;
        }

        #endregion

        #region Properties

        public Customer CNew
        {
            get { return this.cNew; }
        }

        #endregion

        #region Events

        private void btnOK_Click(object sender, EventArgs e)
        {
            Error error = new Error();

            if (this.tbxLastName.Text != "" && this.tbxEmail.Text != "")
            {
                cNew = new Customer(this.cOld.ID, this.cOld.FirstName, this.tbxLastName.Text, this.tbxEmail.Text, cOld.Balance, cOld.LastChange, out error);
                cNew.Addamount(Convert.ToDouble(this.nudAmount.Value));

                if (error.Code == 0 && !(Customer.IsEmailUnique(CNew, this.customers)))
                {
                    error.Code = 5;
                }

                if (error.Code == 0 &&
                    this.cNew.LastName != this.cOld.LastName ||
                    this.cNew.Email != this.cOld.Email ||
                    this.cNew.Balance != this.cOld.Balance)
                {
                    this.cNew.LastChange = DateTime.Now;
                }
            }
            else
            {
                error.Code = 4;
            }

            if (error.Code != 0)
            {
                MessageBox.Show(Error.GetErrorMessage(error.Code));
                DialogResult = DialogResult.None;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        #endregion

    }
}
