using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainManagementWeb.Models.EF;
using TrainManagementWeb.Models.ViewModel;

namespace TrainManagementWeb.Models.DAO
{
    public class BookingDao
    {
        TrainManagementEntities db = null;
        public BookingDao()
        {
            db = new TrainManagementEntities();
        }
        public decimal Insert(tblBooking booking)
        {
            db.tblBookings.Add(booking);
            db.SaveChanges();
            return booking.Id;
        }

        public tblBooking GetBookingInfo(SearchBookingViewModel booking)
        {
            tblBooking bookingInfo = new tblBooking();
            if (booking.PRNno != null)
            {
                bookingInfo = db.tblBookings.Where(x => x.PRN == booking.PRNno).FirstOrDefault();
            }
            else
            {
                bookingInfo = db.tblBookings.Where(x => x.PhoneNumber == booking.PhoneNumber).FirstOrDefault();
            }
            return bookingInfo;
        }

        public bool UpdateTotal (decimal id, decimal total)
        {
            tblBooking bookingInfo = db.tblBookings.Find(id);     
            if(bookingInfo != null)
            {
                bookingInfo.TotalPrice = total;
                db.SaveChanges();
                return true;
            }
            return false;
            
        }
    }
}