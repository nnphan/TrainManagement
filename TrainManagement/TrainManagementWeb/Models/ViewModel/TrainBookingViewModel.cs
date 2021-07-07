using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainManagementWeb.Models.ViewModel
{
    public class TrainBookingViewModel
    {
        public decimal TrainId { get; set; }
        public string TrainCode { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string DepartureDate { get; set; }
        public string DepartureTime { get; set; }
        public decimal SeatPrice1 { get; set; }
        public int Seat1Available { get; set; }
        public decimal SeatPrice2 { get; set; }
        public int Seat2Available { get; set; }
        public decimal SeatPrice3 { get; set; }
        public int Seat3Available { get; set; }
        public Nullable<int> Class { get; set; }
        public double Value { get; set; }
    }
}