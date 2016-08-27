using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BussinesEntities
{
    public partial class HotelEntity
    {
        public HotelEntity()
        {
            this.Habitacion = new HashSet<Habitacion>();
        }
    
        public int idHotel { get; set; }
        public Nullable<int> idCategoria { get; set; }
        public Nullable<int> idLugarHospedaje { get; set; }
    
        public virtual CategoriaHospedaje CategoriaHospedaje { get; set; }
        public virtual ICollection<Habitacion> Habitacion { get; set; }
        public virtual LugarHospedaje LugarHospedaje { get; set; }
    }
}
