using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_PDF.MySqlClass
{
  public  struct StruckTest
    {
     public   int isBusy { get; set; }

       public int Address { get; set; }

        public int Month { get; set; }

        private int hour;
        public int YEAR { get; set; }
        public int Hour
        {
            get { return hour; }
            set
            {
                if(value<22)
                {
                    hour = value + 2;
                }
                else
                {
                    hour = value - 22 ;
                }
            }
        }
    }
}
