using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_PDF.DateTimeConvert
{
   static class TimeConvert
    {
        public static string UnixTimeStampToDateTime(this double unixTimeStamp)
        {
           
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime.ToString();
        }
        public static long ConvertToUnixTimestamp(this Nullable<DateTime> date)
        {
          
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.Value.ToUniversalTime() - origin;
            long time = (long)Math.Floor(diff.TotalSeconds);
            while (time.ToString().Length != 13)
            {
                time = long.Parse(time.ToString() + "0");
            }
            return time;
        }
    }
}
