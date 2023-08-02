using TattoAPI.IRepository;
using TattoAPI.Models;
using TattoAPI.Repository.Lib;
using ExcelDataReader.Log;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TattoAPI.Models.Avigma;
using TattoAPI.Repository.Avigma;

namespace TattoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[EnableCors(,)]
    [EnableCors("AllowAll")]
    public class LoginController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginController> _logger;
        private readonly INotification_Data _notificationService;
        private readonly IUnitOfWork _uof;
        AuthRepository authRepository = new AuthRepository();


        public LoginController(IConfiguration configuration, ILogger<LoginController> logger, INotification_Data notificationService, IUnitOfWork uof)
        {
            _configuration = configuration;
            _logger = logger;
            _notificationService = notificationService;
            _uof = uof;
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Login(RootUserLoginRegistraion input)
        {
            RootUserLogin_input userLogin = new RootUserLogin_input();
            UserLogin userLogin2 = new UserLogin();
            userLogin.User_Name = input.User_Name; 
            userLogin.User_Email = input.User_Email;
            userLogin.User_Password = input.User_Password;
            userLogin.Type = input.Type;

        var user = Authenticate(userLogin);

            if (user != null)
            {
                if (user.User_PkeyID == 0 || user.User_PkeyID == -99 || user.User_PkeyID == -90)
                {
                    switch (user.User_PkeyID)
                    {
                        case 0: { return Ok("The UserCode or password is incorrect."); }
                        case -90: { return Ok("EmailID / Mobile Number is Not Verified."); }
                        case -99: { return Ok("The UserEmail Already Exist."); }
                    }
                }
                else
                {
                    var token = Generate(user);
                    if (userLogin.Type == 2)
                    {
                        GetSetUser getSetUser = new GetSetUser(_configuration);
                        EmailDTO emailDTO = new EmailDTO();
                        EmailTemplate emailTemplate = new EmailTemplate(_configuration);
                        emailDTO.FirstName = userLogin.User_Name;
                        emailDTO.To = userLogin.User_Email;

                        //var MasterDetails = Task.Run(() => emailTemplate.VerifiedRegistration(emailDTO, userLogin.User_OTP));

                        userLogin2.User_PkeyID = user.User_PkeyID;
                        userLogin2.Device = 1;
                        userLogin2.Email_Url = _configuration["sendEmailUrl"];
                        userLogin2.EmailID = userLogin.User_Email;
                        var MasterDetails = Task.Run(() => getSetUser.GetVerificationLink(userLogin2));
                    }

                    return Ok(token);

                }

            }
            return NotFound("User Not Found !");
        }

        private string Generate(UserMaster_DTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("ID", user.User_PkeyID.ToString())
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddYears(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserMaster_DTO Authenticate(RootUserLogin_input userLogin)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            int intUSerId = 0;
            //var user = _UserMasterData.Get(userLogin.Username);
            //var currentUser = UserConstant.User.FirstOrDefault(o => o.Username.ToLower() == userLogin.UserCode.ToLower() && o.Password.ToLower() == userLogin.Password.ToLower());
            DataSet ds = _uof.userMaster_Data.Get_UserMasterLogin(userLogin);

            if (ds.Tables.Count > 0)
            {
                var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                List<UserMaster_DTO> ViewLogin =
                   (from item in myEnumerableFeaprd
                    select new UserMaster_DTO
                    {
                        User_PkeyID = item.Field<Int64>("User_PkeyID"),
                        //User_Name = item.Field<String>("User_Name"),
                        User_IsVerified = item.Field<Boolean?>("User_IsVerified"),
                    }).ToList();
                objDynamic.Add(ViewLogin);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["User_PkeyID"].ToString()))
                    {
                        intUSerId = Convert.ToInt32(ds.Tables[0].Rows[i]["User_PkeyID"].ToString());
                    }

                }

                //Int64 isValid = authRepository.ValidateUser(rootUser);

                //Int64 isValid = authRepository.ValidateUser(model);

                var result = ViewLogin.FirstOrDefault();
                return result;
            }
            //if (currentUser != null) 
            //{
            //    return currentUser;
            //}
            return null;
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationModel notificationModel)
        {
            var result = await _notificationService.SendNotification(notificationModel);
            return Ok(result);
        }

    }
}
