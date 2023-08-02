namespace TattoAPI.Models.Project
{
    public class Tatto_Question_Master_DTO
    {
        public Int64 TQM_PKeyID { get; set; }
        public string TQM_Question_Name { get; set; }
        public string TQM_Question_Description { get; set; }

        public int Type { get; set; }
        public Int64 UserID { get; set; }
        public Boolean? TQM_IsActive { get; set; }
        public Boolean? TQM_IsDelete { get; set; }
    }
    public class Tatto_Question_Master_DTO_Input
    {
        public Int64 TQM_PKeyID { get; set; }
        public int Type { get; set; }
        public string WhereClause { get; set; }
        public int PageNumber { get; set; }
        public int NoofRows { get; set; }
        public string Orderby { get; set; }
        public Int64 UserID { get; set; }

    }
}
