using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP2
{
    public abstract class InstanciaPago
    {
        public int Id { get; set; }
        public static int UId { get; set; } = 0;
        public double MontoBase { get; set; }

        public abstract double CalcularMontoPago();

        public abstract bool EsPagoActivo(DateTime mes);

        public virtual void Validar(double montoBase)
        {
            ValidarMontoBase(montoBase);
        }

        public void ValidarMontoBase(double montoBase)
        {
            if (montoBase <= 0)
            {
                throw new Exception("El monto base debe ser mayor a 0.");
            }
        }
        public InstanciaPago()
        {
            Id = UId++;
        }
        public InstanciaPago(double montoBase)
        {
            Validar(montoBase);
            Id = UId;
            MontoBase = montoBase;
            UId++;
        }

    }
}
