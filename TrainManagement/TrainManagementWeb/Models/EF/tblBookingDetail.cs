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
    using System.Collections.Generic;
    
    public partial class tblBookingDetail
    {
        public decimal Id { get; set; }
        public Nullable<decimal> BookingId { get; set; }
        public Nullable<int> Class { get; set; }
        public Nullable<decimal> StartStation { get; set; }
        public Nullable<decimal> EndStation { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string TrainCode { get; set; }
        public Nullable<System.DateTime> DepDay { get; set; }
        public string DepTime { get; set; }
        public string StartStationName { get; set; }
        public string EndStationName { get; set; }
        public Nullable<int> SeatNum { get; set; }
        public Nullable<int> CoachNum { get; set; }
    
        public virtual tblBooking tblBooking { get; set; }
        public virtual tblClass tblClass { get; set; }
        public virtual tblStation tblStation { get; set; }
        public virtual tblStation tblStation1 { get; set; }
    }
}
