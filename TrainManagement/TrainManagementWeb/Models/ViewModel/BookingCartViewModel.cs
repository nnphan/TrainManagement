using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainManagementWeb.Models.ViewModel
{
    public class BookingCartViewModel
    {
        public List<CartItemViewModel> listItem { set; get; }
        public int quantity { set; get; }
        public decimal total { set; get; }
    }
}