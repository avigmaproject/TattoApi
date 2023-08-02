using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TattoAPI.Models.Avigma
{
    public class NotificationMasterDTO
    {
        public String UserId { get; set; }
        //public String OTP { get; set; }
        public String JSONFormat { get; set; }
    }

    public class ViewNotification
    {
       
        public String UserId { get; set; }
        public String OTP { get; set; }
       
    }

    public class NotificationDTO
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String MobileNumber { get; set; }
        public String EmailId { get; set; }
        public Int64? UserId { get; set; }
        public String message { get; set; }
        public int StatusID { get; set; }
        public Boolean IsActive { get; set; }
        public String Notificationdetails { get; set; }
        public string UserToken { get; set; }
    }
    public class Notification
    {
        //public String FirstName { get; set; }
        //public String LastName { get; set; }
        //public String MobileNumber { get; set; }
        //public String EmailId { get; set; }
        public Int64? UserId { get; set; }
        public String message { get; set; }
        //public int StatusID { get; set; }
        //public Boolean IsActive { get; set; }
        public String UserToken { get; set; }
    }
    public class sendNotification
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String MobileNumber { get; set; }
        public String EmailId { get; set; }
        public Int64? UserId { get; set; }
        public String message { get; set; }
        public String Notificationdetails { get; set; }
        public Boolean IsActive { get; set; }
        public int StatusID { get; set; }
        public String UserToken { get; set; }
        public string ReferenceCode { get; internal set; }
    }
}