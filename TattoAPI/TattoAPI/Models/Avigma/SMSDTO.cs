using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

 namespace TattoAPI.Models.Avigma
{
    public class SMSDTO
    {
        public String Message { get; set; }

        public String MobileNo { get; set; }

    }


    public class SMSval
    {
     
        public String MobileNo { get; set; }

        public String UserName { get; set; }

        public String OTP { get; set; }
        public String password { get; set; }

    }
}