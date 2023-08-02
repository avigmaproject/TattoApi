using TattoAPI.Models.Project;

namespace TattoAPI.IRepository
{
    public interface ITatto_Artist_Data
    {
        public List<dynamic> AddUpdateTattoArtist_Data(Tatto_Artist_DTO model);
        public List<dynamic> Get_TattoArtistDetailsDTO(Tatto_Artist_DTO_Input model);
    }
}
