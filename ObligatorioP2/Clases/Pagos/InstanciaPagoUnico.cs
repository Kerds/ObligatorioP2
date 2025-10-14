using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases.Pagos
{
    public class InstanciaPagoUnico : InstanciaPago
    {
        private DateTime FechaPago { get; set; }
        private int ReciboPago { get; set; }
        public InstanciaPagoUnico(double montoBase, DateTime fechaPago, int reciboPago) : base(montoBase)
        {
            FechaPago = fechaPago;
            ReciboPago = reciboPago;
        }
        public override void Validar()
        {
            base.Validar();
            ValidarFechaPago();
            ValidarReciboPago();
        }
        public DateTime GetFechaPago()
        {
            return FechaPago;
        }
        public int GetReciboPago()
        {
            return ReciboPago;
        }
        private void ValidarFechaPago()
        {
              if (FechaPago == DateTime.MinValue)
              {
                  throw new Exception("El campo fecha de pago no puede estar vacio.");
              }
        }
        private void ValidarReciboPago()
        {
            if (ReciboPago <= 0)
            {
                throw new Exception("El numero de recibo debe ser mayor a 0.");
            }
        }

        public override double CalcularMontoPago(MetodosPago metodoPago)
        {
            
            if(metodoPago == MetodosPago.Efectivo)
            {
                return MontoBase * 0.8;
            }
            else
            {
                return MontoBase * 0.9;
            }
        }

        public override bool EsPagoActivo(DateTime mes)
        {
            return FechaPago.Month == mes.Month && FechaPago.Year == mes.Year;
        }
        public override string MiTipo()
        {
            return "Unico";
        }
        public override string ToString()
        {
            return "Pago Unico";
        }
    }
}
