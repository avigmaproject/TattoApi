using System.Data;
using TattoAPI.Models;
using TattoAPI.Models.Project;

namespace TattoAPI.IRepository
{
    public interface INotification_Data
    {
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
        string Send_Notification(User_Notification_DTO user_Notification_DTO);
        DataSet Get_User_Notification_DetailsByPost_Story(User_Detail_Notification_DTO model);
    }
}
