using System.Data;
using System.Data.SqlClient;
using TattoAPI.IRepository;
using TattoAPI.Models.Avigma;
using TattoAPI.Models.Project;
using TattoAPI.Repository.Lib;
using TattoAPI.Repository.Lib.Security;

namespace TattoAPI.Repository.Project
{
    public class Client_Reference_Image_Data : IClient_Reference_Image_Data
    {
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        ObjectConvert obj = new ObjectConvert();

        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public Client_Reference_Image_Data()
        {
        }
        public Client_Reference_Image_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }


        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        public List<dynamic> AddUpdateClient_Reference_Image_Data(Client_Reference_Image_DTO model)
        {
            string msg = string.Empty;

            List<dynamic> objData = new List<dynamic>();

            using (IDbConnection con = Connection)
            {
                if (con.State == ConnectionState.Closed) con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("CreateUpdate_Client_Reference_Image", (SqlConnection)con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CRI_PKeyID", model.CRI_PKeyID);
                    cmd.Parameters.AddWithValue("@CRI_CM_PKeyID", model.CRI_CM_PKeyID);
                    cmd.Parameters.AddWithValue("@CRI_TAF_PKeyID", model.CRI_TAF_PKeyID);
                    cmd.Parameters.AddWithValue("@CRI_ImageName", model.CRI_ImageName);
                    cmd.Parameters.AddWithValue("@CRI_ImagePath", model.CRI_ImagePath);

                    cmd.Parameters.AddWithValue("@CRI_IsActive", model.CRI_IsActive);
                    cmd.Parameters.AddWithValue("@CRI_IsDelete", model.CRI_IsDelete);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    cmd.Parameters.AddWithValue("@UserID", model.UserID);

                    cmd.Parameters.AddWithValue("@CRI_PkeyID_Out", 0).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@ReturnValue", 0).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    msg = "Client Referenece Image Data Add Successfully Done !";
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

        private DataSet Get_UserMaster(Client_Reference_Image_DTO_Input TattoImage)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_Client_Reference_Image", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CRI_PKeyID", TattoImage.CRI_PKeyID);
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


        public List<dynamic> Get_Client_Reference_ImageDetailsDTO(Client_Reference_Image_DTO_Input model)
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
