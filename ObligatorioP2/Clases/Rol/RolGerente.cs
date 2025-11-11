using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases.Rol 
{
    internal class RolGerente : Rol
    {
        public override bool AddTipoGasto()
        {
            return true;        }

        public override bool CargarNuevoPago()
        {
            return true;
        }

        public override bool ListadoPagosEquipo()
        {
            return true;
        }

        public override string MiRol()
        {
            return "Gerente";
        }

        public override bool RemoveTipoGasto()
        { return true; }

        public override bool VerPagosMesActual()
        {
            return true;
        }
    }
}
