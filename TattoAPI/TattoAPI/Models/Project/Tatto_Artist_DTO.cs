namespace TattoAPI.Models.Project
{
    public class Tatto_Artist_DTO
    {
        public Int64 TA_PKeyID { get; set; }
        public Int64 TA_CM_PKeyID { get; set; }
        public Int64 TA_CTI_PKeyID { get; set; }
        public Int64 TA_UserID { get; set; }

        public int Type { get; set; }
        public Int64 UserID { get; set; }
        public Boolean? TA_IsActive { get; set; }
        public Boolean? TA_IsDelete { get; set; }
    }
    public class Tatto_Artist_DTO_Input
    {
        public Int64 TA_PKeyID { get; set; }
        public int Type { get; set; }
        public string WhereClause { get; set; }
        public int PageNumber { get; set; }
        public int NoofRows { get; set; }
        public string Orderby { get; set; }
        public Int64 UserID { get; set; }

    }
}
