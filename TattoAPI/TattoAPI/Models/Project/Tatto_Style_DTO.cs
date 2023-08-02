namespace TattoAPI.Models.Project
{
    public class Tatto_Style_DTO
    {
        public Int64 TS_PKeyID { get; set; }
        public string TS_Style_Name { get; set; }
        public string TS_Style_Description { get; set; }
        public Int64 TS_CTI_PKeyID { get; set; }
        public Int64 TS_CM_PKeyID { get; set; }

        public int Type { get; set; }
        public Int64 UserID { get; set; }
        public Boolean? TS_IsActive { get; set; }
        public Boolean? TS_IsDelete { get; set; }
    }
    public class Tatto_Style_DTO_Input
    {
        public Int64 TS_PKeyID { get; set; }
        public int Type { get; set; }
        public string WhereClause { get; set; }
        public int PageNumber { get; set; }
        public int NoofRows { get; set; }
        public string Orderby { get; set; }
        public Int64 UserID { get; set; }

    }
}
