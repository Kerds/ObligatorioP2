using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP2
{
    public class InstanciaPagoRecurrente : InstanciaPago
    {
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public InstanciaPagoRecurrente(double montoBase, DateTime fechaInicio, DateTime? fechaFin = null) : base(montoBase)
        {
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Validar(montoBase);
        }
        public override void Validar(double montoBase)
        {
            base.Validar(montoBase);
            if (FechaInicio == DateTime.MinValue)
            {
                throw new Exception("El campo fecha de inicio no puede estar vacio.");
            }
            if (FechaFin < FechaInicio)
            {
                throw new Exception("La fecha fin no puede ser anterior a la fecha de inicio.");
            }
            ValidarFechas(fechaInicio, fechaFin);
        }

        private void ValidarFechas(DateTime fechaInicio, DateTime fechaFin)
        {
        if (fechaInicio == DateTime.MinValue)
         {
          throw new Exception("El campo fecha de inicio no puede estar vacio.");
        }
       if (fechaFin < fechaInicio)
         {
              throw new Exception("La fecha fin no puede ser anterior a la fecha de inicio.");
           }
         }

        public override double CalcularMontoPago()
        {
            throw new NotImplementedException();
        }
        public int CantidadMesesActivos()
        {
            if (FechaFin == null)
            {
                return -1; // Indica que el pago es indefinido   
            }
            DateTime fechaFin = FechaFin ?? DateTime.Now;
            int meses = ((fechaFin.Year - FechaInicio.Year) * 12) + fechaFin.Month - FechaInicio.Month;
            return meses + 1; // +1 para incluir el mes de inicio
        }
        public override bool EsPagoActivo(DateTime mes)
        {
            throw new NotImplementedException();
        }
    }
}
