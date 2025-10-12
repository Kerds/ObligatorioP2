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
                int opcionSelec = int.Parse(Console.ReadLine());

                
                switch (opcionSelec)
                {
                    case 0:
                        Console.WriteLine("Cerrando Programa");
                        exitFlag = true;
                        break;
                    case 1:
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
                            Console.Clear();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 2:
                        Console.WriteLine("Ingrese el email del Usuario:");
                        string? email = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(email))
                        {
                            Console.WriteLine("El email no puede estar vacío.");
                            break;
                        }
                        Usuario usuario = sistema.GetUsuarioPorEmail(email);
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
                                Console.Clear();
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
                                break;
                    case 3:
                        Console.WriteLine("Ingrese Nombre:");
                        string nombre = Console.ReadLine();
                        Console.WriteLine("Ingrese Apellido:");
                        string apellido = Console.ReadLine();
                        Console.WriteLine("Ingrese Contresenia:");
                        string contresenia = Console.ReadLine();
                        Console.WriteLine("Ingrese Equipo:");
                        string equipoIngresado = Console.ReadLine();
                        try
                        {
                         Equipo equipo = sistema.GetEquipoPorNombre(equipoIngresado);
                       
                         Usuario usuNuevo = new Usuario(nombre, apellido, contresenia, equipo);
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
                        Console.Clear();
                        break;
                    case 4:
                       
                            Console.WriteLine("Ingrese el nombre del equipo:");
                            try
                            {
                                String nombreEquipo = Console.ReadLine();
                                List<Usuario> listaUsuariosDeEquipo = sistema.GetUsuariosDeEquipo(nombreEquipo);
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
                            catch (Exception e)
                            {
                                
                                Console.WriteLine(e.Message);
                                Console.WriteLine("");
                                Console.WriteLine("Por favor, ingrese '1' para volver al menú.");
                            }
                            
                            while (Console.ReadLine() != "1")
                            {
                                Console.WriteLine("Por favor, ingrese '1' para volver al menú.");
                            }
                            Console.Clear();

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

    }
}
