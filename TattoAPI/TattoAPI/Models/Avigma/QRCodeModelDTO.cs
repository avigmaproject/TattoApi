using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TattoAPI.Models.Avigma
{
    public class QRCodeModelDTO
    {
        public string QRCodeText { get; set; }
        public string? QRCodeImagePath { get; set; }
        public string? QRCodeImageFileName { get; set; }
        public int QRCodeWidth { get; set; }
        public int QRCodeHeigth { get; set; }
    }
}