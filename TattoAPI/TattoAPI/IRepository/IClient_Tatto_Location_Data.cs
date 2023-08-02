using TattoAPI.Models.Project;

namespace TattoAPI.IRepository
{
    public interface IClient_Tatto_Location_Data
    {
        public List<dynamic> AddUpdateClientTattoLocation_Data(Client_Tatto_Location_DTO model);
        public List<dynamic> Get_ClientTattoLocationDetailsDTO(Client_Tatto_Location_DTO_Input model);
    }
}
