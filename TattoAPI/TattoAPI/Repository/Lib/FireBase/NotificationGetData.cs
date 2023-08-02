using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.Net;
using System.IO;
using TattoAPI.Models.Avigma;
using TattoAPI.Repository.Lib;
using System.Data.SqlClient;

namespace TattoAPI.Repository.Lib.FireBase
{
    public class NotificationGetData
    {
        Log log = new Log();
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public NotificationGetData()
        {
        }
        public NotificationGetData(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }


        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }


        public async Task<TopicManagementResponse> AddFCMTopic()
        {
            string jsonpath = System.Configuration.ConfigurationManager.AppSettings["GooglebucketJsom"];

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", jsonpath);

            FirebaseApp appInstance = null;
            appInstance = FirebaseApp.GetInstance("IPLMessageApp");
            if (appInstance == null)
            {
                appInstance = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.GetApplicationDefault(),
                }, "IPLMessageApp");
            }
            FirebaseMessaging messaging = FirebaseMessaging.GetMessaging(appInstance);
            var registrationTokens = new List<string>()
                {
                    "9E756767-3725-4CBA-B718-46F00FB29FDE",
                    "e05219fc-017f-49bf-8b3c-b1d9c827150a",
                    "d8e58c68-94ac-4d53-9691-595e90cc88c0"
                };

            // Subscribe the devices corresponding to the registration tokens to the
            // topic
            var response = await messaging.SubscribeToTopicAsync(
                registrationTokens, "TESTTopic1");

            return response;
        }

        public string SendNotification(string userToken, string message, string msgtitle, string data)
        {
            var result = "-1";
            try
            {

                WebRequest httpWebRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                httpWebRequest.Method = "post";

                string serverKey = _configuration["ServerKey"];
                string senderId = _configuration["SenderId"];

                httpWebRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                httpWebRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                httpWebRequest.ContentType = "application/json";
                var payload = new
                {
                    to = userToken,

                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        body = message,
                        title = msgtitle,
                        badge = 1,

                    },
                    data = new
                    {
                        key1 = data,

                    }

                };

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(payload);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                log.logInfoMessage("Notifcation Status---------->"+ userToken);
                log.logInfoMessage(result);

            }
            catch (Exception ex)
            {

                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return result;

        }


        public void StoreData(NotificationMasterDTO notificationMaster)
        {
            
            try
            {
               
               
                string strlink = _configuration["FireBaseLink"];
                FirebaseDB firebaseDB = new FirebaseDB(strlink);
                FirebaseDB firebaseDBTeams = firebaseDB.Node("MLM-"+ notificationMaster.UserId);
                //FirebaseDB firebaseDBTeams = firebaseDB;

                #region comment
                //try
                //{

                //    NotificationDataForAPI notificationDataForAPI = new NotificationDataForAPI();

                //    //NotificationMasterDTO notificationMaster = new NotificationMasterDTO();
                //    obj =  notificationDataForAPI.GetNotificationDataAPI(notificationMaster);
                //}
                //catch(Exception ex)
                //{
                //    log.logErrorMessage(ex.StackTrace);
                //}

                //WriteLine("PUT Request");

                //String temp = JsonConvert.SerializeObject(obj);
                #endregion
               
                string val = notificationMaster.JSONFormat;
          //      string val = "{ UserId:\"" + notificationMaster.UserId + "\",UserID: { \r\nOTP:\"" + notificationMaster.OTP + "\"}\r\n}";
                FirebaseResponse putResponse = firebaseDBTeams.Put(val);
                //  WriteLine(putResponse.Success);
                //   WriteLine();
                log.logInfoMessage("FireBase Integration");
                log.logInfoMessage(putResponse.Success.ToString());
            }
            catch (Exception ex)
            {

                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
        

           
        }


        public void PublicStoreData(NotificationMasterDTO notificationMaster)
        {
            
            try
            {


                string strlink = _configuration["NotificationMessage"];
                FirebaseDB firebaseDB = new FirebaseDB(strlink);
                FirebaseDB firebaseDBTeams = firebaseDB.Node("MLM-Notification");

                //FirebaseDB firebaseDBTeams = firebaseDB;

                #region comment
                //try
                //{

                //    NotificationDataForAPI notificationDataForAPI = new NotificationDataForAPI();

                //    //NotificationMasterDTO notificationMaster = new NotificationMasterDTO();
                //    obj =  notificationDataForAPI.GetNotificationDataAPI(notificationMaster);
                //}
                //catch(Exception ex)
                //{
                //    log.logErrorMessage(ex.StackTrace);
                //}

                //WriteLine("PUT Request");

                //String temp = JsonConvert.SerializeObject(obj);
                #endregion

                string val = notificationMaster.JSONFormat;
                //      string val = "{ UserId:\"" + notificationMaster.UserId + "\",UserID: { \r\nOTP:\"" + notificationMaster.OTP + "\"}\r\n}";
                FirebaseResponse putResponse = firebaseDBTeams.Put(val);
                //  WriteLine(putResponse.Success);
                //   WriteLine();
                log.logDebugMessage("FireBase Integration");
                log.logDebugMessage(putResponse.Success.ToString());
            }
            catch (Exception ex)
            {

                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);

            }



        }

      

        public List<dynamic> GetData(NotificationMasterDTO notificationMaster)

        {
            
            List<dynamic> obj = new List<dynamic>();
            string strlink = _configuration["FireBaseLink"];
            FirebaseDB firebaseDB = new FirebaseDB(strlink);
            FirebaseDB firebaseDBTeams = firebaseDB.Node("Notification");
            try
            {
              
        FirebaseResponse getResponse = firebaseDBTeams.Get();
        string temp = getResponse.JSONContent;
        //  obj.Add(temp);
        //notificationMaster = JsonConvert.DeserializeObject<NotificationMasterDTO>(temp);
        //obj.Add(notificationMaster);

        //string json = "{\"ID\": 1, \"Name\": \"Abdullah\"}";
        log.logInfoMessage("FireBase Integration");
                log.logInfoMessage(temp);

                var user = JsonConvert.DeserializeObject<List<ViewNotification>>(temp);

                obj.Add(user);

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }

            return obj;
        }


        public async Task<bool> NotifyAsync(string to, string title, string body)
        {
           
            try
            {
                string strServerKey = _configuration["FireBaseServerKey"];
                string strSenderID = _configuration["FireBaseSenderID"];
                // Get the server key from FCM console
                // var serverKey = string.Format("key={0}", "AAAAV1sZQdg:APA91bFCDlMvTwbqgIcfkwPXXH7SUWdSV4BL8cL0jidLdeb-beoXpWj5w_3TnAry1X_p93FhLAgH_jrKxLyRwkum5CeJ4Z_F_2Ap0rg0BHN5Ee5_Gf4NEV0dSoIV6W9lLmAxlczUqBaN");
                var serverKey = string.Format("key={0}",strServerKey);
                // Get the sender id from FCM console
                //   var senderId = string.Format("id={0}", "375190536664");
                var senderId = string.Format("id={0}",strSenderID);

                var data = new
                {
                    to, // Recipient device token
                    notification = new { title, body }
                };

                // Using Newtonsoft.Json
                var jsonBody = JsonConvert.SerializeObject(data);

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);
                      
                        if (result.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }

            return false;
        }

    }
}