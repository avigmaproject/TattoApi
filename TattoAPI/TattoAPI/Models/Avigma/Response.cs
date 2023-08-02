using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TattoAPI.Models.Avigma
{
    public class Response
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public object Data { get; set; }
        public int totalcount { get; set; }
    }
}