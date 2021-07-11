using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainManagementWeb.Models.DAO;
using TrainManagementWeb.Models.EF;
using TrainManagementWeb.Models.ViewModel;

namespace TrainManagementWeb.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult CartDetail()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult AddItem(string trainCode, string depStation, string arStation, string deDay, string depTime,decimal price, string classSeat )
        {
            var trainDao = new TrainDao();
            var cart = Session[CartSession];
            var trainBooking = new BookingTrain();
            trainBooking.trainCode = trainCode;
            trainBooking.depStation = depStation;
            trainBooking.arStation = arStation;
            trainBooking.deDay = deDay;
            trainBooking.depTime = depTime;
            trainBooking.classSeat = classSeat;
            trainBooking.price = price;
            if (classSeat == "AC Coaches Class")
            {
                trainBooking.seatNum = trainDao.GetSeatBooked(trainCode,1 , deDay);
                trainBooking.classSeatId = 1;
            }
            else if (classSeat == "First Class Coaches")
            {
                trainBooking.seatNum = trainDao.GetSeatBooked(trainCode, 2 , deDay);
                trainBooking.classSeatId = 2;
            }
            else
            {
                trainBooking.seatNum = trainDao.GetSeatBooked(trainCode, 3 , deDay);
                trainBooking.classSeatId = 3;
            }
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.BookingTrain.classSeat == classSeat))
                {

                    foreach (var item in list)
                    {
                        if (item.BookingTrain.classSeat == classSeat)
                        {
                            item.Quantity += 1;
                        }
                    }
                }
                else
                {
                    //Create new item
                    var item = new CartItem();
                    item.BookingTrain = trainBooking;
                    item.Quantity = 1;
                    list.Add(item);
                }
                //Add to Session
                Session[CartSession] = list;
            }
            else
            {
                var item = new CartItem();
                item.BookingTrain = trainBooking;
                item.Quantity = 1;    // Update quantity
                var list = new List<CartItem>();
                list.Add(item);
                //Add to Session
                Session[CartSession] = list;
            }
            return RedirectToAction("CartDetail");
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string userName ,string passporid, string phoneNum, string email)
        {
            var booking = new tblBooking();
            booking.CreatedDate = DateTime.Now;
            booking.PhoneNumber = phoneNum;
            booking.PassportId = passporid;
            booking.UserName = userName;
            var trainDao = new TrainDao();
            var stationDao = new StationDao();

            try
            {
                String date = DateTime.Now.Day.ToString();
                String Month = DateTime.Now.Month.ToString();
                String Year = DateTime.Now.Year.ToString();
                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new BookingDetailDao();
                var bookingDetail = new tblBookingDetail();
                decimal total = 0;
                booking.PRN = passporid + date + Month ;
                var bookingDao = new BookingDao();
                var id = bookingDao.Insert(booking);

                foreach (var item in cart)
                {                   
                    bookingDetail.TrainCode = item.BookingTrain.trainCode;
                    bookingDetail.StartStation = stationDao.GetStationByName(item.BookingTrain.depStation).Id;
                    bookingDetail.EndStation = stationDao.GetStationByName(item.BookingTrain.arStation).Id;
                    bookingDetail.StartStationName = item.BookingTrain.depStation;
                    bookingDetail.EndStationName = item.BookingTrain.arStation;
                    bookingDetail.Class = item.BookingTrain.classSeatId;
                    bookingDetail.SeatNum = item.BookingTrain.seatNum;
                    bookingDetail.CoachNum = item.BookingTrain.coachNum;
                    DateTime depDay = DateTime.ParseExact(item.BookingTrain.deDay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    bookingDetail.DepDay = depDay;
                    bookingDetail.DepTime = item.BookingTrain.depTime;
                    bookingDetail.Price = item.BookingTrain.price;
                    bookingDetail.Quantity = item.Quantity;
                    bookingDetail.BookingId = id;
                    total += (item.BookingTrain.price * item.Quantity);
                    var res = detailDao.Insert(bookingDetail);
                }
                var updateTotal = bookingDao.UpdateTotal(id, total);
                //string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/neworder.html"));

                //content = content.Replace("{{CustomerName}}", shipName);
                //content = content.Replace("{{Phone}}", mobile);
                //content = content.Replace("{{Email}}", email);
                //content = content.Replace("{{Total}}", total.ToString("N0"));
                //var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                //new MailHelper().SendMail(email, "Đơn hàng mới từ OnlineShop", content);
                //new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
            }
            catch (Exception ex)
            {
                return Redirect("/loi-thanh-toan");
            }
            Session.Remove("CartSession");
            return Redirect("/Cart/Success");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}