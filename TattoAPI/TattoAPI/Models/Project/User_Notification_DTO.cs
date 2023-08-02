using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TattoAPI.Models.Project
{
    public class User_Notification_DTO
    {
        public Int64 NT_PKeyID { get; set; }
        public String NT_Name { get; set; }
        public String NT_Description { get; set; }
        public Boolean? NT_IsActive { get; set; }
        public int? PageNumber { get; set; }
        public int? NoofRows { get; set; }
        public String Orderby { get; set; }
        public Boolean? NT_IsDelete { get; set; }
        public Int64? NT_UserID { get; set; }
        public int? Type { get; set; }
        public Int64? UserID { get; set; }
        public Int64? NT_TAF_PKeyID { get; set; }
        public Int64? NT_CM_PKeyID { get; set; }
        public int NT_TAF_Code { get; set; }

        public int? NT_C_L { get; set; }
    }
    public class User_Detail_Notification_DTO 
    {
        public Int64? TAF_PKeyID { get; set; }
        public Int64? CM_PKeyID { get; set; }
        public int TAF_Code { get; set; }

        public String? WhereClause { get; set; }
        public int PageNumber { get; set; }
        public int NoofRows { get; set; }
        public String? Orderby { get; set; }
        public int Type { get; set; }
        public Int64? UserID { get; set; }


    }
}