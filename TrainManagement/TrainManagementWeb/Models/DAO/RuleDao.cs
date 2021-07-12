using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainManagementWeb.Models.EF;
using TrainManagementWeb.Models.ViewModel;

namespace TrainManagementWeb.Models.DAO
{
    public class RuleDao
    {
        TrainManagementEntities db = null;
        public RuleDao()
        {
            db = new TrainManagementEntities();
        }
        public double GetRuleClassValue(int classid)
        {
            var rule = db.tblRuleClasses.ToList().Where(x =>x.Class == classid).Select(x => x.Value).FirstOrDefault().Value;
            return rule;
        }
        public double GetRuleDistanceValue()
        {
            var rule = db.tblRuleDistances.ToList().Select(x => x.Value).FirstOrDefault();
            return rule.Value;
        }

        public RuleViewModel GetRule()
        {
            var ruleDistance = db.tblRuleDistances.FirstOrDefault().Value;
            var ruleCancel = db.tblRuleCancelBookings.FirstOrDefault().Value;
            var ruleClass1 = db.tblRuleClasses.ToList().Where(x => x.Class == 1).Select(x => x.Value).FirstOrDefault().Value;
            var ruleClass2 = db.tblRuleClasses.ToList().Where(x => x.Class == 2).Select(x => x.Value).FirstOrDefault().Value;
            var ruleClass3 = db.tblRuleClasses.ToList().Where(x => x.Class == 3).Select(x => x.Value).FirstOrDefault().Value;
            RuleViewModel ruleViewModel = new RuleViewModel();
            ruleViewModel.RuleDistanceValue = ruleDistance.Value;
            ruleViewModel.RuleCancelValue = ruleCancel.Value;
            ruleViewModel.RuleClass1Value = ruleClass1;
            ruleViewModel.RuleClass2Value = ruleClass2;
            ruleViewModel.RuleClass3Value = ruleClass3;
            return ruleViewModel;
        }

        public bool UpdateRule(RuleViewModel ruleViewModel)
        {
            var ruleDistance = db.tblRuleDistances.FirstOrDefault();
            var ruleCancel = db.tblRuleCancelBookings.FirstOrDefault();
            var ruleClass1 = db.tblRuleClasses.ToList().Where(x => x.Class == 1).FirstOrDefault();
            var ruleClass2 = db.tblRuleClasses.ToList().Where(x => x.Class == 2).FirstOrDefault();
            var ruleClass3 = db.tblRuleClasses.ToList().Where(x => x.Class == 3).FirstOrDefault();
            ruleDistance.Value = ruleViewModel.RuleDistanceValue;
            ruleCancel.Value = ruleViewModel.RuleCancelValue;
            ruleClass1.Value = ruleViewModel.RuleClass1Value;
            ruleClass2.Value = ruleViewModel.RuleClass2Value;
            ruleClass3.Value = ruleViewModel.RuleClass3Value;
            db.SaveChanges();
            return true;
        }


    }
}