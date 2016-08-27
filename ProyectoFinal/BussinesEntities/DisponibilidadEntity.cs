using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BussinesEntities
{
 public   class DisponibilidadEntity
    {


        public int idDisponibilidad { get; set; }
        public Nullable<System.DateTime> fechaDesde { get; set; }
        public Nullable<System.DateTime> fechaHasta { get; set; }
        public Nullable<int> idHabitacion { get; set; }
        public Nullable<int> idCasaODpto { get; set; }
        public Nullable<bool> habilitado { get; set; }
        public Nullable<int> idEstado { get; set; }


     // Variables que las uso para consultar disponibilidad 
        public DateTime fechaDisponible  { get;set; }

        public int estaOcupado { get; set; }

     //**********************************************************************//

        public virtual CasaDptoOCabanaEntity CasaDptoOCabana { get; set; }
        public virtual EstadoReservaEntity EstadoReserva { get; set; }
        public virtual HabitacionEntity Habitacion { get; set; }


    }
}
