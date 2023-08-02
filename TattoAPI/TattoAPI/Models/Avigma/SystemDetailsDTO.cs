using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TattoAPI.Models.Avigma
{
    public class SystemDetailsDTO
    {
        public int? Coins { get; set; }   
        public String ErrorCode { get; set; }
    }
    //public class  SystemCity
    //{
    //    public int? CityID { get; set; }
    //    public String Name { get; set; }
    //    public int? StateID { get; set; }
    //}

    //public class SystemCountry
    //{
    //    public int? CountryID { get; set; }
    //    public String Name { get; set; }
    //}

    //public class SystemState
    //{
    //    public int? StateID { get; set; }
    //    public String Name { get; set; }
    //    public int? CountryID { get; set; }
    //}

    public class SystemAmt
    {
        public Decimal? UserJoinAmount { get; set; }
        public int? ReferenceThreshold { get; set; }
        public Decimal? UsdAmount { get; set; }
    }

    public class SystemDataMasterDTO
    {
        public Int64 UserJoinAmountId { get; set; }
        public Decimal? UserJoinAmount { get; set; }
        public int? ReferenceThreshold { get; set; }
        public Decimal? PerUserAmount { get; set; }
        public int? RefCodeValidDays { get; set; }
        public Decimal? UsdAmount { get; set; }
        public Int64 CurrUserId { get; set; }
        public int Type { get; set; }
        public Boolean? IsActive { get; set; }
    }
}