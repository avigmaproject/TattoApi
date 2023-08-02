using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TattoAPI.Models.Avigma
{
    public class User_Address_Master_DTO
    {
        public Int64 UAM_PkeyID { get; set; }
        public Int64? UAM_User_PkeyID { get; set; }
        public String UAM_Company_Name { get; set; }
        public String UAM_Shipping_Address1 { get; set; }
        public String UAM_Shipping_Address2 { get; set; }
        public String UAM_Shipping_Pincode { get; set; }
        public String UAM_Shipping_City { get; set; }
        public String UAM_Shipping_State { get; set; }
        public String UAM_Shipping_Country { get; set; }
        public String UAM_Lat { get; set; }
        public String UAM_Long { get; set; }
        public String UAM_Billing_Address1 { get; set; }
        public String UAM_Billing_Address2 { get; set; }
        public String UAM_Billing_Pincode { get; set; }
        public String UAM_Billing_City { get; set; }
        public String UAM_Billing_State { get; set; }
        public String UAM_Billing_Country { get; set; }
        public Boolean? UAM_IsActive { get; set; }
        public Boolean? UAM_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64? UserID { get; set; }

    }
}