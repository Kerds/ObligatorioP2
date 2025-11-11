using System.Diagnostics.Metrics;
using Clases;
using Clases.Usuarios;
using Clases.Pagos;
using Clases.Roles;
namespace ObligatorioP2
{
    
    internal class Program
    {

        static void Main(string[] args)
        {
            Sistema sistema;

            try {
             sistema = Sistema.GetSistema();
            }catch (Exception e) {
                Console.WriteLine(e.Message);
                return;
            }
            bool exitFlag = false;
            while (!exitFlag)
            {
                MostrarMenu();
                int.TryParse(Console.ReadLine(), out int opcionSelec);

                switch (opcionSelec)
                {
                    case 0:
                        Console.WriteLine("Cerrando Programa");
                        exitFlag = true;
                        break;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Listando usuarios, por favor espere:");
                        try
                        {
                            List<Usuario> listaUsuarios = sistema.GetUsuarios();
                            ListarUsuarios(listaUsuarios);

                            // Espera hasta que el usuario escriba "1" para volver al menú
                            while (Console.ReadLine() != "1")
                            {
                                Console.WriteLine("Por favor, ingrese '1' para volver al menú.");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 2:
                        Console.Clear();
                        ListarPagosPorEmail(sistema);
                        break;
                    case 3:
                        Console.Clear();
                        CrearUsuario(sistema);

                        break;
                    case 4:
                        Console.Clear();
                            try
                            {
                            Equipo equipo = ListarEquiposParaSelec(sistema, "Seleccione el nombre del equipo:");
                            if (equipo != null)
                            {
                                List<Usuario> listaUsuariosDeEquipo = sistema.GetUsuariosDeEquipo(equipo.GetNombre());
                                if (listaUsuariosDeEquipo.Count == 0)
                                {
                                    Console.WriteLine("El equipo ingresado no contiene usuarios");
                                    Console.WriteLine("");
                                    Console.WriteLine("1 - Volver al menu ");
                                }
                                else
                                {
                                    foreach (Usuario u in listaUsuariosDeEquipo)
                                    {
                                        Console.WriteLine(u.StringMiembrosEquipo());
                                    }
                                }
                            }
                            else { Console.WriteLine("Debe seleccionar un equipo valido."); }
                            }
                            catch (Exception e)
                            {
                                
                                Console.WriteLine(e.Message);
                                Console.WriteLine("");
                                Console.WriteLine("Por favor, ingrese '1' para volver al menú.");
                            }
                        Console.WriteLine("1 - Volver al menu ");

                        while (Console.ReadLine() != "1")
                            {
                                Console.WriteLine("Por favor, ingrese '1' para volver al menú.");
                            }
                        break;
                    default:
                        Console.WriteLine("Por favor seleccione una opcion correcta.");
                        break;
                }
            }
            Console.ReadKey();

            
        }

        private static void ListarPagos(List<Pago> listPagos)
        {
            if (listPagos.Count > 0)
            {
                foreach (Pago p in listPagos)
                {
                    Console.WriteLine(p.ToString());
                }
                Console.WriteLine("Fin de la lista.");
            }
            else
            {
                Console.WriteLine("No hay pagos en el sistema.");
            }
            Console.WriteLine("");
            Console.WriteLine("1 - Volver al menu ");
            
        }

        private static void ListarUsuarios(List<Usuario> listaUsuarios)
        {
            if (listaUsuarios.Count > 0)
            {
                foreach (Usuario u in listaUsuarios)
                {
                    Console.WriteLine(u.ToString());
                }
                Console.WriteLine("Fin de la lista.");
            }
            else
            {
                Console.WriteLine("No hay usuarios en el sistema.");
            }
            Console.WriteLine("");
            Console.WriteLine("1 - Volver al menu ");
        }

        public static void MostrarMenu()
        {
            Console.WriteLine("----------Seleccione una opcion:---------");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("1. Listar todos los Usuarios del Sistema.");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("2. Listar pagos de usuario por correo.");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("3. Alta de usuario");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("4. Listar los miembros de un Equipo");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("0. Salir");
        }
        private static void ListarPagosPorEmail(Sistema sistema)
        {
            Console.WriteLine("Ingrese el email del Usuario:");
            string? email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("El email no puede estar vacío.");
                return;
            }
            if (!email.Contains("@") || !email.Contains("."))
            {
                Console.WriteLine("El email ingresado no es válido.");
                return;
            }
            if (email.Length < 5)
            {
                Console.WriteLine("El email debe tener al menos 5 caracteres.");
                return;
            }
            if (!email.Contains("laEmpresa"))
            {
                Console.WriteLine("El email debe pertenecer a la empresa (debe contener 'laEmpresa').");
                return;
            }
            Usuario usuario
            try { 
             usuario = sistema.GetUsuarioPorEmail(email);
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            if (usuario != null)
            {
                try
                {
                    List<Pago> listaPagos = sistema.PagosPorUsuario(usuario);
                    ListarPagos(listaPagos);
                    // Espera hasta que el usuario escriba "1" para volver al menú
                    while (Console.ReadLine() != "1")
                    {
                        Console.WriteLine("Por favor, ingrese '1' para volver al menú.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("No existe un usuario con ese email.");
            }
        }
        // Método para crear un usuario nuevo y agregarlo al sistema
        private static void CrearUsuario(Sistema sistema)
        {
            string nombre = "";
            while (true)
            {
                Console.WriteLine("Ingrese Nombre:");
                string input = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(input) && input.Length >= 3)
                {
                    nombre = input;
                    break;
                }
                Console.WriteLine("El nombre debe tener al menos 3 caracteres.");
            }

            string apellido = "";
            while (true)
            {
                Console.WriteLine("Ingrese Apellido:");
                string input = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(input) && input.Length >= 3)
                {
                    apellido = input;
                    break;
                }
                Console.WriteLine("El apellido debe tener al menos 3 caracteres.");
            }

            string contrasenia = "";
            while (true)
            {
                Console.WriteLine("Ingrese Contraseña:");
                string input = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(input) && input.Length >= 8)
                {
                    contrasenia = input;
                    break;
                }
                Console.WriteLine("La contraseña debe tener al menos 8 caracteres.");
            }

            Equipo equipo = null;
            while (equipo == null)
            {
                equipo = ListarEquiposParaSelec(sistema, "Seleccione un Equipo:");
                if (equipo == null)
                {
                    Console.WriteLine("Debe seleccionar un equipo válido.");
                }
            }
            Rol rol = null;
            while (rol == null)
            {
                rol = ListarRol( "Por favor seleccione un Rol");
                if (rol == null)
                {
                    Console.WriteLine("Debe seleccionar un Rol válido.");
                }
            }
            try
            {
                Usuario usuNuevo = new Usuario(nombre, apellido, contrasenia, equipo, rol);
                sistema.AltaUsuario(usuNuevo);
                equipo.AgregarMiembro(usuNuevo);
                Console.WriteLine("Usuario creado correctamente.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            Console.WriteLine();
            Console.WriteLine("1 - Volver al menú");
            while (Console.ReadLine() != "1")
            {
                Console.WriteLine("Por favor, ingrese '1' para volver al menú.");
            }
        }

        private static Rol? ListarRol( string text)
        {
            Console.WriteLine(text);
            Console.WriteLine("1 - Rol Empleado");
            Console.WriteLine("2 - Rol Gerente");
            string? input = Console.ReadLine();
            if (input == null)
            {
                Console.WriteLine("Opción de rol inválida.");
                return null;
            }
            else if (input == "1")
            {
                return new RolEmpleado();
            }
            else if (input == "2")
            {
                return new RolGerente();
            }
            else
            {
                Console.WriteLine("Opción de rol inválida.");
                return null;
            }
            }

        // Método para seleccionar un equipo de la lista de equipos del sistema
        private static Equipo? ListarEquiposParaSelec(Sistema sistema, string text)
        {
            List<Equipo> listaEquipos = sistema.GetEquipos();
            int counter = 0;
            Console.WriteLine(text);
            foreach (Equipo e in listaEquipos)
            {
                Console.WriteLine($"{counter++} - {e.GetNombre()}");
            }

            string? input = Console.ReadLine();
            if (!int.TryParse(input, out int indiceEquipo) || indiceEquipo < 0 || indiceEquipo >= listaEquipos.Count)
            {
                Console.WriteLine("Opción de equipo inválida.");
                return null;
            }
            return listaEquipos[indiceEquipo];
        }
    }
}
