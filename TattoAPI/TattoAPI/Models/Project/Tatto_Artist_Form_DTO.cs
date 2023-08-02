namespace TattoAPI.Models.Project
{
    public class Tatto_Artist_Form_DTO
    {
        public Int64 TAF_PKeyID { get; set; }
        public Int64 TAF_UserID { get; set; }
        public String TAF_Quest_Control_Data { get; set; }
        public String? TAF_QR_Code_URL { get; set; }
       
        public String? TAF_QR_Code_DBPath { get; set; }
        public Boolean? TAF_IsDefault { get; set; }
        public int TAF_RandomNumber { get; set; }
        public int Type { get; set; }
        public Int64 UserID { get; set; }
        public Boolean? TAF_IsActive { get; set; }
        public Boolean? TAF_IsDelete { get; set; }
    }
    public class Tatto_Artist_Form_DTO_Input
    {
        public Int64 TAF_PKeyID { get; set; }
        public int TAF_Code { get; set; }
        public Int64 CM_PKeyID { get; set; }
        public int Type { get; set; }
        public string WhereClause { get; set; }
        public int PageNumber { get; set; }
        public int NoofRows { get; set; }
        public string Orderby { get; set; }
        public Int64 UserID { get; set; }

    }
}
