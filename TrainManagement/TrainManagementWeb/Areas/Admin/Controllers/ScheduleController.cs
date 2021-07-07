using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainManagementWeb.Models.DAO;
using TrainManagementWeb.Models.ViewModel;

namespace TrainManagementWeb.Areas.Admin.Controllers
{
    public class ScheduleController : BaseController
    {
        // GET: Admin/Schedule
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            //var useDao = new UserDao();
            //var model = useDao.GetListUser();
            //return View(model);
            return View();
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

        public ActionResult TrainScheduleMenu()
        {
            var model = new TrainDao().GetListTrain();
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult GetTrainSchedule(decimal id)
        {
            var scheduledao = new TrainScheduleDao();
            var listSchedule = scheduledao.GetTrainScheduleById(id);
            ViewBag.TrainId = id.ToString();
            return View(listSchedule);
        }


        [HttpGet]
        public ActionResult Create(decimal id)
        {
            var scheduledao = new TrainScheduleDao();
            var trainList = new TrainDao().GetListTrain();
            var listSchedule = scheduledao.GetTrainScheduleById(id);
            var trainCode = trainList.Where(x => x.Id == id).Select(x => x.TrainCode).FirstOrDefault();
            ViewBag.TrainCode = trainCode.ToString();
            ViewBag.TrainId = id.ToString();
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(TrainScheduleViewModel trainScheduleModel)
        {
            if (ModelState.IsValid)
            {
                var scheduledao = new TrainScheduleDao();
                var trainList = new TrainDao().GetListTrain();
                //Convert string to date time format
                DateTime departureDate = DateTime.ParseExact(trainScheduleModel.DepartureDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var trainExist = scheduledao.GetTrainScheduleById(trainScheduleModel.TrainId).Where(x => x.DepartureDate == departureDate && x.StationId == trainScheduleModel.StationId).FirstOrDefault();
                if(trainExist != null)
                {
                    SetViewBag();
                    ModelState.AddModelError("", "Station Exist In Train Schedule ");
                }
                else
                {
                    var id = scheduledao.Insert(trainScheduleModel);
                    if (id > 0)
                    {
                        SetViewBag();
                        //SetAlert("Thêm user thành công", "success");  
                        return RedirectToAction("GetTrainSchedule", "Schedule", new { @id = trainScheduleModel.TrainId });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Add Train Schedule Fail");
                    }
                }
                
            }         
            return View();
        }

        [HttpGet]
        public ActionResult Edit(decimal id)
        {
            var scheduledao = new TrainScheduleDao();
            var trainSchedule = scheduledao.GetTrainScheduleDetail(id);

            var trainList = new TrainDao().GetListTrain();
            var listSchedule = scheduledao.GetTrainScheduleById(id);
            var trainCode = trainList.Where(x => x.Id == trainSchedule.TrainId).Select(x => x.TrainCode).FirstOrDefault();
            ViewBag.TrainCode = trainCode.ToString();
            ViewBag.TrainId = trainSchedule.TrainId.ToString();
            SetViewBag();
            return View(trainSchedule);
        }

        [HttpPost]
        public ActionResult Edit(TrainScheduleViewModel trainScheduleModel)
        {
            if (ModelState.IsValid)
            {
                var trainScheduleDao = new TrainScheduleDao();

                var result = trainScheduleDao.Update(trainScheduleModel);
                if (result)
                {
                    //SetAlert("Sửa user thành công", "success");
                    SetViewBag();
                    return RedirectToAction("GetTrainSchedule", "Schedule", new { @id = trainScheduleModel.TrainId });
                }
                else
                {
                    ModelState.AddModelError("", "Update Train Schedule Fail");
                }
            }
            return View("List");
        }

        public ActionResult Delete(decimal id)
        {
            var trainScheduleDao = new TrainScheduleDao();
            var trainSchedule = trainScheduleDao.GetTrainScheduleDetail(id);
            var result = trainScheduleDao.Delete(id);
            if (result)
            {
                SetViewBag();
                return RedirectToAction("GetTrainSchedule", "Schedule", new { @id = trainSchedule.TrainId });
            }
            else
            {
                SetViewBag();
                return RedirectToAction("GetTrainSchedule", "Schedule", new { @id = trainSchedule.TrainId });
            }

        }

    }
}