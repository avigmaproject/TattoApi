namespace TattoAPI.Models.Project
{
    public class Client_Reference_Image_DTO
    {
        public Int64 CRI_PKeyID { get; set; }
        public Int64 CRI_CM_PKeyID { get; set; }
        public Int64 CRI_TAF_PKeyID { get; set; }
        public String CRI_ImageName { get; set; }
        public String CRI_ImagePath { get; set; }

        public int Type { get; set; }
        public Int64 UserID { get; set; }
        public Boolean? CRI_IsActive { get; set; }
        public Boolean? CRI_IsDelete { get; set; }
    }
    public class Client_Reference_Image_DTO_Input
    {
        public Int64 CRI_PKeyID { get; set; }
        public int Type { get; set; }
        public string WhereClause { get; set; }
        public int PageNumber { get; set; }
        public int NoofRows { get; set; }
        public string Orderby { get; set; }
        public Int64 UserID { get; set; }

    }
}
