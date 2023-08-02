namespace TattoAPI.Models.Project
{
    public class Tatto_Client_Master_DTO
    {
        public Int64 TCM_PKeyID { get; set; }
        public Int64 TCM_TM_PKeyID { get; set; }
        public Int64 TCM_User_PKeyID { get; set; }
        public Int64? TCM_CM_PKeyID { get; set; }
        public Int64? TCM_TI_PKeyID { get; set; }
        public Boolean? TCM_Status { get; set; }


        public int Type { get; set; }
        public Boolean? TCM_IsActive { get; set; }
        public Boolean? TCM_IsDelete { get; set; }

    }
    public class Tatto_Client_Master_DTO_Input
    {
        public Int64 TCM_PKeyID { get; set; }
        public int Type { get; set; }
        public string WhereClause { get; set; }
        public int PageNumber { get; set; }
        public int NoofRows { get; set; }
        public string Orderby { get; set; }
        public Int64 UserID { get; set; }

    }
}
