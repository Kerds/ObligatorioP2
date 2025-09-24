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
            SetFechaPago(fechaPago);
            SetReciboPago(reciboPago);
            Validar(montoBase);
        }
        public override void Validar(double montoBase)
        {
            base.Validar(montoBase);
             
        }
        public void SetFechaPago(DateTime fechaPago)
        {
            try
            {
                ValidarFechaPago(fechaPago);
                FechaPago = fechaPago;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SetReciboPago(int reciboPago)
        {
            try
            {
                ValidarReciboPago(reciboPago);
                ReciboPago = reciboPago;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ValidarFechaPago(DateTime fechaPago)
        {
              if (fechaPago == DateTime.MinValue)
              {
                  throw new Exception("El campo fecha de pago no puede estar vacio.");
              }
        }
        public void ValidarReciboPago(int reciboPago)
        {
            if (reciboPago <= 0)
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
            return FechaPago.Month == mes.Month && FechaPago.Year == mes.Year;
        }
        public override string MiTipo()
        {
            return "Unico";
        }
    }
}
