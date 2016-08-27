using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesEntities
{
   public class Day
    {

        public DateTime Date { get; set; }
        public string _Date { get; set; }
        public string dateStr { get; set; }
        public int dtDay { get; set; }
        public int? daycolumn { get; set; }

        public Boolean estaOcupado { get; set; }


    }
}
