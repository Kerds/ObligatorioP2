using Clases.Usuarios;

namespace Clases.Pagos
{
    public class Pago
    {
        public int Id { get; set; }
        public static int UId { get; set; }
        public MetodosPago MetodosPago { get; set; }
        public Usuario UsuarioAsociado { get; set; }
        public TipoGasto TipoGasto { get; set; }
        public string Descripcion { get; set; }
        public InstanciaPago InstanciaPago { get; set; }
        public double MontoFinal { get; set; }

        public Pago()
        {
            Id = UId++;
        }

        public Pago(MetodosPago metodosPago, Usuario usuarioAsociado, TipoGasto tipoGasto, string descripcion, InstanciaPago instanciaPago)
        {
            Id = UId++;
            MetodosPago = metodosPago;
            UsuarioAsociado = usuarioAsociado;
            TipoGasto = tipoGasto;
            Descripcion = descripcion;
            InstanciaPago = instanciaPago;
            SetMontoFinal();
        }

        public void Validar()
        {
           ValidarDescripcion();
           ValidarMetodosPago();
           ValidarTipoGasto();
           ValidarInstanciaPago();
           ValidarMontoFinal();
           ValidarUsuarioAsociado();
        }
        private void ValidarUsuarioAsociado()
        {
            if (UsuarioAsociado == null)
            {
                throw new Exception("El usuario asociado no puede ser nulo");
            }
        }
        private void ValidarMontoFinal()
        {
            if (MontoFinal <= 0)
            {
                throw new Exception("El monto final debe ser mayor a 0");
            }
        }
        private void ValidarInstanciaPago()
        {
            if (InstanciaPago == null)
            {
                throw new Exception("La instancia de pago no puede ser nula");
            }
            InstanciaPago.Validar();
        }

        private void ValidarTipoGasto()
        {
            if (TipoGasto == null)
            {
                throw new Exception("El tipo de gasto no puede ser nulo");
            }
        }

        private double SetMontoFinal()
        {
            try
            {
                if (InstanciaPago == null)
                {
                    throw new Exception("La instancia de pago no puede ser nula");
                }
                double montoFinal = InstanciaPago.CalcularMontoPago(MetodosPago);
                MontoFinal = montoFinal;
                return MontoFinal;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular el monto final: " + ex.Message);
            }
        }
        private void ValidarMetodosPago()
        {
            if (MetodosPago != MetodosPago.Credito && MetodosPago != MetodosPago.Debito &&
                MetodosPago != MetodosPago.Efectivo)
            {
                throw new Exception("Metodos pago invalido");
            }
        }

        private void ValidarDescripcion()
        {
            if (string.IsNullOrEmpty(Descripcion))
            {
                throw new Exception("La descripcion de puede estar vacia");
            }
        }
        
        public override string ToString()
        {
            string texto = $"El Pago {Id} de monto {MontoFinal:F2} fue abonado con el método \"{MetodosPago}\", por el usuario: {(UsuarioAsociado != null ? UsuarioAsociado.ToString() : "N/A")}.";

            if (InstanciaPago.MiTipo() == "Recurrente")
            {
                return texto + $" Detalles: {InstanciaPago.ToString()}";
            }
            return texto;
        }
    
        public override bool Equals(object? obj)
        {
            if (obj is Pago)
            {
                Pago pago = (Pago)obj;
                return Id == pago.Id;
            }
            return false;
        }
    }
}