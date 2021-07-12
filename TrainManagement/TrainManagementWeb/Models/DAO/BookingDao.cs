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

        public List<sp_GetBookingDetailList_Result> GetBookingListForAdmin()
        {
            var lisBooking = db.sp_GetBookingDetailList().ToList();
            return lisBooking;
        }

        public BookingDetailViewModel GetBookingInfo(SearchBookingViewModel booking)
        {
            var tblBooking = db.tblBookings.Where(x => x.PRN == booking.PRNno).FirstOrDefault();
            if (tblBooking != null)
            {
                var bookingDetail = new List<sp_GetBookingDetail_Result>();
                var bookingDetailViewModel = new BookingDetailViewModel();
                var totalPrice = db.tblBookings.Where(x => x.PRN == booking.PRNno).FirstOrDefault().TotalPrice.Value;
                if (booking.PRNno != null)
                {
                    bookingDetail = db.sp_GetBookingDetail().Where(x => x.PRN == booking.PRNno).ToList();
                }
                else
                {
                    bookingDetail = db.sp_GetBookingDetail().Where(x => x.PhoneNumber == booking.PhoneNumber).ToList();
                }
                bookingDetailViewModel.bookingDetail = bookingDetail;
                bookingDetailViewModel.totalPrice = totalPrice;
                return bookingDetailViewModel;
            }
            else
            {
                return null;
            }
            
        }

        public CancelBookingViewModel UpdateCancelBooking(string prn)
        {
            var ruleCancel = db.tblRuleCancelBookings.Select(x => x.Value).FirstOrDefault();
            var booking = db.tblBookings.Where(x => x.PRN == prn).FirstOrDefault();
            CancelBookingViewModel cancelBooking = new CancelBookingViewModel();
            if (booking != null)
            {
                booking.IsCancel = true;
                cancelBooking.IsSuccess = true;
                cancelBooking.RefMoney = (booking.TotalPrice - (booking.TotalPrice * (decimal)ruleCancel)).Value;
                db.SaveChanges();
            }
            else
            {
                cancelBooking.IsSuccess = false;
                cancelBooking.RefMoney = null;
            }
            return cancelBooking;
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