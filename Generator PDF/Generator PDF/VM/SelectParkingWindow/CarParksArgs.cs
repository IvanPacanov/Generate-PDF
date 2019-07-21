using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_PDF.VM
{
  public  class CarParksArgs :EventArgs
    {
        public ObservableCollection<IdParking> CarParks { get; set; }
    }
}
