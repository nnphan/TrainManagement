using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainManagementWeb.Models.ViewModel
{
    public class BookingCart
    {
        public List<CartItem> listItem { set; get; }
        public int? quantity { set; get; }
        public int seatClass1 { set; get; }
        public int seatClass2 { set; get; }
        public int seatClass3 { set; get; }
        public decimal? total { set; get; }
    }
}