using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainManagementWeb.Models.ViewModel
{
    public class CancelBookingViewModel
    {
        public decimal? RefMoney { get; set; }
        public bool IsSuccess { get; set; }
    }
}