using System.Data;
using System.Data.SqlClient;
using TattoAPI.IRepository;
using TattoAPI.Models.Avigma;
using TattoAPI.Models.Project;
using TattoAPI.Repository.Avigma;
using TattoAPI.Repository.Lib;
using TattoAPI.Repository.Lib.Security;

namespace TattoAPI.Repository.Project
{
    public class Client_Master_Data : IClient_Master_Data
    {
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        ObjectConvert obj = new ObjectConvert();

        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public Client_Master_Data()
        {
        }
        public Client_Master_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }


        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }


        private List<dynamic> CreateUpdateClientMaster_Data(Client_Master_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();

            using (IDbConnection con = Connection)
            {
                if (con.State == ConnectionState.Closed) con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("CreateUpdate_ClientMaster", (SqlConnection)con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CM_PkeyID", model.CM_PKeyID);
                    cmd.Parameters.AddWithValue("@CM_Name", model.CM_Name);
                    cmd.Parameters.AddWithValue("@CM_Phone", model.CM_Phone);
                    cmd.Parameters.AddWithValue("@CM_Email", model.CM_Email);
                    cmd.Parameters.AddWithValue("@UserID", model.CM_User_PKeyID);
                    cmd.Parameters.AddWithValue("@CM_TM_PKeyID", model.CM_TM_PKeyID);
                    cmd.Parameters.AddWithValue("@CM_UserName", model.CM_UserName);
                    cmd.Parameters.AddWithValue("@CM_TAF_PKeyID", model.CM_TAF_PKeyID);
                    cmd.Parameters.AddWithValue("@CM_TAF_Code", model.CM_TAF_Code);
                    cmd.Parameters.AddWithValue("@CM_Quest_Json", model.CM_Quest_Json);
                    cmd.Parameters.AddWithValue("@CM_Selected_Area", model.CM_Selected_Area);
                    cmd.Parameters.AddWithValue("@CM_References_Image", model.CM_References_Image);
                    //cmd.Parameters.AddWithValue("@CM_References_Image_Filename", model.CM_References_Image_Filename);
                    cmd.Parameters.AddWithValue("@CM_IsStatus", model.CM_IsStatus);
                    cmd.Parameters.AddWithValue("@CM_IsFavorite", model.CM_IsFavorite);
                    cmd.Parameters.AddWithValue("@CM_EST_Appointments", model.CM_EST_Appointments);
                    cmd.Parameters.AddWithValue("@CM_EST_Hrs_App", model.CM_EST_Hrs_App);
                    cmd.Parameters.AddWithValue("@CM_EST_Price_Per_Hrs_App", model.CM_EST_Price_Per_Hrs_App);
                    cmd.Parameters.AddWithValue("@CM_Deposit_Per_Appointment", model.CM_Deposit_Per_Appointment);
                    cmd.Parameters.AddWithValue("@CM_Total_Amount", model.CM_Total_Amount);
                    cmd.Parameters.AddWithValue("@CM_Artist_Comments", model.CM_Artist_Comments);
                    cmd.Parameters.AddWithValue("@CM_ImageName", model.CM_ImageName);
                    cmd.Parameters.AddWithValue("@CM_ImagePath", model.CM_ImagePath);
                    cmd.Parameters.AddWithValue("@CM_ScreenShot_Img", model.CM_ScreenShot_Img);
                    cmd.Parameters.AddWithValue("@CM_ScreenShot_Img_Filename", model.CM_ScreenShot_Img_Filename);
                    cmd.Parameters.AddWithValue("@CM_Appointments_Date", model.CM_Appointments_Date);
                    cmd.Parameters.AddWithValue("@CM_Bill_IsPaid", model.CM_Bill_IsPaid);
                    cmd.Parameters.AddWithValue("@CM_IPAddress", model.CM_IPAddress);

                    cmd.Parameters.AddWithValue("@CM_IsActive", model.CM_IsActive);
                    cmd.Parameters.AddWithValue("@CM_IsDelete", model.CM_IsDelete);
                    cmd.Parameters.AddWithValue("@Type", model.Type);

                    SqlParameter CM_PkeyID_Out = cmd.Parameters.AddWithValue("@CM_PkeyID_Out", 0);
                    CM_PkeyID_Out.Direction = ParameterDirection.Output;
                    SqlParameter ReturnValue = cmd.Parameters.AddWithValue("@ReturnValue", 0);
                    ReturnValue.Direction = ParameterDirection.Output;
                    SqlParameter RandomNumberOut = cmd.Parameters.AddWithValue("@RandomNumber", 0);
                    RandomNumberOut.Direction = ParameterDirection.Output;


                    cmd.ExecuteNonQuery();
                    objData.Add(CM_PkeyID_Out.Value);
                    objData.Add(ReturnValue.Value);
                    objData.Add(RandomNumberOut.Value);

                }
                catch (Exception ex)
                {
                    log.logErrorMessage(ex.StackTrace);
                    log.logErrorMessage(ex.Message);
                    //msg = ex.Message;
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

        public List<dynamic> AddUpdateClientMaster_Data(Client_Master_DTO model)
        {

            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = CreateUpdateClientMaster_Data(model);

                if (model.Type == 1)
                {
                    Notification_Data notification_Data = new Notification_Data(_configuration);
                    User_Notification_DTO user_Notification_DTO = new User_Notification_DTO();
                    EmailTemplate emailTemplate = new EmailTemplate(_configuration);
                    EmailDTO emailDTO = new EmailDTO();
                    Client_Master_DTO_Input clientMaster = new Client_Master_DTO_Input();

                    if (model.CM_TAF_Code != null)
                    {
                        user_Notification_DTO.NT_TAF_Code = model.CM_TAF_Code;
                        user_Notification_DTO.NT_CM_PKeyID = objData[0];
                        user_Notification_DTO.NT_C_L = 1;
                        user_Notification_DTO.UserID = model.CM_User_PKeyID;
                        notification_Data.Send_Notification(user_Notification_DTO);

                    }

                    clientMaster.CM_PKeyID = objData[0];
                    clientMaster.Type = 5;

                    DataSet ds = Get_ClientMaster(clientMaster);
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            emailDTO.Message = ds.Tables[0].Rows[i]["TAF_URL"].ToString();
                            emailDTO.To = ds.Tables[0].Rows[i]["User_Email"].ToString();
                        }
                    }
                    string Mes = emailDTO.Message;
                    emailTemplate.NewClientRegister(emailDTO, Mes, 1);

                }

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }

            return objData;
        }

        private DataSet Get_ClientMaster(Client_Master_DTO_Input ClientMaster)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_ClientMaster", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CM_PkeyID", ClientMaster.CM_PKeyID);
                cmd.Parameters.AddWithValue("@Type", ClientMaster.Type);

                cmd.Parameters.AddWithValue("@WhereClause", ClientMaster.WhereClause);
                cmd.Parameters.AddWithValue("@PageNumber", ClientMaster.PageNumber);
                cmd.Parameters.AddWithValue("@NoofRows", ClientMaster.NoofRows);
                cmd.Parameters.AddWithValue("@Orderby", ClientMaster.Orderby);
                cmd.Parameters.AddWithValue("@UserID", ClientMaster.UserID);
                cmd.Parameters.AddWithValue("@CM_Code", ClientMaster.CM_Code);

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


        public List<dynamic> Get_ClientMasterDetailsDTO(Client_Master_DTO_Input model)
        {
            string wherecondition = string.Empty;

            if (!string.IsNullOrEmpty(model.WhereClause))
            {
                wherecondition = wherecondition != null ? wherecondition + " And cmu.[CM_IsStatus] like '%" + model.WhereClause + "%'" : " And cmu.[CM_IsStatus] like '%" + model.WhereClause + "%'";
            }
            model.WhereClause = wherecondition;

            List<dynamic> objDynamic = new List<dynamic>();
            try
            {


                DataSet ds = Get_ClientMaster(model);

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        objDynamic.Add(obj.AsDynamicEnumerable(ds.Tables[i]));
                    }

                }

                //var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                //ClientMaster =
                // (from item in myEnumerableFeaprd
                //  select new Client_Master_DTO
                //  {
                //      CM_PKeyID = item.Field<Int64>("CM_PkeyID"),
                //      CM_Name = item.Field<string>("CM_Name"),
                //      CM_Phone = item.Field<string>("CM_Phone"),
                //      CM_Email = item.Field<string>("CM_Email"),
                //      CM_User_PKeyID = item.Field<Int64?>("CM_User_PKeyID"),
                //      CM_TM_PKeyID = item.Field<Int64?>("CM_TM_PKeyID"),

                //      CM_UserName = item.Field<string>("CM_UserName"),
                //      CM_Select_Area = item.Field<Boolean?>("CM_Select_Area"),
                //      CM_Skin_Tone = item.Field<int?>("CM_Skin_Tone"),
                //      CM_Area_Photo = item.Field<Int64?>("CM_Area_Photo"),
                //      CM_Comparing_Size = item.Field<decimal>("CM_Comparing_Size"),
                //      CM_Size_Inch = item.Field<decimal>("CM_Size_Inch"),
                //      CM_Size_CM = item.Field<decimal>("CM_Size_CM"),
                //      CM_References_Images = item.Field<Int64?>("CM_References_Images"),
                //      CM_Description = item.Field<string>("CM_Description"),
                //      CM_When_You_Like_It = item.Field<string>("CM_When_You_Like_It"),
                //      CM_CTL_PKeyID = item.Field<Int64?>("CM_CTL_PKeyID"),
                //      CM_TA_PKeyID = item.Field<Int64?>("CM_TA_PKeyID"),
                //      CM_TS_PKeyID = item.Field<Int64?>("CM_TS_PKeyID"),
                //      CM_Instagram_ID = item.Field<string>("CM_Instagram_ID"),
                //      CM_TikTok_ID = item.Field<string>("CM_TikTok_ID"),
                //      CM_Facebook_ID = item.Field<string>("CM_Facebook_ID"),
                //      CM_Address = item.Field<string>("CM_Address"),
                //      CM_DOB = item.Field<DateTime>("CM_DOB"),
                //      CM_TermCondition = item.Field<Boolean?>("CM_TermCondition"),
                //      CM_Hidden_Info_to_the_Lead = item.Field<string>("CM_Hidden_Info_to_the_Lead"),
                //      CM_City = item.Field<string>("CM_City"),
                //      CM_Country = item.Field<string>("CM_Country"),
                //      CM_Zip = item.Field<string>("CM_Zip"),


                //      CM_IsActive = item.Field<Boolean?>("CM_IsActive"),

                //  }).ToList();


            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }

            return objDynamic;
        }


        private DataSet Get_User_Details(Email_User_Detail_DTO model)
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
