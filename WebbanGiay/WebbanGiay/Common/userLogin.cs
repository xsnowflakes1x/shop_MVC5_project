using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebbanGiay
{
    [Serializable]
    public class userLogin
    {
        public int UserID { set; get; }
        public string UserName { set; get; }
    }
}