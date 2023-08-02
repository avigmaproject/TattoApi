using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TattoAPI.Models.Avigma
{
    public class Menu_Master_DTO
    {
        public Int64 MU_PkeyID { get; set; }
        public String MU_Name { get; set; }
        public String MU_Description { get; set; }
        public Boolean? MU_IsActive { get; set; }
        public Boolean? MU_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64? UserID { get; set; }
    }
}