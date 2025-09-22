using System.ComponentModel.DataAnnotations;

namespace ObligatorioP2
{
    public class Equipo
    {
        public int Id { get; set; }
        public static int UId { get; set; } = 0;
        public string Nombre { get; set; }

        public Equipo(){
            Id = UId;
        }
        public Equipo(string nombre)
        {
            Validar(nombre);
            Id = UId;
            Nombre = nombre;
            UId++;
        }
        public override string ToString()
        {
            return $"Id: {Id}, Nombre: {Nombre}";
        }
        public override bool Equals(object? obj)
        {
            if (obj is Equipo)
            {
                Equipo equipo = (Equipo)obj;
                return this.Id == equipo.Id;
            }
            return false;
        }
        public void Validar(string nombre)
        {
                       ValidarNombre(nombre);
        }
        public void ValidarNombre(string nombre)
        {
            if(nombre.Length < 3)
            {
                throw new Exception("El nombre del equipo debe tener al menos 3 caracteres.");
            }
        }
    }
}