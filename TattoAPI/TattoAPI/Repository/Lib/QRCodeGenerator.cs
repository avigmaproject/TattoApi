using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using ZXing;
using ZXing.QrCode;
using TattoAPI.Models.Avigma;
using TattoAPI.IRepository;

namespace TattoAPI.Repository.Lib
{
    public class QRCodeGenerator : IQRCodeGenerator
    {
        Log log = new Log();

        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }
        public QRCodeGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("Conn_dBcon");
        }


        private string GenerateQRCode(QRCodeModelDTO qRCodeModelDTO)
        {
            string imagePath = string.Empty;
            string DBimagePath = string.Empty;
            try
            {
                string folderPath = _configuration["QRImagePath"];
                string strDBpath = _configuration["QRImageDBPath"];
                var newfileName = Guid.NewGuid() + ".Jpeg";
                imagePath = folderPath + "\\" + newfileName;
                qRCodeModelDTO.QRCodeImageFileName = newfileName;
                // If the directory doesn't exist then create it.
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                BarcodeWriter<Bitmap> barcodeWriter = new BarcodeWriter<Bitmap>()
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new ZXing.Common.EncodingOptions { Height = 100, Width = 100, Margin = 0 },
                    Renderer = new ZXing.Rendering.BitmapRenderer()
                };
                

                barcodeWriter.Format = BarcodeFormat.QR_CODE;
                //barcodeWriter.Renderer = new ZXing.Rendering.IBarcodeRenderer<System.Drawing.Bitmap>();

                var result = barcodeWriter.Write(qRCodeModelDTO.QRCodeText);

                //string barcodePath = HttpContext.Current.Server.MapPath(imagePath);
                string barcodePath = imagePath;
                Size size = new Size();
                size.Height = qRCodeModelDTO.QRCodeHeigth;
                size.Width = qRCodeModelDTO.QRCodeWidth;
                var barcodeBitmap = new Bitmap(result, size);


                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.ReadWrite))
                    {

                        barcodeBitmap.Save(memory, ImageFormat.Jpeg);


                        byte[] bytes = memory.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                        DBimagePath = strDBpath+ "\\" + newfileName;
                    }
                }

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);

                imagePath = ex.Message;

            }

            return imagePath;
        }


        public List<dynamic> GenerateQRImage(QRCodeModelDTO qRCodeModelDTO)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                string imagePath = GenerateQRCode(qRCodeModelDTO);
                qRCodeModelDTO.QRCodeImagePath = imagePath;
                qRCodeModelDTO.QRCodeText = string.Empty;
                objdynamicobj.Add(qRCodeModelDTO);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                qRCodeModelDTO.QRCodeImagePath = ex.Message;
                qRCodeModelDTO.QRCodeText = string.Empty;
                objdynamicobj.Add(qRCodeModelDTO);

            }
            return objdynamicobj;

        }


    }
}