using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainManagementWeb.Models.EF;
using TrainManagementWeb.Models.ViewModel;

namespace TrainManagementWeb.Models.DAO
{
    public class UserDao
    {
        TrainManagementEntities db = null;
        public UserDao()
        {
            db = new TrainManagementEntities();
        }

        public sp_User_Login_Result Login(string email, string passWord, bool isLoginAdmin = false)
        {

            var checkUserExist = db.sp_User_Login(email,passWord).FirstOrDefault();
            return checkUserExist;
        }
       
        public decimal Insert(tblUser user)
        {
            db.tblUsers.Add(user);
            db.SaveChanges();
            return user.Id;
        }

        public List<tblUser> GetListUser()
        {
            var listUser = db.tblUsers.ToList();
            return listUser;
        }

        public CreateUserModel GetUserDetail(decimal id)
        {
            var userDetail = db.tblUsers.Find(id);
            var userViewModel = new CreateUserModel();
            userViewModel.UserName = userDetail.UserName;
            userViewModel.EmailAddress = userDetail.EmailAddress;
            if(userDetail.RoleId == 1 ) // check role admin
            {
                userViewModel.Role = "ADMIN";
            }
            else
            {
                userViewModel.Role = "USER";
            }
            return userViewModel;
        }

        public tblUser GetUserLogin(decimal? id)
        {
            var userDetail = db.tblUsers.Find(id);
            return userDetail;
        }


        public bool Update(CreateUserModel userUpdate)
        {
            try
            {
                var user = db.tblUsers.Find(userUpdate.Id);
                user.UserName = userUpdate.UserName;
                user.EmailAddress = userUpdate.EmailAddress;
                user.PhoneNumber = userUpdate.PhoneNumber;
                if (userUpdate.Role == "ADMIN")
                {
                    user.RoleId = 1;
                }
                else
                {
                    user.RoleId = 2;
                }
                
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
        public bool Delete(decimal id)
        {
            try
            {
                var user = db.tblUsers.Find(id);
                db.tblUsers.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


    }
}