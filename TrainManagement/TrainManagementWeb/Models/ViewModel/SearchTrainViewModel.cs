using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrainManagementWeb.Models.ViewModel
{
    public class SearchTrainViewModel
    {
        public decimal DepartureStationId { get; set; }
        [Required]
        public string DepartureStationName { get; set; }
        public decimal ArrivalStationId { get; set; }
        [Required]
        public string ArrivalStationName { get; set; }
        [Required]
        public string DepartureDate { get; set; }
    }
}
