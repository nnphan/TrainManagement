using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainManagementWeb.Models.EF;

namespace TrainManagementWeb.Models.DAO
{
    public class BookingDetailDao
    {
        TrainManagementEntities db = null;
        public BookingDetailDao()
        {
            db = new TrainManagementEntities();
        }
        public bool Insert(tblBookingDetail detail)
        {
            try
            {
                db.tblBookingDetails.Add(detail);
                //var booking = db.tblBookings.Find(detail.BookingId);
                //booking.TotalPrice = total;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;

            }
        }
    }
}