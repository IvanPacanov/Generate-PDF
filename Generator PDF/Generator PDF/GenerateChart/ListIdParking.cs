using Generator_PDF.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_PDF.GenerateChart
{
   public class ListIdParking :EventArgs
    {
      public int key { get; set; }
        public List<List<IdParking>> value { get; set; }

    }
}
