namespace TattoAPI.IRepository
{
    public interface IUnitOfWork
    {
        IClient_Master_Data client_Master_Data { get; }
        IEmailTemplate emailTemplate { get; }
        IQRCodeGenerator qRCodeGenerator { get; }
        ITatto_CLient_Master_Data tatto_CLient_Master_Data { get; }
        ITatto_Image_Data tatto_Image_Data { get; }
        IUser_Admin_Master_Data user_Admin_Master_Data { get; }
        IUserMaster_Data userMaster_Data { get; }
        ITattoMaster_Data tattoMaster_Data { get; }

        IClient_Tatto_Image_Data client_Tatto_Image_Data { get; }
        IClient_Tatto_Location_Data client_Tatto_Location_Data { get; }
        ITatto_Artist_Data tatto_Artist_Data { get; }
        ITatto_Style_Data tatto_Style_Data { get; }
        ITatto_Question_Master_Data tatto_Question_Master_Data { get; }
        ITatto_Artist_Form_Data tatto_Artist_Form_Data { get; }
        ITAF_QR_Generate_Upload tAF_QR_Generate_Upload { get; }
        ICustomer_Appointments_Data customer_Appointments_Data { get; }
        IClient_Reference_Image_Data client_Reference_Image_Data { get; }
        INotification_Data notification_Data { get; }
    }
}
