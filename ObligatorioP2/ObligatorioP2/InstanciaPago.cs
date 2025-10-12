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

        public InstanciaPago()
        {
            Id = UId++;
        }
        public InstanciaPago(double montoBase)
        {
            Id = UId;
            MontoBase = montoBase;
            UId++;
        }

        public double GetMontoBase()
        {
            return MontoBase;
        }
        
        public abstract double CalcularMontoPago(MetodosPago metodoPago);

        public abstract bool EsPagoActivo(DateTime mes);

        public abstract string MiTipo();
       
        
        public virtual void Validar()
        {
            ValidarMontoBase();
        }

        private void ValidarMontoBase()
        {
            if (MontoBase <= 0)
            {
                throw new Exception("El monto base debe ser mayor a 0.");
            }
        }

    }
}
