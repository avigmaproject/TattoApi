using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TattoAPI.Models.Avigma
{
    public class User_Admin_Master_DTO
    {

        public Int64 Ad_User_PkeyID { get; set; }
        public String Ad_User_First_Name { get; set; }
        public String Ad_User_Last_Name { get; set; }
        public String Ad_User_Address { get; set; }
        public String Ad_User_City { get; set; }
        public String Ad_User_State { get; set; }
        public String Ad_User_Mobile { get; set; }
        public String Ad_User_Login_Name { get; set; }
        public String Ad_User_Password { get; set; }
        public String Ad_User_UserVal { get; set; }
        public String Ad_User_Email { get; set; }
        public Boolean? Ad_User_IsActive { get; set; }
        public Boolean? Ad_User_IsDelete { get; set; }
        public int? Ad_User_Type { get; set; }
        public int? Type { get; set; }
        public Int64? UserID { get; set; }
    }
}