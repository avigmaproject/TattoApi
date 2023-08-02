using TattoAPI.Models.Avigma;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TattoAPI.Models;

namespace TattoAPI.IRepository
{
    public interface ITattoService
    {
        public Task<List<Product>> GetProductListAsync();
        public Task<IEnumerable<Product>> GetProductByIdAsync(int ProductId);
        public Task<int> AddProductAsync(Product product);
        public Task<int> UpdateProductAsync(Product product);
        public Task<int> DeleteProductAsync(int Id);

        public Task<IEnumerable<UserDisplay>> GetUserByIdAsync();
        public Task<int> AddUserAsync(User user);
        //public Task<object> Gets();

        //Upload Excel File into Database
        public Task<UploadExcelFileResponse> UploadExcelFile(UploadExcelFileRequest request, string Path);
        //public Task<IEnumerable<UserMaster_DTO>> Get_UserMaster();
        //public Task<int> AddUserMasterDataAsync(UserMaster_DTO userMaster);
        //public DataSet UserGet(UserMaster_DTO_Input userMaster, out string msg);

    }
}
