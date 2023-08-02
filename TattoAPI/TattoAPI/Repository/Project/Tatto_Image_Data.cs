using System.Data;
using System.Data.SqlClient;
using TattoAPI.IRepository;
using TattoAPI.Models.Avigma;
using TattoAPI.Models.Project;
using TattoAPI.Repository.Lib;
using TattoAPI.Repository.Lib.Security;

namespace TattoAPI.Repository.Project
{
    public class Tatto_Image_Data : ITatto_Image_Data
    {
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        ObjectConvert obj = new ObjectConvert();
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public Tatto_Image_Data()
        {
        }
        public Tatto_Image_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }


        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        public List<dynamic> AddUpdateTattoImage_Data(Tatto_Image_DTO model)
        {
            string msg = string.Empty;

            List<dynamic> objData = new List<dynamic>();

            using (IDbConnection con = Connection)
            {
                if (con.State == ConnectionState.Closed) con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("CreateUpdate_TattoImage", (SqlConnection)con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TI_PkeyID", model.TI_PKeyID);
                    cmd.Parameters.AddWithValue("@TI_TM_PKeyID", model.TI_TM_PKeyID);
                    cmd.Parameters.AddWithValue("@TI_TCM_PKeyID", model.TI_TCM_PKeyID);
                    cmd.Parameters.AddWithValue("@UserID", model.TI_User_PKeyID);
                    cmd.Parameters.AddWithValue("@TI_CM_PKeyID", model.TI_CM_PKeyID);
                    cmd.Parameters.AddWithValue("@TI_ImagePath", model.TI_ImagePath);
                    cmd.Parameters.AddWithValue("@TI_ImageName", model.TI_ImageName);
                    cmd.Parameters.AddWithValue("@TI_ImageType", model.TI_ImageType);
                    cmd.Parameters.AddWithValue("@TI_Description", model.TI_Description);
                    cmd.Parameters.AddWithValue("@TI_Image_Sequence", model.TI_Image_Sequence);
                    cmd.Parameters.AddWithValue("@TI_Price", model.TI_Price);


                    cmd.Parameters.AddWithValue("@TI_IsActive", model.TI_IsActive);
                    cmd.Parameters.AddWithValue("@TI_IsDelete", model.TI_IsDelete);
                    cmd.Parameters.AddWithValue("@Type", model.Type);

                    cmd.Parameters.AddWithValue("@TI_PkeyID_Out", 0).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@ReturnValue", 0).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    msg = "Tatto Image Data Add Successfully Done !";
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

        private DataSet Get_UserMaster(Tatto_Image_DTO_Input TattoImage)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_TattoImage", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TI_PkeyID", TattoImage.TI_PKeyID);
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


        public List<dynamic> Get_TattoImageDetailsDTO(Tatto_Image_DTO_Input model)
        {
            List<dynamic> objDynamic = new List<dynamic>(); try
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
                //  select new Tatto_Image_DTO
                //  {
                //      TI_PKeyID = item.Field<Int64>("TI_PkeyID"),
                //      TI_TM_PKeyID = item.Field<Int64>("TI_TM_PKeyID"),
                //      TI_TCM_PKeyID = item.Field<Int64>("TI_TCM_PKeyID"),
                //      TI_CM_PKeyID = item.Field<Int64>("TI_CM_PKeyID"),
                //      TI_User_PKeyID = item.Field<Int64>("TI_User_PKeyID"),
                //      TI_ImagePath = item.Field<String>("TI_ImagePath"),
                //      TI_ImageName = item.Field<String>("TI_ImageName"),
                //      TI_ImageType = item.Field<String>("TI_ImageType"),
                //      TI_Description = item.Field<String>("TI_Description"),
                //      TI_Image_Sequence = item.Field<int>("TI_Image_Sequence"),
                //      TI_Price = item.Field<Decimal?>("TI_Price"),
                //      TI_IsActive = item.Field<Boolean?>("TI_IsActive"),

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
