using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace TattoAPI.Models.Project
{
    public class Customer_Appointments_DTO
    {
        public Int64 CA_PKeyID { get; set; }
        public string? CA_Name { get; set; }
        public string? CA_Description { get; set; }
        public DateTime CA_Date { get; set; }
        public DateTime CA_Time { get; set; }
        public Int64? CA_CM_PKeyID { get; set; }

        public int Type { get; set; }
        public Int64 UserID { get; set; }
        public Boolean? CA_IsActive { get; set; }
        public Boolean? CA_IsDelete { get; set; }

    }
    public class Customer_Appointments_DTO_Input
    {
        public Int64 CA_PKeyID { get; set; }
        public int Type { get; set; }
        public string WhereClause { get; set; }
        public int PageNumber { get; set; }
        public int NoofRows { get; set; }
        public string Orderby { get; set; }
        public Int64 UserID { get; set; }

    }
}

