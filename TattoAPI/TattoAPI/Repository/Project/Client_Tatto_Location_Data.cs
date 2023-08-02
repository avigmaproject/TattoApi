using System.Data;
using System.Data.SqlClient;
using TattoAPI.IRepository;
using TattoAPI.Models.Avigma;
using TattoAPI.Models.Project;
using TattoAPI.Repository.Lib;
using TattoAPI.Repository.Lib.Security;

namespace TattoAPI.Repository.Project
{
    public class Client_Tatto_Location_Data : IClient_Tatto_Location_Data
    {
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        ObjectConvert obj = new ObjectConvert();

        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public Client_Tatto_Location_Data()
        {
        }
        public Client_Tatto_Location_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }


        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        public List<dynamic> AddUpdateClientTattoLocation_Data(Client_Tatto_Location_DTO model)
        {
            string msg = string.Empty;

            List<dynamic> objData = new List<dynamic>();

            using (IDbConnection con = Connection)
            {
                if (con.State == ConnectionState.Closed) con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("CreateUpdate_ClientTattoLocation", (SqlConnection)con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CTL_PkeyID", model.CTL_PKeyID);
                    cmd.Parameters.AddWithValue("@CTL_Location_Name", model.CTL_Location_Name);
                    cmd.Parameters.AddWithValue("@CTL_Location_Description", model.CTL_Location_Description);
                    cmd.Parameters.AddWithValue("@CTL_CM_PKeyID", model.CTL_CM_PKeyID);
                    cmd.Parameters.AddWithValue("@CTL_CTI_PKeyID", model.CTL_CTI_PKeyID);

                    cmd.Parameters.AddWithValue("@CTL_IsActive", model.CTL_IsActive);
                    cmd.Parameters.AddWithValue("@CTL_IsDelete", model.CTL_IsDelete);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    cmd.Parameters.AddWithValue("@UserID", model.UserID);

                    cmd.Parameters.AddWithValue("@CTL_PkeyID_Out", 0).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@ReturnValue", 0).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    msg = "Tatto Client Location Data Add Successfully Done !";
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

        private DataSet Get_UserMaster(Client_Tatto_Location_DTO_Input TattoImage)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_ClientTattoLocation", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CTL_PkeyID", TattoImage.CTL_PKeyID);
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


        public List<dynamic> Get_ClientTattoLocationDetailsDTO(Client_Tatto_Location_DTO_Input model)
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
                //  select new Client_Tatto_Location_DTO
                //  {
                //      CTL_PKeyID = item.Field<Int64>("CTL_PkeyID"),
                //      CTL_Location_Name = item.Field<string>("CTL_Location_Name"),
                //      CTL_Location_Description = item.Field<string>("CTL_Location_Description"),
                //      CTL_CM_PKeyID = item.Field<Int64>("CTL_CM_PKeyID"),
                //      CTL_CTI_PKeyID = item.Field<Int64>("CTL_CTI_PKeyID"),
                //      CTL_IsActive = item.Field<Boolean?>("CTL_IsActive"),

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
