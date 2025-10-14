using Clases.Pagos;
using Clases.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatorioP2
{
    public class Sistema
    {
        private List<Usuario> Usuarios;
        private List<Pago> Pagos;
        private List<Equipo> Equipos;
        private List<TipoGasto> TipoGastos;
        private static Sistema Instancia;

        public Sistema()
        {
            Usuarios = new List<Usuario>();
            Pagos = new List<Pago>();
            Equipos = new List<Equipo>();
            TipoGastos = new List<TipoGasto>();
            PreargaDatos();
        }
        public static Sistema GetSistema()
        {
            if (Instancia == null)
            {
                Instancia = new Sistema();
            }
            return Instancia;
        }
        public List<Usuario> GetUsuarios()
        {
            return Usuarios;
        }
        public List<Pago> GetPagos()
        {
            return Pagos;
        }
        public List<Equipo> GetEquipos()
        {
            return Equipos;
        }
        public List<TipoGasto> GetTipoGastos()
        {
            return TipoGastos;
        }
        public Usuario GetUsuarioPorEmail(string email)
        {
            foreach (Usuario usuario in Usuarios)
            {
                if (usuario.Email == email)
                {
                    return usuario;
                }
            }
            return null;
        }
        public void AltaUsuario(Usuario usuario)
        {
            try
            {
                if (usuario == null)
                {
                    throw new Exception("El usuario no puede ser nulo.");
                }
                if (Usuarios.Contains(usuario))
                {
                    throw new Exception("El usuario ya existe.");
                }
                usuario.Validar();
                usuario.SetEmail(GenerarEmail(usuario));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar el usuario: " + ex.Message);
            }
            Usuarios.Add(usuario);
        }
        public void AltaEquipo(Equipo equipo)
        {
            try
            {
                if (equipo == null)
                {
                    throw new Exception("El equipo no puede ser nulo.");
                }
                if (Equipos.Contains(equipo))
                {
                    throw new Exception("El equipo ya existe.");
                }
                equipo.Validar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar el equipo: " + ex.Message);
            }
            Equipos.Add(equipo);
        }
        public void AltaTipoGasto(TipoGasto tipoGasto)
        {
            try
            {
                if (tipoGasto == null)
                {
                    throw new Exception("El tipo de gasto no puede ser nulo.");
                }
                if (TipoGastos.Contains(tipoGasto))
                {
                    throw new Exception("El tipo de gasto ya existe.");
                }
                tipoGasto.Validar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar el tipo de gasto: " + ex.Message);
            }
            TipoGastos.Add(tipoGasto);
        }
        public void AltaPago(Pago pago)
        {
            try
            {
                if (pago == null)
                {
                    throw new Exception("El pago no puede ser nulo.");
                }
                if (Pagos.Contains(pago))
                {
                    throw new Exception("El pago ya existe.");
                }
                pago.Validar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar el pago: " + ex.Message);
            }
            Pagos.Add(pago);
        }
        public Usuario BuscarUsuarioPorEmail(string email)
        {
            foreach (Usuario usuario in Usuarios)
            {
                if (usuario.Email == email)
                {
                    return usuario;
                }
            }
            return null;
        }
        public string GenerarEmail(Usuario usuario)
        {
            int counter = 0;
            string email = null;
            bool flag = false;

            while (!flag)
            {
                email = usuario.CreateEmail(counter);

                if (BuscarUsuarioPorEmail(email) == null)
                {
                    flag = true;
                }
                counter++;
            }
            return email;
        }
        public List<Pago> PagosPorUsuario(Usuario user)
        {
            List<Pago> lisAux = new List<Pago>();
            foreach (Pago p in Pagos)
            {
                if (p.UsuarioAsociado.Email == user.Email)
                {
                    lisAux.Add(p);
                }
            }
            return lisAux;
        }

        public TipoGasto GetTipoGasto(String nombreGasto)
        {
            foreach (TipoGasto tipo in TipoGastos)
            {
                if (tipo.Nombre == nombreGasto)
                {
                    return tipo;
                }
            }
            return null;
        }
        public Equipo GetEquipoPorNombre(string nombre)
        {
         
            foreach (Equipo equipo in Equipos)
            {
                if (equipo.Nombre.ToLower() == nombre.ToLower())
                {
                    return equipo;
                }
            }
            return null;
        }

        public List<Usuario> GetUsuariosDeEquipo(string nombreEquipo)
        {
            
            Equipo equipo = GetEquipoPorNombre(nombreEquipo);
            if (equipo == null)
            {
                throw new Exception("El equipo no existe.");
            }

            return equipo.GetMiembros();

        }

        public void PreargaDatos()
        {
            PrecargaEquipo();
            PrecargaUsuarios();
            PrecargaGastos();
            PrecargaPagos();
        }

        public void PrecargaEquipo()
        {
            Equipo contabilidad = new Equipo("Contabilidad");
            AltaEquipo(contabilidad);

            Equipo finanzas = new Equipo("Finanzas");
            AltaEquipo(finanzas);

            Equipo tesoreria = new Equipo("Tesoreria");
            AltaEquipo(tesoreria);

            Equipo auditoria = new Equipo("Auditoria");
            AltaEquipo(auditoria);
            
            Equipo ventas = new Equipo("Ventas");
            AltaEquipo(ventas);
            
        }

        public void PrecargaUsuarios()
        {
            
            
            Equipo contabilidad= GetEquipoPorNombre("Contabilidad");
            Equipo finanzas= GetEquipoPorNombre("Finanzas");
            Equipo tesoreria = GetEquipoPorNombre("Tesoreria"); 
            Equipo auditoria = GetEquipoPorNombre("Auditoria");
           
            
            // --- Contabilidad ---
            Usuario usuario1 = new Usuario("Juana", "Melcer", "12345678",contabilidad);
            AltaUsuario(usuario1); 
            contabilidad.AgregarMiembro(usuario1); 
            
            Usuario usuario2 = new Usuario("Ana", "Perez", "12345678", contabilidad);
            AltaUsuario(usuario2);
            contabilidad.AgregarMiembro(usuario2);

            Usuario usuario3 = new Usuario("Lucas", "Garcia", "12345678", contabilidad);
            AltaUsuario(usuario3);
            contabilidad.AgregarMiembro(usuario3);

            Usuario usuario4 = new Usuario("Martina", "Rodriguez", "12345678", contabilidad);
            AltaUsuario(usuario4);
            contabilidad.AgregarMiembro(usuario4);

            Usuario usuario5 = new Usuario("Santiago", "Fernandez", "12345678", contabilidad);
            AltaUsuario(usuario5);
            contabilidad.AgregarMiembro(usuario5);

            Usuario usuario6 = new Usuario("Valentina", "Lopez", "12345678", contabilidad);
            AltaUsuario(usuario6);
            contabilidad.AgregarMiembro(usuario6);

            // --- Finanzas ---
            Usuario usuario7 = new Usuario("Mateo", "Suarez", "12345678", finanzas);
            AltaUsuario(usuario7);
            finanzas.AgregarMiembro(usuario7);

            Usuario usuario8 = new Usuario("Camila", "Martinez", "12345678", finanzas);
            AltaUsuario(usuario8);
            finanzas.AgregarMiembro(usuario8);

            Usuario usuario9 = new Usuario("Joaquin", "Ruiz", "12345678", finanzas);
            AltaUsuario(usuario9);
            finanzas.AgregarMiembro(usuario9);

            Usuario usuario10 = new Usuario("Sofia", "Silva", "12345678", finanzas);
            AltaUsuario(usuario10);
            finanzas.AgregarMiembro(usuario10);

            Usuario usuario11 = new Usuario("Tomas", "Castro", "12345678", finanzas);
            AltaUsuario(usuario11);
            finanzas.AgregarMiembro(usuario11);

            Usuario usuario12 = new Usuario("Isabella", "Mendez", "12345678", finanzas);
            AltaUsuario(usuario12);
            finanzas.AgregarMiembro(usuario12);

            // --- Tesoreria ---
            Usuario usuario13 = new Usuario("Agustin", "Vega", "12345678", tesoreria);
            AltaUsuario(usuario13);
            tesoreria.AgregarMiembro(usuario13);

            Usuario usuario14 = new Usuario("Mia", "Ortega", "12345678", tesoreria);
            AltaUsuario(usuario14);
            tesoreria.AgregarMiembro(usuario14);

            Usuario usuario15 = new Usuario("Facundo", "Ramirez", "12345678", tesoreria);
            AltaUsuario(usuario15);
            tesoreria.AgregarMiembro(usuario15);

            Usuario usuario16 = new Usuario("Julia", "Morales", "12345678", tesoreria);
            AltaUsuario(usuario16);
            tesoreria.AgregarMiembro(usuario16);

            Usuario usuario17 = new Usuario("Nicolas", "Pereira", "12345678", tesoreria);
            AltaUsuario(usuario17);
            tesoreria.AgregarMiembro(usuario17);

            // --- Auditoria ---
            Usuario usuario18 = new Usuario("Florencia", "Santos", "12345678", auditoria);
            AltaUsuario(usuario18);
            auditoria.AgregarMiembro(usuario18);

            Usuario usuario19 = new Usuario("Andres", "Torres", "12345678", auditoria);
            AltaUsuario(usuario19);
            auditoria.AgregarMiembro(usuario19);

            Usuario usuario20 = new Usuario("Carolina", "Diaz", "12345678", auditoria);
            AltaUsuario(usuario20);
            auditoria.AgregarMiembro(usuario20);

            Usuario usuario21 = new Usuario("Bruno", "Ramos", "12345678", auditoria);
            AltaUsuario(usuario21);
            auditoria.AgregarMiembro(usuario21);
            Usuario usuario22 = new Usuario("Lucia", "Acosta", "12345678", auditoria);
            AltaUsuario(usuario22);
            auditoria.AgregarMiembro(usuario22);
                    
        }

        public void PrecargaGastos()
        {
            TipoGasto tipo1 = new TipoGasto("Alquiler", "Pago mensual por el alquiler de la oficina");
            AltaTipoGasto(tipo1);
            TipoGasto tipo2 = new TipoGasto("Servicios", "Pago de servicios como luz, agua, internet, etc.");
            AltaTipoGasto(tipo2);
            TipoGasto tipo3 = new TipoGasto("Sueldos", "Pago de sueldos a los empleados");
            AltaTipoGasto(tipo3);
            TipoGasto tipo4 = new TipoGasto("Materiales de oficina", "Compra de materiales de oficina como papel, bolígrafos, etc.");
            AltaTipoGasto(tipo4);
            TipoGasto tipo5 = new TipoGasto("Publicidad", "Gastos en publicidad y marketing");
            AltaTipoGasto(tipo5);
            TipoGasto tipo6 = new TipoGasto("Viajes", "Gastos en viajes de negocios");
            AltaTipoGasto(tipo6);

        }

        public void PrecargaPagos()
        {
            List<Usuario> usuarios = GetUsuarios();
            List<TipoGasto> tiposGasto = GetTipoGastos();
            Random rnd = new Random();

            // 25 pagos recurrentes, 5 de ellos pagados por completo (con FechaFin)
            for (int i = 0; i < 25; i++)
            {
                Usuario usuario = usuarios[rnd.Next(usuarios.Count)];
                TipoGasto tipoGasto = tiposGasto[rnd.Next(tiposGasto.Count)];
                double monto = rnd.Next(1000, 5000);

                DateTime fechaInicio = DateTime.Now.AddMonths(-rnd.Next(1, 24));
                DateTime? fechaFin = null;
                if (i < 5)
                {
                    int mesesDuracion = rnd.Next(1, 12);
                    fechaFin = fechaInicio.AddMonths(mesesDuracion);
                }

                InstanciaPagoRecurrente instanciaRecurrente = new InstanciaPagoRecurrente(monto, fechaInicio, fechaFin);
                MetodosPago metodo = (MetodosPago)rnd.Next(1, 4);
                string descripcion = $"Pago recurrente de {tipoGasto.Nombre} para {usuario.Nombre}";

                Pago pago = new Pago(metodo, usuario, tipoGasto, descripcion, instanciaRecurrente);
                AltaPago(pago);
            }

            // 17 pagos únicos (InstanciaPagoUnico) con fecha y recibo válidos
            for (int i = 0; i < 17; i++)
            {
                Usuario usuario = usuarios[rnd.Next(usuarios.Count)];
                TipoGasto tipoGasto = tiposGasto[rnd.Next(tiposGasto.Count)];
                double monto = rnd.Next(500, 3000);
                DateTime fechaPago = DateTime.Now.AddDays(-rnd.Next(1, 365));
                int reciboPago = rnd.Next(1, 99999); // Recibo mayor a 0

                InstanciaPagoUnico instanciaUnico = new InstanciaPagoUnico(monto, fechaPago, reciboPago);
                MetodosPago metodo = (MetodosPago)rnd.Next(1, 4);
                string descripcion = $"Pago único de {tipoGasto.Nombre} para {usuario.Nombre}";

                Pago pago = new Pago(metodo, usuario, tipoGasto, descripcion, instanciaUnico);
                AltaPago(pago);
            }
        }

        internal List<Pago> GetPagosPorEmail(string? email)
        {
            List<Pago> lisAux = new List<Pago>();
            foreach(Pago pago in Pagos)
            {
                if(pago.UsuarioAsociado.Email == email)
                {
                    lisAux.Add(pago);
                }
            }
            return lisAux;
        }
    }
}
