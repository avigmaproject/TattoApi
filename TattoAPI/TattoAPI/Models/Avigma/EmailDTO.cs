using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TattoAPI.Models.Avigma
{
    public class EmailDTO
    {
        public long CustId { get; set; }
        public long UserId { get; set; }
        public string MainBody { get; set; }
        public String From { get; set; }
        public String To { get; set; }
        public String Subject { get; set; }
        public String Message { get; set; }
        public String CC { get; set; }
        public String BCC { get; set; }
        public String Attachment { get; set; }
        public String Password { get; set; }
        public String Username { get; set; }
        public bool IsBodyHtml { get; set; }
        public String FirstName { get; set; }
        public List<dynamic> Attachmentarr { get; set; }
        public List<dynamic> filenamearr { get; set; }
    }
    public class Email_User_Detail_DTO 
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