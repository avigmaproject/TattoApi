namespace TattoAPI.Models.Project
{
    public class Client_Tatto_Location_DTO
    {
        public Int64 CTL_PKeyID { get; set; }
        public string CTL_Location_Name { get; set; }
        public string CTL_Location_Description { get; set; }
        public Int64 CTL_CM_PKeyID { get; set; }
        public Int64 CTL_CTI_PKeyID { get; set; }

        public int Type { get; set; }
        public Int64 UserID { get; set; }
        public Boolean? CTL_IsActive { get; set; }
        public Boolean? CTL_IsDelete { get; set; }
    }
    public class Client_Tatto_Location_DTO_Input
    {
        public Int64 CTL_PKeyID { get; set; }
        public int Type { get; set; }
        public string WhereClause { get; set; }
        public int PageNumber { get; set; }
        public int NoofRows { get; set; }
        public string Orderby { get; set; }
        public Int64 UserID { get; set; }

    }
}
