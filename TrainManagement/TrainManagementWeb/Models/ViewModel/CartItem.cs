using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainManagementWeb.Models.ViewModel
{
    public class CartItem
    {
        public BookingTrain BookingTrain { set; get; }
        public int Quantity { set; get; }
    }
}
