using TattoAPI.IRepository;
using TattoAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TattoAPI.Repository.Lib;
using TattoAPI.Models.Avigma;

namespace TattoAPI.Repository.Avigma
{
    public class EmailTemplate : IEmailTemplate
    {
        Log log = new Log();
        private readonly IConfiguration _configuration;

        public EmailTemplate(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<dynamic> SendRequestPassword(UserLogin  userLogin)
        {
            EmailManager emailManager = new EmailManager(_configuration);
            List<dynamic> objDynamic = new List<dynamic>();

            try
            {
               
                EmailDTO model = new EmailDTO();
                string Touserid = System.Configuration.ConfigurationManager.AppSettings["EmailTo"].ToString();
                string ApplicationName = System.Configuration.ConfigurationManager.AppSettings["ApplicationName"].ToString();
                StringBuilder sb = new StringBuilder();
               
                model.Subject = " Password Changed Successfully – "+ ApplicationName;

                string strtemplate1 = "<p>"+ ApplicationName + ".com</p>";
                sb.Append(strtemplate1);
                string strtemplate2 = "We have received a request to reset your forgotten password";
                sb.Append(strtemplate2);

                string strtemplate3 = "<p>Your User Id is  " + userLogin.EmailID + "</p>";
                sb.Append(strtemplate3);

                string strtemplate3x = "<p>Your Password is   " + userLogin.Password + "</p>";
                sb.Append(strtemplate3x);


                //string NewTemplateAutoGenPwd =  sb.ToString();
                model.Message = sb.ToString();


                model.To = userLogin.EmailID;
                model.CC = Touserid;
                int emailval = emailManager.SendEmail(model);
                objDynamic.Add(emailval);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
           
            return objDynamic;
        }

        public List<dynamic> VerifiedRegistration(EmailDTO model,int OTP)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                EmailManager emailManager = new EmailManager(_configuration);
                model.Subject = "Medise OTP Verification";
                string str = string.Empty;
             
                str = "<html><head><meta name=\"viewport\" content=\"width=device-width\"/><title>successfull-registration</title></head>";
                str += "<body>";
                str += "<style type=\"text/css\">";
                str += "import url(https://fonts.googleapis.com/css?family=Lato:400,700);";
                str += "body{font-size: 15px !important; font-family: 'Lato', sans-serif !important;color: #333; margin:0px; padding:0px; line-height: 24px !important;letter-spacing: 0.10px;}";
                str += "h1{font-size: 24px; font-weight: 700;font-family: 'Lato', sans-serif !important;}";
                str += "td{vertical-align: top;font-family: 'Lato', sans-serif !important;}";
                str += "a{text-decoration: none; color: #5cbd9e;}";
                str += ".gt{font-size: 100% !important;}";
                str += "</style>";
                str += "<table cellpadding=\"0\" cellspacing=\"0\" align=\"center\" width=\"600\" style=\"border:1px solid #ddd; background-color: #fff;\">";
                str += "<tr bgcolor=\"#fff\">";
                // str += "<td align=\"center\" style=\"padding: 30px 0;\"><a href=\"#\"><img src=\"http://win.infomanav.com/Content/User/images/logo.png\"></a></td>";
                str += "</tr><tr>";
                // str += "<td valign=\"center\" style=\"background: url(http://win.infomanav.com/Content/User/images/banner.jpg); width: 600px; height: 113px; vertical-align: middle; color: #fff; text-align: center;font-family: 'Lato', sans-serif !important;\">";
                str += "<h1>Email Verification</h1>";
                str += "</td></tr>";
                str += "<tr><td>";
                str += "<table cellpadding=\"0\" cellspacing=\"0\" width=\"600\" align=\"center\" style=\"padding-left:40px; padding-right: 40px; padding-bottom: 50px;\" bgcolor=\"#fff\">";
                str += "<tr><td>";
                //str += "<p style=\"padding-top: 50px; color: #ea5a56; font-family: 'Lato', sans-serif !important;\">Dear " + Session["SiteUserFirstName"].ToString() + ",</p>";
                str += "<p style=\"padding-top: 50px; color: #ea5a56; font-family: 'Lato', sans-serif !important;\">Dear " + model.FirstName + ",</p>";
                str += "<p style=\"color: #333; font-family: 'Lato', sans-serif !important;\">";
                str += "Congratulations your ‘In order to Activate your Medsie Account please Enter below the OTP. <br><br>";
                str += "Login Id = " + model.To + " <br>";
                str += "OTP = " + OTP + " <br>";
                //str += "<p style=\"padding-top: 10px; font-family: 'Lato', sans-serif !important;\">";
                //str += "Please click on the link below to verify your registration with ‘The Arts Trust’.<br><br>";
                //str += "<a type=\"button\" href=\"" + Link + "\" style=\"border-radius: 3px;background:#ea5a56; border:0px; padding:5px 5px;color:#FFF;cursor:pointer;\"><input type=\"submit\" class=\"button_active\" value=\"Click Here\" style=\"border-radius: 3px;background:#ea5a56; border:0px; padding:5px 5px;;color:#FFF;cursor:pointer;\" /></a><br>";
                //str += "</p>";
                str += "<p style=\"padding-top: 10px; font-family: 'Lato', sans-serif !important;\">";
                //str += "For any further assistance please feel free to write to us at, contact@theartstrust.com or call us on 91-22 2204 8138/39. We will be glad to assist you.<br>";
                str += "</p>";
                str += "<p style=\"padding-top: 10px; font-family: 'Lato', sans-serif !important;\">";
                str += "Warm Regards,<br>";
                str += "Team Medise";
                str += "</p>";
                str += "</tr></table></td></tr>";
                str += "<tr bgcolor=\"#f1f4f6\">";
                str += "<td style=\"padding: 10px 40px; color: #333333; font-size: 13px;font-family: 'Lato', sans-serif !important; line-height: 24px;\">Copyright 2017 The Arts Trust. All rights reserved.</td>";
                str += "</tr>";
                str += "</table></body></html>";
                model.Message = str;
                var emaildata =   emailManager.email_send(model);



            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);

            }
            return objDynamic;
        }


        
        public List<dynamic> NewBusinessRegister(EmailDTO model, string Buss_Name, string Buss_Number,string Buss_Address)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                EmailManager emailManager = new EmailManager(_configuration);
                model.Subject = "Buissness Activated SucessFully";
                string str = string.Empty;

