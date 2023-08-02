using TattoAPI.Models.Avigma;

namespace TattoAPI.IRepository
{
    public interface IUser_Admin_Master_Data
    {
        public List<dynamic> CreateUpdate_User_Admin_Master_DataDetails(User_Admin_Master_DTO model);
        public List<dynamic> Get_User_Admin_MasterDetails(Int64? Ad_User_PkeyID, Int64? UserID, int? Type);
    }
}
