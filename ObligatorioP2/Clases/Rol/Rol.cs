using Clases.Pagos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases.Rol
{
    public abstract class Rol
    {
        public Rol() { }
        public abstract string MiRol();
        public abstract bool CargarNuevoPago();
        public abstract bool VerPagosMesActual();
        public abstract bool AddTipoGasto();
        public abstract bool RemoveTipoGasto();
        public abstract bool ListadoPagosEquipo();


    }
}
