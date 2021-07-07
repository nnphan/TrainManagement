using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainManagementWeb.Common;
using TrainManagementWeb.Models;
using TrainManagementWeb.Models.DAO;
using TrainManagementWeb.Models.EF;
using TrainManagementWeb.Models.ViewModel;

namespace TrainManagementWeb.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        //private readonly TrainManagementEntities _db;
        //public AccountController(TrainManagementEntities db)
        //{
        //    _db = db ;
        //}
        // GET: Admin/Account
        public ActionResult Index()
        {
           // var user = _db.tblUsers.ToList();
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.ViewModel.LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var userDao = new UserDao();
                var checkLogin = userDao.Login(model.Email, Encryptor.MD5Hash(model.Password), model.RememberMe);
                if (checkLogin != null && checkLogin.Result == true)
                {
                    var userDetail = userDao.GetUserLogin(checkLogin.UserId);
                    var userSession = new UserLogin();
                    userSession.UserID = userDetail.Id;
                    userSession.UserName = userDetail.UserName;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("List", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is not correct.");
                }
            }
            
            return View("Login");
        }
    }
}