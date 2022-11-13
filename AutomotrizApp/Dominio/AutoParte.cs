using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomotrizApp.dominio
{
   public class AutoParte
    {
        public int AutoParteNro { get; set; }
        public double Precio { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
        public string Modelo { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public AutoParte(int AutoPnro, double pre,string nombre, string color,/* string modelo,*/ string descripcion)
        {
            AutoParteNro = AutoPnro;
            Precio = pre;
           Nombre = nombre;
            Color = color;
            //Modelo = modelo;
            Descripcion = descripcion;
            Activo = true;
        }
    }
}
