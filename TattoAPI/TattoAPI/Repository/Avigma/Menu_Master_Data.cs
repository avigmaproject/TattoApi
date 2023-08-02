using TattoAPI.Repository.Lib;
using TattoAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TattoAPI.Repository.Lib.Security;
using System.Data.SqlClient;
using TattoAPI.Models.Avigma;

namespace TattoAPI.Repository.Avigma
{
    public class Menu_Master_Data
    {
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }

        public Menu_Master_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }

        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        private List<dynamic> CreateUpdate_Menu_Master(Menu_Master_DTO model)
        {
            //List<dynamic> objData = new List<dynamic>();

            //string insertProcedure = "[CreateUpdate_Menu_Master]";

            //Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            string msg = string.Empty;

            List<dynamic> objData = new List<dynamic>();

            using (IDbConnection con = Connection)
            {
                if (Connection.State == ConnectionState.Closed) con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("CreateUpdate_Menu_Master", (SqlConnection)con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MU_PkeyID", model.MU_PkeyID);
                    cmd.Parameters.AddWithValue("@MU_Name", model.MU_Name);
                    cmd.Parameters.AddWithValue("@MU_Description", model.MU_Description);
                    cmd.Parameters.AddWithValue("@MU_IsActive", 1 + "#bit#" + model.MU_IsActive);
                    cmd.Parameters.AddWithValue("@MU_IsDelete", 1 + "#bit#" + model.MU_IsDelete);

                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    cmd.Parameters.AddWithValue("@UserID", model.UserID);
                    cmd.Parameters.AddWithValue("@MU_Pkey_Out", 0).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@ReturnValue", 0).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    msg = "Add Success";


                }
                catch (Exception ex)
                {
                    log.logErrorMessage(ex.StackTrace);
                    log.logErrorMessage(ex.Message);
                }
                return objData;

            }
        }

        private DataSet Get_Menu_Master(Menu_Master_DTO model)
        {
            DataSet ds = new DataSet();
            try
            {
                //string selectProcedure = "[Get_Menu_Master]";
                //Dictionary<string, string> input_parameters = new Dictionary<string, string>();
                SqlCommand cmd = new SqlCommand("Get_Menu_Master", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MU_PkeyID", model.MU_PkeyID);

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
        public List<dynamic> CreateUpdate_Menu_Master_DataDetails(Menu_Master_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = CreateUpdate_Menu_Master(model);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return objData;
        }

        public List<dynamic> Get_Menu_MasterDetails(Menu_Master_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_Menu_Master(model);

                var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                List<Menu_Master_DTO> Get_details =
                   (from item in myEnumerableFeaprd
                    select new Menu_Master_DTO
                    {
                        MU_PkeyID = item.Field<Int64>("MU_PkeyID"),
                        MU_Name = item.Field<String>("MU_Name"),
                        MU_Description = item.Field<String>("MU_Description"),
                        MU_IsActive = item.Field<Boolean?>("MU_IsActive"),

                    }).ToList();

                objDynamic.Add(Get_details);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }

            return objDynamic;





        }

    }
}