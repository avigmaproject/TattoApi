using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TattoAPI.Models;
using System.Net;
using GoogleMaps.LocationServices;
using System.Xml.Linq;
using System.Device.Location;
using TattoAPI.Repository.Lib;
using TattoAPI.Models.Avigma;

namespace TattoAPI.Repository.Lib
{

    public class GoogleLocation
    {
        Log log = new Log();

        public GoogleLocationDTO GetLatitudeLogitudeWithoutAPIKey(GoogleLocationDTO googleLocationDTO)
        {
            try
            {
                GoogleLocationService googleLocationService = new GoogleLocationService();
                var point = googleLocationService.GetLatLongFromAddress(googleLocationDTO.Address);
                var latitude = point.Latitude;
                var longitude = point.Longitude;
                googleLocationDTO.Latitude = latitude.ToString();
                googleLocationDTO.Longitude = longitude.ToString();


            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);


            }
            return googleLocationDTO;
        }

        public GoogleLocationDTO GetDistanceBetwenLatLong(GoogleLocationDTO googleLocationDTO)
        {
            try
            {
                if (!string.IsNullOrEmpty(googleLocationDTO.Latitude) && !string.IsNullOrEmpty(googleLocationDTO.Longitude))
                {
                    if (!string.IsNullOrEmpty(googleLocationDTO.Latitude_A) && !string.IsNullOrEmpty(googleLocationDTO.Longitude_A))
                    {
                        var locA = new GeoCoordinate(Convert.ToDouble(googleLocationDTO.Latitude), Convert.ToDouble(googleLocationDTO.Longitude));
                        var locB = new GeoCoordinate(Convert.ToDouble(googleLocationDTO.Latitude_A), Convert.ToDouble(googleLocationDTO.Longitude_A));
                        double distance = locA.GetDistanceTo(locB); // metres
                        googleLocationDTO.Distance = distance;
                    }


                }
              

            }
            catch (Exception ex)
            {
                log.logErrorMessage(googleLocationDTO.Address);
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);


            }
            return googleLocationDTO;
        }

        public GoogleLocationDTO GetLatitudeLogitudeWithAPIKey(GoogleLocationDTO googleLocationDTO)
        {
            try
            {

                string YOUR_API_KEY = System.Configuration.ConfigurationManager.AppSettings["GoogleMapAPIKey"];
                string URL = System.Configuration.ConfigurationManager.AppSettings["GoogleMapURL"];
                string requestUri = string.Format(URL + "key={1}&address={0}&sensor=false", Uri.EscapeDataString(googleLocationDTO.Address), YOUR_API_KEY);

                WebRequest request = WebRequest.Create(requestUri);
                WebResponse response = request.GetResponse();
                XDocument xdoc = XDocument.Load(response.GetResponseStream());
                if (xdoc != null)
                {
                    XElement statuselement = xdoc.Element("GeocodeResponse").Element("status");
                    if (statuselement.Value == "OK")
                    {
                        XElement result = xdoc.Element("GeocodeResponse").Element("result");
                        XElement locationElement = result.Element("geometry").Element("location");
                        XElement lat = locationElement.Element("lat");
                        XElement lng = locationElement.Element("lng");
                        googleLocationDTO.Latitude = lat.Value;
                        googleLocationDTO.Longitude = lng.Value;
                    }
                    else
                    {
                        log.logErrorMessage("Maps Error from Google--------------->" + statuselement.Value);
                    }


                }



            }
            catch (Exception ex)
            {
                log.logErrorMessage(googleLocationDTO.Address);
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);


            }
            return googleLocationDTO;
        }

    }
}