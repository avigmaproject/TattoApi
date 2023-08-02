using TattoAPI.Repository.Lib;
using TattoAPI.Models.Avigma;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TattoAPI.Repository.Lib.Security;
using System.Data.SqlClient;

namespace TattoAPI.Repository.Avigma
{
    public class Get_User_Admin_Role_Data
    {

        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }

        public Get_User_Admin_Role_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }

        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }


        private DataSet Get_User_Admin_Role(User_Admin_Master_DTO model)
        {
            DataSet ds = new DataSet();
            try
            {
                //string selectProcedure = "[Get_User_Admin_Role]";
                //Dictionary<string, string> input_parameters = new Dictionary<string, string>();
                SqlCommand cmd = new SqlCommand("Get_User_Admin_Role", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", model.UserID);

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

        public List<dynamic> Get_User_Admin_RoleDetails(User_Admin_Master_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_User_Admin_Role(model);

                var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                List<User_Admin_Master_DTO> Get_details =
                   (from item in myEnumerableFeaprd
                    select new User_Admin_Master_DTO
                    {
                        Ad_User_PkeyID = item.Field<Int64>("Ad_User_PkeyID"),
                        Ad_User_Type = item.Field<int?>("Ad_User_Type"),

                    }).ToList();

                objDynamic.Add(Get_details);
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