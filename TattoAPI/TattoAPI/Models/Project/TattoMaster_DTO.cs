namespace TattoAPI.Models.Project
{
    public class TattoMaster_DTO
    {
        public Int64 TM_PKeyID { get; set; }
        public string TM_Name { get; set; }
        public string TM_Description { get; set; }
        public string? TM_ImagePath { get; set; }
        public string? TM_Image_Base { get; set; }
        public string? TM_ImageName { get; set; }
        public string TM_ImageType { get; set; }
        public Int64 TM_User_PKeyID { get; set; }
        public string TM_Tatto_Image { get; set; }

        public int Type { get; set; }
        public Boolean? TM_IsActive { get; set; }
        public Boolean? TM_IsDelete { get; set; }
        public Int64? TM_CM_PKeyID { get; set; }
        public Int64? TM_TI_PKeyID { get; set; }
        public Decimal? TM_Price { get; set; }
        public int TM_Tatto_Type { get; set; }
        public int TM_System_Type { get; set; }
    }
    public class TattoMaster_DTO_Input
    {
        public int Type { get; set; }
        public Int64 TM_PKeyID { get; set; }
        public string WhereClause { get; set; }
        public int PageNumber { get; set; }
        public int NoofRows { get; set; }
        public string Orderby { get; set; }
        public Int64 UserID { get; set; }

    }
}
