using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainManagementWeb.Models.ViewModel
{
    public class TrainScheduleViewModel
    {
        public decimal id { get; set; }
        public decimal TrainId { get; set; }
        public decimal StationId { get; set; }
        public string DepartureDate { get; set; }
        public string DepartureTime { get; set; }
    }
}