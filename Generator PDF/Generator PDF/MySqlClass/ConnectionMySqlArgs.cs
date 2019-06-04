using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_PDF.MySqlClass
{
    public class ConnectionMySqlArgs : EventArgs
    {
        public ConnectionMySql CurrentconnectionMySql { get; set; }
    }
}
