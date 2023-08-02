using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TattoAPI.Models.Avigma
{


    //for api call


    public class UserLogin
    {
        public String UserCode { get; set; }
        public String UserToken { get; set; }
        public String ErrorCode { get; set; }
        public String Password { get; set; }
        public String EmailID { get; set; }
        public int Type { get; set; }
        public int Device { get; set; }
        public string Email_Url { get; set; }
        public Int64 User_PkeyID { get; set; }
        public Int64 UserID { get; set; }

    }
    public class RootUserLogin
    {
        public String UserToken { get; set; }
        public String UserCode { get; set; }
        public String Um_Password { get; set; }

        public String User_MacID { get; set; }
        public String Um_Picture { get; set; }
        public String Um_Name { get; set; }

        public String MobileNumber { get; set; }
        public String Um_Email { get; set; }
        public String Um_Address { get; set; }
        public String Um_CustID { get; set; }
        public int? OTP { get; set; }

        public String Um_Country { get; set; }
        public String Um_City { get; set; }
        public String Um_Zip { get; set; }
        public int Type { get; set; }

        public String User_latitude { get; set; }
        public String User_longitude { get; set; }
        public int? User_Login_Type { get; set; }
        public String User_Token_val { get; set; }
        public String User_LastName { get; set; }
        public String User_Company { get; set; }
        public string User_FB_GM_Token_val { get; set; }
        public String? User_IPAddress { get; set; }

    }
    public class RootUserLoginRegistraion
    {
        public String? User_Name { get; set; }
        public String User_Email { get; set; }
        public String User_Password { get; set; }
        public int Type { get; set; }

    }
    public class RootUserLogin_input
    {
        //public String? User_Email { get; set; }
        //public String? User_Password { get; set; }
        //public String? User_Name { get; set; }
        //public String? User_Phone { get; set; }
        //public String? User_Address { get; set; }
        //public String? User_MacID { get; set; }
        //public int? User_OTP { get; set; }
        //public String? User_latitude { get; set; }
        //public String? User_longitude { get; set; }
        //public int? User_Login_Type { get; set; }
        //public String? User_Token_val { get; set; }
        //public String? User_LastName { get; set; }
        //public String? User_Company { get; set; }
        //public String? User_IPAddress { get; set; }
        //public String? User_MotiID { get; set; }
        //public int? Type { get; set; }


        public String? User_Name { get; set; }
        public String User_Email { get; set; }
        public String User_Password { get; set; }
        public String? User_Phone { get; set; }
        public String User_Address { get; set; }
        public String User_City { get; set; }
        public String User_Country { get; set; }
        public String User_Zip { get; set; }
        public DateTime? User_DOB { get; set; }
        public int? User_Type { get; set; }
        public String User_Image_Path { get; set; }
        public String User_MacID { get; set; }
        public Boolean? User_IsVerified { get; set; }
        public Boolean? User_IsActive { get; set; }
        public Boolean? User_IsDelete { get; set; }
        public Int64 UserID { get; set; }
        public int? User_OTP { get; set; }
        public String User_latitude { get; set; }
        public String User_longitude { get; set; }
        public Int64? User_PkeyID_Master { get; set; }
        public int? User_Login_Type { get; set; }
        public int? User_Gender { get; set; }
        public String User_Image_Base { get; set; }
        public String User_LastName { get; set; }
        public String User_Company { get; set; }
        public int? User_Language { get; set; }
        public String User_Occupation { get; set; }
        public String User_Token_val { get; set; }
        public String User_IPAddress { get; set; }
        public Boolean? User_IsLogin { get; set; }
        public String User_Login_Token { get; set; }
        public int Type { get; set; }

    }
    public class ViewLogin
    {
        public String UserId { get; set; }
        public bool?  IsVerified { get; set; }
        public Int64 ?intUSerId { get; set; }
        public String status { get; set; }
        public String ErrorMessage { get; set; }
    }

    public class ImageData
    {
        public String Image_Path { get; set; }
        public String? Image_Name { get; set; }
        public String Image_Base { get; set; }
        public int Type { get; set; }
        public Int64 UserID { get; set; }
    }

}