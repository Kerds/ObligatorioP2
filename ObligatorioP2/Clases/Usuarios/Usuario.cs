using Clases.Roles;
using ObligatorioP2;
using System.Diagnostics.Metrics;

namespace Clases.Usuarios
{
    public class Usuario : IValidar 
    {
        public int Id { get; set; }
        public static int UId { get; set; } = 0;
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contrasenia { get; set; }
        public string Email { get; set; }

        public Rol? MiRol { get; set; }
        public Equipo Equipo { get; set; }
        public DateTime FechaAlta { get; set; }
        public Usuario()
        {
            Id = UId++;
        }
        public Usuario(string nombre, string apellido, string contrasenia, Equipo equipo, Rol? rol)
        {   Id = UId++;
            Nombre = nombre;
            Apellido = apellido;
            Contrasenia = contrasenia;
            Equipo = equipo;
            FechaAlta = DateTime.Now;
            if (rol != null)
            {
                MiRol = rol;
            }
        }
        public override string ToString()
        {
            return $"Usuario: {Nombre} {Apellido}, Email: {Email}, Equipo: {Equipo.Nombre}";
        }
        public void Validar()
        {
            ValidarNombre();
            ValidarApellido();
            ValidarContrasenia();
            ValidarEquipo();
            ValidarFechaAlta();
        }
        private void ValidarNombre() 
        {
            if (Nombre.Length < 3)
            {
                throw new Exception("El nombre debe tener al menos 3 caracteres.");
            }
        }
        public void SetRol( Rol rol)
        {
            if (rol == null)
            {
                throw new Exception("El rol no puede ser nulo.");
            }
            MiRol = rol;
        }
        private void ValidarApellido()
        {
            if (Apellido.Length < 3)
            {
                throw new Exception("El apellido debe tener al menos 3 caracteres.");
            }
        }
        private void ValidarContrasenia()
        {
            if (Contrasenia.Length < 8)
            {
                throw new Exception("La contraseña debe tener al menos 8 caracteres.");
            }
        }
        private void ValidarEquipo()
        {
            if (Equipo == null)
            {
                throw new Exception("El equipo no Existe.");
            }
        }
        private void ValidarFechaAlta()
        {
            if (FechaAlta == DateTime.MinValue)
            {
                throw new Exception("La fecha de alta no puede estar vacia.");
            }
        }
        public string FirstThreeLetters(string str)
        {
            if (str.Length < 3)
            {
                return str;
            }
            return str.Substring(0, 3);
        }
        public string CreateEmail(int counter)
        {
            string email;
            if (counter > 0)
            {

                email = FirstThreeLetters(Nombre.ToLower()) + FirstThreeLetters(Apellido.ToLower()) + counter + "@laEmpresa.com";
                SetEmail(email);
                return email;
            }
            email = FirstThreeLetters(Nombre.ToLower()) + FirstThreeLetters(Apellido.ToLower()) + "@laEmpresa.com";
            SetEmail(email);
            return email;
        }
        public void SetEmail(string email)
        {
            ValidarEmail(email);
            Email = email;
        }
        public void ValidarEmail(string email)
        {
            if (email.Length < 10)
            {
                throw new Exception("El email no es valido.");
            }
            if (!email.Contains("@") || !email.Contains("."))
            {
                throw new Exception("El email no es válido.");
            }
        }
        
        public string GetEmail()
        {
            return Email;
        }
        public override bool Equals(object? obj)
        {
            if (obj is Usuario)
            {
                Usuario usuario = (Usuario)obj;
                return Id == usuario.Id || Email == usuario.Email;
            }
            return false;
        }
        public IEnumerable<Usuario> GetMiembrosEquipo()
        {
         
            if (Equipo == null)
            {
                throw new Exception("El usuario no pertenece a ningún equipo.");
            }
            if (Equipo.GetMiembros().Count() == 0)
            {
                throw new Exception("El equipo no tiene miembros.");
            }
            
            if (MiRol.VerMiembrosEquipo() == false)
            {
                throw new Exception("El usuario no tiene permiso para ver los miembros del equipo.");
            }
            return Equipo.GetMiembrosAsc();
        }

        public string StringMiembrosEquipo()
        {
            return $"Usuario: {Nombre} {Apellido}, Email: {Email} ";
        }
        public string GetNombreEquipo()
        {
            return Equipo.Nombre;
        }

        public dynamic GetDtoUser()
        {
            return new
            {
                nombreCompleto = this.Nombre + " " + this.Apellido,
                email = this.Email,
                equipo = this.Equipo.Nombre,
                rol = this.MiRol != null ? this.MiRol.ToString() : null,
                incorporacion = this.FechaAlta,
                totalMes = 0,
            };

        }
        public int CompareTo(Usuario other)
        {
            // Orden ascendente por Nombre
            return this.Email.CompareTo(other.Email);
        }
    }
}