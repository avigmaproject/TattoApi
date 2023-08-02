using TattoAPI.Models.Project;

namespace TattoAPI.IRepository
{
    public interface ITatto_Style_Data
    {
        public List<dynamic> AddUpdateTattoStyle_Data(Tatto_Style_DTO model);
        public List<dynamic> Get_TattoStyleDetailsDTO(Tatto_Style_DTO_Input model);
    }
}
