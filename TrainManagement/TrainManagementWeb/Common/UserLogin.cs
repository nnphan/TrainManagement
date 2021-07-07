using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainManagementWeb.Common
{
    [Serializable]
    public class UserLogin
    {
        public decimal UserID { set; get; }
        public string UserName { set; get; }
        public string GroupID { set; get; }
    }
}