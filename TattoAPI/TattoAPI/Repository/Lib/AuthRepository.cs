using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using TattoAPI.Models.Avigma;
using TattoAPI.Repository.Avigma;
using System.Data;

namespace TattoAPI.Repository.Lib
{
    #region
    public class AuthRepository
    {
        public Int64 ValidateUser(RootUserLogin model)
        {
            GetSetUser GetSetUser = new GetSetUser();
            Int64 retval = 0;
            Boolean IsVerified = false;
            List<dynamic> objDynamic = new List<dynamic>();

            if (model.Type == 90)
            {
                model.Type = 1;
                objDynamic = GetSetUser.GetAdminloginDetails(model);
                if (objDynamic[0].Count > 0)
                {
                    retval = objDynamic[0][0].intUSerId;
                    IsVerified = objDynamic[0][0].IsVerified;
                }
                else
                {
                    retval = 0;
                }
            }

            else
            {
                objDynamic = GetSetUser.GetloginDetails(model);
                if (objDynamic[0].Count > 0)
                {
                    retval = objDynamic[0][0].intUSerId;
                    IsVerified = objDynamic[0][0].IsVerified;
                    if (IsVerified == false && model.Type != 2 && retval != -99 && model.Type != 4)
                    {
                        retval = -90;
                    }
                }
                else
                {
                    retval = 0;
                }


                
            }



            return retval;
        }
    }

    //public class AuthRepository : IDisposable
    //{
    //    private AuthContext _ctx;

    //    private UserManager<IdentityUser> _userManager;
    //    Log log = new Log();

    //    public AuthRepository()
    //    {
    //        _ctx = new AuthContext();
    //        _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
    //    }

    //    public async Task<IdentityResult> RegisterUser(UserModel userModel)
    //    {
    //        IdentityUser user = new IdentityUser
    //        {
    //            UserName = userModel.UserName
    //        };

    //        try
    //        {
    //            var result = await _userManager.CreateAsync(user, userModel.Password);

    //            return result;
    //        }
    //        catch (Exception ex)
    //        {
    //            log.logErrorMessage(ex.Message);
    //            return null;
    //        }

    //    }

    //    public async Task<IdentityUser> FindUser(string userName, string password)
    //    {
    //        IdentityUser user = await _userManager.FindAsync(userName, password);

    //        return user;
    //    }

    //    public void Dispose()
    //    {
    //        _ctx.Dispose();
    //        _userManager.Dispose();

    //    }
    //}

    //public class UserModel
    //{

    //    public string UserName { get; set; }
    //    public string Password { get; set; }
    //    public string ConfirmPassword { get; set; }
    //}

    #endregion


    #region
    //public class AuthProvider : OAuthAuthorizationServerProvider
    //{
    //    public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
    //    {
    //        // Resource owner password credentials does not provide a client ID.
    //        if (context.ClientId == null)
    //        {
    //            context.Validated();
    //        }

    //        return Task.FromResult<object>(null);
    //    }

    //    public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
    //    {
    //        string MobileNum = context.UserName;
    //        string Otp = context.Password;
    //        int CountryId = 0;
    //        var data = await context.Request.ReadFormAsync();
    //        var obj = data.FirstOrDefault(c => c.Key == "CountryId").Value;
    //        if (!string.IsNullOrEmpty(obj[0].ToString()))
    //            CountryId = Convert.ToInt32(obj[0]);
    //        else
    //        {
    //            context.SetError("invalid_grant", "Invalid Country");
    //            return;
    //        }

    //        context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

    //        try
    //        {
    //            //TODO: remove this instances this should come from Capsule
    //            var builder = new ContainerBuilder();


    //            var container = builder.Build();


    //            int Id = 0;

    //            if (Id > 0)
    //            {
    //                ClaimsIdentity ClaimsInToken = new ClaimsIdentity(context.Options.AuthenticationType);
    //                ClaimsInToken.AddClaim(new Claim("Id", Id.ToString()));
    //                context.Validated(ClaimsInToken);
    //            }
    //            else if (Id < 0)
    //            {
    //                context.SetError("invalid_grant", "Timed Out");
    //                return;
    //            }
    //            else
    //            {
    //                context.SetError("invalid_grant", "Invalid OTP");
    //                return;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            context.SetError("Server Error", "Server error occured");
    //        }
    //    }

    //    public override Task TokenEndpoint(OAuthTokenEndpointContext context)
    //    {
    //        foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
    //        {
    //            context.AdditionalResponseParameters.Add(property.Key, property.Value);
    //        }

    //        return Task.FromResult<object>(null);
    //    }
    //}
    #endregion

}