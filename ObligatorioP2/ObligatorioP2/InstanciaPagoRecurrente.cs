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
            SetFechaInicio(fechaInicio);
            SetFechaFin(fechaFin ?? DateTime.MinValue);
            Validar(montoBase);
        }
        public DateTime GetFechaInicio()
        {
            return FechaInicio;
        }
        public DateTime? GetFechaFin()
        {
            return FechaFin;
        }
        public void SetFechaInicio(DateTime fechaInicio)
        {
            try
            {
                ValidarFechaInicio(fechaInicio);
                FechaInicio = fechaInicio;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void SetFechaFin(DateTime fechaFin)
        {
            ValidarFechaFin(fechaFin);
            FechaFin = fechaFin;
        }
        public override void Validar(double montoBase)
        {
            base.Validar(montoBase);
            if (FechaFin != null && FechaFin < FechaInicio)
            {
                throw new Exception("La fecha fin no puede ser anterior a la fecha de inicio.");
            }
        }
        public void ValidarFechaInicio(DateTime fechaInicio)
        {
            if (fechaInicio == DateTime.MinValue)
            {
                throw new Exception("El campo fecha de inicio no puede estar vacio.");
            }
        }
        public void ValidarFechaFin(DateTime fechaFin)
        {
            DateTime fechaInicio = GetFechaInicio();
            if (fechaFin < fechaInicio )
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
            return mes >= FechaInicio && (FechaFin == null || mes <= FechaFin);
        }
        public override string MiTipo()
        {
            return "Recurrente";
        }
    }
}
