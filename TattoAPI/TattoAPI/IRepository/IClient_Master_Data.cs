using TattoAPI.Models.Project;

namespace TattoAPI.IRepository
{
    public interface IClient_Master_Data
    {
        public List<dynamic> AddUpdateClientMaster_Data(Client_Master_DTO model);
        public List<dynamic> Get_ClientMasterDetailsDTO(Client_Master_DTO_Input model);
    }
}
