namespace ObligatorioP2
{
    public class TipoGasto
    {
        public int Id { get; set; }
        public static int UId {get; set;} = 0;
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public TipoGasto()
        {
            Id = UId;
        }

        public TipoGasto(string nombre, string descripcion)
        {
            Id = UId;
            Nombre = nombre;
            Descripcion = descripcion;
            Validar();
        }

        public void Validar()
        {
            ValidarNombre();
            ValidarDescripcion();
        }

        public void ValidarNombre()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new Exception("El nombre no puede estar vacio");
            }
            
        }

        public void ValidarDescripcion()
        {
            if (string.IsNullOrEmpty(Descripcion))
            {
                throw new Exception("La descripcion no puede estar vacia");
            }
        }

        public override string ToString()
        {
            return Nombre ;
        }
    }
}