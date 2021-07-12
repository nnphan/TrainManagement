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
        [HttpGet]
        public ActionResult CartDetail()
        {
            var cart = Session[CartSession];
            BookingCart listCart = new BookingCart();
            BookingCartViewModel bookingCartViewModel = new BookingCartViewModel();
            List<CartItemViewModel> listItem = new List<CartItemViewModel>();
            var cartItemViewModel1 = new CartItemViewModel();
            cartItemViewModel1.trainCode = "T01";
            cartItemViewModel1.depStation = "Sai gon";
            cartItemViewModel1.classSeat = "Nha Trang";
            cartItemViewModel1.deDay = "22/07/2021";
            cartItemViewModel1.depTime = "20:20";
            cartItemViewModel1.price = 1234455;

            if (cart != null)
            {
               
                listCart = (BookingCart)cart;
                if(listCart.total == null)
                {
                    bookingCartViewModel.total = 0;
                }
                else
                {
                    bookingCartViewModel.total = listCart.total.Value;
                }
                bookingCartViewModel.quantity = 1;
               var k = listCart.listItem;
                foreach (CartItem i in k)
                {
                    CartItemViewModel cartItemViewModel = new CartItemViewModel();
                    cartItemViewModel.trainCode = i.BookingTrain.trainCode;
                    cartItemViewModel.depStation = i.BookingTrain.depStation;
                    cartItemViewModel.arStation = i.BookingTrain.arStation;
                    cartItemViewModel.deDay = i.BookingTrain.deDay; 
                    cartItemViewModel.depTime = i.BookingTrain.depTime;
                    cartItemViewModel.price = i.BookingTrain.price;
                    cartItemViewModel.classSeatId = i.BookingTrain.classSeatId;
                    cartItemViewModel.classSeat = i.BookingTrain.classSeat;
                    cartItemViewModel.seatNum = i.BookingTrain.seatNum;
                    cartItemViewModel.coachNum = i.BookingTrain.coachNum;
                    listItem.Add(cartItemViewModel);
                }
                return View(listItem);
            }
            else
            {
                listItem = null;
                return View(listItem);
            }
        }

      

        public ActionResult InfoCart(string trainCode, string depStation, string arStation, string deDay, string depTime)
        {
            DateTime date = DateTime.ParseExact(deDay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            deDay = date.ToString("MM/dd/yyyy");
            List<CartItemViewModel> listItem = new List<CartItemViewModel>();
            var cartItemViewModel1 = new CartItemViewModel();

            var cart = Session[CartSession];
            BookingCart listCart = new BookingCart();
            listCart = (BookingCart)cart;
            foreach (CartItem i in listCart.listItem)
            {
                CartItemViewModel cartItemViewModel = new CartItemViewModel();
                cartItemViewModel.trainCode = i.BookingTrain.trainCode;
                cartItemViewModel.depStation = i.BookingTrain.depStation;
                cartItemViewModel.arStation = i.BookingTrain.arStation;
                cartItemViewModel.deDay = i.BookingTrain.deDay;
                cartItemViewModel.depTime = i.BookingTrain.depTime;
                cartItemViewModel.price = i.BookingTrain.price;
                cartItemViewModel.classSeatId = i.BookingTrain.classSeatId;
                cartItemViewModel.classSeat = i.BookingTrain.classSeat;
                cartItemViewModel.seatNum = i.BookingTrain.seatNum;
                cartItemViewModel.coachNum = i.BookingTrain.coachNum;
                listItem.Add(cartItemViewModel);
            }

            var total = listCart.total.Value.ToString("N0");
            ViewBag.trainCode = trainCode;
            ViewBag.depStation = depStation;
            ViewBag.arStation = arStation;
            ViewBag.deDay = deDay;
            ViewBag.depTime = depTime;
            ViewBag.Total = total;
            return View(listItem);

        }



        public ActionResult DeleteAll()
        {
            Session[CartSession] = null;
            var list = new List<CartItem>();
            return RedirectToAction("CartInfo");
            //return Json(new
            //{
            //    status = true
            //});
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
                //trainBooking.seatNum = trainDao.GetSeatBooked(trainCode,1 , deDay) + 1;
                trainBooking.classSeatId = 1;
            }
            else if (classSeat == "First Class Coaches")
            {
                //trainBooking.seatNum = trainDao.GetSeatBooked(trainCode, 2 , deDay) + 1;
                trainBooking.classSeatId = 2;
            }
            else
            {
                //trainBooking.seatNum = trainDao.GetSeatBooked(trainCode, 3 , deDay) + 1;
                trainBooking.classSeatId = 3;
            }
            if (cart != null)
            {
                var cartList = (BookingCart)cart;
                if (cartList.listItem.Exists(x => x.BookingTrain.classSeat == classSeat))
                {
                    if(classSeat == "AC Coaches Class")
                    {
                        trainBooking.seatNum = cartList.seatClass1 + 1;
                        trainBooking.coachNum = 2;
                    }
                    else if (classSeat ==  "First Class Coaches")
                    {
                        trainBooking.seatNum = cartList.seatClass2 + 1;
                        trainBooking.coachNum = 5;
                    }
                    else
                    {
                        trainBooking.seatNum = cartList.seatClass3 + 1;
                        trainBooking.coachNum = 8;
                    }

                }
                else
                {
                    //Create new item
                    var item = new CartItem();
                    item.BookingTrain = trainBooking;
                    if (classSeat == "AC Coaches Class")
                    {
                        trainBooking.seatNum = cartList.seatClass1 + 1;
                        trainBooking.seatNum = trainDao.GetSeatBooked(trainCode, 1, deDay) + 1;
                        trainBooking.coachNum = 2;
                    }
                    else if (classSeat == "First Class Coaches")
                    {
                        trainBooking.seatNum = cartList.seatClass2 + 1;
                        trainBooking.seatNum = trainDao.GetSeatBooked(trainCode, 2, deDay) + 1;
                        trainBooking.coachNum = 5;
                    }
                    else
                    {
                        trainBooking.seatNum = cartList.seatClass3 + 1;
                        trainBooking.seatNum = trainDao.GetSeatBooked(trainCode, 3, deDay) + 1;
                        trainBooking.coachNum = 8;
                    }
                    cartList.quantity += 1;
                    cartList.total += price;
                    cartList.listItem.Add(item);
                }
                //Add to Session               
                Session[CartSession] = cartList;
            }
            else
            {
                var cartList = new BookingCart();
                var item = new CartItem();
                item.BookingTrain = trainBooking;
                if (classSeat == "AC Coaches Class")
                {
                    trainBooking.seatNum = trainDao.GetSeatBooked(trainCode, 1, deDay) + 1;
                    trainBooking.coachNum = 2;
                    cartList.seatClass1 = trainBooking.seatNum;
                }
                else if (classSeat == "First Class Coaches")
                {
                    trainBooking.seatNum = trainDao.GetSeatBooked(trainCode, 2, deDay) + 1;
                    trainBooking.coachNum = 5;
                    cartList.seatClass2 = trainBooking.seatNum;
                }
                else
                {
                    trainBooking.seatNum = trainDao.GetSeatBooked(trainCode, 3, deDay) + 1;
                    trainBooking.coachNum = 8;
                    cartList.seatClass3 = trainBooking.seatNum;

                }
                item.Quantity = 1;    // Update quantity
                
                List<CartItem> listItem = new List<CartItem>();
                listItem.Add(item);
                cartList.quantity = 1;
                cartList.total = price;
                cartList.listItem = listItem;
                //Add to Session
                Session[CartSession] = cartList;
            }
            var cartSession = Session[CartSession];
            var cartListSession = (BookingCart)cartSession;
            return RedirectToAction("InfoCart","Cart", new { @trainCode = trainCode, @depStation = depStation, @arStation = arStation, @deDay = deDay, @depTime = depTime });
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                var cartList = (BookingCart)cart;
                list = cartList.listItem;
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
            string PRN = string.Empty;
            string totalPrice = string.Empty;
            try
            {
                String date = DateTime.Now.Day.ToString();
                String Month = DateTime.Now.Month.ToString();
                String Year = DateTime.Now.Year.ToString();
                var cart = (BookingCart)Session[CartSession];
                var detailDao = new BookingDetailDao();
                var bookingDetail = new tblBookingDetail();
                decimal total = 0;
                total = cart.total.Value;
                totalPrice = cart.total.Value.ToString("N0");
                booking.PRN = passporid + date + Month ;
                PRN = passporid + date + Month;
                var bookingDao = new BookingDao();
                var id = bookingDao.Insert(booking);

                foreach (var item in cart.listItem)
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
                return Redirect("/");
            }
            Session.Remove("CartSession");
            return RedirectToAction("Success", "Cart", new { @prn = PRN, @total = totalPrice });

        }

        public ActionResult Success(string prn, string total)
        {
            ViewBag.prn = prn;
            ViewBag.total = total;
            return View();
        }
    }
}