                str = "<html><head><meta name=\"viewport\" content=\"width=device-width\"/><title>successfull-registration</title></head>";
                str += "<body>";
                str += "<style type=\"text/css\">";
                str += "import url(https://fonts.googleapis.com/css?family=Lato:400,700);";
                str += "body{font-size: 15px !important; font-family: 'Lato', sans-serif !important;color: #333; margin:0px; padding:0px; line-height: 24px !important;letter-spacing: 0.10px;}";
                str += "h1{font-size: 24px; font-weight: 700;font-family: 'Lato', sans-serif !important;}";
                str += "td{vertical-align: top;font-family: 'Lato', sans-serif !important;}";
                str += "a{text-decoration: none; color: #5cbd9e;}";
                str += ".gt{font-size: 100% !important;}";
                str += "</style>";
                str += "<table cellpadding=\"0\" cellspacing=\"0\" align=\"center\" width=\"600\" style=\"border:1px solid #ddd; background-color: #fff;\">";
                str += "<tr bgcolor=\"#fff\">";
                // str += "<td align=\"center\" style=\"padding: 30px 0;\"><a href=\"#\"><img src=\"http://win.infomanav.com/Content/User/images/logo.png\"></a></td>";
                str += "</tr><tr>";
                // str += "<td valign=\"center\" style=\"background: url(http://win.infomanav.com/Content/User/images/banner.jpg); width: 600px; height: 113px; vertical-align: middle; color: #fff; text-align: center;font-family: 'Lato', sans-serif !important;\">";
                str += "<h1>Buissness Activated SucessFully</h1>";
                str += "</td></tr>";
                str += "<tr><td>";
                str += "<table cellpadding=\"0\" cellspacing=\"0\" width=\"600\" align=\"center\" style=\"padding-left:40px; padding-right: 40px; padding-bottom: 50px;\" bgcolor=\"#fff\">";
                str += "<tr><td>";
                //str += "<p style=\"padding-top: 50px; color: #ea5a56; font-family: 'Lato', sans-serif !important;\">Dear " + Session["SiteUserFirstName"].ToString() + ",</p>";
                str += "<p style=\"padding-top: 50px; color: #ea5a56; font-family: 'Lato', sans-serif !important;\">Dear " + model.FirstName + ",</p>";
                str += "<p style=\"color: #333; font-family: 'Lato', sans-serif !important;\">";
                str += "Congratulations Your Business has been Register Following Details . <br><br>";
                str += "Buissness Name = " + Buss_Name + " <br>";
                str += "Buissness Contact Number  = " + Buss_Number + " <br>";
                str += "Buissness Address  = " + Buss_Address + " <br>";
                str += "Once Admin Aprrove it . It will be seen in the Listing . <br><br>";
                //str += "<p style=\"padding-top: 10px; font-family: 'Lato', sans-serif !important;\">";
                //str += "Please click on the link below to verify your registration with ‘The Arts Trust’.<br><br>";
                //str += "<a type=\"button\" href=\"" + Link + "\" style=\"border-radius: 3px;background:#ea5a56; border:0px; padding:5px 5px;color:#FFF;cursor:pointer;\"><input type=\"submit\" class=\"button_active\" value=\"Click Here\" style=\"border-radius: 3px;background:#ea5a56; border:0px; padding:5px 5px;;color:#FFF;cursor:pointer;\" /></a><br>";
                //str += "</p>";
                str += "<p style=\"padding-top: 10px; font-family: 'Lato', sans-serif !important;\">";
                //str += "For any further assistance please feel free to write to us at, contact@theartstrust.com or call us on 91-22 2204 8138/39. We will be glad to assist you.<br>";
                str += "</p>";
                str += "<p style=\"padding-top: 10px; font-family: 'Lato', sans-serif !important;\">";
                str += "Warm Regards,<br>";
                str += "Team Medise";
                str += "</p>";
                str += "</tr></table></td></tr>";
                str += "<tr bgcolor=\"#f1f4f6\">";
                str += "<td style=\"padding: 10px 40px; color: #333333; font-size: 13px;font-family: 'Lato', sans-serif !important; line-height: 24px;\">Copyright 2017 The Arts Trust. All rights reserved.</td>";
                str += "</tr>";
                str += "</table></body></html>";
                model.Message = str;
                var emaildata = emailManager.email_send(model);



            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);

            }
            return objDynamic;
        }


        public List<dynamic> BusinessActive(EmailDTO model, string Buss_Name, string Buss_Number, string Buss_Address)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                EmailManager emailManager = new EmailManager(_configuration);
                model.Subject = "New Buissness Register";
                string str = string.Empty;

                str = "<html><head><meta name=\"viewport\" content=\"width=device-width\"/><title>successfull-registration</title></head>";
                str += "<body>";
                str += "<style type=\"text/css\">";
                str += "import url(https://fonts.googleapis.com/css?family=Lato:400,700);";
                str += "body{font-size: 15px !important; font-family: 'Lato', sans-serif !important;color: #333; margin:0px; padding:0px; line-height: 24px !important;letter-spacing: 0.10px;}";
                str += "h1{font-size: 24px; font-weight: 700;font-family: 'Lato', sans-serif !important;}";
                str += "td{vertical-align: top;font-family: 'Lato', sans-serif !important;}";
                str += "a{text-decoration: none; color: #5cbd9e;}";
                str += ".gt{font-size: 100% !important;}";
                str += "</style>";
                str += "<table cellpadding=\"0\" cellspacing=\"0\" align=\"center\" width=\"600\" style=\"border:1px solid #ddd; background-color: #fff;\">";
                str += "<tr bgcolor=\"#fff\">";
                // str += "<td align=\"center\" style=\"padding: 30px 0;\"><a href=\"#\"><img src=\"http://win.infomanav.com/Content/User/images/logo.png\"></a></td>";
                str += "</tr><tr>";
                // str += "<td valign=\"center\" style=\"background: url(http://win.infomanav.com/Content/User/images/banner.jpg); width: 600px; height: 113px; vertical-align: middle; color: #fff; text-align: center;font-family: 'Lato', sans-serif !important;\">";
                str += "<h1>New Buissness Register</h1>";
                str += "</td></tr>";
                str += "<tr><td>";
                str += "<table cellpadding=\"0\" cellspacing=\"0\" width=\"600\" align=\"center\" style=\"padding-left:40px; padding-right: 40px; padding-bottom: 50px;\" bgcolor=\"#fff\">";
                str += "<tr><td>";
                //str += "<p style=\"padding-top: 50px; color: #ea5a56; font-family: 'Lato', sans-serif !important;\">Dear " + Session["SiteUserFirstName"].ToString() + ",</p>";
                str += "<p style=\"padding-top: 50px; color: #ea5a56; font-family: 'Lato', sans-serif !important;\">Dear " + model.FirstName + ",</p>";
                str += "<p style=\"color: #333; font-family: 'Lato', sans-serif !important;\">";
                str += "Congratulations  Your Business with Following Details has been Activated Successfully. <br><br>";
                str += "Buissness Name = " + Buss_Name + " <br>";
                str += "Buissness Contact Number  = " + Buss_Number + " <br>";
                str += "Buissness Address  = " + Buss_Address + " <br>";
                //str += "<p style=\"padding-top: 10px; font-family: 'Lato', sans-serif !important;\">";
                //str += "Please click on the link below to verify your registration with ‘The Arts Trust’.<br><br>";
                //str += "<a type=\"button\" href=\"" + Link + "\" style=\"border-radius: 3px;background:#ea5a56; border:0px; padding:5px 5px;color:#FFF;cursor:pointer;\"><input type=\"submit\" class=\"button_active\" value=\"Click Here\" style=\"border-radius: 3px;background:#ea5a56; border:0px; padding:5px 5px;;color:#FFF;cursor:pointer;\" /></a><br>";
                //str += "</p>";
                str += "<p style=\"padding-top: 10px; font-family: 'Lato', sans-serif !important;\">";
                //str += "For any further assistance please feel free to write to us at, contact@theartstrust.com or call us on 91-22 2204 8138/39. We will be glad to assist you.<br>";
                str += "</p>";
                str += "<p style=\"padding-top: 10px; font-family: 'Lato', sans-serif !important;\">";
                str += "Warm Regards,<br>";
                str += "Team Medise";
                str += "</p>";
                str += "</tr></table></td></tr>";
                str += "<tr bgcolor=\"#f1f4f6\">";
                str += "<td style=\"padding: 10px 40px; color: #333333; font-size: 13px;font-family: 'Lato', sans-serif !important; line-height: 24px;\">Copyright 2017 The Arts Trust. All rights reserved.</td>";
                str += "</tr>";
                str += "</table></body></html>";
                model.Message = str;
                var emaildata = emailManager.email_send(model);



            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);

            }
            return objDynamic;
        }

        public String GetEmailMessageText(string changePasswordLink, string Name, string Email, int Type)
        {
            string Message = string.Empty;
            string ApplicationName = _configuration["ApplicationName"].ToString();
            string str = string.Empty;
            str = "<html><head><meta name=\"viewport\" content=\"width=device-width\"/><title>"+ ApplicationName  +  " New Register User</title></head>";
            str += "<body>";
            str += "<style type=\"text/css\">";
            str += "import url(https://fonts.googleapis.com/css?family=Lato:400,700);";
            str += "body{font-size: 15px !important; font-family: 'Lato', sans-serif !important;color: #333; margin:0px; padding:0px; line-height: 24px !important;letter-spacing: 0.10px;}";
            str += "h1{font-size: 24px; font-weight: 700;font-family: 'Lato', sans-serif !important;}";
            str += "td{vertical-align: top;font-family: 'Lato', sans-serif !important;}";
            str += "a{text-decoration: none; color: #5cbd9e;}";
            str += ".gt{font-size: 100% !important;}";
            str += "</style>";
            str += "<table cellpadding=\"0\" cellspacing=\"0\" align=\"center\" width=\"600\" style=\"border:1px solid #ddd; background-color: #fff;\">";
            str += "<tr bgcolor=\"#fff\">";
            // str += "<td align=\"center\" style=\"padding: 30px 0;\"><a href=\"#\"><img src=\"http://win.infomanav.com/Content/User/images/logo.png\"></a></td>";
            str += "</tr><tr>";
            // str += "<td valign=\"center\" style=\"background: url(http://win.infomanav.com/Content/User/images/banner.jpg); width: 600px; height: 113px; vertical-align: middle; color: #fff; text-align: center;font-family: 'Lato', sans-serif !important;\">";

            switch (Type)
            {
                case 1:
                    {
                        str += "<h1>Email Verification</h1>";
                        break;
                    }
                case 2:
                    {
                        str += "<h1>Change Password</h1>";
                        break;
                    }
            }
           


            str += "</td></tr>";
            str += "<tr><td>";
            str += "<table cellpadding=\"0\" cellspacing=\"0\" width=\"600\" align=\"center\" style=\"padding-left:40px; padding-right: 40px; padding-bottom: 50px;\" bgcolor=\"#fff\">";
            str += "<tr><td>";
            str += "<p style=\"padding-top: 50px; color: #ea5a56; font-family: 'Lato', sans-serif !important;\">Hello " + Name + ",</p>";
            str += "<p style=\"color: #333; font-family: 'Lato', sans-serif !important;\">";
            //str += "Please Find the Below Link of the Memory <br><br>";
            //str += "<br/>";
            // str += "Password = " + UserPassword + " <br>";
            str += "<p style=\"padding-top: 10px; font-family: 'Lato', sans-serif !important;\">";
            switch (Type)
            {
                case 1:
                    {
                        str += "Please click on the link below to Verify Your Email’.<br><br>";
                        break;
                    }
                case 2:
                    {
                        str += "Please click on the link below to Change Password’.<br><br>";
                        break;
                    }
            }
          
            str += "<a type=\"button\" href=\"" + changePasswordLink + "\" style=\"border-radius: 3px;background:#ea5a56; border:0px; padding:5px 5px;color:#FFF;cursor:pointer;\"><input type=\"submit\" class=\"button_active\" value=\"Click Here\" style=\"border-radius: 3px;background:#ea5a56; border:0px; padding:5px 5px;;color:#FFF;cursor:pointer;\" /></a><br>";
            str += " " + changePasswordLink + " ";
            str += "</p>";
            str += "<p style=\"padding-top: 10px; font-family: 'Lato', sans-serif !important;\">";
            // str += "For any further assistance please feel free to write to us at, contact@theartstrust.com or call us on 91-22 2204 8138/39. We will be glad to assist you.<br>";
            str += "</p>";
            str += "<p style=\"padding-top: 10px; font-family: 'Lato', sans-serif !important;\">";
            str += "Warm Regards,<br>";
            str += "Team "+ ApplicationName;
            str += "</p>";
            str += "</tr></table></td></tr>";
            str += "<tr bgcolor=\"#f1f4f6\">";
            str += "<td style=\"padding: 10px 40px; color: #333333; font-size: 13px;font-family: 'Lato', sans-serif !important; line-height: 24px;\">Copyright 2020. All rights reserved.</td>";
            str += "</tr>";
            str += "</table></body></html>";
            Message = str;
            return Message;
        }

        public List<dynamic> NewUserRegister(EmailDTO model, string mess, int Type)
        {
            EmailManager emailManager = new EmailManager(_configuration);
            List<dynamic> objDynamic = new List<dynamic>();
            string ApplicationName = _configuration["ApplicationName"].ToString();
            try
            {

            
                string Touserid = _configuration["EmailTo"].ToString();
                StringBuilder sb = new StringBuilder();

                switch (Type)
                {
                    case 1:
                        {
                            model.Subject = ApplicationName + " New User Register";
                            break;
                        }
                    case 2:
                        {
                            model.Subject = ApplicationName+"   New Password Generated for  " + model.To;
                            break;
                        }
                }

                sb.Append(mess);
                string strtemplate1 = "<br/><p> </p>";
                sb.Append(strtemplate1);

                model.Message = sb.ToString();


             
                var emailval = emailManager.SendEmail(model);
                objDynamic.Add(emailval);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }

            return objDynamic;
        }


        public List<dynamic> NewClientRegister(EmailDTO model, string mess, int Type)
        {
            EmailManager emailManager = new EmailManager(_configuration);
            List<dynamic> objDynamic = new List<dynamic>();
            string ApplicationName = _configuration["ApplicationName"].ToString();
            try
            {


                string Touserid = _configuration["EmailTo"].ToString();
                StringBuilder sb = new StringBuilder();

                switch (Type)
                {
                    case 1:
                        {
                            model.Subject = "New Client Register";
                            break;
                        }
                    case 2:
                        {
                            model.Subject = ApplicationName + "   New Password Generated for  " + model.To;
                            break;
                        }
                }

                sb.Append(mess);
                string strtemplate1 = "<br/><p> </p>";
                sb.Append(strtemplate1);

                model.Message = sb.ToString();



                var emailval = emailManager.SendEmail(model);
                objDynamic.Add(emailval);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }

            return objDynamic;
        }
    }
}