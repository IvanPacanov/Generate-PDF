using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_PDF.VM
{
  public  class IdParking
    {
        public int idParking { get; set; }
        public string name { get; set; }
        public int GrupuByTime { get; set; }

        public int Year { get; set; }

        public int GrubByInEachHour { get; set; }
        public double count { get; set; }
    }
}
