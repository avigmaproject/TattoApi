using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TattoAPI.Data;
using TattoAPI.Models;
using System.Dynamic;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using ExcelDataReader;
using ExcelDataReader.Log;
using TattoAPI.Models;
using log4net;
using TattoAPI.IRepository;
using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2;
using System.Drawing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.Common;
using Log = TattoAPI.Repository.Lib.Log;

namespace TattoAPI.Repository.Avigma
{
    public class TattoService : ITattoService
    {
        Log log = new Log();

        private readonly DbContextClass _dbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TattoService> _logger;


        public TattoService(DbContextClass dbContext, IConfiguration config, ILogger<TattoService> logger)
        {
            _dbContext = dbContext;
            _configuration = config;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
            _logger = logger;
        }
        public string ConnectionString { get; }

        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }


        // Below are old Method
        //public DataSet UserGet(UserMaster_DTO_Input userMaster, out string msg)
        //{
        //    msg = string.Empty;
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        SqlCommand com = new SqlCommand("Get_UserMaster", (SqlConnection)Connection);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.Parameters.AddWithValue("@User_PkeyID", userMaster.User_PkeyID);
        //        com.Parameters.AddWithValue("@User_PkeyID_Master", userMaster.User_PkeyID_Master);
        //        com.Parameters.AddWithValue("@Type", userMaster.Type);

        //        SqlDataAdapter da = new SqlDataAdapter(com);
        //        da.Fill(ds);
        //        msg = "Success";

        //    }
        //    catch (Exception ex)
        //    {
        //        msg = ex.Message;
        //        log.logErrorMessage(ex.StackTrace);
        //        log.logErrorMessage(ex.Message);

        //    }
        //    return ds;

        //}
        //public async Task<int> AddUserMasterDataAsync(UserMaster_DTO userMaster)
        //{
        //    try
        //    {
        //        var parameter = new List<SqlParameter>();
        //        parameter.Add(new SqlParameter("@User_PkeyID", SqlDbType.BigInt, 1, userMaster.User_PkeyID.ToString()));
        //        parameter.Add(new SqlParameter("@User_PkeyID_Master", SqlDbType.BigInt, 1, userMaster.User_PkeyID_Master.ToString()));
        //        parameter.Add(new SqlParameter("@User_Name", SqlDbType.VarChar, 50, userMaster.User_Name));
        //        parameter.Add(new SqlParameter("@User_Email", SqlDbType.NVarChar, 1000, userMaster.User_Email));
        //        parameter.Add(new SqlParameter("@User_Password", SqlDbType.NVarChar, 1000, userMaster.User_Password));
        //        parameter.Add(new SqlParameter("@User_Phone", SqlDbType.NVarChar, 1000, userMaster.User_Phone));
        //        parameter.Add(new SqlParameter("@User_Address", SqlDbType.NVarChar, 1000, userMaster.User_Address));
        //        parameter.Add(new SqlParameter("@User_City", SqlDbType.VarChar, 1000, userMaster.User_City));
        //        parameter.Add(new SqlParameter("@User_Country", SqlDbType.VarChar, 1000, userMaster.User_Country));
        //        parameter.Add(new SqlParameter("@User_Zip", SqlDbType.NVarChar, 1000, userMaster.User_Zip));
        //        parameter.Add(new SqlParameter("@User_DOB", SqlDbType.DateTime, 1000, userMaster.User_DOB.ToString()));
        //        parameter.Add(new SqlParameter("@User_Type", SqlDbType.Int, 10, userMaster.User_Type.ToString()));
        //        parameter.Add(new SqlParameter("@User_Gender", SqlDbType.Int, 10, userMaster.User_Gender.ToString()));
        //        parameter.Add(new SqlParameter("@User_Image_Path", SqlDbType.VarChar, 1000, userMaster.User_Image_Path));
        //        parameter.Add(new SqlParameter("@User_Image_Base", SqlDbType.VarChar, 1000, userMaster.User_Image_Base));
        //        parameter.Add(new SqlParameter("@User_MacID", SqlDbType.VarChar, 1000, userMaster.User_MacID));
        //        parameter.Add(new SqlParameter("@User_IsVerified", SqlDbType.Bit, 1000, userMaster.User_IsVerified.ToString()));
        //        parameter.Add(new SqlParameter("@User_IsActive", SqlDbType.Bit, 1000, userMaster.User_IsActive.ToString()));
        //        parameter.Add(new SqlParameter("@User_IsDelete", SqlDbType.Bit, 1000, userMaster.User_IsDelete.ToString()));
        //        parameter.Add(new SqlParameter("@Type", SqlDbType.Int, 1000, userMaster.Type.ToString()));
        //        parameter.Add(new SqlParameter("@UserID", SqlDbType.BigInt, 1000, userMaster.UserID.ToString()));
        //        parameter.Add(new SqlParameter("@User_latitude", SqlDbType.NVarChar, 1000, userMaster.User_latitude));
        //        parameter.Add(new SqlParameter("@User_longitude", SqlDbType.NVarChar, 1000, userMaster.User_longitude));
        //        parameter.Add(new SqlParameter("@User_Token_val", SqlDbType.NVarChar, 1000, userMaster.User_Token_val));
        //        parameter.Add(new SqlParameter("@User_Login_Type", SqlDbType.Int, 1000, userMaster.User_Login_Type.ToString()));
        //        parameter.Add(new SqlParameter("@User_IsActive_Prof", SqlDbType.Int, 1000, userMaster.User_IsActive_Prof.ToString()));
        //        parameter.Add(new SqlParameter("@User_LastName", SqlDbType.NVarChar, 500, userMaster.User_LastName));
        //        parameter.Add(new SqlParameter("@User_Company", SqlDbType.NVarChar, 500, userMaster.User_Company));
        //        parameter.Add(new SqlParameter("@User_PkeyID_Out", SqlDbType.NVarChar, 1000, null));
        //        parameter.Add(new SqlParameter("@ReturnValue", SqlDbType.NVarChar, 1000, null));


