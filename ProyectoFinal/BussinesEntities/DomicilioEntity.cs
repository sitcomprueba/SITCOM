using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.ComponentModel.DataAnnotations;

namespace BussinesEntities
{
    public class DomicilioEntity
    {
         public DomicilioEntity()
        {
            this.Persona = new HashSet<Persona>();
            this.Sucursal = new HashSet<Sucursal>();
        }
    
        public int idDomicilio { get; set; }

        [Required(ErrorMessage="Se debe completar la calle.")]
        public string calle { get; set; }
        [Required(ErrorMessage = "Se debe completar el número.")]
        public Nullable<int> numero { get; set; }
        public string dpto { get; set; }
        public string barrio { get; set; }
        public Nullable<int> idLocalidad { get; set; }
    
        public virtual Localidad Localidad { get; set; }
        public virtual ICollection<Persona> Persona { get; set; }
        public virtual ICollection<Sucursal> Sucursal { get; set; }

        public int paisSeleccionado { get; set; }
        public int provinciaSeleccionada { get; set; }
       public int localidadSeleccionada { get; set; }

        public IEnumerable<Pais> listPaises { get; set; }
        public IEnumerable<Provincia> listProvincias { get; set; }
        public IEnumerable<Localidad> listLocalidades { get; set; }
        public IEnumerable<Localidad> listLocalidadesCercanas { get; set; }
    }
}
