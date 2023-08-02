namespace TattoAPI.Models
{
    public class UploadExcelFileRequest
    {
        public IFormFile File { get; set; }
    }
    public class UploadExcelFileResponse
    {
        public bool IsSucces { get; set; }
        public string Message { get; set; }
    }
    public class ExcelBulkUploadParameter
    {
        public string? Particulars { get; set; }
        public string? Au_2020 { get; set; }
        public string? Au_2021 { get; set; }
        public string? Au_2022 { get; set; }
        public string? Au_2023 { get; set; }
        public string? Au_2024 { get; set; }
        public string? Au_2025 { get; set; }
        public string? Au_2026 { get; set; }
        public string? Au_2027 { get; set; }
    }
}
