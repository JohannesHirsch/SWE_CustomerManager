using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using customerDLL;

namespace CustomerManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool noFile = false;        //If there is no CSV file, an Exeption is raised, the file is created and the Dialog is started again.
            do
            {
                try
                {
                    if (!noFile)
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        FrmLogin login = new FrmLogin();

                        if (login.ShowDialog() == DialogResult.OK)
                        {
                            Application.Run(new FrmTop());
                        }
                    }
                    else
                    {
                        Application.Run(new FrmTop());
                        noFile = false;
                    }
                }
                catch (FileNotFoundException)
                {
                    noFile = true;
                    CSV.CreateCSV("Data/data.csv");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            } while (noFile);
        }
    }
}
