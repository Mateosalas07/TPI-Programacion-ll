using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomotrizApp.dominio
{
    public class DetalleFactura
    {
        public Vehiculo Vehiculo { get; set; }
        public AutoParte AutoParte { get; set; }

        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public DetalleFactura(Vehiculo p,AutoParte autoParte, int cant, double precio) {
            Vehiculo = p;
            AutoParte = autoParte;
            Cantidad = cant;
            Precio = precio;
        }

        public double CalcularSubTotal() {
            return Vehiculo.Precio * Cantidad;
        }

    }
}
