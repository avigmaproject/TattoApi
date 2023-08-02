using TattoAPI.Models;
using CorePush.Google;
using Microsoft.Extensions.Options;
using static TattoAPI.Models.GoogleNotification;
using System.Net.Http.Headers;
using TattoAPI.IRepository;
using CorePush.Apple;
using TattoAPI.Repository.Lib;
using System.Data;
using TattoAPI.Repository.Lib.FireBase;
using TattoAPI.Models.Project;
using System.Data.SqlClient;
using TattoAPI.Repository.Project;
using TattoAPI.Models.Avigma;

namespace TattoAPI.Repository.Avigma
{
    public class Notification_Data : INotification_Data
    {
        Log log = new Log();
        //private readonly FcmNotificationSetting _fcmNotificationSetting;
        //public Notification_Data(IOptions<FcmNotificationSetting> settings)
        //{
        //    _fcmNotificationSetting = settings.Value;
        //}

        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public Notification_Data()
        {
        }
        public Notification_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }


        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }
        public async Task<ResponseModel> SendNotification(NotificationModel notificationModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                if (notificationModel.IsAndroiodDevice)
                {
                    /* FCM Sender (Android Device) */
                    FcmSettings settings = new FcmSettings()
                    {
                        SenderId = _configuration["SenderId"],
                        ServerKey = _configuration["ServerKey"]
                    };
                    HttpClient httpClient = new HttpClient();

                    string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
                    string deviceToken = notificationModel.DeviceId;

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpClient.DefaultRequestHeaders.Accept
                            .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    DataPayload dataPayload = new DataPayload();
                    dataPayload.Title = notificationModel.Title;
                    dataPayload.Body = notificationModel.Body;

                    GoogleNotification notification = new GoogleNotification();
                    notification.Data = dataPayload;
                    notification.Notification = dataPayload;

                    var fcm = new FcmSender(settings, httpClient);
                    var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                    if (fcmSendResponse.IsSuccess())
                    {
                        response.IsSuccess = true;
                        response.Message = "Notification sent successfully";
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = fcmSendResponse.Results[0].Error;
                        return response;
                    }
                }
                else
                {
                    /* Code here for APN Sender (iOS Device) */
                    //var apn = new ApnSender(apnSettings, httpClient);
                    //await apn.SendAsync(notification, deviceToken);
                }
                return response;
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);

                response.IsSuccess = false;
                response.Message = "Something went wrong";
                return response;
            }
        }

        public string Send_Notification(User_Notification_DTO user_Notification_DTO)
        {
            var result = "-1";
            string message = string.Empty, msgtitle = string.Empty, UserToken = string.Empty, Username = string.Empty, User_Name_Post = string.Empty;
            Int64? User_PkeyID = 0, TAF_UserID = 0, UPC_User_PkeyID = 0;
            NotificationGetData notificationGet = new NotificationGetData(_configuration);
            User_Notification_Data user_Notification_Data = new User_Notification_Data(_configuration);
            //user_Detail_DTO user_Detail_DTO = new user_Detail_DTO();
            User_Detail_Notification_DTO user_Detail_DTO = new User_Detail_Notification_DTO();
            EmailDTO emailDTO = new EmailDTO();
            try
            {
                #region comment
                //if (user_Notification_DTO.NT_UP_PKeyID != null)
                //{
                //    user_Detail_DTO.UP_PKeyID = Convert.ToInt64(user_Notification_DTO.NT_UP_PKeyID);
                //    user_Detail_DTO.Type = 1;
                //    user_Detail_DTO.UL_PKeyID = user_Notification_DTO.NT_UL_PKeyID;
                //}

                //else if (user_Notification_DTO.NT_US_PKeyID != null)
                //{
                //    user_Detail_DTO.US_PKeyID = Convert.ToInt64(user_Notification_DTO.NT_US_PKeyID);
                //    user_Detail_DTO.Type = 2;
                //    user_Detail_DTO.UPC_PkeyID = user_Notification_DTO.NT_UPC_PkeyID;
                //}
                //else if (user_Notification_DTO.NT_SP_PKeyID != null)
                //{
                //    user_Detail_DTO.SUB_PKeyID = Convert.ToInt64(user_Notification_DTO.NT_SP_PKeyID);
                //    user_Detail_DTO.Type =3;
                //}
                //else if (user_Notification_DTO.NT_FLL_PKeyID != null)
                //{
                //    user_Detail_DTO.FLL_PKeyID = Convert.ToInt64(user_Notification_DTO.NT_FLL_PKeyID);
                //    user_Detail_DTO.Type = 4;
                //}
                #endregion

                switch (user_Notification_DTO.NT_C_L)
                {
                    case 1:
                        {

                            user_Detail_DTO.TAF_Code = user_Notification_DTO.NT_TAF_Code;
                            user_Detail_DTO.CM_PKeyID = user_Notification_DTO.NT_CM_PKeyID;
                            user_Detail_DTO.Type = 1;
                            user_Detail_DTO.UserID = user_Notification_DTO.UserID;
                            break;
                        }
                    //case 2:
                    //    {
                    //        user_Detail_DTO.UP_PKeyID = user_Notification_DTO.NT_UP_PKeyID;
                    //        user_Detail_DTO.UL_PKeyID = user_Notification_DTO.NT_UL_PKeyID;
                    //        user_Detail_DTO.Type = 1;
                    //        user_Detail_DTO.UserID = user_Notification_DTO.UserID;
                    //        break;

                    //    }
                    //case 3:
                    //    {

                    //        user_Detail_DTO.UPC_PkeyID = user_Notification_DTO.NT_UPC_PkeyID;
                    //        user_Detail_DTO.UPR_PkeyID = user_Notification_DTO.NT_UPR_PkeyID;
                    //        user_Detail_DTO.Type = 2;
                    //        user_Detail_DTO.UserID = user_Notification_DTO.UserID;
                    //        break;
                    //    }

                }

                user_Notification_DTO.NT_IsActive = true;
                //DataSet ds = Get_UserDetailsByPost_Story(user_Detail_DTO);
                DataSet ds = Get_User_Notification_DetailsByPost_Story(user_Detail_DTO);

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //emailDTO.Message = ds.Tables[0].Rows[i]["TAF_URL"].ToString();
                        //emailDTO.To = ds.Tables[0].Rows[i]["User_Email"].ToString();

                        UserToken = ds.Tables[0].Rows[i]["User_Token_val"].ToString();
                        Username = ds.Tables[0].Rows[i]["User_Name"].ToString();
                        if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["User_PkeyID"].ToString()))
                        {
                            User_PkeyID = Convert.ToInt64(ds.Tables[0].Rows[i]["User_PkeyID"].ToString());

                        }

                        if (!string.IsNullOrWhiteSpace(UserToken))
                        {
                            switch (user_Notification_DTO.NT_C_L)
                            {
                                case 1:
                                    {
                                        if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["TAF_UserID"].ToString()))
                                        {
                                            TAF_UserID = Convert.ToInt64(ds.Tables[0].Rows[i]["TAF_UserID"].ToString());
                                            user_Notification_DTO.NT_UserID = TAF_UserID;
                                            user_Notification_DTO.UserID = TAF_UserID;
                                        }
                                        User_Name_Post = ds.Tables[0].Rows[i]["Client_Master_Name"].ToString();
                                        //message = "Notification Received for Comments "+ User_Name_Post;
                                        //msgtitle = "New Notification Received for Comments  " + User_Name_Post;
                                        message = User_Name_Post + " Fill Artist Form";
                                        msgtitle = User_Name_Post + " Fill Artist Form";
                                        user_Notification_DTO.NT_Description = message;
                                        user_Notification_DTO.NT_Name = msgtitle;
                                        user_Notification_DTO.Type = 1;
                                        break;
                                    }
                                case 2:
                                    {
                                        if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["TAF_UserID"].ToString()))
                                        {
                                            TAF_UserID = Convert.ToInt64(ds.Tables[0].Rows[i]["TAF_UserID"].ToString());
                                            user_Notification_DTO.NT_UserID = TAF_UserID;
                                        }
                                        User_Name_Post = ds.Tables[0].Rows[i]["Client_Master_Name"].ToString();
                                        //message = "Notification Received for Like  "+ User_Name_Post;
                                        //msgtitle = "New Notification Received for Like " + User_Name_Post ;

                                        message = User_Name_Post + " liked your post";
                                        msgtitle = User_Name_Post + " liked your post";
                                        user_Notification_DTO.NT_Description = message;
                                        user_Notification_DTO.NT_Name = msgtitle;
                                        user_Notification_DTO.Type = 1;
                                        break;
                                    }
                                case 3:
                                    {
                                        if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["UPC_User_PkeyID"].ToString()))
                                        {
                                            UPC_User_PkeyID = Convert.ToInt64(ds.Tables[0].Rows[i]["UPC_User_PkeyID"].ToString());
                                            user_Notification_DTO.NT_UserID = UPC_User_PkeyID;
                                        }
                                        User_Name_Post = ds.Tables[0].Rows[i]["Client_Master_Name"].ToString();
                                        //message = "Notification Received for Comments "+ User_Name_Post;
                                        //msgtitle = "New Notification Received for Comments  " + User_Name_Post;
                                        message = User_Name_Post + " Get Review on your Comment";
                                        msgtitle = User_Name_Post + " Get Review on your Comment";
                                        user_Notification_DTO.NT_Description = message;
                                        user_Notification_DTO.NT_Name = msgtitle;
                                        user_Notification_DTO.Type = 1;
                                        break;

                                    }

                            }
                            notificationGet.SendNotification(UserToken, message, msgtitle, "1");


                            user_Notification_Data.CreateUpdate_User_NotificationDetails(user_Notification_DTO);

                        }
                        else
                        {
                            log.logInfoMessage("No UserToken Found " + User_PkeyID);
                            user_Notification_DTO.Type = 1;
                            user_Notification_DTO.NT_UserID = User_PkeyID;
                            user_Notification_Data.CreateUpdate_User_NotificationDetails(user_Notification_DTO);
                        }

                    }
                }
                else
                {
                    log.logErrorMessage("No Taable Data Found in Notification");
                }

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }

            return result;


        }

        public DataSet Get_User_Notification_DetailsByPost_Story(User_Detail_Notification_DTO model)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_Notification_Data", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TAF_PKeyID", model.TAF_PKeyID);
                cmd.Parameters.AddWithValue("@CM_PKeyID", model.CM_PKeyID);
                cmd.Parameters.AddWithValue("@TAF_Code", model.TAF_Code);
                cmd.Parameters.AddWithValue("@UserID", model.UserID);
                cmd.Parameters.AddWithValue("@WhereClause", model.WhereClause);
                cmd.Parameters.AddWithValue("@PageNumber", model.PageNumber);
                cmd.Parameters.AddWithValue("@NoofRows", model.NoofRows);
                cmd.Parameters.AddWithValue("@Orderby", model.Orderby);
                cmd.Parameters.AddWithValue("@Type", model.Type);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }

            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }



            return ds;
        }

    }
}
