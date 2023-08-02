using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Data;
using System;
using TattoAPI.Repository.Lib;
using TattoAPI.Models.Avigma;

namespace TattoAPI.Repository.Lib
{
    public class CustomAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        Log log = new Log();
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var form = await context.Request.ReadFormAsync();

           // context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            RootUserLogin model = new RootUserLogin();

            AuthRepository authRepository = new AuthRepository();

            log.logDebugMessage("----------------------Request Recived---------------------------");
            int loginType = 1;
            if (!string.IsNullOrWhiteSpace(form.Get("User_Login_Type")))
            {
                loginType = Convert.ToInt32(form.Get("User_Login_Type"));
            }

            if (!string.IsNullOrEmpty(form.Get("ClientId")))
            {
                model.Type = Convert.ToInt32(form.Get("ClientId"));
            }
            switch (model.Type)
            {
                
                case 1:
                case 3:
                case 4:
               
                    {
                        model.Um_Email = context.UserName;
                        model.Um_Password = context.Password;
                        model.User_latitude = form.Get("latitude");
                        model.User_longitude = form.Get("longitude");
                        model.User_Login_Type = loginType;
                        model.User_Token_val = form.Get("User_Token_val");
                        model.User_LastName = form.Get("User_LastName");
                        model.User_Company = form.Get("User_Company");
                        break;

                    }

                case 2:
                case 5: // For FaceBook and Gmail ,Apple
                case 6:
                    {
                        model.Um_Email = context.UserName;
                        model.Um_Password = context.Password; ;
                        model.Um_Name = form.Get("FirstName");
                        model.MobileNumber = form.Get("MobileNumber");
                        model.User_LastName = form.Get("User_LastName");
                        model.User_Company = form.Get("User_Company");
                        model.User_MacID = form.Get("IMEI");
                        model.User_latitude = form.Get("latitude");
                        model.User_longitude = form.Get("longitude");
                        model.User_Login_Type = loginType;
                        model.User_Token_val = form.Get("User_Token_val");
                        model.User_FB_GM_Token_val = form.Get("User_FB_GM_Token_val");
                        break;
                    }

                //Only For Admin Website login
                case 90:
                    {
                        model.Um_Email = context.UserName;
                        model.Um_Password = context.Password;

                        break;
                    }


            }
            Int64 isValid = authRepository.ValidateUser(model);

            if (isValid == 0 || isValid == -99 || isValid == -90)
            {
                switch (isValid)
                {
                    case 0:
                        {
                            context.SetError("0", "The UserCode or password is incorrect.");
                            break;
                        }
                    case -90:
                        {
                            context.SetError("-90", "EmailID / Mobile Number is Not Verified.");
                            break;
                        }

                    case -99:
                        {
                            context.SetError("-99", "The UserEmail Already Exist.");
                            break;
                        }
                   
                }
                
                return;
            }
            log.logDebugMessage("----------------------Response Given---------------------------");
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (model.Type == 90)
            {
                identity.AddClaim(new Claim("Id", "AD_"+isValid.ToString()));
            }
            else
            {
                identity.AddClaim(new Claim("Id", "UD_"+isValid.ToString()));
            }
          

            context.Validated(identity);

            #region comment
            //AuthRepository authRepository = new AuthRepository();
            //bool isValid = authRepository.ValidateUser(context.UserName, context.Password);
            //if (!isValid)
            //{
            //    context.SetError("invalid_grant", "The user name or password is incorrect.");
            //}


            //using (AuthRepository _repo = new AuthRepository())
            //{
            //    IdentityUser user = await _repo.FindUser(context.UserName, context.Password);

            //    if (user == null)
            //    {
            //        context.SetError("invalid_grant", "The user name or password is incorrect.");
            //        return;
            //    }
            //}


            //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            //identity.AddClaim(new Claim(ClaimTypes.Role, "User"));

            //context.Validated(identity);
            #endregion
        }
    }
}