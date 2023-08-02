using Amazon.S3.Model;
using Amazon.S3;
using TattoAPI.Models;
using TattoAPI.Repository.Lib;
using Newtonsoft.Json.Linq;
using TattoAPI.Models.Project;

namespace TattoAPI.Repository.Avigma
{
    public class Convert_Json_Array
    {

        Log log = new Log();
        public Client_Master_DTO ConvertJsonArray(Client_Master_DTO Data)
        {
            try
            {
                JArray jsonArray = JArray.Parse(Data.CM_Quest_Json);
                //for (int i = 0; i < jsonArray.Count; i++)
                //{
                //    if (jsonArray[i] ["TQM_PkeyID"].ToString() == "14")
                //    {
                //        Data.CM_UserName = jsonArray[i]["TQM_PkeyID"].ToString();
                //    }
                //}

                JObject jsonData = jsonArray[0][0] as JObject;

                Data.CM_Name = jsonData["ArrForm"][0]["nameNew"].ToString() +" "+ jsonData["ArrForm"][0]["surname"].ToString();
                Data.CM_Email = jsonData["ArrForm"][0]["email"].ToString();
                Data.CM_Phone = jsonData["ArrForm"][0]["phone"].ToString();
                Data.CM_Selected_Area = jsonData["ArrForm"][0]["slectBodyPart"].ToString();
                Data.CM_References_Image = jsonData["ArrForm"][0]["referenceImage"].ToString();
                //Data.CM_References_Image_Filename = jsonData["ArrForm"][0]["referenceImageFilename"].ToString();
                Data.CM_ScreenShot_Img = jsonData["ArrForm"][0]["ScreenShotUrl"].ToString();
                Data.CM_ScreenShot_Img_Filename = jsonData["ArrForm"][0]["ScreenShotFilename"].ToString();


            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }

            return Data;

        }
    }
}
