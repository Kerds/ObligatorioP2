using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clases.Usuarios;

namespace Clases.Usuarios
{
    public class DTOUser
    {
        public string nombreCompleto { get; set; }
        public string email { get; set; }
        public string equipo { get; set; }
        public string rol { get; set; }
        public DateTime fechaAlta { get; set; }
        public double totalMes { get; set; }
        public DTOUser()
        {
        }
        public DTOUser(Usuario u)
        {
            nombreCompleto = u.Nombre + " " + u.Apellido;
            email = u.Email;
            equipo = u.Equipo.Nombre;
            rol = u.MiRol.GetNombreRol();
            fechaAlta = u.FechaAlta;
            totalMes = 0;
        }
    }
}
