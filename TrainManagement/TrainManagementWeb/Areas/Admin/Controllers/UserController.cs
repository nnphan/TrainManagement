using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainManagementWeb.Common;
using TrainManagementWeb.Models.DAO;
using TrainManagementWeb.Models.EF;
using TrainManagementWeb.Models.ViewModel;

namespace TrainManagementWeb.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/User
        public ActionResult List()
        {
            var useDao = new UserDao();
            var model = useDao.GetListUser();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateUserModel createUserModel)
        {
            if (ModelState.IsValid)
            {
                var userPassword = "P@ssword123";
                var userDao = new UserDao();
                var encryptedMd5Pas = Encryptor.MD5Hash(userPassword);

                tblUser user = new tblUser();
                user.UserName = createUserModel.UserName;
                user.EmailAddress = createUserModel.EmailAddress;
                user.Password = encryptedMd5Pas;
                user.PhoneNumber = createUserModel.PhoneNumber;
                user.CreatedDate = DateTime.Now;
                if (createUserModel.Role == "Admin")
                {
                    user.RoleId = 1;
                }
                else
                    user.RoleId = 2;

                decimal id = userDao.Insert(user);
                if (id > 0)
                {
                    //SetAlert("Thêm user thành công", "success");  
                    return RedirectToAction("List", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user không thành công");
                }
            }
            return View("Index");
        }

       [HttpGet]
        public ActionResult Edit(decimal id)
        {
            var user = new UserDao().GetUserDetail(id);
            return View(user);
        }


        [HttpPost]
        //[HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(CreateUserModel user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                //if (!string.IsNullOrEmpty(user.Password))
                //{
                //    var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                //    user.Password = encryptedMd5Pas;
                //}


                var result = dao.Update(user);
                if (result)
                {
                    //SetAlert("Sửa user thành công", "success");
                    return RedirectToAction("List", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật user không thành công");
                }
            }
            return View("List");
        }
        [HttpDelete]
        //[HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(decimal id)
        {
            var userDao = new UserDao();
            var result = userDao.Delete(id);
            if (result)
            {
                //SetAlert("Delete User Successfull", "success");
                return RedirectToAction("List", "User");
            }
            else
            {
                //SetAlert("Delete User Fail"", "success");
                return RedirectToAction("List", "User");
            }
            
        }
    }
}