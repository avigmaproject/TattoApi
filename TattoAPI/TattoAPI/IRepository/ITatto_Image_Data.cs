using TattoAPI.Models.Project;

namespace TattoAPI.IRepository
{
    public interface ITatto_Image_Data
    {
        public List<dynamic> AddUpdateTattoImage_Data(Tatto_Image_DTO model);
        public List<dynamic> Get_TattoImageDetailsDTO(Tatto_Image_DTO_Input model);
    }
}
