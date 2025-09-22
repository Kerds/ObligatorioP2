using System.Diagnostics.Metrics;

namespace ObligatorioP2
{
    internal class Usuario
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
            Id = UId;
        }
        public Usuario(string nombre, string apellido, string contrasenia, Equipo equipo)
        {
            Validar(nombre, apellido, contrasenia, equipo);
            Id = UId;
            Nombre = nombre;
            Apellido = apellido;
            Contrasenia = contrasenia;
            Equipo = equipo;
            FechaAlta = DateTime.Now;
            UId++;
        }
        public override string ToString()
        {
            return $"El usuario {Nombre} {Apellido} pertenece al equipo: {Equipo.Nombre}";
        }
        public override bool Equals(object? obj)
        {
            if (obj is Usuario)
            {
                Usuario usuario = (Usuario)obj;
                return this.Id == usuario.Id;
            }
            return false;
        }
        public void Validar(string nombre, string apellido, string contrasenia, Equipo equipo)
        {
            ValidarNombre(nombre);
            ValidarApellido(apellido);
            ValidarContrasenia(contrasenia);
            ValidarEquipo(equipo);
        }
        public void ValidarNombre(string nombre)
        {
            if (nombre.Length < 3)
            {
                throw new Exception("El nombre debe tener al menos 3 caracteres.");
            }
        }
        public void ValidarApellido(string apellido)
        {
            if (apellido.Length < 3)
            {
                throw new Exception("El apellido debe tener al menos 3 caracteres.");
            }
        }
        public void ValidarContrasenia(string contrasenia)
        {
            if (contrasenia.Length < 8)
            {
                throw new Exception("La contraseña debe tener al menos 8 caracteres.");
            }
        }
        public void ValidarEquipo(Equipo equipo)
        {
            if (equipo == null)
            {
                throw new Exception("El equipo no puede ser nulo.");
            }
        }
        public string CreateEmail(int counter)
        {
            string email;
            if (counter > 0)
            {
                email = Nombre.ToLower() + "." + Apellido.ToLower() + counter + "@laEmpresa.com";
                SetEmail(email);
                return email;
            }
            email = Nombre.ToLower() + "." + Apellido.ToLower() + "@laEmpresa.com";
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
    }
}