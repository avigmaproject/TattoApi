using System.Data;
using System.Data.SqlClient;
using TattoAPI.IRepository;
using TattoAPI.Models.Avigma;
using TattoAPI.Models;
using TattoAPI.Models.Project;
using TattoAPI.Repository.Lib;
using TattoAPI.Repository.Lib.Security;

namespace TattoAPI.Repository.Project
{
    public class TAF_QR_Generate_Upload : ITAF_QR_Generate_Upload
    {
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        ObjectConvert obj = new ObjectConvert();
        FileUploadAwsBukcet fileUploadOnAWS = new FileUploadAwsBukcet();

        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public TAF_QR_Generate_Upload()
        {
        }
        public TAF_QR_Generate_Upload(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }


        public IDbConnection Connection
        {
            get { return new SqlConnection(ConnectionString); }
        }

        public List<dynamic> AddUpdateTattoArtistForm_QR_Code_Data(int RandomNumber)
        {
            var URL = _configuration["TAFUrl"] + RandomNumber;
            var model = GenerateUploadQR(URL);
            model.TAF_RandomNumber = RandomNumber;
            //Tatto_Artist_Form_DTO model = new Tatto_Artist_Form_DTO();
            //model.TAF_QR_Code_DBPath = outPut[0].
            model.Type = 5;

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
                    SqlParameter RandomNumberOut = cmd.Parameters.AddWithValue("@RandomNumber", 0);
                    RandomNumberOut.Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    msg = "Tatto Artist form ID is " + TAF_Pkey_Out.Value + " Data Added Successfully !";
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

        private Tatto_Artist_Form_DTO GenerateUploadQR(String URL) 
        {
            List<dynamic> objData = new List<dynamic>();
            Tatto_Artist_Form_DTO tatto_Artist_Form_DTO = new Tatto_Artist_Form_DTO();
            QRCodeModelDTO qRCodeModelDTO = new QRCodeModelDTO();

            QRCodeGenerator qrGen = new QRCodeGenerator(_configuration);
            qRCodeModelDTO.QRCodeText = URL;
            qRCodeModelDTO.QRCodeHeigth = 100;
            qRCodeModelDTO.QRCodeWidth = 100;

            var aa = qrGen.GenerateQRImage(qRCodeModelDTO);

            AmazonBucketDTO amazonBucketDTO = new AmazonBucketDTO();
            amazonBucketDTO.BucketName = _configuration["Bucket"];
            amazonBucketDTO.AccessKey = _configuration["accessKey"];
            amazonBucketDTO.SecretKey = _configuration["secretKey"];
            amazonBucketDTO.FilePath = qRCodeModelDTO.QRCodeImagePath;
            amazonBucketDTO.FileName = qRCodeModelDTO.QRCodeImageFileName;
            //amazonBucketDTO.keyName = qRCodeModelDTO.QRCodeImageFileName;

            var QR_Code_Path = fileUploadOnAWS.UploadFileAmazonBuket(amazonBucketDTO);

            tatto_Artist_Form_DTO.TAF_QR_Code_DBPath = QR_Code_Path.ReturnURL;
            tatto_Artist_Form_DTO.TAF_QR_Code_URL = URL;
            objData.Add(tatto_Artist_Form_DTO);

            return tatto_Artist_Form_DTO;
        }

    }
}
