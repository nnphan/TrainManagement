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
    
    public partial class View_GetTrainSchedule
    {
        public decimal id { get; set; }
        public Nullable<decimal> TrainId { get; set; }
        public string TrainNo { get; set; }
        public string TrainName { get; set; }
        public decimal StationId { get; set; }
        public string StationName { get; set; }
        public Nullable<System.DateTime> DepartureDate { get; set; }
        public string DepartureTime { get; set; }
    }
}
