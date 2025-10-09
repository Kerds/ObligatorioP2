using System.Diagnostics.Metrics;

namespace ObligatorioP2
{
    public class Usuario
    {
        public int Id { get; set; }
        public static int UId { get; set; } = 0;
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contrasenia { get; set; }
        public string Email { get; set; }

        public Equipo Equipo { get; set; }
        public DateTime FechaAlta { get; set; }
        public Usuario()
        {
            Id = UId++;
        }
        public Usuario(string nombre, string apellido, string contrasenia, Equipo equipo)
        {   Id = UId++;
            Nombre = nombre;
            Apellido = apellido;
            Contrasenia = contrasenia;
            Equipo = equipo;
            FechaAlta = DateTime.Now;
        }
        public override string ToString()
        {
            return $"El usuario {Nombre} {Apellido}, con email {Email} pertenece al equipo: {Equipo.Nombre}";
        }
        public void Validar()
        {
            ValidarNombre();
            ValidarApellido();
            ValidarContrasenia();
            ValidarEquipo();
            ValidarFechaAlta();
        }
        public void ValidarNombre()
        {
            if (Nombre.Length < 3)
            {
                throw new Exception("El nombre debe tener al menos 3 caracteres.");
            }
        }
        public void ValidarApellido()
        {
            if (Apellido.Length < 3)
            {
                throw new Exception("El apellido debe tener al menos 3 caracteres.");
            }
        }
        public void ValidarContrasenia()
        {
            if (Contrasenia.Length < 8)
            {
                throw new Exception("La contraseña debe tener al menos 8 caracteres.");
            }
        }
        public void ValidarEquipo()
        {
            if (Equipo == null)
            {
                throw new Exception("El equipo no puede ser nulo.");
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
        public void ValidarFechaAlta()
        {
            if (FechaAlta == DateTime.MinValue)
            {
                throw new Exception("La fecha de alta no puede estar vacia.");
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
                return this.Id == usuario.Id || this.Email == usuario.Email;
            }
            return false;
        }
    }
}