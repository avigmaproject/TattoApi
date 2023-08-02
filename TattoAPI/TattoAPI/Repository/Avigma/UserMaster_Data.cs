using TattoAPI.Repository.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TattoAPI.Repository.Lib.Security;
using TattoAPI.IRepository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.SqlClient;
using TattoAPI.Models.Avigma;
using TattoAPI.Data;
using System.Security.Claims;
using TattoAPI.Models;

namespace TattoAPI.Repository.Avigma
{
    public class UserMaster_Data : IUserMaster_Data
    {
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public UserMaster_Data()
        {
        }
        public UserMaster_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }
        

        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }


        private List<dynamic> AddUpdateUserMaster_Data(UserMaster_DTO model)
        {
            string msg = string.Empty;

            List<dynamic> objData = new List<dynamic>();

            using (IDbConnection con = Connection)
            {
                if (Connection.State == ConnectionState.Closed) con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("CreateUpdate_UserMaster", (SqlConnection)con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@User_PkeyID", model.User_PkeyID);
                    cmd.Parameters.AddWithValue("@User_Name", model.User_Name);
                    cmd.Parameters.AddWithValue("@User_Email", model.User_Email);
                    cmd.Parameters.AddWithValue("@User_Password", model.User_Password);
                    cmd.Parameters.AddWithValue("@User_Phone", model.User_Phone);
                    cmd.Parameters.AddWithValue("@User_Address", model.User_Address);
                    cmd.Parameters.AddWithValue("@User_City", model.User_City);
                    cmd.Parameters.AddWithValue("@User_Country", model.User_Country);
                    cmd.Parameters.AddWithValue("@User_Zip", model.User_Zip);
                    cmd.Parameters.AddWithValue("@User_DOB", model.User_DOB);
                    cmd.Parameters.AddWithValue("@User_Gender", model.User_Gender);
                    cmd.Parameters.AddWithValue("@User_Type", model.User_Type);
                    cmd.Parameters.AddWithValue("@User_Image_Path", model.User_Image_Path);
                    cmd.Parameters.AddWithValue("@User_MacID", model.User_MacID);
                    cmd.Parameters.AddWithValue("@User_IsVerified", model.User_IsVerified);
                    cmd.Parameters.AddWithValue("@User_IsActive", model.User_IsActive);
                    cmd.Parameters.AddWithValue("@User_IsDelete", model.User_IsDelete);
                    cmd.Parameters.AddWithValue("@User_latitude", model.User_latitude);
                    cmd.Parameters.AddWithValue("@User_longitude", model.User_longitude);
                    cmd.Parameters.AddWithValue("@User_PkeyID_Master", model.User_PkeyID_Master);
                    
                    cmd.Parameters.AddWithValue("@User_Token_val", model.User_Token_val);
                    cmd.Parameters.AddWithValue("@User_Login_Type", model.User_Login_Type);
                    cmd.Parameters.AddWithValue("@User_Image_Base", model.User_Image_Base);
                    cmd.Parameters.AddWithValue("@User_LastName", model.User_LastName);
                    cmd.Parameters.AddWithValue("@User_Company", model.User_Company);
                    cmd.Parameters.AddWithValue("@User_Language", model.User_Language);
                    cmd.Parameters.AddWithValue("@User_IPAddress", model.User_IPAddress);

                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    cmd.Parameters.AddWithValue("@UserID", model.UserID);

                    cmd.Parameters.AddWithValue("@User_Occupation", model.UserID);
                    cmd.Parameters.AddWithValue("@User_MotiID", model.UserID);
                    cmd.Parameters.AddWithValue("@User_Sponsorable_Img", model.UserID);
                    cmd.Parameters.AddWithValue("@User_Sponsorable_Img_link", model.UserID);
                    cmd.Parameters.AddWithValue("@User_Video_Path", model.UserID);

                    cmd.Parameters.AddWithValue("@User_PkeyID_Out", 0).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@ReturnValue", 0).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    msg = "Add Success";
                }
                catch (Exception ex)
                {
                    log.logErrorMessage(ex.StackTrace);
                    log.logErrorMessage(ex.Message);
                    msg = ex.Message;
                }
                finally
                {
                    if (Connection.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            return objData;
        }

        public List<dynamic> AddUserMaster_Data(UserMaster_DTO model)
        {
            ImageGenerator imageGenerator = new ImageGenerator(_configuration);
            string imgPath = string.Empty;
            List<dynamic> objData = new List<dynamic>();
            try
            {
                if (model.Type == 6)
                {
                    if (!String.IsNullOrEmpty(model.User_Image_Base))
                    {
                        imgPath = imageGenerator.Base64ToImage(model.User_Image_Base);
                        model.User_Image_Path = imgPath;
                        model.User_Image_Base = string.Empty;
                    }
                }
                objData.Add(AddUpdateUserMaster_Data(model));
                if (model.Type == 6)
                {
                    objData.Add(imgPath);

                }
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return objData;
        }


        private List<dynamic> DeleteUserMaster(UserMaster_DTO model)
        {
            string msg = string.Empty;
            List<dynamic> objData = new List<dynamic>();

            using (IDbConnection con = Connection)
            {
                if (Connection.State == ConnectionState.Closed) con.Open();

                //string insertProcedure = "[Delete_User_Master]";

                //Dictionary<string, string> input_parameters = new Dictionary<string, string>();
                try
                {
                    SqlCommand cmd = new SqlCommand("Delete_User_Master", (SqlConnection)con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@User_PkeyID", model.User_PkeyID);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    cmd.Parameters.AddWithValue("@UserID", model.UserID);
                    cmd.Parameters.AddWithValue("@User_PkeyID_Out", 0).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@ReturnValue", 0).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    msg = "Delete Success";


                }
                catch (Exception ex)
                {
                    log.logErrorMessage(ex.StackTrace);
                    log.logErrorMessage(ex.Message);
                }
                return objData;

            }
        }

        public List<dynamic> DeleteUserMasterDetails(UserMaster_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = DeleteUserMaster(model);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return objData;

        }


        public List<dynamic> ChangePassword(UserMaster_ChangePassword model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                Int64 User_PkeyID = 0;
                if (!string.IsNullOrEmpty(model.User_PkeyID))
                {
                    User_PkeyID = Convert.ToInt64(securityHelper.Decode(model.User_PkeyID));
                }
                UserMaster_DTO userMaster_DTO = new UserMaster_DTO();
                userMaster_DTO.Type = model.Type;
                userMaster_DTO.User_PkeyID = User_PkeyID;
                userMaster_DTO.User_Password = model.User_Password;

                objData = AddUpdateUserMaster_Data(userMaster_DTO);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return objData;
        }

        public List<dynamic> ChangePasswordByEmail(UserMaster_ChangePassword model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {



                switch (model.User_Type)
                {
                    case 1:
                        {
                            UserMaster_DTO userMaster_DTO = new UserMaster_DTO();
                            userMaster_DTO.Type = model.Type;
                            userMaster_DTO.User_Email = model.User_Email;
                            userMaster_DTO.User_Password = model.User_Password;
                            objData = AddUpdateUserMaster_Data(userMaster_DTO);
                            break;
                        }
                    case 2:
                        {
                            break;
                        }
                }

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return objData;
        }



        public List<dynamic> VerifyUserByEmail(UserMaster_DTO userMaster_DTO)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                userMaster_DTO.Type = 10;
                objData = AddUpdateUserMaster_Data(userMaster_DTO);

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return objData;
        }

        // Below are Done
        private DataSet Get_UserMaster(UserMaster_DTO userMaster)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_UserMaster", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@User_PkeyID", userMaster.User_PkeyID);
                cmd.Parameters.AddWithValue("@User_PkeyID_Master", userMaster.User_PkeyID_Master);
                cmd.Parameters.AddWithValue("@Type", userMaster.Type);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return ds;

        }


        private DataSet Get_UserMasterDetail(UserMaster_DTO_Input userMaster)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_HomeData", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@User_PkeyID", userMaster.User_PkeyID);
                cmd.Parameters.AddWithValue("@Type", userMaster.Type);

                cmd.Parameters.AddWithValue("@WhereClause", userMaster.WhereClause);
                cmd.Parameters.AddWithValue("@PageNumber", userMaster.PageNumber);
                cmd.Parameters.AddWithValue("@NoofRows", userMaster.NoofRows);
                cmd.Parameters.AddWithValue("@Orderby", userMaster.Orderby);
                cmd.Parameters.AddWithValue("@UserID", userMaster.UserID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return ds;

        }

        public List<UserMaster_DTO> Get_UserMasterDetailsDTO(UserMaster_DTO model)
        {
            List<UserMaster_DTO> UserMaster = new List<UserMaster_DTO>();
            try
            {


                DataSet ds = Get_UserMaster(model);

                var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                UserMaster =
                 (from item in myEnumerableFeaprd
                  select new UserMaster_DTO
                  {

                      User_PkeyID = item.Field<long>("User_PkeyID"),
                      //User_PkeyID_Master = item.Field<long?>("User_PkeyID_Master"),
                      User_Name = item.Field<string>("User_Name"),
                      User_Email = item.Field<string>("User_Email"),
                      //User_Password = item.Field<string>("User_Password"),
                      //User_Phone = item.Field<string>("User_Phone"),
                      //User_Address = item.Field<string>("User_Address"),
                      //User_City = item.Field<string>("User_City"),
                      //User_Country = item.Field<string>("User_Country"),
                      //User_Zip = item.Field<string>("User_Zip"),
                      //User_DOB = item.Field<DateTime?>("User_DOB"),
                      //User_Gender = item.Field<int?>("User_Gender"),
                      //User_Type = item.Field<int?>("User_Type"),
                      //User_Image_Path = item.Field<string>("User_Image_Path"),
                      //User_MacID = item.Field<string>("User_MacID"),
                      //User_IsVerified = item.Field<bool?>("User_IsVerified"),
                      User_IsActive = item.Field<bool?>("User_IsActive"),
                      //User_latitude = item.Field<string>("User_latitude"),
                      //User_longitude = item.Field<string>("User_longitude"),
                      //User_Token_val = item.Field<string>("User_Token_val"),
                      //User_Login_Type = item.Field<int?>("User_Login_Type"),
                      //User_Image_Base = item.Field<string>("User_Image_Base"),
                      //User_Company = item.Field<string>("User_Company"),
                      //User_LastName = item.Field<string>("User_LastName"),
                      //User_Store_URL = item.Field<string>("User_Store_URL"),



                  }).ToList();


            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }

            return UserMaster;
        }


        public List<dynamic> Get_UserMasterDetails(UserMaster_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_UserMaster(model);

                var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                List<UserMaster_DTO> UserMaster =
                   (from item in myEnumerableFeaprd
                    select new UserMaster_DTO
                    {
                        User_PkeyID = item.Field<Int64>("User_PkeyID"),
                        User_PkeyID_Master = item.Field<Int64?>("User_PkeyID_Master"),
                        User_Name = item.Field<String>("User_Name"),
                        User_Email = item.Field<String>("User_Email"),
                        User_Password = item.Field<String>("User_Password"),
                        User_Phone = item.Field<String>("User_Phone"),
                        User_Address = item.Field<String>("User_Address"),
                        User_City = item.Field<String>("User_City"),
                        User_Country = item.Field<String>("User_Country"),
                        User_Zip = item.Field<String>("User_Zip"),
                        User_DOB = item.Field<DateTime?>("User_DOB"),
                        User_Gender = item.Field<int?>("User_Gender"),
                        User_Type = item.Field<int?>("User_Type"),
                        User_Image_Path = item.Field<String>("User_Image_Path"),
                        User_MacID = item.Field<String>("User_MacID"),
                        User_IsVerified = item.Field<Boolean?>("User_IsVerified"),
                        User_IsActive = item.Field<Boolean?>("User_IsActive"),
                        User_latitude = item.Field<String>("User_latitude"),
                        User_longitude = item.Field<String>("User_longitude"),
                        User_Token_val = item.Field<String>("User_Token_val"),
                        User_Login_Type = item.Field<int?>("User_Login_Type"),
                        User_Image_Base = item.Field<String>("User_Image_Base"),
                        //User_IsActive_Prof = item.Field<Boolean?>("User_IsActive_Prof"),
                        User_Company = item.Field<String>("User_Company"),
                        User_LastName = item.Field<String>("User_LastName"),



                    }).ToList();

                objDynamic.Add(UserMaster);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }

            return objDynamic;
        }

        public DataSet Get_UserMasterLogin(RootUserLogin_input model)
        {
            DataSet ds = new DataSet();
            try
            {
                //string selectProcedure = "[Get_UserMasterLogin]";
                //Dictionary<string, string> input_parameters = new Dictionary<string, string>();
                //List<dynamic> objdynamic = new List<dynamic>();
                //List<dynamic> objdynamicret = new List<dynamic>();
                SqlCommand cmd = new SqlCommand("Get_UserMasterLogin", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@User_Name", model.User_Name);
                cmd.Parameters.AddWithValue("@User_Email", model.User_Email);
                cmd.Parameters.AddWithValue("@User_Password", model.User_Password);
                cmd.Parameters.AddWithValue("@User_Phone", model.User_Phone);
                cmd.Parameters.AddWithValue("@User_Address", model.User_Address);
                cmd.Parameters.AddWithValue("@User_City", model.User_City);
                cmd.Parameters.AddWithValue("@User_Country", model.User_Country);
                cmd.Parameters.AddWithValue("@User_Zip", model.User_Zip);
                cmd.Parameters.AddWithValue("@User_DOB", model.User_DOB);
                cmd.Parameters.AddWithValue("@User_Type", model.User_Type);
                cmd.Parameters.AddWithValue("@User_Image_Path", model.User_Image_Path);
                cmd.Parameters.AddWithValue("@User_MacID", model.User_MacID);
                cmd.Parameters.AddWithValue("@User_IsVerified", model.User_IsVerified);
                cmd.Parameters.AddWithValue("@User_IsActive", model.User_IsActive);
                cmd.Parameters.AddWithValue("@User_IsDelete", model.User_IsDelete);
                cmd.Parameters.AddWithValue("@UserID", model.UserID);
                cmd.Parameters.AddWithValue("@User_OTP", model.User_OTP);
                cmd.Parameters.AddWithValue("@User_latitude", model.User_latitude);
                cmd.Parameters.AddWithValue("@User_longitude", model.User_longitude);
                cmd.Parameters.AddWithValue("@User_PkeyID_Master", model.User_PkeyID_Master);
                cmd.Parameters.AddWithValue("@User_Login_Type", model.User_Login_Type);
                cmd.Parameters.AddWithValue("@User_Gender", model.User_Gender);
                cmd.Parameters.AddWithValue("@User_Image_Base", model.User_Image_Base);
                cmd.Parameters.AddWithValue("@User_LastName", model.User_LastName);
                cmd.Parameters.AddWithValue("@User_Company", model.User_Company);
                cmd.Parameters.AddWithValue("@User_Language", model.User_Language);
                cmd.Parameters.AddWithValue("@User_Occupation", model.User_Occupation);
                cmd.Parameters.AddWithValue("@User_Token_val", model.User_Token_val);
                cmd.Parameters.AddWithValue("@User_IPAddress", model.User_IPAddress);
                cmd.Parameters.AddWithValue("@User_IsLogin", model.User_IsLogin);
                cmd.Parameters.AddWithValue("@User_Login_Token", model.User_Login_Token);
                cmd.Parameters.AddWithValue("@Type", model.Type);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }

            return ds;
        }

        // Below for UserDetail after login

        public List<dynamic> Get_LoginUserDetails(UserMaster_DTO_Input model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {
                DataSet ds = Get_UserMasterDetail(model);

                var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                List<UserMaster_DTO> UserMaster =
                   (from item in myEnumerableFeaprd
                    select new UserMaster_DTO
                    {
                        User_PkeyID = item.Field<Int64>("User_PkeyID"),
                        User_Name = item.Field<String>("User_Name"),

                    }).ToList();

                objDynamic.Add(UserMaster);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }

            return objDynamic;
        }

    }
}