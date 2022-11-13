﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomotrizApp.dominio;
using AutomotrizApp.Datos.Intefaz;
using AutomotrizApp.Datos;
using AutomotrizApp.Dominio;

namespace AutomotrizApp.Datos.Implementacion
{
   public class FacturaDao : IDaoFactura
    {
        public int ObtenerProximoNro()
        {
            string sp = "SP_PROXIMO_ID";
            return HelperDao.ObtenerInstancia().ConsultaEscalarSQL(sp, "@next");
        }

        bool IDaoFactura.ConsultarUsuario(Usuario usuario)
        {
            string sp = "SP_CONSULTAR_USUARIO";
            //Usuario user = new Usuario();
            List<Usuario> lst = new List<Usuario>();
            return HelperDao.ObtenerInstancia().Login(sp, usuario);
            //return t_usuario;
        }

        public List<Vehiculo> ObtenerVehiculos()
        {
            List<Vehiculo> lst = new List<Vehiculo>();

            string sp = "SP_CONSULTAR_VEHICULOS";
            DataTable t = HelperDao.ObtenerInstancia().ConsultaSQL(sp, null);

            foreach (DataRow dr in t.Rows)
            {
                //Mapear un registro a un objeto del modelo de dominio
                int nro = int.Parse(dr["id_Vehículo"].ToString());
                string modelo = dr["Modelo"].ToString();
                string tipo = dr["Tipo de Vehículo"].ToString();
                int precio = int.Parse(dr["Precio"].ToString());
                string color = dr["Color"].ToString();
                DateTime año = DateTime.Parse(dr["Año"].ToString());
                string descripcion = dr["Descripcion"].ToString();
                bool activo = dr["Produccion"].ToString().Equals("Si");

                Vehiculo aux = new Vehiculo(nro,precio,color,modelo,año,tipo,descripcion);
                aux.Produccion = activo;
                lst.Add(aux);

            }

            return lst;
        }

        public List<AutoParte> ObtenerAutopartes()
        {
            List<AutoParte> lst = new List<AutoParte>();

            string sp = "SP_CONSULTAR_AUTOPARTES";
            DataTable t = HelperDao.ObtenerInstancia().ConsultaSQL(sp, null);

            foreach (DataRow dr in t.Rows)
            {
                //Mapear un registro a un objeto del modelo de dominio
                int nro = int.Parse(dr["id_autoparte"].ToString());                
                int precio = int.Parse(dr["precio_act"].ToString());
                //DateTime año = DateTime.Parse(dr["año"].ToString());
                string nombre = dr["nombre"].ToString();
                string color = dr["color"].ToString();
                bool produccion = dr["activo"].ToString().Equals("S");
                string descripcion = dr["descripcion"].ToString();

                AutoParte aux = new AutoParte(nro, precio, nombre, color , descripcion);
                aux.Activo = produccion;
                lst.Add(aux);

            }

            return lst;
        }

        public List<TipoClientes> ConsultarTipoClientes()
        {
            List<TipoClientes> lstTipos = new List<TipoClientes>();
            string sp = "SP_CONSULTAR_TIPOCLIENTES";
            DataTable t = HelperDao.ObtenerInstancia().ConsultaSQL(sp, null);

            foreach (DataRow dr in t.Rows)
            {
                //Mapear un registro a un objeto del modelo de dominio
                int id = int.Parse(dr["id_tipo_cliente"].ToString());
                string tipo = dr["tipo_cliente"].ToString();
                

                TipoClientes aux = new TipoClientes(id, tipo);

                lstTipos.Add(aux);

            }

            return lstTipos;
        }

