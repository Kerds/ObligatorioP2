using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP2
{
    public class InstanciaPagoRecurrente : InstanciaPago
    {
        private DateTime FechaInicio { get; set; }
        private DateTime? FechaFin { get; set; }


        public InstanciaPagoRecurrente(double montoBase, DateTime fechaInicio, DateTime? fechaFin = null) : base(montoBase)
        {

            FechaInicio = fechaInicio;
            if (fechaFin != null)
            {
                FechaFin = fechaFin;
            }
           
        }
        public DateTime GetFechaInicio()
        {
            return FechaInicio;
        }
        public DateTime? GetFechaFin()
        {
            return FechaFin;
        }

        public override void Validar()
        {
            base.Validar();
            ValidarFechaInicio();
            ValidarFechaFin();
        }
        public void ValidarFechaInicio()
        {
            if (FechaInicio == DateTime.MinValue)
            {
                throw new Exception("El campo fecha de inicio no puede estar vacio.");
            }
        }
        public void ValidarFechaFin()
        {
            DateTime fechaInicio = GetFechaInicio();
            if (FechaFin < fechaInicio)
            {
                throw new Exception("La fecha fin no puede ser anterior a la fecha de inicio.");
            }
        }

        private double CalcularRecargo()
        {
            int cuotasRestantes = GetCuotas();
            double recargo = 0;
            if (cuotasRestantes == -1)
            {
                recargo = 1.03;
            }else if (cuotasRestantes >= 10)
            {
                recargo = 1.10;
            }else if (cuotasRestantes >= 6 && cuotasRestantes <= 9)
            {
                recargo = 1.05;
            }else if (cuotasRestantes <= 5)
            {
                recargo = 1.03;
            }
            return recargo;
        }
        

        public override double CalcularMontoPago()
        {
            double montoBase = GetMontoBase();
            int cuotasRestantes = GetCuotas();
            double recargo = CalcularRecargo();

            if (cuotasRestantes == -1)
            {
                return montoBase * recargo;
            }
            else
            {
                return montoBase * cuotasRestantes * recargo;
            }
        }
        public int GetCuotas()
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
            return mes >= FechaInicio && (FechaFin == null || mes <= FechaFin);
        }
        public override string MiTipo()
        {
            return "Recurrente";
        }
    }
}
