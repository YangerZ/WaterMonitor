﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminLTE
{
    public class Util
    {
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}