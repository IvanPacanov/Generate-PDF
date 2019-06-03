using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_PDF.VM
{
    class GeneratePossiblyOperation : BaseViewClass
    {
    

        public static ObservableCollection<Operation> CreatePossiblyOperation()
        {
            ObservableCollection<Operation> operations = new ObservableCollection<Operation>();
            operations.Add(new Operation { operation=  "Ilość Pojazdów" } );
            operations.Add(new Operation { operation = "Ilość Pobudzeń" });
            operations.Add(new Operation { operation = "Ilość Pobudzeń Procentowe" });

            return operations;
        }
    }
}
