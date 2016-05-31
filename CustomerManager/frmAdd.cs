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
    public partial class FrmAdd : FrmUser
    {
        #region MemberVariables
        private int id;
        private Customer cNew = null;
        private List<Customer> customers;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new frmAdd form.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customers"></param>
        public FrmAdd(int id, List<Customer> customers)
        {
            InitializeComponent();
            this.id = id;
            this.customers = customers;
            this.cNew = null;            
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

            if (this.tbxFirstName.Text != "" && this.tbxLastName.Text != "" && this.tbxEmail.Text != "")
            {
                cNew = new Customer(this.id, this.tbxFirstName.Text, this.tbxLastName.Text, this.tbxEmail.Text, out error);
            }
            else
            {
                error.Code = 4;
            }


            if (error.Code == 0 && !(Customer.IsEmailUnique(CNew, this.customers)))
            {
                error.Code = 5;
            }


            if (error.Code != 0)
            {
                MessageBox.Show(error.Code.ToString());
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
