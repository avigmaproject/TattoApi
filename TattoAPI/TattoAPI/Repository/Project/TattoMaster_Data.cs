using System.Data;
using System.Data.SqlClient;
using TattoAPI.IRepository;
using TattoAPI.Models.Avigma;
using TattoAPI.Models.Project;
using TattoAPI.Repository.Lib;
using TattoAPI.Repository.Lib.Security;

namespace TattoAPI.Repository.Project
{
    public class TattoMaster_Data : ITattoMaster_Data
    {
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        ObjectConvert obj = new ObjectConvert();

        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public TattoMaster_Data()
        {
        }
        public TattoMaster_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }


        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        private List<dynamic> AddUpdateTattoMaster_Data(TattoMaster_DTO model)
        {
            string msg = string.Empty;

            List<dynamic> objData = new List<dynamic>();

            using (IDbConnection con = Connection)
            {
                if (con.State == ConnectionState.Closed) con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("CreateUpdate_TattoMaster", (SqlConnection)con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TM_PkeyID", model.TM_PKeyID);
                    cmd.Parameters.AddWithValue("@TM_Name", model.TM_Name);
                    cmd.Parameters.AddWithValue("@TM_Description", model.TM_Description);
                    cmd.Parameters.AddWithValue("@TM_ImagePath", model.TM_ImagePath);
                    cmd.Parameters.AddWithValue("@TM_ImageName", model.TM_ImageName);
                    cmd.Parameters.AddWithValue("@TM_ImageType", model.TM_ImageType);
                    cmd.Parameters.AddWithValue("@UserID", model.TM_User_PKeyID);
                    cmd.Parameters.AddWithValue("@TM_Tatto_Image", model.TM_Tatto_Image);

                    cmd.Parameters.AddWithValue("@TM_CM_PkeyID", model.TM_CM_PKeyID);
                    cmd.Parameters.AddWithValue("@TM_TI_PkeyID", model.TM_TI_PKeyID);
                    cmd.Parameters.AddWithValue("@TM_Price", model.TM_Price);


                    cmd.Parameters.AddWithValue("@TM_IsActive", model.TM_IsActive);
                    cmd.Parameters.AddWithValue("@TM_IsDelete", model.TM_IsDelete);
                    cmd.Parameters.AddWithValue("@Type", model.Type);

                    cmd.Parameters.AddWithValue("@TM_PkeyID_Out", 0).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@ReturnValue", 0).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    msg = "Tatto Master Data Add Successfully Done !";
                    objData.Add(msg);
                }
                catch (Exception ex)
                {
                    log.logErrorMessage(ex.StackTrace);
                    log.logErrorMessage(ex.Message);
                    msg = ex.Message;
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

        public List<dynamic> AddTattoMaster_Data(TattoMaster_DTO model)
        {
            ImageGenerator imageGenerator = new ImageGenerator(_configuration);
            string imgPath = string.Empty;
            List<dynamic> objData = new List<dynamic>();
            try
            {
                if (model.Type == 6)
                {
                    if (!String.IsNullOrEmpty(model.TM_Image_Base))
                    {
                        imgPath = imageGenerator.Base64ToImage(model.TM_Image_Base);
                        model.TM_ImagePath = imgPath;
                        model.TM_Image_Base = string.Empty;
                    }
                }
                objData.Add(AddUpdateTattoMaster_Data(model));
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


        private DataSet Get_UserMaster(TattoMaster_DTO_Input tattoMaster)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_TattoMaster", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TM_PkeyID", tattoMaster.TM_PKeyID);
                cmd.Parameters.AddWithValue("@Type", tattoMaster.Type);

                cmd.Parameters.AddWithValue("@WhereClause", tattoMaster.WhereClause);
                cmd.Parameters.AddWithValue("@PageNumber", tattoMaster.PageNumber);
                cmd.Parameters.AddWithValue("@NoofRows", tattoMaster.NoofRows);
                cmd.Parameters.AddWithValue("@Orderby", tattoMaster.Orderby);
                cmd.Parameters.AddWithValue("@UserID", tattoMaster.UserID);

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


        public List<dynamic> Get_TattoMasterDetailsDTO(TattoMaster_DTO_Input model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {


                DataSet ds = Get_UserMaster(model);

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        objDynamic.Add(obj.AsDynamicEnumerable(ds.Tables[i]));
                    }

                }

                //var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                //TattoMaster =
                // (from item in myEnumerableFeaprd
                //  select new TattoMaster_DTO
                //  {
                //      TM_PKeyID = item.Field<Int64>("TM_PkeyID"),
                //      TM_Name = item.Field<String>("TM_Name"),
                //      TM_Description = item.Field<String>("TM_Description"),
                //      TM_ImagePath = item.Field<String?>("TM_ImagePath"),
                //      TM_ImageName = item.Field<String?>("TM_ImageName"),
                //      TM_ImageType = item.Field<String?>("TM_ImageType"),
                //      TM_User_PKeyID = item.Field<Int64>("TM_User_PKeyID"),
                //      TM_CM_PKeyID = item.Field<Int64?>("TM_CM_PKeyID"),
                //      TM_TI_PKeyID = item.Field<Int64?>("TM_TI_PKeyID"),
                //      TM_Price = item.Field<Decimal?>("TM_Price"),


                //      TM_Tatto_Image = item.Field<String>("Tatto_Image"),
                //      TM_IsActive = item.Field<Boolean?>("TM_IsActive"),

                //  }).ToList();


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
