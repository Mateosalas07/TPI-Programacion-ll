using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomotrizApp.dominio
{
    public class Factura
    {
        public int nro_factura { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public int Forma_pago { get; set; }

        public List<DetalleFactura> Detalles { get; set; }

        public Factura()
        {
            Detalles = new List<DetalleFactura>();
        }

        public void AgregarDetalle(DetalleFactura detalle) {
            Detalles.Add(detalle);
        }

        public void QuitarDetalle(int indice) {
            Detalles.RemoveAt(indice);
        }


        public double CalcularTotal() {
            double total = 0;
            foreach (DetalleFactura item in Detalles)
                total += item.CalcularSubTotal();
            return total;
        }
        public double CalcularTotalConDescuento()
        {
            double final = this.CalcularTotal();
            //if (Descuento > 0)
            //{
            //    final -= final * Descuento / 100;
            //}
            return final;
        }

    }
}
