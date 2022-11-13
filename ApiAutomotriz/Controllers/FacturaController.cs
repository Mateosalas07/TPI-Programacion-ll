using AutomotrizApp.Fachada;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutomotrizApp.dominio;
using ApiAutomotriz.Resultados;
using System;
using AutomotrizApp.Dominio;

namespace ApiAutomotriz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaController : ControllerBase
    {
        private IDataApi dataApi; //punto de acceso a la API

        private Resultado resultado;
        public FacturaController()
        {
            dataApi = new DataApiImp();
            resultado = new Resultado();
        }

        [HttpGet("/Vehiculos")]
        public IActionResult GetVehiculos()
        {  
            try
            {
               var listaVehiculos = dataApi.GetVehiculos();
                if (listaVehiculos != null)
                {
                    return Ok(listaVehiculos);
                }
                else
                {
                    resultado.SetError("Error al intentar consultar los Vehiculos");
                    resultado.StatusCode = 400;
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }

        }

        [HttpGet("/Autopartes")]
        public IActionResult GetAutopartes()
        {
            try
            {
                var listaAutopartes = dataApi.GetAutoPartes();
                if (listaAutopartes != null)
                {
                    return Ok(listaAutopartes);
                }
                else
                {
                    resultado.SetError("Error al intentar consultar las Autopartes");
                    resultado.StatusCode = 400;
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }

        }

        [HttpGet("/TipoCliente")]
        public IActionResult GetTipoCliente()
        {
            try
            {
                var listaTipoclientes = dataApi.GetTipoClientes();
                if (listaTipoclientes != null)
                {
                    return Ok(listaTipoclientes);
                }
                else
                {
                    resultado.SetError("Error al intentar consultar los Tipos de Clientes");
                    resultado.StatusCode = 400;
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }

        }

        [HttpGet("/Clientes")]
        public IActionResult GetClientes()
        {
            try
            {

                var listaClientes = dataApi.GetClientes();
                if (listaClientes != null)
                {
                    return Ok(listaClientes);
                }
                else
                {
                    resultado.SetError("Error al intentar consultar los Clientes");
                    resultado.StatusCode = 400;
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }

        }

        [HttpGet("/ProximoID")]
        public IActionResult GetProximoID()
        {
            try
            {

                var nroFactura = dataApi.ProximaFactura();
                if (nroFactura != 0)
                {
                    resultado.StatusCode = 200;
                    resultado.Ok = true;
                    return Ok(nroFactura);
                }
                else
                {
                    resultado.StatusCode = 400;
                    resultado.SetError("Error al obtener el ID");
                    return Ok(resultado);
                }

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet("/FormaPago")]
        public IActionResult GetFormaPago()
        {
            try
            {

                var listaForma = dataApi.GetForma_Pago();
                if (listaForma != null)
                {
                    return Ok(listaForma);
                }
                else
                {
                    resultado.SetError("Error al intentar consultar las Formas de Pago");
                    resultado.StatusCode = 400;
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }

        }

        [HttpPost("/Factura")]
        public IActionResult PostFactura(Factura factura)
        {
            try
            {
                if (factura == null)
                {
                    return BadRequest("Datos de factura incorrectos!");
                }

                return Ok(dataApi.SaveFactura(factura));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPost("/Usuarios")]
        public IActionResult PostUsuario(Usuario usuario)
        {
            try
            {
                if (usuario == null)
                {
                    return BadRequest("Ingresar Usuario y contraseña");
                }

                return Ok(dataApi.PostConsultarUsuario(usuario));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }


        [HttpDelete("eliminarVehiculo")]
        public IActionResult DeleteVehiculo(int id)
        {
            try
            {
                if (id == 0)
                {
                    resultado.StatusCode = 400;
                    resultado.SetError("ID Vehiculo incorrecta");
                    return BadRequest(resultado);
                }
                else
                {
                    return Ok(dataApi.EliminarVehiculo(id));
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("UpdateVehiculo")]
        public IActionResult EditarVehiculo(Vehiculo oVehiculo)
        {
            try
            {
                if (oVehiculo != null)
                {
                    return Ok(dataApi.EditarVehiculo(oVehiculo));
                }
                else
                {
                    resultado.StatusCode = 400;
                    resultado.Ok = false;
                    resultado.SetError("Error al editar el Vehiculo");
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}