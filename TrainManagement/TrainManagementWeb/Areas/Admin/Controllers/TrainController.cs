using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainManagementWeb.Models.DAO;
using TrainManagementWeb.Models.EF;

namespace TrainManagementWeb.Areas.Admin.Controllers
{
    public class TrainController : BaseController
    {
        // GET: Admin/Train
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Train
        public ActionResult List()
        {
            var trainDao = new TrainDao();
            var listTrain = trainDao.GetListTrain();
            return View(listTrain);
        }

        

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tblTrain trainModel)
        {
            if (ModelState.IsValid)
            {
                var trainDao = new TrainDao();
                var id = trainDao.Insert(trainModel);
                if (id > 0)
                {
                    //SetAlert("Thêm user thành công", "success");  
                    return RedirectToAction("List", "Train");
                }
                else
                {
                    ModelState.AddModelError("", "Add Train Fail");
                }
            }
            return View("Index");

        }

        
        [HttpGet]
        public ActionResult Edit(decimal id)
        {
            var train = new TrainDao().GetTrainDetail(id);
            return View(train);
        }

        [HttpPost]
        public ActionResult Edit(tblTrain user)
        {
            if (ModelState.IsValid)
            {
                var trainDao = new TrainDao();

                var result = trainDao.Update(user);
                if (result)
                {
                    //SetAlert("Sửa user thành công", "success");
                    return RedirectToAction("List", "Train");
                }
                else
                {
                    ModelState.AddModelError("", "Update Train Fail");
                }
            }
            return View("List");
        }


        public ActionResult Delete(decimal id)
        {
            var trainDao = new TrainDao();
            var result = trainDao.Delete(id);
            if (result)
            {
                //SetAlert("Delete User Successfull", "success");
                return RedirectToAction("List", "Train");
            }
            else
            {
                //SetAlert("Delete User Fail"", "success");
                return RedirectToAction("List", "Train");
            }

        }


    }

}