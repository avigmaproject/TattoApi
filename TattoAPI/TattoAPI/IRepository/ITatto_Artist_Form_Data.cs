using TattoAPI.Models.Project;

namespace TattoAPI.IRepository
{
    public interface ITatto_Artist_Form_Data
    {
        public List<dynamic> AddUpdateTattoArtistForm_Data(Tatto_Artist_Form_DTO model);
        public List<dynamic> Get_TattoArtistFormDetailsDTO(Tatto_Artist_Form_DTO_Input model);
    }
}
