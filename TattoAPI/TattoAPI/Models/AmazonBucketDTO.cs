namespace TattoAPI.Models
{
    public class AmazonBucketDTO
    {
        public string BucketName { get; set; }
        public string FilePath { get; set; }
        public string keyName { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string FileName { get; set; }
        public string ReturnURL { get; set; }
    }
    //public class AmazonBucketDTO
    //{
    //    public string BucketName { get; set; }
    //    public IFormFile file { get; set; }

    //}
    //public class AmazonBucketDTOResponse
    //{
    //    public bool IsSuccess { get; set; }
    //    public string Message { get; set; }
    //    public string ReturnURL { get; set; }
    //}
}
