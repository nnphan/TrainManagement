using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainManagementWeb.Models.DAO;
using TrainManagementWeb.Models.ViewModel;

namespace TrainManagementWeb.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetListTrainSchedule(SearchTrainViewModel searchTrain )
        {
            if (ModelState.IsValid)
            {
                TrainScheduleDao trainScheduleDao = new TrainScheduleDao();
                var listTrainSchedule = trainScheduleDao.GetTrainScheduleClient(searchTrain.DepartureStationName, searchTrain.ArrivalStationName, searchTrain.DepartureDate);
                return View(listTrainSchedule);
            }
            return View("Index");
        }
        [HttpGet]
        public ActionResult GetListBooking(SearchBookingViewModel searchBooking)
        {
            if (ModelState.IsValid)
            {
                BookingDao bookingDao = new BookingDao();
                var bookingInfo = bookingDao.GetBookingInfo(searchBooking);
                if (bookingInfo != null)
                {
                    ViewBag.PRN = searchBooking.PRNno;
                    return View(bookingInfo);
                }
                else
                {
                    ModelState.AddModelError("", "Booking info detail is not found.");
                }
                               
            }
            return View("Index");
        }

        public ActionResult CancelBooking(string prn)
        {
            if (ModelState.IsValid)
            {
                BookingDao bookingDao = new BookingDao();
                var cancelBooking = bookingDao.UpdateCancelBooking(prn);
                if (cancelBooking.IsSuccess  == true)
                {
                    ViewBag.PRN = prn;
                    ViewBag.RefundMoney = cancelBooking.RefMoney.Value.ToString("N0");
                    // returm success cancel view
                    return Redirect("/Booking/SuccessCancel");
                }
                else
                {
                    return Redirect("/Booking/FailCancel");
                }

            }
            return View("Index");
        }

        public void SetViewBag(decimal? selectedId = null)
        {
            var stationDao = new StationDao();
            var trainDao = new TrainDao();
            var stationList = stationDao.GetListStation();
            var trainList = trainDao.GetListTrain();
            //ViewBag.CategoryID = new SelectList(stationDao.GetListStation, "ID", "Name", selectedId);
            ViewBag.StationId = new SelectList(stationList, "Id", "StationName", selectedId);
        }

        public ActionResult Detail(string trainCode, string departureStationName, string arrivalStationName, string departureDate)
        {
            TrainScheduleDao trainScheduleDao = new TrainScheduleDao();
            var trainDao = new TrainDao();
            var trainScheduleDetail = trainScheduleDao.GetTrainScheduleList(departureStationName, arrivalStationName, departureDate).Where(x => x.Train == trainCode).FirstOrDefault();
            var train = trainDao.GetTrainDetail(trainScheduleDetail.TrainId.Value);
            var station = new StationDao();
            var rule = new RuleDao();
            TrainBookingViewModel model = new TrainBookingViewModel();
            var arrivalStation = station.GetStationByName(trainScheduleDetail.ArrivalStation);
            var arrivalStationDistance = arrivalStation.PositonDistance;
            model.TrainId = trainScheduleDetail.TrainId.Value;
            model.TrainCode = trainScheduleDetail.Train;
            model.Departure = trainScheduleDetail.DepartureStation;
            model.Arrival = trainScheduleDetail.ArrivalStation;
            model.DepartureDate = trainScheduleDetail.DepartureDate.Value.ToString("dd/MM/yyyy");
            model.DepartureTime = trainScheduleDetail.DepartureTime;
            var seatPrice1 = train.ProposedPrice.Value * (decimal)rule.GetRuleClassValue(1);
            var seatPrice2 = train.ProposedPrice.Value * (decimal)rule.GetRuleClassValue(2);
            var seatPrice3 = train.ProposedPrice.Value * (decimal)rule.GetRuleClassValue(3);
            var priceDistance = arrivalStationDistance * rule.GetRuleDistanceValue();
            model.SeatPrice1 = seatPrice1 + (decimal)priceDistance;
            model.SeatPrice2 = seatPrice2 + (decimal)priceDistance;
            model.SeatPrice3 = seatPrice3 + (decimal)priceDistance;
            model.Seat1Available = trainDao.GetSeatAvailable(trainCode,1, departureDate) ;
            model.Seat2Available = trainDao.GetSeatAvailable(trainCode,2, departureDate);
            model.Seat3Available = trainDao.GetSeatAvailable(trainCode,3, departureDate);

            return View(model);
        }

        [HttpGet]
        public ActionResult SearchTrain()
        {
            var model = new TrainDao().GetListTrain();
            SetViewBag();
            return PartialView();
        }

        public ActionResult SearchReservation()
        {
            var model = new TrainDao().GetListTrain();
            SetViewBag();
            return PartialView();
        }

        public ActionResult SuccessCancel()
        {
            return View();
        }
        public ActionResult FailCancel()
        {
            return View();
        }

        
    }
}