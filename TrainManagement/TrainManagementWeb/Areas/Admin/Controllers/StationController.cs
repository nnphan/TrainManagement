using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TrainManagementWeb.Models.DAO;
using TrainManagementWeb.Models.EF;
using TrainManagementWeb.Models.ViewModel;

namespace TrainManagementWeb.Areas.Admin.Controllers
{
    public class StationController : BaseController
    {
        // GET: Admin/Station
        public ActionResult Index()
        {
            return View();
        }
        // GET: Admin/Station
        public ActionResult List()
        {
            var stationDao = new StationDao();
            var listStation = stationDao.GetListStation();
            return View(listStation);
        }

        [HttpGet]
        public JsonResult GetStations(string search)

        {
            var stationDao = new StationDao();
            var station = stationDao.GetListStation();
            List<string> stations = station.Where(s => s.StationName.StartsWith(search))
                .Select(x => x.StationName).ToList();

            return new JsonResult { Data = stations, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetSearchValue(string search)
        {
            var stationDao = new StationDao();
            var listStation = stationDao.GetListStationJson(search);
            return new JsonResult { Data = listStation, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateStationModel createStationModel) 
        {
            if (ModelState.IsValid)
            {
                tblStation station = new tblStation();
                station.StationName = createStationModel.StationName;
                station.StationCode = createStationModel.StationCode;
                station.RailwayDivisionName = createStationModel.RailwayDivisionName;
                station.PositonDistance = createStationModel.PositionDistance;
                station.CreatedDate = DateTime.Now;
                var stationDao = new StationDao();
                var id = stationDao.Insert(station);
                if(id > 0)
                {
                    //SetAlert("Thêm Station thành công", "success");  
                    return RedirectToAction("List", "Station");
                }
                else
                {
                    ModelState.AddModelError("", "Add Train Fail");
                }
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult Edit( decimal id)
        {
            var stationDao = new StationDao();
            var station = stationDao.GetStationDetail(id);
            return View(station);
        }


        [HttpPost]
        public ActionResult Edit(tblStation stationModel)
        {
            if (ModelState.IsValid)
            {
                var stationDao = new StationDao();
                //if (!string.IsNullOrEmpty(user.Password))
                //{
                //    var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                //    user.Password = encryptedMd5Pas;
                //}


                var result = stationDao.Update(stationModel);
                if (result)
                {
                    //SetAlert("Sửa user thành công", "success");
                    return RedirectToAction("List", "Station");
                }
                else
                {
                    ModelState.AddModelError("", "Update Station Fail");
                }
            }
            return View("List");
        }

        [HttpDelete]
        public ActionResult Delete(decimal id)
        {
            var stationDao = new StationDao();
            var result = stationDao.Delete(id);
            if (result)
            {
                //SetAlert("Delete User Successfull", "success");
                return RedirectToAction("List", "Station");
            }
            else
            {
                //SetAlert("Delete User Fail"", "success");
                return RedirectToAction("List", "Station");
            }

        }



    }
}