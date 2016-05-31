using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerManager
{
    public partial class FrmLogin : Form
    {

        #region Constructor
        public FrmLogin()
        {
            InitializeComponent();
            this.btnOK.Focus();
        }
        #endregion

        #region Events
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (tbxPassword.Text == "test")
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                tbxPassword.Clear();
            }
        }
        #endregion                
    }
}
