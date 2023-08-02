using TattoAPI.Models.Project;

namespace TattoAPI.IRepository
{
    public interface ITatto_Question_Master_Data
    {
        //public List<dynamic> AddUpdateTattoStyle_Data(Tatto_Question_Master_DTO model);
        public List<dynamic> Get_TattoQuestionMasterDetailsDTO(Tatto_Question_Master_DTO_Input model);
    }
}
