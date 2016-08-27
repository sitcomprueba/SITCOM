using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesEntities
{
  public  class CasaDptoOCabanaEntity
    {

      public CasaDptoOCabanaEntity()
        {
            this.Disponibilidad = new HashSet<DisponibilidadEntity>();
        }
    
        public int idCasaDptoOCabana { get; set; }
        public string nombreCasaDptoOCabana { get; set; }
        public Nullable<int> idLugarHospedaje { get; set; }
        public Nullable<int> cantidadHabitaciones { get; set; }
        public Nullable<int> cantidadAmbientes { get; set; }
        public Nullable<int> cantidadBanios { get; set; }
        public Nullable<int> idComplejo { get; set; }
    
        public virtual ComplejoEntity Complejo { get; set; }
        public virtual LugarHospedajeEntity LugarHospedaje { get; set; }
        public virtual ICollection<DisponibilidadEntity> Disponibilidad { get; set; }


    }
}
