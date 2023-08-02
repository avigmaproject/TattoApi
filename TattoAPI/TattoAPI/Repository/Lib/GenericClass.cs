using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TattoAPI.Models.Avigma;

namespace TattoAPI.Repository.Lib
{
    public class GenericClass
    {
        public GenericClassDTO GetSplitTime(string strTime)
        {
            GenericClassDTO genericClassDTO = new GenericClassDTO();
            char[] spearator = { ':' };
            string[] SplitStartTime = strTime.Split(spearator);
            genericClassDTO.intHrs = Convert.ToInt32(SplitStartTime[0]);
            genericClassDTO.intMins = Convert.ToInt32(SplitStartTime[1]);
            return genericClassDTO;
        }

        public string GetTimeFormat(GenericClassDTO genericClassDTO)
        {
            string strtime = genericClassDTO.intHrs.ToString() + ":" + genericClassDTO.intMins.ToString();
            DateTime d = DateTime.Parse(strtime);
            string time = d.ToString("HH:mm tt");
            return time;
        }
    }
}