using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.Util;
using TattoAPI.IRepository;
using TattoAPI.Models.Avigma;
using TattoAPI.Repository;
using TattoAPI.Repository.Lib;
using TattoAPI.Repository.Lib.Security;
using Azure.Core;
using ExcelDataReader.Log;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Formatting;
using System.Numerics;
using System.Security.Claims;
using System.Configuration;
using System.Web;
using System.IO;
using TattoAPI.Models;
using TattoAPI.Repository.Avigma;
using TattoAPI.Models.Project;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using TattoAPI.Repository.Project;

namespace TattoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class TattoController : BaseController
    {
        SecurityHelper securityHelper = new SecurityHelper();
        FileUploadAwsBukcet fileUploadOnAWS = new FileUploadAwsBukcet();

        TattoAPI.Repository.Lib.Log log = new TattoAPI.Repository.Lib.Log();
        string msg = string.Empty;
        

        private readonly ITattoService _tattoService;
        //private readonly IUserMaster_Data _UserMasterData;
        //private readonly IUser_Admin_Master_Data _User_Admin_Master_Data;
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TattoController> _logger;
        private readonly INotification_Data _notificationService;
        private readonly IUnitOfWork _uof;


        public TattoController(ITattoService tattoService,IAmazonS3 s3Client, IConfiguration configuration, ILogger<TattoController> logger, INotification_Data notificationService, IUnitOfWork uof)
        {
            _tattoService = tattoService;
            _s3Client = s3Client;
            _configuration = configuration;
            _logger = logger;
            //_UserMasterData = userMaster_Data;
            //_User_Admin_Master_Data = user_Admin_Master_Data;
            _notificationService = notificationService;
            _uof = uof;
        }


        [HttpPost]
        [AllowAnonymous]
        [Authorize]
        [Route("Log")]
        public IActionResult Index()
        {
            log.logDebugMessage("Log Message from Debug Method");
            log.logErrorMessage("Log Message from Error Method");
            log.logInfoMessage("Log Message from Info Method");

            return Ok();

        }


        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("sendNotification")]
        public async Task<IActionResult> SendNotification(NotificationModel notificationModel)
        {
            var result = await _notificationService.SendNotification(notificationModel);
            return Ok(result);
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetTattoQuestionMasterData")]
        public async Task<List<dynamic>> GetTattoQuestionMasterData(Tatto_Question_Master_DTO_Input model)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();
            Tatto_Question_Master_DTO Data = new Tatto_Question_Master_DTO();

            try
            {
                //Data.Type = TattoLocation.Type;
                //Data.TQM_PKeyID = TattoLocation.TQM_PKeyID;
                //Data.UserID = LoggedInUserId;
                model.UserID = LoggedInUserId;

                var Output = await Task.Run(() => _uof.tatto_Question_Master_Data.Get_TattoQuestionMasterDetailsDTO(model));
                objdynamicobj.Add(Output);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }


        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateTattoStyleData")]
        public async Task<List<dynamic>> AddTattoStyleData(Tatto_Style_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                Data.UserID = LoggedInUserId;
                var result = await Task.Run(() => _uof.tatto_Style_Data.AddUpdateTattoStyle_Data(Data));

                return result;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetTattoStyleData")]
        public async Task<List<dynamic>> GetTattoStyleData(Tatto_Style_DTO_Input InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                InputData.UserID = LoggedInUserId;

                var result = await Task.Run(() => _uof.tatto_Style_Data.Get_TattoStyleDetailsDTO(InputData));
                objdynamicobj.Add(result);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateTattoArtistData")]
        public async Task<List<dynamic>> AddTattoArtistData(Tatto_Artist_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                Data.UserID = LoggedInUserId;

                var result = await Task.Run(() => _uof.tatto_Artist_Data.AddUpdateTattoArtist_Data(Data));

                return result;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetTattoArtistData")]
        public async Task<List<dynamic>> GetTattoArtistData(Tatto_Artist_DTO_Input InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();
            //Tatto_Artist_DTO Data = new Tatto_Artist_DTO();

            try
            {
                //Data.Type = InputData.Type;
                //Data.TA_PKeyID = InputData.TA_PKeyID;
                //Data.UserID = LoggedInUserId;
                InputData.UserID = LoggedInUserId;

                var result = await Task.Run(() => _uof.tatto_Artist_Data.Get_TattoArtistDetailsDTO(InputData));
                objdynamicobj.Add(result);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }


        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateClientTattoLocationData")]
        public async Task<List<dynamic>> AddClientTattoLocationData(Client_Tatto_Location_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                Data.UserID = LoggedInUserId;
                var result = await Task.Run(() => _uof.client_Tatto_Location_Data.AddUpdateClientTattoLocation_Data(Data));

                return result;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetClientTattoLocationData")]
        public async Task<List<dynamic>> GetClientTattoLocationData(Client_Tatto_Location_DTO_Input InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();
            //Client_Tatto_Location_DTO Data = new Client_Tatto_Location_DTO();

            try
            {
                //Data.Type = TattoLocation.Type;
                //Data.CTL_PKeyID = TattoLocation.CTL_PKeyID;
                //Data.UserID = LoggedInUserId;
                InputData.UserID = LoggedInUserId;

                var result = await Task.Run(() => _uof.client_Tatto_Location_Data.Get_ClientTattoLocationDetailsDTO(InputData));
                objdynamicobj.Add(result);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }


        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateClientTattoImageData")]
        public async Task<List<dynamic>> AddClientTattoImageData(Client_Tatto_Image_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                Data.UserID = LoggedInUserId;
                var TattoImageDetails = await Task.Run(() => _uof.client_Tatto_Image_Data.AddUpdateClientTattoImage_Data(Data));

                return TattoImageDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetClientTattoImageData")]
        public async Task<List<dynamic>> GetClientTattoImageData(Client_Tatto_Image_DTO_Input InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                InputData.UserID = LoggedInUserId;

                var TattoImageDetails = await Task.Run(() => _uof.client_Tatto_Image_Data.Get_ClientTattoImageDetailsDTO(InputData));
                objdynamicobj.Add(TattoImageDetails);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }


        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateTattoImageData")]
        public async Task<List<dynamic>> AddTattoImageData(Tatto_Image_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                Data.TI_User_PKeyID = LoggedInUserId;
                var TattoImageDetails = await Task.Run(() => _uof.tatto_Image_Data.AddUpdateTattoImage_Data(Data));

                return TattoImageDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetTattoImageData")]
        public async Task<List<dynamic>> GetTattoImageData(Tatto_Image_DTO_Input InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                InputData.UserID = LoggedInUserId;

                var TattoImageDetails = await Task.Run(() => _uof.tatto_Image_Data.Get_TattoImageDetailsDTO(InputData));
                objdynamicobj.Add(TattoImageDetails);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateTattoClientMasterData")]
        public async Task<List<dynamic>> AddTattoClientMasterData(Tatto_Client_Master_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                Data.TCM_User_PKeyID = LoggedInUserId;
                var TattoClientMasterDetails = await Task.Run(() => _uof.tatto_CLient_Master_Data.AddUpdateTattoClientMaster_Data(Data));

                return TattoClientMasterDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetTattoClientMasterData")]
        public async Task<List<dynamic>> GetTattoClientMasterData(Tatto_Client_Master_DTO_Input InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                InputData.UserID = LoggedInUserId;

                var TattoClientMasterDetails = await Task.Run(() => _uof.tatto_CLient_Master_Data.Get_TattoClientMasterDetailsDTO(InputData));
                objdynamicobj.Add(TattoClientMasterDetails);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateClientMasterData")]
        public async Task<List<dynamic>> AddClientMasterData(Client_Master_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();
            Convert_Json_Array convert_Json_Array = new Convert_Json_Array();
            try
            {
                Client_Master_DTO result = new Client_Master_DTO();
                Data.CM_User_PKeyID = LoggedInUserId;
                if (Data.CM_Quest_Json != null)
                {
                    result = convert_Json_Array.ConvertJsonArray(Data);
                }
                else
                {
                    result = Data;
                }

                var ClientMasterDetails = await Task.Run(() => _uof.client_Master_Data.AddUpdateClientMaster_Data(result));

                return ClientMasterDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetClientMasterData")]
        public async Task<List<dynamic>> GetClientMasterData(Client_Master_DTO_Input InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                InputData.UserID = LoggedInUserId;

                var ClientMasterDetails = await Task.Run(() => _uof.client_Master_Data.Get_ClientMasterDetailsDTO(InputData));
                objdynamicobj.Add(ClientMasterDetails);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }



        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateTattoMasterData")]
        public async Task<List<dynamic>> AddTattoMasterData(TattoMaster_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                Data.TM_User_PKeyID = LoggedInUserId;
                var tattoMasterDetails = await Task.Run(() => _uof.tattoMaster_Data.AddTattoMaster_Data(Data));

                return tattoMasterDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetTattoMasterData")]
        public async Task<List<dynamic>> GetTattoMasterData(TattoMaster_DTO_Input InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                InputData.UserID = LoggedInUserId;

                var tattoMasterDetails = await Task.Run(() => _uof.tattoMaster_Data.Get_TattoMasterDetailsDTO(InputData));
                objdynamicobj.Add(tattoMasterDetails);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }



        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateUserMasterData")]
        public async Task<List<dynamic>> AddUserMasterData(UserMaster_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                Data.UserID = LoggedInUserId;
                if (Data.Type != 1)
                {
                    if (Data.Type == 8)
                    {
                        Data.Type = 2;
                    }
                    else if (Data.Type != 9 && Data.Type != 4)
                    {
                        Data.User_PkeyID = LoggedInUserId;
                    }
                    else
                    {
                        //Data.User_PkeyID = LoggedInUserId;
                    }

                }
                var userMasterDetails = await Task.Run(() => _uof.userMaster_Data.AddUserMaster_Data(Data));

                return userMasterDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetUserMasterData")]
        public async Task<List<dynamic>> GetUserMasterData(UserMaster_DTO_Input UserMaster)
        {
            
            List<dynamic> objdynamicobj = new List<dynamic>();
            UserMaster_DTO Data = new UserMaster_DTO();

            try
            {
                Data.Type = UserMaster.Type;
                Data.User_PkeyID = UserMaster.User_PkeyID;
                Data.User_PkeyID_Master = UserMaster.User_PkeyID_Master;
                Data.UserID = LoggedInUserId;
                if (Data.Type == 2)
                {
                    Data.User_PkeyID = LoggedInUserId;
                }
                else
                {
                    if (Data.Type != 1)
                    {
                        if (Data.Type == 4)
                        {
                            Data.Type = 2;
                        }
                        else
                        {
                            // Data.User_PkeyID = LoggedInUserId;
                        }

                    }
                }

                var userMasterDetails = await Task.Run(() => _uof.userMaster_Data.Get_UserMasterDetails(Data));

                return userMasterDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("DeleteUserMaster")]
        public async Task<List<dynamic>> DeleteUserMaster(UserMaster_DTO_Input UserMaster)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                UserMaster_DTO Data = new UserMaster_DTO();
                UserMaster_Data userMaster_Data = new UserMaster_Data();
                //System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                //System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                //System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                //string s = reader.ReadToEnd();
                //log.logDebugMessage(s);
                //var Data = JsonConvert.DeserializeObject<UserMaster_DTO>(s);

                Data.Type = UserMaster.Type;
                Data.UserID = LoggedInUserId;
                Data.User_PkeyID = LoggedInUserId;
                var userMasterDetails = await Task.Run(() => userMaster_Data.DeleteUserMasterDetails(Data));

                return userMasterDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateUserAdminMaster")]
        public async Task<List<dynamic>> CreateUpdate_User_Admin_Master(User_Admin_Master_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                Data.UserID = LoggedInUserId;

                var CreateUpdate_User_Admin_Master_DataDetails = await Task.Run(() => _uof.user_Admin_Master_Data.CreateUpdate_User_Admin_Master_DataDetails(Data));

                return CreateUpdate_User_Admin_Master_DataDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("CreateUpdateUserAdminMaster");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }


        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetUserAdminMaster")]
        public async Task<List<dynamic>> Get_User_Admin_Master(Int64? Ad_User_PkeyID, Int64? UserID,int? Type)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                UserID = LoggedInUserId;

                var Get_User_Admin_MasterDetails = await Task.Run(() => _uof.user_Admin_Master_Data.Get_User_Admin_MasterDetails(Ad_User_PkeyID, UserID, Type));

                return Get_User_Admin_MasterDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("GetUserAdminMaster");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }


        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("qrCodeGenerate")]
        public IActionResult QR_CodeGenerate(QRCodeModelDTO qRCodeModelDTO)
        {
            QRCodeGenerator qrGen = new QRCodeGenerator(_configuration);
            var aa = qrGen.GenerateQRImage(qRCodeModelDTO);



            return Ok(new { aa });

        }

        [AllowAnonymous]
        [HttpGet("Admin")]
        [Authorize]
        public IActionResult Admin() 
        {
            var CurrentUser = GetCurrentUser();
            return Ok($"Hi, { CurrentUser.FirstName}, You are an {CurrentUser.Role}");
        }

        [AllowAnonymous]
        [Authorize]
        [HttpGet("getproductlist")]
        public async Task<List<Product>> GetProductListAsync()
        {
            try
            {
                //var UserID = LoggedInUserId;

                return await _tattoService.GetProductListAsync();
                //return await _repoWrapper.Product.FindAll().ToListAsync();
            }
            catch(Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                return null;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpGet("getproductbyid")]
        public async Task<IEnumerable<Product>> GetProductByIdAsync(int ProductId)
        {
            try
            {
                var response = await _tattoService.GetProductByIdAsync(ProductId);
                //var response = await _repoWrapper.Product.FindByCondition(Id);

                if (response == null)
                {
                    return null;
                }

                return response;
            }
            catch(Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                return null;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost("addproduct")]
        public async Task<IActionResult> AddProductAsync(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            try
            {
                var response = await _tattoService.AddProductAsync(product);

                return Ok(response);
            }
            catch(Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                return null;
            }
        }
        [AllowAnonymous]
        [Authorize]
        [HttpPut("updateproduct")]
        public async Task<IActionResult> UpdateProductAsync(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await _tattoService.UpdateProductAsync(product);
                return Ok(result);
            }
            catch(Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                return null;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpDelete("deleteproduct")]
        public async Task<int> DeleteProductAsync(int Id)
        {
            try
            {
                var response = await _tattoService.DeleteProductAsync(Id);
                return response;
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                return 0;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpGet("getuserbyid")]
        public async Task<IEnumerable<UserDisplay>> GetUserByIdAsync()
        {
            try
            {
                var response = await _tattoService.GetUserByIdAsync();
                //var response = await _repoWrapper.Product.FindByCondition(Id);

                if (response == null)
                {
                    return null;
                }

                return response;
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                return null;
            }
        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost("adduser")]
        public async Task<IActionResult> AddUserAsync(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            try
            {
                var response = await _tattoService.AddUserAsync(user);

                return Ok(response);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                return null;
            }
        }

        //[HttpGet("getuser")]
        //public async Task<IEnumerable<UserDisplay>> GetUserAsync()
        //{
        //    try
        //    {
        //        var response = await _fashionService.Gets();
        //        List<UserDisplay> myList = JsonConvert.DeserializeObject<List<UserDisplay>>(JsonConvert.SerializeObject(response));

        //        //var response = await _repoWrapper.Product.FindByCondition(Id);

        //        if (response == null)
        //        {
        //            return null;
        //        }

        //        return myList;
        //    }
        //catch(Exception ex)
        //{
        //    log.logErrorMessage(ex.Message);
        //    log.logErrorMessage(ex.StackTrace);
        //}
        //}

        //[HttpGet("getUserProduct")]
        //public async Task<object> GetUserAsync()
        //{
        //    try
        //    {
        //        UserDisplay us = new UserDisplay();
        //        var response = await _fashionService.Gets();

        //        //List<UserDisplay> myList = JsonConvert.DeserializeObject<List<UserDisplay>>(JsonConvert.SerializeObject(response));

        //        //us.Products = response[0].Products;

        //        if (response == null)
        //        {
        //            return null;
        //        }

        //        return response;
        //    }
        //catch(Exception ex)
        //{
        //    log.logErrorMessage(ex.Message);
        //    log.logErrorMessage(ex.StackTrace);
        //}
        //}

        [HttpPost("ExcelUpload")]
        public async Task<IActionResult> UploadExcelFile([FromForm] UploadExcelFileRequest request)
        {
            UploadExcelFileResponse response = new UploadExcelFileResponse();
            string Path = "UploadExcelFile/" + request.File.FileName;
            try
            {
                using (FileStream stream = new FileStream(Path, FileMode.CreateNew))
                {
                    await request.File.CopyToAsync(stream);
                }
                response = await _tattoService.UploadExcelFile(request, Path);

                string[] Files = Directory.GetFiles("UploadExcelFile/");
                foreach (string File in Files)
                {
                    System.IO.File.Delete(File);
                    Console.WriteLine($"{File} is Deleted");
                }
            }
            catch (Exception ex)
            {
                response.IsSucces = false;
                response.Message = ex.Message;
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);

            }
            return Ok(response);
        }

        // File Upload on AWS Server
        [AllowAnonymous]
        [Authorize]
        [HttpPost("UploadFileAmazonBuket")]
        public AmazonBucketDTO UploadFileAmazonBuket(AmazonBucketDTO amazonBucketDTO)
        {
            try
            {
                return fileUploadOnAWS.UploadFileAmazonBuket(amazonBucketDTO);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
                return null;
            }
        }
        //[HttpPost("uploadOnAWS")]
        //public async Task<AmazonBucketDTOResponse> UploadFileAmazonBuket([FromForm]AmazonBucketDTO amazonBucketDTO)
        //{
        //    AmazonBucketDTOResponse response= new AmazonBucketDTOResponse();

        //    try
        //    {
        //        var request = new PutObjectRequest()
        //        {
        //            BucketName = amazonBucketDTO.BucketName,
        //            Key = amazonBucketDTO.file.FileName,
        //            InputStream = amazonBucketDTO.file.OpenReadStream()
        //        };

        //        request.Metadata.Add("Content-Type", amazonBucketDTO.file.ContentType);


        //        var expiryUrlRequest = new GetPreSignedUrlRequest()
        //        {
        //            BucketName = amazonBucketDTO.BucketName,
        //            Key = amazonBucketDTO.file.FileName,
        //            Expires = DateTime.UtcNow.AddHours(1)
        //        };

        //        var responseObject = _s3Client.PutObjectAsync(request).Result;
        //        var s3Url = _s3Client.GetPreSignedURL(expiryUrlRequest);
        //        response.ReturnURL = s3Url;
        //        response.IsSuccess= true;
        //        response.Message = "File Uploaded Successfully";

        //    }
        //    catch (Exception ex)
        //    {

        //        response.IsSuccess= false;
        //        response.Message= ex.Message;
        //        response.ReturnURL = "";
        //        log.logErrorMessage(ex.StackTrace);
        //        log.logErrorMessage(ex.Message);

        //    }

        //    return response;
        //}


        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserModel
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    FirstName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    LastName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value                   
                };
            }
            return null;
        }


        [Route("GetHomeData")]
        [HttpPost]
        [Authorize]
        [AllowAnonymous]
        public async Task<dynamic> HomeData(UserMaster_DTO_Input InputData)
        {

            //UserMaster_DTO userMaster = new UserMaster_DTO();
            InputData.User_PkeyID = LoggedInUserId;
            InputData.UserID = LoggedInUserId;
            InputData.Type = InputData.Type;

            var result = _uof.userMaster_Data.Get_LoginUserDetails(InputData);
            return result;
        }


        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateTattoArtistFormData")]
        public async Task<List<dynamic>> AddTattoArtistFormData(Tatto_Artist_Form_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {

                Data.UserID = LoggedInUserId;
                var TattoArtistFormDetails = await Task.Run(() => _uof.tatto_Artist_Form_Data.AddUpdateTattoArtistForm_Data(Data));

                if (Data.Type == 1)
                {
                    var RandomNumber = TattoArtistFormDetails[2];
                    var TattoArtistFormUpdate = await Task.Run(() => _uof.tAF_QR_Generate_Upload.AddUpdateTattoArtistForm_QR_Code_Data(RandomNumber));
                    return TattoArtistFormUpdate;

                }
                return TattoArtistFormDetails;
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetTattoArtistFormData")]
        public async Task<List<dynamic>> GetTattoArtistFormData(Tatto_Artist_Form_DTO_Input InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();
            //Tatto_Artist_DTO Data = new Tatto_Artist_DTO();

            try
            {
                //Data.Type = InputData.Type;
                //Data.TA_PKeyID = InputData.TA_PKeyID;
                //Data.UserID = LoggedInUserId;
                InputData.UserID = LoggedInUserId;

                var result = await Task.Run(() => _uof.tatto_Artist_Form_Data.Get_TattoArtistFormDetailsDTO(InputData));
                objdynamicobj.Add(result);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }


        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateCustomerAppointmentsData")]
        public async Task<List<dynamic>> AddCustomerAppointmentsData(Customer_Appointments_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {

                Data.UserID = LoggedInUserId;
                var CustomerAppointmentsDetails = await Task.Run(() => _uof.customer_Appointments_Data.AddUpdateCustomerAppointments_Data(Data));

                return CustomerAppointmentsDetails;
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetCustomerAppointmentsData")]
        public async Task<List<dynamic>> GetCustomerAppointmentsData(Customer_Appointments_DTO_Input InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();
            //Tatto_Artist_DTO Data = new Tatto_Artist_DTO();

            try
            {

                InputData.UserID = LoggedInUserId;

                var result = await Task.Run(() => _uof.customer_Appointments_Data.Get_CustomerAppointmentsDetailsDTO(InputData));
                objdynamicobj.Add(result);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }


        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateClient_Reference_ImageData")]
        public async Task<List<dynamic>> AddClient_Reference_ImageData(Client_Reference_Image_DTO Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {

                Data.UserID = LoggedInUserId;
                var Client_Reference_ImageDetails = await Task.Run(() => _uof.client_Reference_Image_Data.AddUpdateClient_Reference_Image_Data(Data));

                return Client_Reference_ImageDetails;
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetClient_Reference_ImageData")]
        public async Task<List<dynamic>> GetClient_Reference_ImageData(Client_Reference_Image_DTO_Input InputData)
        {

            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                InputData.UserID = LoggedInUserId;

                var result = await Task.Run(() => _uof.client_Reference_Image_Data.Get_Client_Reference_ImageDetailsDTO(InputData));
                objdynamicobj.Add(result);

                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("ConvertBase64ToImage")]
        public async Task<List<dynamic>> ConvertBase64ToImage(ImageData Data)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();
            ImageGenerator imageGenerator = new ImageGenerator(_configuration);
            try
            {

                var ImagePath = imageGenerator.GetImagePath(Data);


                AmazonBucketDTO amazonBucketDTO = new AmazonBucketDTO();
                amazonBucketDTO.BucketName = _configuration["Bucket"];
                amazonBucketDTO.AccessKey = _configuration["accessKey"];
                amazonBucketDTO.SecretKey = _configuration["secretKey"];
                amazonBucketDTO.FilePath = _configuration["UserImagePath"]+"\\"+Data.Image_Path;
                amazonBucketDTO.FileName = Data.Image_Path;
                //amazonBucketDTO.keyName = qRCodeModelDTO.QRCodeImageFileName;

                var QR_Code_Path = fileUploadOnAWS.UploadFileAmazonBuket(amazonBucketDTO);
                Data.Image_Path = QR_Code_Path.ReturnURL;
                Data.Image_Name = QR_Code_Path.FileName;

                objdynamicobj.Add(Data);
                return objdynamicobj;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }
    }

}
