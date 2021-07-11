using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainManagementWeb.Models.DAO;
using TrainManagementWeb.Models.ViewModel;

namespace TrainManagementWeb.Areas.Admin.Controllers
{
    public class RuleController : Controller
    {
        // GET: Admin/Rule
        public ActionResult Detail()
        {
            var ruleDao = new RuleDao();
            var rule = ruleDao.GetRule();
            return View(rule);
        }

        public ActionResult UpdateRule(RuleViewModel ruleViewModel)
        {
            var ruleDao = new RuleDao();
            var rule = ruleDao.UpdateRule(ruleViewModel);
            return View("Detail");
        }
    }
}