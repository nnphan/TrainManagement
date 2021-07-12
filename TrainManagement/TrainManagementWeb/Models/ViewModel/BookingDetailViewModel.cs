using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainManagementWeb.Models.EF;

namespace TrainManagementWeb.Models.ViewModel
{
    public class BookingDetailViewModel
    {
        public List<sp_GetBookingDetail_Result> bookingDetail { set; get; }
        public decimal totalPrice { set; get; }
    }
}