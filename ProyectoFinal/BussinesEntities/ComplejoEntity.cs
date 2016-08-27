using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BussinesEntities
{
    public class ComplejoEntity
    {
     public ComplejoEntity()
        {
            this.CasaDptoOCabana = new HashSet<CasaDptoOCabana>();
        }
    
        public int idComplejo { get; set; }
        public Nullable<int> idTipoComplejo { get; set; }
        public Nullable<int> idCategoria { get; set; }
        public Nullable<int> idLugarHospedaje { get; set; }
    
        public virtual ICollection<CasaDptoOCabana> CasaDptoOCabana { get; set; }
        public virtual CategoriaHospedaje CategoriaHospedaje { get; set; }
        public virtual TipoComplejo TipoComplejo { get; set; }
        public virtual LugarHospedaje LugarHospedaje { get; set; }
    }
}
