using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BussinesEntities
{
    public class LugarHospedajeEntity
    {
        public LugarHospedajeEntity()
        {
            this.CaracteristicasHospedaje = new HashSet<CaracteristicasHospedajeEntity>();
            this.CasaDptoOCabana = new HashSet<CasaDptoOCabana>();
            this.Habitacion = new HashSet<Habitacion>();
            this.Complejo = new HashSet<ComplejoEntity>();
            this.Hotel = new HashSet<HotelEntity>();
        }

        public int idLugarHospedaje { get; set; }
        public Nullable<int> idNegocio { get; set; }
        public Nullable<int> idTipoLugarHospedaje { get; set; }
        public Nullable<bool> moduloReservas { get; set; }

        public virtual ICollection<CaracteristicasHospedajeEntity> CaracteristicasHospedaje { get; set; }
        public virtual ICollection<CasaDptoOCabana> CasaDptoOCabana { get; set; }
        public virtual ICollection<Habitacion> Habitacion { get; set; }
        public virtual Negocio Negocio { get; set; }
        public virtual TipoLugarHospedaje TipoLugarHospedaje { get; set; }
        public virtual ICollection<ComplejoEntity> Complejo { get; set; }
        public virtual ICollection<HotelEntity> Hotel { get; set; }
    }
}