        public List<Clientes> ConsultarClientes()
        {
            List<Clientes> lstCLientes = new List<Clientes>();
            string sp = "SP_CONSULTAR_CLIENTES";
            DataTable t = HelperDao.ObtenerInstancia().ConsultaSQL(sp, null);

            foreach (DataRow dr in t.Rows)
            {
                //Mapear un registro a un objeto del modelo de dominio
                
                int id_cliente = int.Parse(dr["cod_cliente"].ToString());
                string nombre = dr["Nombre"].ToString();

                Clientes aux = new Clientes(id_cliente, nombre);

                lstCLientes.Add(aux);

            }

            return lstCLientes;
        }

        public bool Crear(Factura oFactura)
        {
            bool ok = true;
            SqlConnection cnn = HelperDao.ObtenerInstancia().ObtenerConexion();
            SqlTransaction ?t = null;
            SqlCommand cmd = new SqlCommand();
            try
            {

                cnn.Open();
                t = cnn.BeginTransaction();
                cmd.Connection = cnn;
                cmd.Transaction = t;
                cmd.CommandText = "SP_INSERTAR_FACTURA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_forma_pago", oFactura.Forma_pago);
                cmd.Parameters.AddWithValue("@cod_cliente", oFactura.Cliente);

                //cmd.Parameters.AddWithValue("@total", oFactura.CalcularTotal());

                //parámetro de salida:
                SqlParameter pOut = new SqlParameter();
                pOut.ParameterName = "@nro_factura";
                pOut.DbType = DbType.Int32;
                pOut.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pOut);
                cmd.ExecuteNonQuery();

                int presupuestoNro = (int)pOut.Value;

                SqlCommand cmdDetalle;
                int detalleNro = 1;
                foreach (DetalleFactura item in oFactura.Detalles)
                {
                    cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE", cnn, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@detalle", detalleNro);
                    cmdDetalle.Parameters.AddWithValue("@nro_factura", oFactura.nro_factura);
                    cmdDetalle.Parameters.AddWithValue("@id_vehiculo", item.Vehiculo.VehiculoNro);
                    cmdDetalle.Parameters.AddWithValue("@id_autoparte", item.AutoParte.AutoParteNro);
                    cmdDetalle.Parameters.AddWithValue("@precio", item.Precio);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", item.Cantidad);
                    cmdDetalle.ExecuteNonQuery();

