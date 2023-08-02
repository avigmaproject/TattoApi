using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TattoAPI.Models.Avigma
{
    public class GoogleLocationDTO
    {
        public String Latitude { get; set; }
        public String Longitude { get; set; }

        public String Address { get; set; }

        public String Latitude_A { get; set; }
        public String Longitude_A { get; set; }

        public Double? Distance { get; set; }
    }
}