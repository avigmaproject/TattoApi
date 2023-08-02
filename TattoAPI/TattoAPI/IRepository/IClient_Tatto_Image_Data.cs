using TattoAPI.Models.Project;

namespace TattoAPI.IRepository
{
    public interface IClient_Tatto_Image_Data
    {
        public List<dynamic> AddUpdateClientTattoImage_Data(Client_Tatto_Image_DTO model);
        public List<dynamic> Get_ClientTattoImageDetailsDTO(Client_Tatto_Image_DTO_Input model);
    }
}
