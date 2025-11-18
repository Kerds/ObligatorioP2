using Clases.Pagos;
using Clases.Roles;
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

        private Sistema()
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
        public IEnumerable<TipoGasto> GetTipoGastos()
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
                tipoGasto.Validar();

                if (TipoGastos.Contains(tipoGasto))
                {
                    TipoGasto t = GetTipoGastoByName(tipoGasto.Nombre);
                    if (t.Activo) { 
                    throw new Exception("El tipo de gasto ya existe.");
                    }
                    else {
                        t.Activo = true;
                        return;
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
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
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("El email no puede estar vacío.");
                
            }
            if (!email.Contains("@") || !email.Contains("."))
            {
                throw new Exception("El email ingresado no es válido.");
            }
            if (email.Length < 5)
            {
                throw new Exception("El email debe tener al menos 5 caracteres.");
                
            }
            if (!email.Contains("laEmpresa"))
            {
                throw new Exception("El email debe pertenecer a la empresa (debe contener 'laEmpresa').");
            }
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

        public TipoGasto GetTipoGasto(int id)
        {
            foreach (TipoGasto tipo in TipoGastos)
            {
                if (tipo.Id == id)
                {
                    return tipo;
                }
            }
            return null;
        }
        public TipoGasto GetTipoGastoByName(string name)
        {
            foreach (TipoGasto tipo in TipoGastos)
            {
                if (tipo.Nombre == name)
                {
                    return tipo;
                }
            }
            return null;
        }
        public Equipo GetEquipoPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new Exception("El nombre del equipo no puede estar vacío.");
            }
            if (nombre.Length < 3)
            {
                throw new Exception("El nombre del equipo debe tener al menos 3 caracteres.");
            }

            foreach (Equipo equipo in Equipos)
            {
                if (equipo.Nombre.ToLower() == nombre.ToLower())
                {
                    return equipo;
                }
            }
            return null;
        }

        public IEnumerable<Usuario> GetUsuariosDeEquipo(string nombreEquipo)
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
    // Equipos existentes
    Equipo contabilidad = GetEquipoPorNombre("Contabilidad");
    Equipo finanzas     = GetEquipoPorNombre("Finanzas");
    Equipo tesoreria    = GetEquipoPorNombre("Tesoreria");
    Equipo auditoria    = GetEquipoPorNombre("Auditoria");
            Rol Gerente = new RolGerente();
            Rol Empleado = new RolEmpleado();
            // ---- Contabilidad (6) ----
            Usuario usuario1 = new Usuario("Juana", "Melcer", "12345678", contabilidad, Gerente);
    AltaUsuario(usuario1);
    contabilidad.AgregarMiembro(usuario1);

    Usuario usuario2 = new Usuario("Ana", "Perez", "12345678", contabilidad, Empleado);
    AltaUsuario(usuario2);
    contabilidad.AgregarMiembro(usuario2);

    Usuario usuario3 = new Usuario("Lucas", "Garcia", "12345678", contabilidad, Gerente);
    AltaUsuario(usuario3);
    contabilidad.AgregarMiembro(usuario3);

    Usuario usuario4 = new Usuario("Martina", "Rodriguez", "12345678", contabilidad, Empleado);
    AltaUsuario(usuario4);
    contabilidad.AgregarMiembro(usuario4);

    Usuario usuario5 = new Usuario("Santiago", "Fernandez", "12345678", contabilidad, Gerente);
    AltaUsuario(usuario5);  
    contabilidad.AgregarMiembro(usuario5);

    Usuario usuario6 = new Usuario("Valentina", "Gomez", "12345678", contabilidad, Empleado);
    AltaUsuario(usuario6);
    contabilidad.AgregarMiembro(usuario6);

    // ---- Finanzas (5) ----
    Usuario usuario7 = new Usuario("Mateo", "Suarez", "12345678", finanzas, Gerente);
    AltaUsuario(usuario7);
    finanzas.AgregarMiembro(usuario7);

    Usuario usuario8 = new Usuario("Camila", "Lopez", "12345678", finanzas, Empleado);
    AltaUsuario(usuario8);
    finanzas.AgregarMiembro(usuario8);

    Usuario usuario9 = new Usuario("Joaquin", "Alvarez", "12345678", finanzas, Gerente);
    AltaUsuario(usuario9);
    finanzas.AgregarMiembro(usuario9);

    Usuario usuario10 = new Usuario("Sofia", "Martinez", "12345678", finanzas, Empleado);
    AltaUsuario(usuario10);
    finanzas.AgregarMiembro(usuario10);

    Usuario usuario11 = new Usuario("Tomas", "Pereira", "12345678", finanzas, Gerente);
    AltaUsuario(usuario11);
    finanzas.AgregarMiembro(usuario11);

    // ---- Tesoreria (5) ----
    Usuario usuario12 = new Usuario("Isabella", "Santos", "12345678", tesoreria, Empleado);
    AltaUsuario(usuario12);
    tesoreria.AgregarMiembro(usuario12);

    Usuario usuario13 = new Usuario("Agustin", "Cabrera", "12345678", tesoreria, Gerente);
    AltaUsuario(usuario13);
    tesoreria.AgregarMiembro(usuario13);

    Usuario usuario14 = new Usuario("Mia", "Torres", "12345678", tesoreria, Empleado);
    AltaUsuario(usuario14);
    tesoreria.AgregarMiembro(usuario14);

    Usuario usuario15 = new Usuario("Facundo", "Silva", "12345678", tesoreria, Empleado);
    AltaUsuario(usuario15);
    tesoreria.AgregarMiembro(usuario15);

    Usuario usuario16 = new Usuario("Julia", "Ramos", "12345678", tesoreria,Gerente);
    AltaUsuario(usuario16);
    tesoreria.AgregarMiembro(usuario16);

    // ---- Auditoria (5) ----
    Usuario usuario17 = new Usuario("Nicolas", "Castro", "12345678", auditoria, Empleado);
    AltaUsuario(usuario17);
    auditoria.AgregarMiembro(usuario17);

    Usuario usuario18 = new Usuario("Florencia", "Vega", "12345678", auditoria, Gerente);
    AltaUsuario(usuario18);
    auditoria.AgregarMiembro(usuario18);

    Usuario usuario19 = new Usuario("Andres", "Morales", "12345678", auditoria, Empleado);
    AltaUsuario(usuario19);
    auditoria.AgregarMiembro(usuario19);

    Usuario usuario20 = new Usuario("Carolina", "Ruiz", "12345678", auditoria, Gerente);
    AltaUsuario(usuario20);
    auditoria.AgregarMiembro(usuario20);

    Usuario usuario21 = new Usuario("Bruno", "Herrera", "12345678", auditoria, Empleado);
    AltaUsuario(usuario21);
    auditoria.AgregarMiembro(usuario21);
    Usuario usuario22 = new Usuario("Lucia", "Acosta", "12345678", auditoria, Gerente);
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
    // ===== 6 pagos RECURRENTES SIN fecha de fin (EndDate = null) =====

    AltaPago(new Pago(MetodosPago.Credito,  Usuarios[0],  TipoGastos[0],  "Alquiler mensual",new InstanciaPagoRecurrente(28000, DateTime.Now.AddMonths(-12), null)));
    AltaPago(new Pago(MetodosPago.Debito,   Usuarios[1],  TipoGastos[1],  "Servicios (UTE/ANTEL)",new InstanciaPagoRecurrente(6500,  DateTime.Now.AddMonths(-9),  null)));
    AltaPago(new Pago(MetodosPago.Credito, Usuarios[2],  TipoGastos[2],  "Sueldos",new InstanciaPagoRecurrente(120000,DateTime.Now.AddMonths(-10), null)));
    AltaPago(new Pago(MetodosPago.Credito,  Usuarios[3],  TipoGastos[3],  "Mantenimiento",new InstanciaPagoRecurrente(4200,  DateTime.Now.AddMonths(-6),  null)));
    AltaPago(new Pago(MetodosPago.Debito,   Usuarios[4],  TipoGastos[4],  "Internet",new InstanciaPagoRecurrente(1800,  DateTime.Now.AddMonths(-8),  null)));
    AltaPago(new Pago(MetodosPago.Credito, Usuarios[5],  TipoGastos[5],  "Limpieza",new InstanciaPagoRecurrente(3500,  DateTime.Now.AddMonths(-5),  null)));

    // ===== 9 pagos RECURRENTES con fecha de fin en el FUTURO =====
    AltaPago(new Pago(MetodosPago.Credito,  Usuarios[6],  TipoGastos[0],  "Licencia software",new InstanciaPagoRecurrente(2200,  DateTime.Now.AddMonths(-4),  DateTime.Now.AddMonths(8))));
    AltaPago(new Pago(MetodosPago.Debito,   Usuarios[7],  TipoGastos[1],  "Seguro flota",new InstanciaPagoRecurrente(9500,  DateTime.Now.AddMonths(-7),  DateTime.Now.AddMonths(5))));
    AltaPago(new Pago(MetodosPago.Credito, Usuarios[8],  TipoGastos[2],  "Alquiler depósito",new InstanciaPagoRecurrente(54000, DateTime.Now.AddMonths(-2),  DateTime.Now.AddMonths(10))));
    AltaPago(new Pago(MetodosPago.Credito,  Usuarios[9],  TipoGastos[3],  "Servicio guardia",new InstanciaPagoRecurrente(4100,  DateTime.Now.AddMonths(-3),  DateTime.Now.AddMonths(4))));
    AltaPago(new Pago(MetodosPago.Debito,   Usuarios[10], TipoGastos[4],  "Telefonía móvil",new InstanciaPagoRecurrente(2600,  DateTime.Now.AddMonths(-1),  DateTime.Now.AddMonths(12))));
    AltaPago(new Pago(MetodosPago.Credito, Usuarios[11], TipoGastos[5],  "Mantenimiento HVAC",new InstanciaPagoRecurrente(7800,  DateTime.Now.AddMonths(-6),  DateTime.Now.AddMonths(2))));
    AltaPago(new Pago(MetodosPago.Credito,  Usuarios[12], TipoGastos[0],  "Soporte técnico",new InstanciaPagoRecurrente(3300,  DateTime.Now.AddMonths(-5),  DateTime.Now.AddMonths(6))));
    AltaPago(new Pago(MetodosPago.Debito,   Usuarios[13], TipoGastos[1],  "Licencias antivirus",new InstanciaPagoRecurrente(1450,  DateTime.Now.AddMonths(-8),  DateTime.Now.AddMonths(3))));
    AltaPago(new Pago(MetodosPago.Credito, Usuarios[14], TipoGastos[2],  "Estacionamiento",new InstanciaPagoRecurrente(2100,  DateTime.Now.AddMonths(-2),  DateTime.Now.AddMonths(7))));

    // ===== 10 pagos RECURRENTES con fecha de fin en el PASADO =====
    AltaPago(new Pago(MetodosPago.Credito,  Usuarios[0],  TipoGastos[3],  "Campaña marketing",new InstanciaPagoRecurrente(12500, DateTime.Now.AddMonths(-14), DateTime.Now.AddMonths(-6))));
    AltaPago(new Pago(MetodosPago.Debito,   Usuarios[1],  TipoGastos[4],  "Contrato limpieza",new InstanciaPagoRecurrente(3800,  DateTime.Now.AddMonths(-12), DateTime.Now.AddMonths(-3))));
    AltaPago(new Pago(MetodosPago.Credito, Usuarios[2],  TipoGastos[5],  "Consultoría RRHH",new InstanciaPagoRecurrente(9200,  DateTime.Now.AddMonths(-9),  DateTime.Now.AddMonths(-1))));
    AltaPago(new Pago(MetodosPago.Credito,  Usuarios[3],  TipoGastos[0],  "Capacitación personal",new InstanciaPagoRecurrente(4600,  DateTime.Now.AddMonths(-10), DateTime.Now.AddMonths(-2))));
    AltaPago(new Pago(MetodosPago.Debito,   Usuarios[4],  TipoGastos[1],  "Servicio backup",new InstanciaPagoRecurrente(1700,  DateTime.Now.AddMonths(-11), DateTime.Now.AddMonths(-5))));
    AltaPago(new Pago(MetodosPago.Credito, Usuarios[5],  TipoGastos[2],  "Alquiler sala reuniones",new InstanciaPagoRecurrente(3000,  DateTime.Now.AddMonths(-7),  DateTime.Now.AddMonths(-4))));
    AltaPago(new Pago(MetodosPago.Credito,  Usuarios[6],  TipoGastos[3],  "Publicidad trimestral",new InstanciaPagoRecurrente(8000,  DateTime.Now.AddMonths(-8),  DateTime.Now.AddMonths(-2))));
    AltaPago(new Pago(MetodosPago.Debito,   Usuarios[7],  TipoGastos[4],  "Soporte ERP",new InstanciaPagoRecurrente(5100,  DateTime.Now.AddMonths(-15), DateTime.Now.AddMonths(-9))));
    AltaPago(new Pago(MetodosPago.Debito, Usuarios[8],  TipoGastos[5],  "Servicio jardinería",new InstanciaPagoRecurrente(2400,  DateTime.Now.AddMonths(-13), DateTime.Now.AddMonths(-8))));
    AltaPago(new Pago(MetodosPago.Credito,  Usuarios[9],  TipoGastos[0],  "Monitoreo cámaras",new InstanciaPagoRecurrente(3500,  DateTime.Now.AddMonths(-6),  DateTime.Now.AddMonths(-1))));

    // ===== 17 pagos ÚNICOS (con número de recibo agregado) =====
    AltaPago(new Pago(MetodosPago.Efectivo, Usuarios[10], TipoGastos[1], "Compra insumos oficina",new InstanciaPagoUnico(2700,  DateTime.Now.AddDays(-25), 2001)));
    AltaPago(new Pago(MetodosPago.Debito,   Usuarios[11], TipoGastos[2], "Flete puntual mercadería",new InstanciaPagoUnico(15400, DateTime.Now.AddDays(-10), 2002)));
    AltaPago(new Pago(MetodosPago.Efectivo,  Usuarios[12], TipoGastos[3], "Reparación impresora",new InstanciaPagoUnico(4300,  DateTime.Now.AddDays(-40), 2003)));
    AltaPago(new Pago(MetodosPago.Efectivo, Usuarios[13], TipoGastos[4], "Catering reunión clientes",new InstanciaPagoUnico(9600,  DateTime.Now.AddDays(-5),  2004)));
    AltaPago(new Pago(MetodosPago.Debito,   Usuarios[14], TipoGastos[5], "Taxi traslado urgente",new InstanciaPagoUnico(850,   DateTime.Now.AddDays(-3),  2005)));
    AltaPago(new Pago(MetodosPago.Efectivo,  Usuarios[0],  TipoGastos[0], "Reposición sillas",new InstanciaPagoUnico(11800, DateTime.Now.AddDays(-60), 2006)));
    AltaPago(new Pago(MetodosPago.Efectivo, Usuarios[1],  TipoGastos[1], "Papelería y timbres",new InstanciaPagoUnico(2100,  DateTime.Now.AddDays(-15), 2007)));
    AltaPago(new Pago(MetodosPago.Debito,   Usuarios[2],  TipoGastos[2], "Carga combustible única",new InstanciaPagoUnico(4200,  DateTime.Now.AddDays(-7),  2008)));
    AltaPago(new Pago(MetodosPago.Debito,  Usuarios[3],  TipoGastos[3], "Compra luces LED",new InstanciaPagoUnico(7800,  DateTime.Now.AddDays(-20), 2009)));
    AltaPago(new Pago(MetodosPago.Efectivo, Usuarios[4],  TipoGastos[4], "Pago peaje excepcional",new InstanciaPagoUnico(340,   DateTime.Now.AddDays(-1),  2010)));
    AltaPago(new Pago(MetodosPago.Debito,   Usuarios[5],  TipoGastos[5], "Servicio plomería",new InstanciaPagoUnico(5200,  DateTime.Now.AddDays(-13), 2011)));
    AltaPago(new Pago(MetodosPago.Debito,  Usuarios[6],  TipoGastos[0], "Compra monitor 27",new InstanciaPagoUnico(18500, DateTime.Now.AddDays(-33), 2012)));
    AltaPago(new Pago(MetodosPago.Efectivo, Usuarios[7],  TipoGastos[1], "Cafetera nueva",new InstanciaPagoUnico(4200,  DateTime.Now.AddDays(-18), 2013)));
    AltaPago(new Pago(MetodosPago.Debito,   Usuarios[8],  TipoGastos[2], "Reemplazo neumático",new InstanciaPagoUnico(9300,  DateTime.Now.AddDays(-22), 2014)));
    AltaPago(new Pago(MetodosPago.Efectivo,  Usuarios[9],  TipoGastos[3], "Servicio desinfección",new InstanciaPagoUnico(6100,  DateTime.Now.AddDays(-27), 2015)));
    AltaPago(new Pago(MetodosPago.Efectivo, Usuarios[10], TipoGastos[4], "Compra matafuegos",new InstanciaPagoUnico(3900,  DateTime.Now.AddDays(-45), 2016)));
    AltaPago(new Pago(MetodosPago.Debito,   Usuarios[1], TipoGastos[5], "Reparación notebook",new InstanciaPagoUnico(7200,  DateTime.Now.AddDays(-12), 2017)));
}

        public Usuario ObtenerUsuario(string email, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("El email y la contraseña no pueden estar vacíos.");

            }
            if (!email.Contains("@") || !email.Contains("."))
            {
                throw new Exception("El email ingresado no es válido.");
            }
            if (email.Length < 5)
            {
                throw new Exception("El email debe tener al menos 5 caracteres.");

            }
            if (!email.Contains("laEmpresa"))
            {
                throw new Exception("El email debe pertenecer a la empresa (debe contener 'laEmpresa').");
            }
            if ( string.IsNullOrWhiteSpace(contrasena))
            {
                throw new Exception("El email y la contraseña no pueden estar vacíos.");
            }
            foreach (Usuario usuario in Usuarios)
            {
                if (usuario.Email == email && usuario.Contrasenia == contrasena)
                {
                    return usuario;
                }
            }
            return null;
        }

        public void BajaGasto(TipoGasto tipoGasto)
        {
            tipoGasto.Activo = false;
        }
        public double GetTotalPagosUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new Exception("El usuario no puede ser nulo.");
            }
            if (!Usuarios.Contains(usuario))
            {
                throw new Exception("El usuario no existe en el sistema.");
            }
            if (Pagos.Count == 0)
            {
                throw new Exception("No hay pagos registrados en el sistema.");
            }
            double total = 0;
            IEnumerable<Pago> pagosUsuario = GetPagosUsuario(usuario);
            foreach (Pago pago in pagosUsuario)
            {
                total += pago.GetPagoMes();
            }
            return total;
        }
        public IEnumerable<Pago> GetPagosUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new Exception("El usuario no puede ser nulo.");
            }
            if (!Usuarios.Contains(usuario))
            {
                throw new Exception("El usuario no existe en el sistema.");
            }
            if (Pagos.Count == 0)
            {
                throw new Exception("No hay pagos registrados en el sistema.");
            }
            List<Pago> pagosUsuario = new List<Pago>();
            foreach (Pago pago in Pagos)
            {
                if (pago.UsuarioAsociado.Email == usuario.Email && pago.EsPagoActivo(DateTime.Now))
                {
                    pagosUsuario.Add(pago);
                }
            }
          
            return pagosUsuario.AsEnumerable();
        }

        public IEnumerable<Pago> GetPagosMiembrosEquipo(string nombreEquipo, Usuario user)
        {
            Equipo e = GetEquipoPorNombre(nombreEquipo);
            if (e == null)
            {
                throw new Exception("El equipo no existe.");
            }
            if (e.GetMiembros().Count() == 0)
            {
                throw new Exception("El equipo no tiene miembros.");
            }
            if (Pagos.Count == 0)
            {
                throw new Exception("No hay pagos registrados en el sistema.");
            }
            List<Pago> pagosEquipo = new List<Pago>();
            foreach (Usuario u in e.GetMiembros())
            {
                if (u.Email == user.Email)
                {
                    continue;
                }
                foreach (Pago p in GetPagosUsuario(u))
                {
                    pagosEquipo.Add(p);
                }
            }
            pagosEquipo.Sort();
            return pagosEquipo;
        }
    
            
            

        public void TipoGastoTienePago(TipoGasto t)
        {
            if (t == null)
            {
                throw new Exception("El tipo de gasto no existe."); 
                
            }
            foreach (Pago p in Pagos)
            {
                if (p.UsaTipoGasto(t))
                {
                    throw new Exception("No se puede eliminar, el Tipo de gasto tiene un pago asociado.");
                }
            }
        }

        public void AltaPagoDesdeDTO(DTOpago dto, Usuario usuario)
        {
            try
            {
                if (dto == null)
                {
                    throw new Exception("El DTO de pago no puede ser nulo.");
                }
                dto.Validar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar el DTO de pago: " + ex.Message);
            }

            TipoGasto tipoGasto = GetTipoGastoByName(dto.TipoGasto);
            if (tipoGasto == null)
            {
                throw new Exception("El tipo de gasto no existe.");
            }
            MetodosPago metodoPago;
            if (!Enum.TryParse(dto.MetodosPago, out metodoPago))
            {
                throw new Exception("El método de pago no es válido.");
            }
            InstanciaPago instanciaPago;
            if (dto.TipoPago == "Unico")
            {
                instanciaPago = new InstanciaPagoUnico(dto.MontoBase, dto.FechaPago.Value, dto.ReciboPago);
            }
            else if (dto.TipoPago == "Recurrente")
            {
                instanciaPago = new InstanciaPagoRecurrente(dto.MontoBase, dto.FechaPago.Value, dto.FechaFin);
            }
            else
            {
                throw new Exception("El tipo de pago no es válido.");
            }

            Pago nuevoPago = new Pago(metodoPago, usuario, tipoGasto, dto.Descripcion, instanciaPago);
            try
            {
                AltaPago(nuevoPago);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al dar de alta el pago: " + ex.Message);
            }
        }
    }
}
