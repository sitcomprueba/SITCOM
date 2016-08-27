using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BussinesEntities
{
    public class CaracteristicasHospedajeEntity
    {
        public int idCaracteristicaHospedaje { get; set; }
        public Nullable<int> idLugarHospedaje { get; set; }
        public Nullable<int> idCaracteristica { get; set; }
        public virtual Caracteristica Caracteristica { get; set; }
        public virtual LugarHospedaje LugarHospedaje { get; set; }
    }
}
