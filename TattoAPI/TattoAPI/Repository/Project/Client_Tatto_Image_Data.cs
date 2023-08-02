using System.Data;
using System.Data.SqlClient;
using TattoAPI.IRepository;
using TattoAPI.Models.Avigma;
using TattoAPI.Models.Project;
using TattoAPI.Repository.Lib;
using TattoAPI.Repository.Lib.Security;

namespace TattoAPI.Repository.Project
{
    public class Client_Tatto_Image_Data : IClient_Tatto_Image_Data
    {
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        ObjectConvert obj = new ObjectConvert();
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public Client_Tatto_Image_Data()
        {
        }
        public Client_Tatto_Image_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }


        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        public List<dynamic> AddUpdateClientTattoImage_Data(Client_Tatto_Image_DTO model)
        {
            string msg = string.Empty;

            List<dynamic> objData = new List<dynamic>();

            using (IDbConnection con = Connection)
            {
                if (con.State == ConnectionState.Closed) con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("CreateUpdate_ClientTattoImage", (SqlConnection)con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CTI_PkeyID", model.CTI_PKeyID);
                    cmd.Parameters.AddWithValue("@CTI_Skin_Tone", model.CTI_Skin_Tone);
                    cmd.Parameters.AddWithValue("@CTI_Size_CM", model.CTI_Size_CM);
                    cmd.Parameters.AddWithValue("@CTI_Size_Inch", model.CTI_Size_Inch);
                    cmd.Parameters.AddWithValue("@CTI_Description_Path", model.CTI_Description_Path);
                    cmd.Parameters.AddWithValue("@CTI_Description_Type", model.CTI_Description_Type);
                    cmd.Parameters.AddWithValue("@CTI_Description_FileName", model.CTI_Description_FileName);
                    cmd.Parameters.AddWithValue("@CTI_From_Date", model.CTI_From_Date);
                    cmd.Parameters.AddWithValue("@CTI_To_Date", model.CTI_To_Date);
                    cmd.Parameters.AddWithValue("@CTI_ImageName", model.CTI_ImageName);
                    cmd.Parameters.AddWithValue("@CTI_ImageType", model.CTI_ImageType);
                    cmd.Parameters.AddWithValue("@CTI_ImagePath", model.CTI_ImagePath);

                    cmd.Parameters.AddWithValue("@CTI_IsActive", model.CTI_IsActive);
                    cmd.Parameters.AddWithValue("@CTI_IsDelete", model.CTI_IsDelete);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    cmd.Parameters.AddWithValue("@UserID", model.UserID);

                    cmd.Parameters.AddWithValue("@CTI_PkeyID_Out", 0).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@ReturnValue", 0).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    msg = "Client Tatto Image Data Add Successfully Done !";
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

        private DataSet Get_UserMaster(Client_Tatto_Image_DTO_Input TattoImage)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_ClientTattoImage", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CTI_PkeyID", TattoImage.CTI_PKeyID);
                cmd.Parameters.AddWithValue("@Type", TattoImage.Type);

                cmd.Parameters.AddWithValue("@WhereClause", TattoImage.WhereClause);
                cmd.Parameters.AddWithValue("@PageNumber", TattoImage.PageNumber);
                cmd.Parameters.AddWithValue("@NoofRows", TattoImage.NoofRows);
                cmd.Parameters.AddWithValue("@Orderby", TattoImage.Orderby);
                cmd.Parameters.AddWithValue("@UserID", TattoImage.UserID);

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


        public List<dynamic> Get_ClientTattoImageDetailsDTO(Client_Tatto_Image_DTO_Input model)
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
                //TattoImage =
                // (from item in myEnumerableFeaprd
                //  select new Client_Tatto_Image_DTO
                //  {
                //      CTI_PKeyID = item.Field<Int64>("CTI_PkeyID"),
                //      CTI_Skin_Tone = item.Field<int>("CTI_Skin_Tone"),
                //      CTI_Size_CM = item.Field<decimal>("CTI_Size_CM"),
                //      CTI_Size_Inch = item.Field<decimal>("CTI_Size_Inch"),
                //      CTI_Description_Path = item.Field<string>("CTI_Description_Path"),
                //      CTI_Description_Type = item.Field<string>("CTI_Description_Type"),
                //      CTI_Description_FileName = item.Field<string>("CTI_Description_FileName"),
                //      CTI_From_Date = item.Field<DateTime>("CTI_From_Date"),
                //      CTI_To_Date = item.Field<DateTime>("CTI_To_Date"),
                //      CTI_ImagePath = item.Field<String>("CTI_ImagePath"),
                //      CTI_ImageName = item.Field<String>("CTI_ImageName"),
                //      CTI_ImageType = item.Field<String>("CTI_ImageType"),
                //      CTI_IsActive = item.Field<Boolean?>("CTI_IsActive"),

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
