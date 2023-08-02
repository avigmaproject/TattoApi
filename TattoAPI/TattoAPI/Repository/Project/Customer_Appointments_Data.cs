using System.Data;
using System.Data.SqlClient;
using TattoAPI.IRepository;
using TattoAPI.Models.Avigma;
using TattoAPI.Models.Project;
using TattoAPI.Repository.Lib;
using TattoAPI.Repository.Lib.Security;

namespace TattoAPI.Repository.Project
{
    public class Customer_Appointments_Data : ICustomer_Appointments_Data
    {
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        ObjectConvert obj = new ObjectConvert();
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public Customer_Appointments_Data()
        {
        }
        public Customer_Appointments_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }


        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        public List<dynamic> AddUpdateCustomerAppointments_Data(Customer_Appointments_DTO model)
        {
            string msg = string.Empty;

            List<dynamic> objData = new List<dynamic>();

            using (IDbConnection con = Connection)
            {
                if (con.State == ConnectionState.Closed) con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("CreateUpdate_Customer_Appointments", (SqlConnection)con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CA_PkeyID", model.CA_PKeyID);
                    cmd.Parameters.AddWithValue("@CA_Name", model.CA_Name);
                    cmd.Parameters.AddWithValue("@CA_Description", model.CA_Description);
                    cmd.Parameters.AddWithValue("@CA_Date", model.CA_Date);
                    cmd.Parameters.AddWithValue("@CA_Time", model.CA_Time);
                    cmd.Parameters.AddWithValue("@CA_CM_PKeyID", model.CA_CM_PKeyID);

                    cmd.Parameters.AddWithValue("@CA_IsActive", model.CA_IsActive);
                    cmd.Parameters.AddWithValue("@CA_IsDelete", model.CA_IsDelete);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    cmd.Parameters.AddWithValue("@UserID", model.UserID);

                    cmd.Parameters.AddWithValue("@CA_PkeyID_Out", 0).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@ReturnValue", 0).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    msg = "Customer Appointment Data Add Successfully Done !";
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

        private DataSet Get_UserMaster(Customer_Appointments_DTO_Input TattoImage)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_Customer_Appointments", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CA_PkeyID", TattoImage.CA_PKeyID);
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


        public List<dynamic> Get_CustomerAppointmentsDetailsDTO(Customer_Appointments_DTO_Input model)
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
