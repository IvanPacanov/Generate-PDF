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
            operations.Add(new Operation { operation=  "Ilość pojazdów" } );
            operations.Add(new Operation { operation = "Ilość wzbudzeń" });
            operations.Add(new Operation { operation = "Ilość wzbudzeń procentowe" });
            operations.Add(new Operation { operation = "Ilośd wzbudzeń na parkingu w danym miesiącu" });
            operations.Add(new Operation { operation = "Zestawienie godzinowe" });
            operations.Add(new Operation { operation = "Zestawienie godzinowe dla każdego parkingu" });
            operations.Add(new Operation { operation = "Zestawienie godzinowe dla każdego parkingu wzgledem miesięcy" });

            return operations;
        }
    }
}
