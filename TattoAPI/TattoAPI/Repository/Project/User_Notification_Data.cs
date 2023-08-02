using TattoAPI.Repository.Lib;
using TattoAPI.Models.Project;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TattoAPI.Repository.Lib.FireBase;
using TattoAPI.Repository.Lib.Security;
using System.Data.SqlClient;
using TattoAPI.Repository.Avigma;
using TattoAPI.IRepository;

namespace TattoAPI.Repository.Project
{
    public class User_Notification_Data : IUser_Notification_Data
    {
        //MyDataSourceFactory obj = new MyDataSourceFactory();
        //Log log = new Log();
        //SecurityHelper securityHelper = new SecurityHelper();


        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        ObjectConvert obj = new ObjectConvert();
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public User_Notification_Data()
        {
        }
        public User_Notification_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }


        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        private List<dynamic> CreateUpdate_User_Notification(User_Notification_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();

            //string insertProcedure = "[CreateUpdate_User_Notification]";

            //Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            //try
            //{


            using (IDbConnection con = Connection)
            {
                if (Connection.State == ConnectionState.Closed) con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("CreateUpdate_User_Notification", (SqlConnection)con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NT_PKeyID", model.NT_PKeyID);
                    cmd.Parameters.AddWithValue("@NT_Name", model.NT_Name);
                    cmd.Parameters.AddWithValue("@NT_Description", model.NT_Description);
                    cmd.Parameters.AddWithValue("@NT_IsActive", model.NT_IsActive);
                    cmd.Parameters.AddWithValue("@NT_IsDelete", model.NT_IsDelete);
                    cmd.Parameters.AddWithValue("@NT_UserID", model.NT_UserID);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    cmd.Parameters.AddWithValue("@UserID", model.UserID);
                    cmd.Parameters.AddWithValue("@NT_C_L", model.NT_C_L);

                    SqlParameter NT_Pkey_Out = cmd.Parameters.AddWithValue("@NT_Pkey_Out", 0);
                    NT_Pkey_Out.Direction = ParameterDirection.Output;
                    SqlParameter ReturnValue = cmd.Parameters.AddWithValue("@ReturnValue", 0);
                    ReturnValue.Direction = ParameterDirection.Output;


                    cmd.ExecuteNonQuery();
                    objData.Add(NT_Pkey_Out.Value);
                    objData.Add(ReturnValue.Value);

                }
                catch (Exception ex)
                {
                    log.logErrorMessage(ex.StackTrace);
                    log.logErrorMessage(ex.Message);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            return objData;

        }

        private DataSet Get_User_Notification(User_Notification_DTO model)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_User_Notification", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NT_PKeyID", model.NT_PKeyID);
                cmd.Parameters.AddWithValue("@UserID", model.UserID);
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

        public List<dynamic> CreateUpdate_User_NotificationDetails(User_Notification_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = CreateUpdate_User_Notification(model);
                if (model.Type == 6)
                {
                    SendFireBaseNotification(model);
                }
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return objData;
        }

        public int SendFireBaseNotification(User_Notification_DTO model)
        {
            int ret = 0;
            try
            {
                string message = string.Empty, msgtitle = string.Empty, UserToken = string.Empty, Username = string.Empty, User_Name_Post = string.Empty;
                Int64? User_PkeyID = 0, UP_UserID = 0;
                NotificationGetData notificationGet = new NotificationGetData(_configuration);
                User_Notification_Data user_Notification_Data = new User_Notification_Data();
                Notification_Data notification_Data = new Notification_Data(_configuration);
                User_Detail_Notification_DTO user_Post_DTO = new User_Detail_Notification_DTO();
                user_Post_DTO.Type = 6;
                user_Post_DTO.UserID = model.UserID;
                DataSet ds = notification_Data.Get_User_Notification_DetailsByPost_Story(user_Post_DTO);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    UserToken = ds.Tables[0].Rows[i]["User_Token_val"].ToString();
                    Username = ds.Tables[0].Rows[i]["User_Name"].ToString();
                    if (!string.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["User_PkeyID"].ToString()))
                    {
                        User_PkeyID = Convert.ToInt64(ds.Tables[0].Rows[i]["User_PkeyID"].ToString());

                    }

                    for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                    {
                        string name = ds.Tables[1].Rows[i]["User_Name"].ToString();
                        message = name + " is now Live";
                        msgtitle = name + " is now Live";
                    }
                    //message = "Live Streaming Started by User"+ Username;
                    //msgtitle = "Live Streaming Started by User" + Username;

                    notificationGet.SendNotification(UserToken, message, msgtitle,"1");
                    ret = 1;
                }
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return ret;
        }
        public List<dynamic> Get_User_NotificationDetails(User_Notification_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_User_Notification(model);

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        objDynamic.Add(obj.AsDynamicEnumerable(ds.Tables[i]));
                    }

                }
                //objDynamic.Add(Get_details);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }

            return objDynamic;
        }

    }
}