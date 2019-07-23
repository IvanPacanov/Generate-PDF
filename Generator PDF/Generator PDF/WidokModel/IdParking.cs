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
        public int month { get; set; }

        public int year { get; set; }

        public int hours { get; set; }
        public double count { get; set; }
    }
}
