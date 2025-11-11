using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases.Rol
{
    internal class RolEmpleado : Rol
    {
        public override bool AddTipoGasto()
        {
            return false;
        }

        public override bool CargarNuevoPago()
        {
            return true;
        }

        public override bool ListadoPagosEquipo()
        {
            return false;  }

        public override string MiRol()
        {
            return "Empleado";
        }

        public override bool RemoveTipoGasto()
        {
            return false;
        }

        public override bool VerPagosMesActual()
        {
            return true;        }
    }
}
