using System.ComponentModel.DataAnnotations;

namespace ObligatorioP2
{
    public class Equipo
    {
        public int Id { get; set; }
        public static int UId { get; set; } = 0;
        public string Nombre { get; set; }
        public List<Usuario> Miembros { get; set; } = new List<Usuario>();
        
        public Equipo(){
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

        public void Validar()
        {
                ValidarNombre();
        }
        public void ValidarNombre()
        {
            if(Nombre.Length < 3)
            {
                throw new Exception("El nombre del equipo debe tener al menos 3 caracteres.");
            }
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
                return this.Id == equipo.Id;
            }
            return false;
        }
    }
}