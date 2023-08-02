using System.Data;
using System.Data.SqlClient;
using TattoAPI.IRepository;
using TattoAPI.Models.Avigma;
using TattoAPI.Models.Project;
using TattoAPI.Repository.Lib;
using TattoAPI.Repository.Lib.Security;

namespace TattoAPI.Repository.Project
{
    public class Tatto_Style_Data : ITatto_Style_Data
    {
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        ObjectConvert obj = new ObjectConvert();

        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public Tatto_Style_Data()
        {
        }
        public Tatto_Style_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }


        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        public List<dynamic> AddUpdateTattoStyle_Data(Tatto_Style_DTO model)
        {
            string msg = string.Empty;

            List<dynamic> objData = new List<dynamic>();

            using (IDbConnection con = Connection)
            {
                if (con.State == ConnectionState.Closed) con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("CreateUpdate_TattoStyle", (SqlConnection)con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TS_PkeyID", model.TS_PKeyID);
                    cmd.Parameters.AddWithValue("@TS_Style_Name", model.TS_Style_Name);
                    cmd.Parameters.AddWithValue("@TS_Style_Description", model.TS_Style_Description);
                    cmd.Parameters.AddWithValue("@TS_CTI_PKeyID", model.TS_CTI_PKeyID);
                    cmd.Parameters.AddWithValue("@TS_CM_PKeyID", model.TS_CM_PKeyID);

                    cmd.Parameters.AddWithValue("@TS_IsActive", model.TS_IsActive);
                    cmd.Parameters.AddWithValue("@TS_IsDelete", model.TS_IsDelete);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    cmd.Parameters.AddWithValue("@UserID", model.UserID);

                    cmd.Parameters.AddWithValue("@TS_PkeyID_Out", 0).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@ReturnValue", 0).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    msg = "Tatto Style Data Add Successfully Done !";
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

        private DataSet Get_UserMaster(Tatto_Style_DTO_Input TattoImage)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_TattoStyle", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TS_PkeyID", TattoImage.TS_PKeyID);
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


        public List<dynamic> Get_TattoStyleDetailsDTO(Tatto_Style_DTO_Input model)
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
                //  select new Tatto_Style_DTO
                //  {
                //      TS_PKeyID = item.Field<Int64>("TS_PkeyID"),
                //      TS_Style_Name = item.Field<string>("TS_Style_Name"),
                //      TS_Style_Description = item.Field<string>("TS_Style_Description"),
                //      TS_CTI_PKeyID = item.Field<Int64>("TS_CTI_PKeyID"),
                //      TS_CM_PKeyID = item.Field<Int64>("TS_CM_PKeyID"),
                //      TS_IsActive = item.Field<Boolean?>("TS_IsActive"),

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
