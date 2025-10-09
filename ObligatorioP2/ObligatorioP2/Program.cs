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

                            if (Console.ReadLine() == "1")
                            {
                                Console.Clear();
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 2:

                        break;
                    default:
                        Console.WriteLine("Por favor seleccione una opcion correcta.");
                        break;
                }
            }
            Console.ReadKey();

            
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
