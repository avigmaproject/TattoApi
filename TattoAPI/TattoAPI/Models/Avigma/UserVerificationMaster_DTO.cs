using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TattoAPI.Models.Avigma
{
    public class UserVerificationMaster_DTO
    {
        public Int64 UserVeriPKID { get; set; }
        public String User_LoginName { get; set; }
        public String UserVerificationID { get; set; }
        public Int64? UserID { get; set; }
        public String VerificationCode { get; set; }
        public Boolean? Verification_IsActive { get; set; }
        public Boolean? Verification_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64? VUserId { get; set; }
        public String User_Password { get; set; }
    }

    public class UserUserVerificationMaster_Details
    {
        public Int64 User_pkeyID { get; set; }
        public String User_Email { get; set; }
        public String User_LoginName { get; set; }
        public String User_FirstName { get; set; }
        public String User_LastName { get; set; }
        public int Device { get; set; }
        public String Email_Url { get; set; }


    }
}