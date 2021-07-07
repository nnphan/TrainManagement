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
            var trainScheduleDetail = trainScheduleDao.GetTrainScheduleList(departureStationName, arrivalStationName, departureDate).Where(x => x.Train == trainCode).FirstOrDefault();
            var model = new TrainDao().GetTrainDetail(trainScheduleDetail.TrainId.Value);
            return View();
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

        
    }
}