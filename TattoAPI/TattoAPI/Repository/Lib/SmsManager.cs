using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http.Headers;
using RestSharp;
using TattoAPI.Models.Avigma;

namespace TattoAPI.Repository.Lib
{
    public class SmsManager
    {
        Log Log = new Log();
        public string SendSms(SMSDTO smsDTO)
        {
            string val = string.Empty;
            try
            {
                string strURL = System.Configuration.ConfigurationManager.AppSettings["SMSUrl"];
                string strUserID = System.Configuration.ConfigurationManager.AppSettings["SmsUSer"];
                string strPassword = System.Configuration.ConfigurationManager.AppSettings["SmsPass"];
                string strSid = System.Configuration.ConfigurationManager.AppSettings["Sid"];

                string strFinalURL = string.Empty;
                // strFinalURL = strURL + "?user=" + strUserID + "&password=" + strPassword + "&msisdn=91" + smsDTO.MobileNo + "&sid=" + strSid + "&msg=" + smsDTO.Message + "&fl=0&gwid=2";
                //strFinalURL = strURL + "?AUTH_KEY="+strPassword+"&message="+smsDTO.Message+"&senderId="+strSid+"&routeId=3&mobileNos="+smsDTO.MobileNo+"&smsContentType=english";
                //strFinalURL = strURL + "?AUTH_KEY=a79bc417601ea61e17cf954c3e6ea7b&message=&senderId=Gcoinm&routeId=3&mobileNos=8850804079&smsContentType=english

                strFinalURL = strURL +"?user=goMICM&key=29b1afac0eXX&mobile="+smsDTO.MobileNo+"&message="+ smsDTO.Message +"&senderid=goMICM&accusage=1"; 
                var client = new RestClient(strFinalURL);
                var request = new RestRequest(strFinalURL,Method.Get);
                RestResponse response = client.Execute(request);
                val = Convert.ToString(response.Content);
                Log.logDebugMessage("SMSURL: "+ strFinalURL);
                Log.logDebugMessage("SMSURLRes: " + val);

            }
            catch (Exception ex)
            {

                Log.logErrorMessage(ex.StackTrace);
            }
          
            return val;
            
        }

    }
}