        //        string combinedString = string.Join(", ", parameter);
        //        var storedProc = "exec Create_UserMaster " + combinedString;

        //        var result = await Task.Run(() => _dbContext.Database
        //       .ExecuteSqlRawAsync(storedProc, parameter.ToArray()));

        //        return result;

        //    }
        //    catch (Exception ex)
        //    {

        //        log.logErrorMessage(ex.StackTrace);
        //        log.logErrorMessage(ex.Message);

        //    }
        //}


        //public async Task<IEnumerable<UserMaster_DTO>> Get_UserMaster()
        //{
        //    try
        //    {
        //        var productDetails = await Task.Run(() => _dbContext.userMasters
        //                            .FromSqlRaw(@"exec Get_UserMasterTest").ToListAsync());

        //        return productDetails;
        //    }
        //    catch (Exception ex)
        //    {

        //        log.logErrorMessage(ex.StackTrace);
        //        log.logErrorMessage(ex.Message);

        //    }



        //    //var parameter = new List<Object>();
        //    //parameter.Add(new SqlParameter("@User_PkeyID", User_PkeyID));
        //    //parameter.Add(new SqlParameter("@User_PkeyID_Master", User_PkeyID_Master));
        //    //parameter.Add(new SqlParameter("@Type", Type));

        //    //string combinedString = string.Join(", ", parameter);
        //    //var storedProc = "exec Get_UserMaster " + combinedString;





        //    ////var param = new SqlParameter("@Type", Type);

        //    ////var storedProc = "exec Get_UserMaster " + param;

        //    //var productDetails = await Task.Run(() => _dbContext.userMasters
        //    //        .FromSqlRaw(storedProc, parameter));

        //    //return productDetails;
        //}

        public async Task<List<Product>> GetProductListAsync()
        {
            return await _dbContext.Product
                .FromSqlRaw("GetPrductList")
                .ToListAsync();

        }

        public async Task<IEnumerable<Product>> GetProductByIdAsync(int ProductId)
        {

            var param = new SqlParameter("@ProductId", ProductId);
            var storedProc = "exec GetPrductByID " + param;

            var productDetails = await Task.Run(() => _dbContext.Product
                            .FromSqlRaw(storedProc, param).ToListAsync());

            //var productDetails = await Task.Run(() => _dbContext.Product
            //                .FromSqlRaw(@"exec GetPrductByID @ProductId", param).ToListAsync());

            return productDetails;
        }

