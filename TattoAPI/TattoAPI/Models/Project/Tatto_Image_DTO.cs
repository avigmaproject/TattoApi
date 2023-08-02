namespace TattoAPI.Models.Project
{
    public class Tatto_Image_DTO
    {
        public Int64 TI_PKeyID { get; set; }
        public Int64 TI_TM_PKeyID { get; set; }
        public Int64 TI_TCM_PKeyID { get; set; }
        public Int64 TI_CM_PKeyID { get; set; }
        public Int64 TI_User_PKeyID { get; set; }
        public string TI_ImagePath { get; set; }
        public string TI_ImageName { get; set; }
        public string TI_ImageType { get; set; }
        public string TI_Description { get; set; }
        public int TI_Image_Sequence { get; set; }
        public decimal? TI_Price { get; set; }

        public int Type { get; set; }
        public Boolean? TI_IsActive { get; set; }
        public Boolean? TI_IsDelete { get; set; }
    }
    public class Tatto_Image_DTO_Input
    {
        public Int64 TI_PKeyID { get; set; }
        public int Type { get; set; }
        public string WhereClause { get; set; }
        public int PageNumber { get; set; }
        public int NoofRows { get; set; }
        public string Orderby { get; set; }
        public Int64 UserID { get; set; }

    }
}
