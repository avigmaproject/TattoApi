using TattoAPI.Models.Project;

namespace TattoAPI.IRepository
{
    public interface ITattoMaster_Data
    {
        public List<dynamic> AddTattoMaster_Data(TattoMaster_DTO model);
        public List<dynamic> Get_TattoMasterDetailsDTO(TattoMaster_DTO_Input model);
    }
}