                    detalleNro++;
                }
                t.Commit();
            }

            catch (Exception)
            {
                if (t != null)
                    t.Rollback();
                ok = false;
            }

            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }

            return ok;
        }

        public bool ActualizarFactura(Factura oFactura)
        {
            bool ok = true;
            SqlConnection cnn = HelperDao.ObtenerInstancia().ObtenerConexion();
            SqlTransaction t = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                cmd.Connection = cnn;
                cmd.Transaction = t;
                cmd.CommandText = "SP_MODIFICAR_MAESTRO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cliente", oFactura.Cliente);
                cmd.Parameters.AddWithValue("@id_forma_pago",oFactura.Forma_pago);
                cmd.Parameters.AddWithValue("@total", oFactura.CalcularTotal());
                cmd.Parameters.AddWithValue("@nro_factura", oFactura.Detalles);
                cmd.Parameters.AddWithValue("@nro_factura", oFactura.nro_factura);
                cmd.ExecuteNonQuery();

                SqlCommand cmdDetalle;
                int detalleNro = 1;
                foreach (DetalleFactura item in oFactura.Detalles )
                {
                    cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE", cnn, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@nro_factura", oFactura.nro_factura);
                    cmdDetalle.Parameters.AddWithValue("@detalle", detalleNro);
                    cmdDetalle.Parameters.AddWithValue("@id_autoparte", item.AutoParte);
                    cmdDetalle.Parameters.AddWithValue("@id_vehiculo", item.Vehiculo);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", item.Cantidad);
                    cmdDetalle.ExecuteNonQuery();

                    detalleNro++;
                }
                t.Commit();
            }

            catch (Exception)
            {
                if (t != null)
                    t.Rollback();
                ok = false;
            }

            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }

            return ok;
        }

        public bool BorrarVehiculo(int nro)
        {
            string sp = "SP_ELIMINAR_FACTURA";
            List<Parametro> lst = new List<Parametro>();
            lst.Add(new Parametro("@presupuesto_nro", nro));
            int afectadas = HelperDao.ObtenerInstancia().EjecutarSQL(sp, lst);
            return afectadas > 0;
        }

        public List<Factura> ObtenerFacturaPorFiltros(DateTime desde, DateTime hasta, string cliente)
        {
            List<Factura> facturas = new List<Factura>();
            string sp = "SP_CONSULTAR_FACTURA";
            List<Parametro> lst = new List<Parametro>();
            lst.Add(new Parametro("@fecha_desde", desde));
            lst.Add(new Parametro("@fecha_hasta", hasta));
            lst.Add(new Parametro("@cliente", cliente));
            DataTable dt = HelperDao.ObtenerInstancia().ConsultaSQL(sp, lst);

            foreach (DataRow row in dt.Rows)
            {
                Factura factura = new Factura();
                factura.Cliente = row["cliente"].ToString();
                factura.nro_factura = int.Parse(row["presupuesto_nro"].ToString());
                factura.Fecha = DateTime.Parse(row["fecha"].ToString());
                factura.Forma_pago = int.Parse(row["forma_pago"].ToString());
                facturas.Add(factura);
            }

            return facturas;
        }

        public DataTable ObtenerReporte(DateTime desde, DateTime hasta)
        {
            string sp = "SP_REPORTE_VEHICULOS";
            List<Parametro> lst = new List<Parametro>();
            lst.Add(new Parametro("@fecha_desde", desde));
            lst.Add(new Parametro("@fecha_hasta", hasta));
            DataTable dt = HelperDao.ObtenerInstancia().ConsultaSQL(sp, lst);
            return dt;
        }

        public Factura ObtenerFacturaPorNro(int nro)
        {
            Factura ofactura = new Factura();
            string sp = "SP_CONSULTAR_DETALLES_FACTURA";
            List<Parametro> lst = new List<Parametro>();
            lst.Add(new Parametro("@presupuesto_nro", nro));

            DataTable dt = HelperDao.ObtenerInstancia().ConsultaSQL(sp, lst);
            bool primero = true;

            foreach (DataRow fila in dt.Rows)
            {
                if (primero)
                {
                    ofactura.Cliente = fila["cliente"].ToString();
                    ofactura.Fecha = DateTime.Parse(fila["fecha"].ToString());
                    ofactura.Forma_pago = int.Parse(fila["forma_pago"].ToString());
                    primero = false;
                }
                int nroProducto = int.Parse(fila["id_producto"].ToString());
                string nombre = fila["n_producto"].ToString();
                double precio = double.Parse(fila["precio"].ToString());
                //Vehiculo producto = new Vehiculo(nro,precio,color,modelo,año);
                int cantidad = int.Parse(fila["cantidad"].ToString());
                //DetallePresupuesto detalle = new DetallePresupuesto(producto, cantidad);
                //presupuesto.AgregarDetalle(detalle);

            }

            return ofactura;
        }

        public bool ActualizarVehiculo(Vehiculo vehiculo)
        {
            throw new NotImplementedException();
        }


        public List<FormadePago> ObtenerFormadePago()
        {
            List<FormadePago> lstFormas = new List<FormadePago>();
            string sp = "SP_CONSULTAR_FORMAS_PAGO";
            DataTable t = HelperDao.ObtenerInstancia().ConsultaSQL(sp, null);

            foreach (DataRow dr in t.Rows)
            {
                //Mapear un registro a un objeto del modelo de dominio

                int id_forma = int.Parse(dr["id_forma"].ToString());
                string forma = dr["forma_pago"].ToString();

                FormadePago aux = new FormadePago(id_forma, forma);

                lstFormas.Add(aux);

            }

            return lstFormas;
        }

    }
}
