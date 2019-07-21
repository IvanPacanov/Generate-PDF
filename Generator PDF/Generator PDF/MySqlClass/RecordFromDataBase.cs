using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_PDF.MySqlClass
{
   public class RecordFromDataBase
    {
    
        public int idHistory { get; set; }   
        public int Address { get; set; }
        public double DateTime { get; set; }
        public byte IsBusy { get; set; }




    }
}
