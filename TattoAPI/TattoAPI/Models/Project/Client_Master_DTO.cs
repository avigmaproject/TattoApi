using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace TattoAPI.Models.Project
{
    public class Client_Master_DTO
    {
        public Int64 CM_PKeyID { get; set; }
        public string? CM_Name { get; set; }
        public string? CM_Phone { get; set; }
        public string? CM_Email { get; set; }
        public Int64? CM_User_PKeyID { get; set; }
        public Int64? CM_TM_PKeyID { get; set; }
        public string? CM_Selected_Area { get; set; }
        public string? CM_References_Image { get; set; }
        public string? CM_References_Image_Filename { get; set; }

        public Int64? CM_IsStatus { get; set; }
        public Boolean? CM_IsFavorite { get; set; }
        public Int64? CM_EST_Appointments { get; set; }
        public int CM_EST_Hrs_App { get; set; }
        public Decimal? CM_EST_Price_Per_Hrs_App { get; set; }
        public Decimal? CM_Deposit_Per_Appointment { get; set; }
        public Decimal? CM_Total_Amount { get; set; }
        public String? CM_Artist_Comments { get; set; }
        public String? CM_ImageName { get; set; }
        public String? CM_ImagePath { get; set; }
        public String? CM_ScreenShot_Img { get; set; }
        public String? CM_ScreenShot_Img_Filename { get; set; }
        public String? CM_Appointments_Date { get; set; }
        public Boolean? CM_Bill_IsPaid { get; set; }
        public String? CM_IPAddress { get; set; }

        public string? CM_UserName { get; set; }
        public Int64? CM_TAF_PKeyID { get; set; }
        public String? CM_Quest_Json { get; set; }
        public int CM_TAF_Code { get; set; }
        public int Type { get; set; }
        public Boolean? CM_IsActive { get; set; }
        public Boolean? CM_IsDelete { get; set; }

    }
    public class Client_Master_DTO_Input
    {
        public Int64 CM_PKeyID { get; set; }
        public int Type { get; set; }
        public string? WhereClause { get; set; }
        public int PageNumber { get; set; }
        public int NoofRows { get; set; }
        public string Orderby { get; set; }
        public Int64 UserID { get; set; }
        public int CM_Code { get; set; }

    }
}

