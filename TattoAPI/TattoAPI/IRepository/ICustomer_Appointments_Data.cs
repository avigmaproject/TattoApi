using TattoAPI.Models.Project;

namespace TattoAPI.IRepository
{
    public interface ICustomer_Appointments_Data
    {
        public List<dynamic> AddUpdateCustomerAppointments_Data(Customer_Appointments_DTO model);
        public List<dynamic> Get_CustomerAppointmentsDetailsDTO(Customer_Appointments_DTO_Input model);
    }
}
