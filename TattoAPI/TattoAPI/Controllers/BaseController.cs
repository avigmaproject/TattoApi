using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TattoAPI.Controllers
{
    public class BaseController : ControllerBase
    {

        public Int64 LoggedInUserId
        {
            get
            {
                Int64 id = 0;
                //var identity1 = HttpContext.User.Identity as ClaimsIdentity;
                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims1 = identity.Claims;
                for (int i = 0; i < claims1.ToList().Count; i++)
                {
                    if (claims1.ToList()[i].Type == "ID")
                    {
                        if (!string.IsNullOrEmpty(claims1.ToList()[i].Value))
                        {
                            string val = claims1.ToList()[i].Value;
                            //string ID = val.Split('_')[1];
                            id = Convert.ToInt64(val);
                            break;
                        }
                        else
                        {
                            id = -1;

                            id = Convert.ToInt64(claims1.ToList()[i].Value);
                        }
                    }
                    

                }
                //var claims = (Request.GetRequestContext().Principal as ClaimsPrincipal).Claims;
                //if (claims != null && claims.ToList().Exists(u => u.Type == "Id"))
                //    return Convert.ToInt32(claims.Where(u => u.Type == "Id").First().Value);
                //else

                return id;

            }
        }
    }
}
