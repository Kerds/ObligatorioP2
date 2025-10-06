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
    }
}
