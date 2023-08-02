using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TattoAPI.Models.Avigma;
using TattoAPI.Repository.Lib;

namespace TattoAPI.Repository.Lib.FireBase
{
    public class AddNotification
    {
        public List<dynamic> submitNotification(sendNotification sendNotification)
        {
            Log log = new Log();
            List<dynamic> obj = new List<dynamic>();
            
            var Data = JsonConvert.DeserializeObject<List<NotificationDTO>>(sendNotification.Notificationdetails);
            NotificationDTO mSGData = new NotificationDTO();

            try
            {
              
                for (int i = 0; i < Data.Count; i++)
                {
                   
                  

                    Notification notification = new Notification();
                    mSGData = Data[i];
                    //notification.FirstName = mSGData.FirstName;
                    //notification.LastName = mSGData.LastName;
                    //notification.EmailId = mSGData.EmailId;
                    //notification.MobileNumber = mSGData.MobileNumber;
                    notification.UserId = mSGData.UserId;
                   // notification.StatusID = mSGData.StatusID ;
                    //notification.IsActive = mSGData.IsActive;
                    notification.message = sendNotification.message;
                    notification.UserToken = mSGData.UserToken;
                    if (mSGData.StatusID==1)
                    {
                       
                        obj.Add(notification);
                    }
                   
                    
                }
            }
            catch (Exception ex)
            {
                log.logErrorMessage("NotificationMasterData");
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return obj;

        }

    }
}