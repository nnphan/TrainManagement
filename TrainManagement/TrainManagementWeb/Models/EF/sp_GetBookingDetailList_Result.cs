//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrainManagementWeb.Models.EF
{
    using System;
    
    public partial class sp_GetBookingDetailList_Result
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string PassportId { get; set; }
        public string PRN { get; set; }
        public Nullable<bool> isCancel { get; set; }
        public Nullable<decimal> totalprice { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string traincode { get; set; }
        public Nullable<int> Class { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<int> quantity { get; set; }
        public string startstationname { get; set; }
        public string endstationname { get; set; }
        public string deptime { get; set; }
        public Nullable<System.DateTime> depday { get; set; }
        public Nullable<decimal> RefuncMoney { get; set; }
    }
}
