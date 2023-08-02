using System.Data;
using System.Data.SqlClient;
using TattoAPI.IRepository;
using TattoAPI.Models.Avigma;
using TattoAPI.Models.Project;
using TattoAPI.Repository.Lib;
using TattoAPI.Repository.Lib.Security;

namespace TattoAPI.Repository.Project
{
    public class Tatto_Question_Master_Data : ITatto_Question_Master_Data
    {
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        ObjectConvert obj = new ObjectConvert();

        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public Tatto_Question_Master_Data()
        {
        }
        public Tatto_Question_Master_Data(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }


        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        //public List<dynamic> AddUpdateTattoStyle_Data(Tatto_Question_Master_DTO model)
        //{
        //    string msg = string.Empty;

        //    List<dynamic> objData = new List<dynamic>();

        //    using (IDbConnection con = Connection)
        //    {
        //        if (Connection.State == ConnectionState.Closed) con.Open();

        //        try
        //        {
        //            SqlCommand cmd = new SqlCommand("CreateUpdate_TattoStyle", (SqlConnection)con);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@TQM_PkeyID", model.TQM_PKeyID);
        //            cmd.Parameters.AddWithValue("@TQM_Style_Name", model.TQM_Style_Name);
        //            cmd.Parameters.AddWithValue("@TQM_Style_Description", model.TQM_Style_Description);
        //            cmd.Parameters.AddWithValue("@TQM_CTI_PKeyID", model.TQM_CTI_PKeyID);
        //            cmd.Parameters.AddWithValue("@TQM_CM_PKeyID", model.TQM_CM_PKeyID);

        //            cmd.Parameters.AddWithValue("@TQM_IsActive", model.TQM_IsActive);
        //            cmd.Parameters.AddWithValue("@TQM_IsDelete", model.TQM_IsDelete);
        //            cmd.Parameters.AddWithValue("@Type", model.Type);
        //            cmd.Parameters.AddWithValue("@UserID", model.UserID);

        //            cmd.Parameters.AddWithValue("@TQM_PkeyID_Out", 0).Direction = ParameterDirection.Output;
        //            cmd.Parameters.AddWithValue("@ReturnValue", 0).Direction = ParameterDirection.Output;

        //            cmd.ExecuteNonQuery();
        //            msg = "Tatto Style Data Add Successfully Done !";
        //            objData.Add(msg);
        //        }
        //        catch (Exception ex)
        //        {
        //            log.logErrorMessage(ex.StackTrace);
        //            log.logErrorMessage(ex.Message);
        //            msg = ex.Message;
        //        }
        //        finally
        //        {
        //            if (Connection.State == ConnectionState.Open)
        //            {
        //                con.Close();
        //            }
        //        }
        //    }
        //    return objData;
        //}

        private DataSet Get_Data(Tatto_Question_Master_DTO_Input TattoImage)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("Get_TattoQuestionMaster", (SqlConnection)Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TQM_PkeyID", TattoImage.TQM_PKeyID);
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


        public List<dynamic> Get_TattoQuestionMasterDetailsDTO(Tatto_Question_Master_DTO_Input model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {


                DataSet ds = Get_Data(model);

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
                //  select new Tatto_Question_Master_DTO
                //  {
                //      TQM_PKeyID = item.Field<Int64>("TQM_PkeyID"),
                //      TQM_Style_Name = item.Field<string>("TQM_Style_Name"),
                //      TQM_Style_Description = item.Field<string>("TQM_Style_Description"),
                //      TQM_CTI_PKeyID = item.Field<Int64>("TQM_CTI_PKeyID"),
                //      TQM_CM_PKeyID = item.Field<Int64>("TQM_CM_PKeyID"),
                //      TQM_IsActive = item.Field<Boolean?>("TQM_IsActive"),

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
