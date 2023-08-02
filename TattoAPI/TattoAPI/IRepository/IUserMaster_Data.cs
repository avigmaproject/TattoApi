using System.Data;
using TattoAPI.Models.Avigma;

namespace TattoAPI.IRepository
{
    public interface IUserMaster_Data
    {
        public List<dynamic> AddUserMaster_Data(UserMaster_DTO model);
        public List<dynamic> ChangePassword(UserMaster_ChangePassword model);
        public List<dynamic> ChangePasswordByEmail(UserMaster_ChangePassword model);
        public List<dynamic> VerifyUserByEmail(UserMaster_DTO userMaster_DTO);
        public List<UserMaster_DTO> Get_UserMasterDetailsDTO(UserMaster_DTO model);
        public List<dynamic> Get_UserMasterDetails(UserMaster_DTO model);
        public DataSet Get_UserMasterLogin(RootUserLogin_input model);
        public List<dynamic> Get_LoginUserDetails(UserMaster_DTO_Input model);


    }
}
