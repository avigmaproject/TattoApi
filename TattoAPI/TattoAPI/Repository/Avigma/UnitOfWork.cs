using System.Data;
using System.Data.SqlClient;
using TattoAPI.Data;
using TattoAPI.IRepository;
using TattoAPI.Models;
using Microsoft.Extensions.Options;
using TattoAPI.Repository.Project;
using TattoAPI.Repository.Lib;

namespace TattoAPI.Repository.Avigma
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;

        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public IEmailTemplate emailTemplate => new EmailTemplate(_configuration);
        public IQRCodeGenerator qRCodeGenerator => new QRCodeGenerator(_configuration);
        public IUser_Admin_Master_Data user_Admin_Master_Data => new User_Admin_Master_Data(_configuration);
        public IUserMaster_Data userMaster_Data => new UserMaster_Data(_configuration);
        public ITattoMaster_Data tattoMaster_Data => new TattoMaster_Data(_configuration);

        public IClient_Master_Data client_Master_Data => new Client_Master_Data(_configuration);

        public ITatto_CLient_Master_Data tatto_CLient_Master_Data => new Tatto_CLient_Master_Data(_configuration);

        public ITatto_Image_Data tatto_Image_Data => new Tatto_Image_Data(_configuration);

        public IClient_Tatto_Image_Data client_Tatto_Image_Data => new Client_Tatto_Image_Data(_configuration);

        public IClient_Tatto_Location_Data client_Tatto_Location_Data => new Client_Tatto_Location_Data(_configuration);

        public ITatto_Artist_Data tatto_Artist_Data => new Tatto_Artist_Data(_configuration);

        public ITatto_Style_Data tatto_Style_Data => new Tatto_Style_Data(_configuration);
        public ITatto_Question_Master_Data tatto_Question_Master_Data => new Tatto_Question_Master_Data(_configuration);

        public ITatto_Artist_Form_Data tatto_Artist_Form_Data => new Tatto_Artist_Form_Data(_configuration);
        public ITAF_QR_Generate_Upload tAF_QR_Generate_Upload => new TAF_QR_Generate_Upload(_configuration);
        public ICustomer_Appointments_Data customer_Appointments_Data => new Customer_Appointments_Data(_configuration);

        public IClient_Reference_Image_Data client_Reference_Image_Data => new Client_Reference_Image_Data(_configuration);
        public INotification_Data notification_Data => new Notification_Data(_configuration);
    }
}
