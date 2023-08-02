using TattoAPI.Models.Project;

namespace TattoAPI.IRepository
{
    public interface IClient_Reference_Image_Data
    {
        public List<dynamic> AddUpdateClient_Reference_Image_Data(Client_Reference_Image_DTO model);
        public List<dynamic> Get_Client_Reference_ImageDetailsDTO(Client_Reference_Image_DTO_Input model);
    }
}
