using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainManagementWeb.Models.ViewModel
{
    public class CreateStationModel
    {
        public string StationName { get; set; }
        public string StationCode { get; set; }
        public string RailwayDivisionName { get; set; }
        public int PositionDistance { get; set; }
    }
}