using ObligatorioP2;

namespace Clases.Pagos
{
    public class TipoGasto : IValidar 
    {
        public int Id { get; set; }
        public static int UId {get; set;} = 0;
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public TipoGasto()
        {
            Id = UId++;
        }

        public TipoGasto(string nombre, string descripcion)
        {
            Id = UId++;
            Nombre = nombre;
            Descripcion = descripcion;
        }

        public void Validar()
        {
            ValidarNombre();
            ValidarDescripcion();
        }

        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new Exception("El nombre no puede estar vacio");
            }
            
        }

        private void ValidarDescripcion()
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
        
        public override bool Equals(object? obj)
        {
            if (obj is TipoGasto)
            {
                TipoGasto tipoGasto = (TipoGasto)obj;
                return Nombre == tipoGasto.Nombre || Id == tipoGasto.Id;
            }
            return false;
        }
        
    }
}