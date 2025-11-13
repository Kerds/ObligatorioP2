using ObligatorioP2;
using System.ComponentModel.DataAnnotations;

namespace Clases.Usuarios
{
    public class Equipo : IValidar 
    {
        public int Id { get; set; }
        public static int UId { get; set; } = 0;
        public string Nombre { get; set; }
        private List<Usuario> Miembros { get; set; } = new List<Usuario>();

        public Equipo()
        {
            Id = UId++;
        }

        public Equipo(string nombre)
        {
            Id = UId;
            Nombre = nombre;
            UId++;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Nombre: {Nombre}";
        }
        
        public string GetNombre()
        {
            return Nombre;
        }

        public void Validar()
        {
                ValidarNombre();
        }
        
        private void ValidarNombre()
        {
            if(Nombre.Length < 3)
            {
                throw new Exception("El nombre del equipo debe tener al menos 3 caracteres.");
            }
        }

        public IEnumerable<Usuario> GetMiembros()
        {
           
            if (Miembros.Count == 0)
            {
                throw new Exception("El equipo no tiene miembros.");
            }
            
            return Miembros;
        }
        public void AgregarMiembro(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new Exception("El usuario no puede ser nulo.");
            }
            if (Miembros.Contains(usuario))
            {
                throw new Exception("El usuario ya es miembro del equipo.");
            }
            Miembros.Add(usuario);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Equipo)
            {
                Equipo equipo = (Equipo)obj;
                return Id == equipo.Id || Nombre == equipo.Nombre;
            }
            return false;
        }
    }
}