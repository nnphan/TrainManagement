using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainManagementWeb.Models.ViewModel
{
    public class CartItemViewModel
    {
        public string trainCode { set; get; }
        public string depStation { set; get; }
        public string arStation { set; get; }
        public string deDay { set; get; }
        public string depTime { set; get; }
        public decimal? price { set; get; }
        public int classSeatId { set; get; }
        public string classSeat { set; get; }
        public int? seatNum { set; get; }
        public int? coachNum { set; get; }
    }
}