        public async Task<int> AddProductAsync(Product product)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ProductName", product.ProductName));
            parameter.Add(new SqlParameter("@ProductDescription", product.ProductDescription));
            parameter.Add(new SqlParameter("@ProductPrice", product.ProductPrice));
            parameter.Add(new SqlParameter("@ProductStock", product.ProductStock));

            string combinedString = string.Join(", ", parameter);
            var storedProc = "exec AddNewProduct " + combinedString;

            var result = await Task.Run(() => _dbContext.Database
           .ExecuteSqlRawAsync(storedProc, parameter.ToArray()));

            return result;
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@ProductId", product.ProductId));
            parameter.Add(new SqlParameter("@ProductName", product.ProductName));
            parameter.Add(new SqlParameter("@ProductDescription", product.ProductDescription));
            parameter.Add(new SqlParameter("@ProductPrice", product.ProductPrice));
            parameter.Add(new SqlParameter("@ProductStock", product.ProductStock));


            string combinedString = string.Join(", ", parameter);
            var storedProc = "exec UpdateProduct " + combinedString;

            var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(storedProc, parameter.ToArray()));
            return result;
        }
        public async Task<int> DeleteProductAsync(int ProductId)
        {
            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"DeletePrductByID {ProductId}"));
        }


        public async Task<IEnumerable<UserDisplay>> GetUserByIdAsync()
        {
            try
            {

                var userDetails = await Task.Run(() => _dbContext.UsersDisplay
                                .FromSqlRaw(@"exec GetUserByID").ToListAsync());

                //UserDisplay userDisplay = new UserDisplay();

                //var param = new SqlParameter("@UserId", Id);
                //var storedProc = "exec GetUserByID " + param;

                //var userDetails = await Task.Run(() => _dbContext.UsersDisplay
                //                .FromSqlRaw(storedProc, param).ToListAsync());


                //var productDetails = await Task.Run(() => _dbContext.Product
                //                .FromSqlRaw(@"exec GetPrductByID @ProductId", param).ToListAsync());


                return userDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
                return null;
            }
        }

        public async Task<int> AddUserAsync(User user)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@UserName", user.Name));
            parameter.Add(new SqlParameter("@UserEmail", user.Email));
            parameter.Add(new SqlParameter("@UserPassword", user.Password));
            parameter.Add(new SqlParameter("@ProductId", user.ProductId));

            string combinedString = string.Join(", ", parameter);
            var storedProc = "exec AddNewUser " + combinedString;

            var result = await Task.Run(() => _dbContext.Database
           .ExecuteSqlRawAsync(storedProc, parameter.ToArray()));

            return result;
        }



        //public async Task<IEnumerable<UserMaster_DTO>> Get_UserMaster(UserMaster_DTO model)
        //{
        //    //List<dynamic> objdynamicobj = new List<dynamic>();
        //    var result= new List<UserMaster_DTO>();
        //    try
        //    {
        //        //var parameter = new List<SqlParameter>();

        //        //var selectProcedure = "[Get_UserMaster]";
        //        var input_parameters = new List<SqlParameter>();

        //        input_parameters.Add(new SqlParameter("@User_PkeyID", model.User_PkeyID));
        //        input_parameters.Add(new SqlParameter("@User_PkeyID_Master", model.User_PkeyID_Master));
        //        input_parameters.Add(new SqlParameter("@Type", model.Type));

        //        string combinedString = string.Join(", ", input_parameters);
        //        var storedProc = @"exec Get_UserMaster " + combinedString;

        //        //ds = obj.SelectSql(selectProcedure, input_parameters);
        //        result = await Task.Run(() => _dbContext.userMasters
        //   .FromSqlRaw<UserMaster_DTO>(storedProc, input_parameters.ToArray()).ToList());

        //        return result;
        //    }

        //    catch (Exception ex)
        //    {
        //        log.logErrorMessage(ex.StackTrace);
        //        log.logErrorMessage(ex.Message);
        //        return result;
        //    }

        //}

        // Below method for run multiple select query in single procedure

        //public async Task<object> Gets()
        //{
        //    dynamic obj = new ExpandoObject();
        //    using (IDbConnection con = Connection)
        //    {
        //        if (con.State == ConnectionState.Closed) con.Open();
        //        string query = @"GetUserByID";
        //        using (var multi = con.QueryMultiple(query, null, null, null, commandType: CommandType.StoredProcedure))

        //        //string query = @"select * from Product; Select * from Users";
        //        //using (var multi=con.QueryMultiple(query,null))
        //        {
        //            obj.Product = multi.Read<Product>().ToList();
        //            obj.User = multi.Read<User>().ToList();
        //        }
        //        var a = con.QueryMultiple("GetUserByID", commandType: CommandType.StoredProcedure);
        //        con.Close();
        //    }
        //    return obj;
        //}


        public async Task<UploadExcelFileResponse> UploadExcelFile(UploadExcelFileRequest request, string Path)
        {
            UploadExcelFileResponse response = new UploadExcelFileResponse();
            List<ExcelBulkUploadParameter> Parameter = new List<ExcelBulkUploadParameter>();
            response.IsSucces = true;
            response.Message = "Successfull";
            try
            {
                using (IDbConnection con = Connection)
                {

                    if (Connection.State == ConnectionState.Closed) con.Open();
                    if (request.File.FileName.ToLower().Contains(".xlsx") || request.File.FileName.ToLower().Contains(".xls"))
                    {
                        FileStream stream = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
                        DataSet dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            UseColumnDataType = false,
                            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });

                        for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                        {
                            ExcelBulkUploadParameter rows = new ExcelBulkUploadParameter();
                            rows.Particulars = dataSet.Tables[0].Rows[i].ItemArray[0] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[0]) : "-1";
                            rows.Au_2020 = dataSet.Tables[0].Rows[i].ItemArray[1] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[1]) : "-1";
                            rows.Au_2021 = dataSet.Tables[0].Rows[i].ItemArray[2] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[2]) : "-1";
                            rows.Au_2022 = dataSet.Tables[0].Rows[i].ItemArray[3] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[3]) : "-1";
                            rows.Au_2023 = dataSet.Tables[0].Rows[i].ItemArray[4] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[4]) : "-1";
                            rows.Au_2024 = dataSet.Tables[0].Rows[i].ItemArray[5] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[5]) : "-1";
                            rows.Au_2025 = dataSet.Tables[0].Rows[i].ItemArray[6] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[6]) : "-1";
                            rows.Au_2026 = dataSet.Tables[0].Rows[i].ItemArray[7] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[7]) : "-1";
                            rows.Au_2027 = dataSet.Tables[0].Rows[i].ItemArray[8] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[8]) : "-1";
                            Parameter.Add(rows);
                        }
                        stream.Close();
                        if (Parameter.Count > 0)
                        {
                            string sqlQuery = @"ExcelDataInsert";
                            foreach (ExcelBulkUploadParameter rows in Parameter)
                            {
                                //string query = @"GetUserByID";
                                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, (SqlConnection)con))
                                {
                                    sqlCommand.CommandType = CommandType.StoredProcedure;
                                    sqlCommand.CommandTimeout = 18000;
                                    sqlCommand.Parameters.AddWithValue("@Particulars", rows.Particulars);
                                    sqlCommand.Parameters.AddWithValue("@Au_2020", rows.Au_2020);
                                    sqlCommand.Parameters.AddWithValue("@Au_2021", rows.Au_2021);
                                    sqlCommand.Parameters.AddWithValue("@Au_2022", rows.Au_2022);
                                    sqlCommand.Parameters.AddWithValue("@Au_2023", rows.Au_2023);
                                    sqlCommand.Parameters.AddWithValue("@Au_2024", rows.Au_2024);
                                    sqlCommand.Parameters.AddWithValue("@Au_2025", rows.Au_2025);
                                    sqlCommand.Parameters.AddWithValue("@Au_2026", rows.Au_2026);
                                    sqlCommand.Parameters.AddWithValue("@Au_2027", rows.Au_2027);
                                    int status = await sqlCommand.ExecuteNonQueryAsync();
                                    if (status <= 0)
                                    {
                                        response.IsSucces = false;
                                        response.Message = "Query Not Executed";
                                        return response;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        response.IsSucces = false;
                        response.Message = "Incorrect File";
                    }
                }
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);

                response.IsSucces = false;
                response.Message = ex.Message;
            }
            finally
            {
                Connection.Close();
                Connection.Dispose();

            }
            return response;
        }



    }
}
