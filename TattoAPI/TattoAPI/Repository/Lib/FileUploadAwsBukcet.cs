using Amazon;
using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Amazon.Runtime.SharedInterfaces;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TattoAPI.Models;
using TattoAPI.Repository.Lib;

namespace TattoAPI.Repository.Lib
{
    public class FileUploadAwsBukcet
    {
        Log log = new Log();
        public AmazonBucketDTO UploadFileAmazonBuket(AmazonBucketDTO amazonBucketDTO)
        {
            try
            {

                log.logDebugMessage(amazonBucketDTO.FilePath);
                using (var client = new AmazonS3Client(amazonBucketDTO.AccessKey, amazonBucketDTO.SecretKey, RegionEndpoint.USEast2))
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = amazonBucketDTO.BucketName,
                        Key = amazonBucketDTO.FileName,
                        FilePath = amazonBucketDTO.FilePath
                    };
                    //long expirationTimeInSeconds = DateTimeOffset.UtcNow.AddYears(10).ToUnixTimeSeconds();
                    //DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(expirationTimeInSeconds);
                    //DateTime dateTime = dateTimeOffset.DateTime;

                    var expiryUrlRequest = new GetPreSignedUrlRequest()
                    {
                        BucketName = amazonBucketDTO.BucketName,
                        Key = amazonBucketDTO.FileName,
                        Expires = DateTime.UtcNow.AddDays(7)
                        //Expires = dateTime
                    };


                    var response = client.PutObjectAsync(request).Result;
                    var s3Url = client.GetPreSignedURL(expiryUrlRequest);
                    amazonBucketDTO.ReturnURL = s3Url;
                    if (s3Url != null)
                    {
                        File.Delete(amazonBucketDTO.FilePath);
                    }


                }

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }

            return amazonBucketDTO;

        }

    }
}