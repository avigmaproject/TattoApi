using System.Data;
using System.Data.SqlClient;
using TattoAPI.IRepository;
using TattoAPI.Models.Avigma;
using TattoAPI.Models.Project;
using TattoAPI.Repository.Lib;
using TattoAPI.Repository.Lib.Security;

namespace TattoAPI.Repository.Project
{
    public class Tatto_Artist_Form_Data : ITatto_Artist_Form_Data
    {
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        ObjectConvert obj = new ObjectConvert();

        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public Tatto_Artist_Form_Data()
        {
        }
        public Tatto_Artist_Form_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }


        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        public List<dynamic> AddUpdateTattoArtistForm_Data(Tatto_Artist_Form_DTO model)
        {
            string msg = string.Empty;

            List<dynamic> objData = new List<dynamic>();

            using (IDbConnection con = Connection)
            {
                if (con.State == ConnectionState.Closed) con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("CreateUpdate_Tatto_Artist_Form", (SqlConnection)con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TAF_PkeyID", model.TAF_PKeyID);
                    cmd.Parameters.AddWithValue("@TAF_Quest_Control_Data", model.TAF_Quest_Control_Data);
                    cmd.Parameters.AddWithValue("@TAF_IsDefault", model.TAF_IsDefault);
                    cmd.Parameters.AddWithValue("@TAF_QR_Code", model.TAF_QR_Code_DBPath);
                    cmd.Parameters.AddWithValue("@TAF_URL", model.TAF_QR_Code_URL);

                    cmd.Parameters.AddWithValue("@TAF_IsActive", model.TAF_IsActive);
                    cmd.Parameters.AddWithValue("@TAF_IsDelete", model.TAF_IsDelete);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    cmd.Parameters.AddWithValue("@UserID", model.UserID);
                    cmd.Parameters.AddWithValue("@TAF_Code", model.TAF_RandomNumber);

                    SqlParameter TAF_Pkey_Out = cmd.Parameters.AddWithValue("@TAF_Pkey_Out", 0);
                    TAF_Pkey_Out.Direction = ParameterDirection.Output;
                    SqlParameter ReturnValue = cmd.Parameters.AddWithValue("@ReturnValue", 0);
                    ReturnValue.Direction = ParameterDirection.Output;
                    SqlParameter RandomNumber = cmd.Parameters.AddWithValue("@RandomNumber", 0);
                    RandomNumber.Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    //msg = TAF_Pkey_Out.Value.ToString();
                    objData.Add(TAF_Pkey_Out.Value);
                    objData.Add(ReturnValue.Value);
                    objData.Add(RandomNumber.Value);
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

        private DataSet Get_UserMaster(Tatto_Artist_Form_DTO_Input model)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_Tatto_Artist_Form", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TAF_PkeyID", model.TAF_PKeyID);
                cmd.Parameters.AddWithValue("@CM_PKeyID", model.CM_PKeyID);
                cmd.Parameters.AddWithValue("@TAF_Code", model.TAF_Code);
                cmd.Parameters.AddWithValue("@Type", model.Type);

                cmd.Parameters.AddWithValue("@WhereClause", model.WhereClause);
                cmd.Parameters.AddWithValue("@PageNumber", model.PageNumber);
                cmd.Parameters.AddWithValue("@NoofRows", model.NoofRows);
                cmd.Parameters.AddWithValue("@Orderby", model.Orderby);
                cmd.Parameters.AddWithValue("@UserID", model.UserID);

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


        public List<dynamic> Get_TattoArtistFormDetailsDTO(Tatto_Artist_Form_DTO_Input model)
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
                //  select new Tatto_Artist_Form_DTO
                //  {
                //      TAF_PKeyID = item.Field<Int64>("TAF_PkeyID"),
                //      TAF_CM_PKeyID = item.Field<Int64>("TAF_CM_PKeyID"),
                //      TAF_CTI_PKeyID = item.Field<Int64>("TAF_CTI_PKeyID"),
                //      TAF_UserID = item.Field<Int64>("TAF_UserID"),
                //      TAF_IsActive = item.Field<Boolean?>("TAF_IsActive"),

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
