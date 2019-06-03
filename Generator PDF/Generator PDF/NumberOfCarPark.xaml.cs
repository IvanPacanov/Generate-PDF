using Generator_PDF.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Generator_PDF
{
    /// <summary>
    /// Interaction logic for NumberOfCarPark.xaml
    /// </summary>
    public partial class NumberOfCarPark : Window
    {
        public NumberOfCarPark()
        {
            InitializeComponent();
            MVCarParksChoice carParksChoice = new MVCarParksChoice();
        
          this.DataContext = carParksChoice;
        }

    }
}
