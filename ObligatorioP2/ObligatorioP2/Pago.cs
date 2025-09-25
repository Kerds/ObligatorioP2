namespace ObligatorioP2
{
    internal class Pago
    {
        public int Id { get; set; }
        public static int UId {get; set;} = 0;
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

        public Pago(MetodosPago metodosPago, Usuario usuarioAsociado, TipoGasto tipoGasto, string descripcion, InstanciaPago instanciaPago, double montoFinal)
        {
            MetodosPago = metodosPago;
            UsuarioAsociado = usuarioAsociado;
            TipoGasto = tipoGasto;
            Descripcion = descripcion;
            InstanciaPago = instanciaPago;
            MontoFinal = montoFinal;
            Validar();
        }

        public void Validar()
        {
            ValidarDescripcion();
           ValidarMetodosPago();
         
        }
        
      

       
        public void ValidarMetodosPago()
        {
            if (MetodosPago != MetodosPago.Credito && MetodosPago != MetodosPago.Debito &&
                MetodosPago != MetodosPago.Efectivo)
            {
                throw new Exception("Metodos pago invalido");
            }
        }

        public void ValidarDescripcion()
        {
            if (string.IsNullOrEmpty(Descripcion))
            {
                throw new Exception("La descripcion de puede estar vacia");
            }
        }
        
        
        
      
        
        
        public override string ToString()
        {
            return Descripcion;
        }
    }
}