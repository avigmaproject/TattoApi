using TattoAPI.Models.Project;

namespace TattoAPI.IRepository
{
    public interface ITatto_CLient_Master_Data
    {
        public List<dynamic> AddUpdateTattoClientMaster_Data(Tatto_Client_Master_DTO model);
        public List<dynamic> Get_TattoClientMasterDetailsDTO(Tatto_Client_Master_DTO_Input model);
    }
}
