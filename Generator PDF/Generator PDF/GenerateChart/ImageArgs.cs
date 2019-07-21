using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_PDF.GenerateChart
{
    class ImageArgs : EventArgs
    {
        public System.Drawing.Bitmap image  { get; set; }
        public AbstractChart chart { get; set; }
    }
}
