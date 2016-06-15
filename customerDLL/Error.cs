using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace customerDLL
{
    public struct Error
    {
        public int Code { get; set; }
        //public enum Message { OK, Test1, Test2, Test3 }
        //public Message msg { get; set; }
        // Message, Wargning....

        public static string GetErrorMessage(int code)
        {
            string msg = "";

            switch (code)
            {
                case 0: msg = "Kein Error"; break;
                case 1: msg = "First name not OK"; break;
                case 2: msg = "Last Name not OK";break;
                case 3: msg = "Email not OK"; break;
                case 4: msg = "Blank Textbox"; break;
                case 5: msg = "Email not unique"; break;
                default: msg = "Error code not handled"; break;
            }



            return msg;
        }
    }
}
