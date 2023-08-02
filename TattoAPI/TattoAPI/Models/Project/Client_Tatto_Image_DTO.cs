namespace TattoAPI.Models.Project
{
    public class Client_Tatto_Image_DTO
    {
        public Int64 CTI_PKeyID { get; set; }
        public int CTI_Skin_Tone { get; set; }
        public decimal CTI_Size_CM { get; set; }
        public decimal CTI_Size_Inch { get; set; }
        public string CTI_Description_Path { get; set; }
        public string CTI_Description_Type { get; set; }
        public string CTI_Description_FileName { get; set; }

        public DateTime CTI_From_Date { get; set; }
        public DateTime CTI_To_Date { get; set; }

        public string CTI_ImageName { get; set; }
        public string CTI_ImagePath { get; set; }
        public string CTI_ImageType { get; set; }

        public int Type { get; set; }
        public Int64 UserID { get; set; }
        public Boolean? CTI_IsActive { get; set; }
        public Boolean? CTI_IsDelete { get; set; }
    }
    public class Client_Tatto_Image_DTO_Input
    {
        public Int64 CTI_PKeyID { get; set; }
        public int Type { get; set; }
        public string WhereClause { get; set; }
        public int PageNumber { get; set; }
        public int NoofRows { get; set; }
        public string Orderby { get; set; }
        public Int64 UserID { get; set; }

    }
}
