using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP2
{
    public class instanciaPagoUnico : InstanciaPago
    {
        public DateTime FechaPago { get; set; }
        public int ReciboPago { get; set; }
        public instanciaPagoUnico(double montoBase, DateTime fechaPago, int reciboPago) : base(montoBase)
        {

            Validar(montoBase);
            FechaPago = fechaPago;
            ReciboPago = reciboPago;
        }
        public override void Validar(double montoBase)
        {
            base.Validar(montoBase);
            ValidarFechaPago();
            ValidarReciboPago();
        }
        public void ValidarFechaPago()
        {
              if (FechaPago == DateTime.MinValue)
              {
                  throw new Exception("El campo fecha de pago no puede estar vacio.");
              }
        }
        public void ValidarReciboPago()
        {
            if (ReciboPago <= 0)
            {
                throw new Exception("El numero de recibo debe ser mayor a 0.");
            }
        }

        public override double CalcularMontoPago()
        {
            throw new NotImplementedException();
        }

        public override bool EsPagoActivo(DateTime mes)
        {
            throw new NotImplementedException();
        }
    }
}
