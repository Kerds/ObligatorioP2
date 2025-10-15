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
            PrecargaDatos();
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
        
        public string GenerarEmail(Usuario usuario)
        {
            int counter = 0;
            string email = null;
            bool flag = false;

            while (!flag)
            {
                email = usuario.CreateEmail(counter);

                if (GetUsuarioPorEmail(email) == null)
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

        public void PrecargaDatos()
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
            // 6 pagos recurrentes SIN fecha de fin (siempre faltan cuotas)
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[0], TipoGastos[0], "Pago recurrente sin fin de Alquiler para Juana", new InstanciaPagoRecurrente(2000, DateTime.Now.AddMonths(-10), null)));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[1], TipoGastos[1], "Pago recurrente sin fin de Servicios para Ana", new InstanciaPagoRecurrente(1500, DateTime.Now.AddMonths(-8), null)));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[2], TipoGastos[2], "Pago recurrente sin fin de Sueldos para Lucas", new InstanciaPagoRecurrente(3000, DateTime.Now.AddMonths(-12), null)));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[3], TipoGastos[3], "Pago recurrente sin fin de Materiales para Martina", new InstanciaPagoRecurrente(1200, DateTime.Now.AddMonths(-6), null)));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[4], TipoGastos[4], "Pago recurrente sin fin de Publicidad para Santiago", new InstanciaPagoRecurrente(1800, DateTime.Now.AddMonths(-15), null)));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[5], TipoGastos[5], "Pago recurrente sin fin de Viajes para Valentina", new InstanciaPagoRecurrente(2500, DateTime.Now.AddMonths(-9), null)));

            // 9 pagos recurrentes CON fecha de fin en el futuro (faltan cuotas por abonar)
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[6], TipoGastos[0], "Pago recurrente con fin de Alquiler para Mateo", new InstanciaPagoRecurrente(2100, DateTime.Now.AddMonths(-7), DateTime.Now.AddMonths(2))));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[7], TipoGastos[1], "Pago recurrente con fin de Servicios para Camila", new InstanciaPagoRecurrente(1700, DateTime.Now.AddMonths(-5), DateTime.Now.AddMonths(3))));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[8], TipoGastos[2], "Pago recurrente con fin de Sueldos para Joaquin", new InstanciaPagoRecurrente(3200, DateTime.Now.AddMonths(-11), DateTime.Now.AddMonths(1))));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[9], TipoGastos[3], "Pago recurrente con fin de Materiales para Sofia", new InstanciaPagoRecurrente(1300, DateTime.Now.AddMonths(-7), DateTime.Now.AddMonths(4))));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[10], TipoGastos[4], "Pago recurrente con fin de Publicidad para Tomas", new InstanciaPagoRecurrente(1900, DateTime.Now.AddMonths(-13), DateTime.Now.AddMonths(2))));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[11], TipoGastos[5], "Pago recurrente con fin de Viajes para Isabella", new InstanciaPagoRecurrente(2600, DateTime.Now.AddMonths(-10), DateTime.Now.AddMonths(3))));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[12], TipoGastos[0], "Pago recurrente con fin de Alquiler para Agustin", new InstanciaPagoRecurrente(2200, DateTime.Now.AddMonths(-8), DateTime.Now.AddMonths(2))));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[13], TipoGastos[1], "Pago recurrente con fin de Servicios para Mia", new InstanciaPagoRecurrente(1600, DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(1))));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[14], TipoGastos[2], "Pago recurrente con fin de Sueldos para Facundo", new InstanciaPagoRecurrente(3100, DateTime.Now.AddMonths(-14), DateTime.Now.AddMonths(2))));

            // 10 pagos recurrentes CON fecha de fin en el pasado (todas las cuotas abonadas)
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[15], TipoGastos[3], "Pago recurrente con fin de Materiales para Julia", new InstanciaPagoRecurrente(1400, DateTime.Now.AddMonths(-8), DateTime.Now.AddMonths(-2))));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[16], TipoGastos[4], "Pago recurrente con fin de Publicidad para Nicolas", new InstanciaPagoRecurrente(2000, DateTime.Now.AddMonths(-12), DateTime.Now.AddMonths(-3))));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[17], TipoGastos[5], "Pago recurrente con fin de Viajes para Florencia", new InstanciaPagoRecurrente(2700, DateTime.Now.AddMonths(-9), DateTime.Now.AddMonths(-1))));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[18], TipoGastos[0], "Pago recurrente con fin de Alquiler para Andres", new InstanciaPagoRecurrente(2300, DateTime.Now.AddMonths(-10), DateTime.Now.AddMonths(-2))));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[19], TipoGastos[1], "Pago recurrente con fin de Servicios para Carolina", new InstanciaPagoRecurrente(1800, DateTime.Now.AddMonths(-7), DateTime.Now.AddMonths(-1))));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[20], TipoGastos[2], "Pago recurrente con fin de Sueldos para Bruno", new InstanciaPagoRecurrente(3300, DateTime.Now.AddMonths(-13), DateTime.Now.AddMonths(-2))));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[21], TipoGastos[3], "Pago recurrente con fin de Materiales para Lucia", new InstanciaPagoRecurrente(1500, DateTime.Now.AddMonths(-6), DateTime.Now.AddMonths(-2))));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[0], TipoGastos[4], "Pago recurrente con fin extra de Publicidad para Juana", new InstanciaPagoRecurrente(2100, DateTime.Now.AddMonths(-11), DateTime.Now.AddMonths(-3))));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[1], TipoGastos[5], "Pago recurrente con fin extra de Viajes para Ana", new InstanciaPagoRecurrente(2800, DateTime.Now.AddMonths(-8), DateTime.Now.AddMonths(-2))));
            AltaPago(new Pago(MetodosPago.Credito, Usuarios[2], TipoGastos[0], "Pago recurrente con fin extra de Alquiler para Lucas", new InstanciaPagoRecurrente(2400, DateTime.Now.AddMonths(-9), DateTime.Now.AddMonths(-1))));

            // 17 pagos únicos
            AltaPago(new Pago(MetodosPago.Efectivo, Usuarios[0], TipoGastos[1], "Pago único de Servicios para Juana", new InstanciaPagoUnico(900, DateTime.Now.AddDays(-30), 1001)));
            AltaPago(new Pago(MetodosPago.Efectivo, Usuarios[1], TipoGastos[2], "Pago único de Sueldos para Ana", new InstanciaPagoUnico(1100, DateTime.Now.AddDays(-60), 1002)));
            AltaPago(new Pago(MetodosPago.Debito, Usuarios[2], TipoGastos[3], "Pago único de Materiales para Lucas", new InstanciaPagoUnico(800, DateTime.Now.AddDays(-90), 1003)));
            AltaPago(new Pago(MetodosPago.Efectivo, Usuarios[3], TipoGastos[4], "Pago único de Publicidad para Martina", new InstanciaPagoUnico(950, DateTime.Now.AddDays(-120), 1004)));
            AltaPago(new Pago(MetodosPago.Debito, Usuarios[4], TipoGastos[5], "Pago único de Viajes para Santiago", new InstanciaPagoUnico(1000, DateTime.Now.AddDays(-150), 1005)));
            AltaPago(new Pago(MetodosPago.Debito, Usuarios[5], TipoGastos[0], "Pago único de Alquiler para Valentina", new InstanciaPagoUnico(1200, DateTime.Now.AddDays(-180), 1006)));
            AltaPago(new Pago(MetodosPago.Efectivo, Usuarios[6], TipoGastos[1], "Pago único de Servicios para Mateo", new InstanciaPagoUnico(1300, DateTime.Now.AddDays(-210), 1007)));
            AltaPago(new Pago(MetodosPago.Efectivo, Usuarios[7], TipoGastos[2], "Pago único de Sueldos para Camila", new InstanciaPagoUnico(1400, DateTime.Now.AddDays(-240), 1008)));
            AltaPago(new Pago(MetodosPago.Debito, Usuarios[8], TipoGastos[3], "Pago único de Materiales para Joaquin", new InstanciaPagoUnico(1500, DateTime.Now.AddDays(-270), 1009)));
            AltaPago(new Pago(MetodosPago.Efectivo, Usuarios[9], TipoGastos[4], "Pago único de Publicidad para Sofia", new InstanciaPagoUnico(1600, DateTime.Now.AddDays(-300), 1010)));
            AltaPago(new Pago(MetodosPago.Debito, Usuarios[10], TipoGastos[5], "Pago único de Viajes para Tomas", new InstanciaPagoUnico(1700, DateTime.Now.AddDays(-330), 1011)));
            AltaPago(new Pago(MetodosPago.Debito, Usuarios[11], TipoGastos[0], "Pago único de Alquiler para Isabella", new InstanciaPagoUnico(1800, DateTime.Now.AddDays(-360), 1012)));
            AltaPago(new Pago(MetodosPago.Efectivo, Usuarios[12], TipoGastos[1], "Pago único de Servicios para Agustin", new InstanciaPagoUnico(1900, DateTime.Now.AddDays(-390), 1013)));
            AltaPago(new Pago(MetodosPago.Efectivo, Usuarios[13], TipoGastos[2], "Pago único de Sueldos para Mia", new InstanciaPagoUnico(2000, DateTime.Now.AddDays(-420), 1014)));
            AltaPago(new Pago(MetodosPago.Debito, Usuarios[14], TipoGastos[3], "Pago único de Materiales para Facundo", new InstanciaPagoUnico(2100, DateTime.Now.AddDays(-450), 1015)));
            AltaPago(new Pago(MetodosPago.Efectivo, Usuarios[15], TipoGastos[4], "Pago único de Publicidad para Julia", new InstanciaPagoUnico(2200, DateTime.Now.AddDays(-480), 1016)));
            AltaPago(new Pago(MetodosPago.Debito, Usuarios[16], TipoGastos[5], "Pago único de Viajes para Nicolas", new InstanciaPagoUnico(2300, DateTime.Now.AddDays(-510), 1017)));
}
    }
}
