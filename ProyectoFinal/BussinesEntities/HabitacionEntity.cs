using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesEntities
{
  public  class HabitacionEntity
    {

        public HabitacionEntity()
        {
            this.Disponibilidad = new HashSet<DisponibilidadEntity>();
        }
    
        public int idHabitacion { get; set; }
        public Nullable<int> idHotel { get; set; }
        public Nullable<int> idTipoHabitacion { get; set; }
        public Nullable<int> cantidadBanios { get; set; }
        public Nullable<bool> balcon { get; set; }
        public Nullable<bool> heladera { get; set; }
        public Nullable<bool> microondas { get; set; }
    
        public virtual ICollection<DisponibilidadEntity> Disponibilidad { get; set; }
        public virtual HotelEntity Hotel { get; set; }
        public virtual TipoHabitacionEntity TipoHabitacion { get; set; }


    }
}
