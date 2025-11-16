using Clases.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases.Pagos
{
    public class DTOpago
    {
        public string MetodosPago { get; set; }
        public string TipoGasto { get; set; }
        public string Descripcion { get; set; }
        public double MontoBase { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime? FechaFin { get; set; }
        public int ReciboPago { get; set; }
        public string TipoPago { get; set; }
        public DTOpago()
        {
        }
        public DTOpago(string metodosPago, string tipoGasto, string descripcion, double montoBase, DateTime? fechaPago, int reciboPago,string tipoPago, DateTime? fechaFin )
        {
            MetodosPago = metodosPago;
            TipoGasto = tipoGasto;
            Descripcion = descripcion;
            MontoBase = montoBase;
            FechaPago = fechaPago;
            ReciboPago = reciboPago;
            FechaFin = fechaFin;
            TipoPago = tipoPago;
        }
        public void Validar()
        {
            
            ValidarTipoPago();
            ValidarMetodosPago();
            ValidarTipoGasto();
            ValidarDescripcion();
            ValidarMontoBase();
            if (TipoPago == "Unico")
            {
                ValidarPagoUnico();
            }
            else if (TipoPago == "Recurrente")
            {
                ValidarPagoRecurrente();
            }
        }
        public void ValidarPagoUnico()
        {
            if (FechaPago == null)
            {
                throw new Exception("La fecha de pago no puede estar vacia.");
            }
            if (ReciboPago <= 0)
            {
                throw new Exception("El numero de recibo debe ser mayor a 0.");
            }
        }
        public void ValidarPagoRecurrente()
        {
            if (FechaPago == null)
            {
                throw new Exception("La fecha de inicio no puede estar vacia.");
            }
            if (FechaFin != null)
            {
                if (FechaFin <= FechaPago)
                {
                    throw new Exception("La fecha de fin debe ser mayor a la fecha de inicio.");
                }
            }
        }
        public void ValidarMetodosPago()
        {
            if (string.IsNullOrWhiteSpace(MetodosPago))
            {
                throw new Exception("El metodo de pago no puede estar vacio.");
            }
        }
        public void ValidarTipoGasto()
        {
            if (string.IsNullOrWhiteSpace(TipoGasto))
            {
                throw new Exception("El tipo de gasto no puede estar vacio.");
            }
        }
        public void ValidarDescripcion()
        {
            if (string.IsNullOrWhiteSpace(Descripcion))
            {
                throw new Exception("La descripcion no puede estar vacia.");
            }
        }
        public void ValidarMontoBase()
        {
            if (MontoBase <= 0)
            {
                throw new Exception("El monto base debe ser mayor a 0.");
            }
        }
        public void ValidarTipoPago()
        {
            if (string.IsNullOrWhiteSpace(TipoPago))
            {
                throw new Exception("El tipo de pago no puede estar vacio.");
            }
        }
    }
}
