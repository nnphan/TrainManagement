using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainManagementWeb.Models.EF;

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
            var rule = db.tblRuleClasses.ToList().Where(x =>x.Class == classid).Select(x => x.Value).FirstOrDefault();
            return rule;
        }
        public double GetRuleDistanceValue()
        {
            var rule = db.tblRuleDistances.ToList().Select(x => x.Value).FirstOrDefault();
            return rule.Value;
        }


    }
}