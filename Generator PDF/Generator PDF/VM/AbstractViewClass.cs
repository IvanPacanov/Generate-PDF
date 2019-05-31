using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Generator_PDF.VM
{
  public  class AbstractViewClass
    {
        public void CloseApp()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
