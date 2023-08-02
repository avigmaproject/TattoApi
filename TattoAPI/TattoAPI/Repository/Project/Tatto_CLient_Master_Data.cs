using System.Data;
using System.Data.SqlClient;
using TattoAPI.IRepository;
using TattoAPI.Models.Avigma;
using TattoAPI.Models.Project;
using TattoAPI.Repository.Lib;
using TattoAPI.Repository.Lib.Security;

namespace TattoAPI.Repository.Project
{
    public class Tatto_CLient_Master_Data : ITatto_CLient_Master_Data
    {
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        ObjectConvert obj = new ObjectConvert();

        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public Tatto_CLient_Master_Data()
        {
        }
        public Tatto_CLient_Master_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }


        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        public List<dynamic> AddUpdateTattoClientMaster_Data(Tatto_Client_Master_DTO model)
        {
            string msg = string.Empty;

            List<dynamic> objData = new List<dynamic>();

            using (IDbConnection con = Connection)
            {
                if (con.State == ConnectionState.Closed) con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("CreateUpdate_TattoClientMaster", (SqlConnection)con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TCM_PkeyID", model.TCM_PKeyID);
                    cmd.Parameters.AddWithValue("@TCM_TM_PKeyID", model.TCM_TM_PKeyID);
                    cmd.Parameters.AddWithValue("@UserID", model.TCM_User_PKeyID);
                    cmd.Parameters.AddWithValue("@TCM_Status", model.TCM_Status);
                    cmd.Parameters.AddWithValue("@TCM_CM_PKeyID", model.TCM_TM_PKeyID);
                    cmd.Parameters.AddWithValue("@TCM_TI_PKeyID", model.TCM_TM_PKeyID);


                    cmd.Parameters.AddWithValue("@TCM_IsActive", model.TCM_IsActive);
                    cmd.Parameters.AddWithValue("@TCM_IsDelete", model.TCM_IsDelete);
                    cmd.Parameters.AddWithValue("@Type", model.Type);

                    cmd.Parameters.AddWithValue("@TCM_PkeyID_Out", 0).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@ReturnValue", 0).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    msg = "Tatto Client Master Data Add Successfully Done !";
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

        private DataSet Get_TattoClientMaster(Tatto_Client_Master_DTO_Input tattoMaster)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_Tatto_Client_Master", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TCM_PkeyID", tattoMaster.TCM_PKeyID);
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


        public List<dynamic> Get_TattoClientMasterDetailsDTO(Tatto_Client_Master_DTO_Input model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {


                DataSet ds = Get_TattoClientMaster(model);
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
                //  select new Tatto_Client_Master_DTO
                //  {
                //      TCM_PKeyID = item.Field<Int64>("TCM_PkeyID"),
                //      TCM_TM_PKeyID = item.Field<Int64>("TCM_TM_PkeyID"),
                //      TCM_User_PKeyID = item.Field<Int64>("TCM_User_PKeyID"),
                //      TCM_CM_PKeyID = item.Field<Int64?>("TCM_CM_PkeyID"),
                //      TCM_TI_PKeyID = item.Field<Int64?>("TCM_TI_PkeyID"),
                //      TCM_Status = item.Field<Boolean?>("TCM_Status"),
                //      TCM_IsActive = item.Field<Boolean?>("TCM_IsActive"),

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
