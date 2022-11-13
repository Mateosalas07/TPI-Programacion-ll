using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomotrizApp.dominio
{
    public class Vehiculo
    {
        public int VehiculoNro { get; set; }
        public int Precio { get; set; }
        public DateTime Año { get; set; }
        public string Color { get; set; }
        public string Modelo { get; set; }
        public bool Produccion { get; set; }
        public string Marca { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }

        public Vehiculo(int nro, int pre,string color,string modelo,DateTime año,  string tipo, string descripcion)
        {
            VehiculoNro = nro;
            Precio = pre;
            Color = color;
            Modelo = modelo;
            Produccion = true;
            Año = año;
            Tipo = tipo;
            Descripcion = descripcion;

        }

        public override string ToString()
        {
            return Modelo;
        }

    }
}
