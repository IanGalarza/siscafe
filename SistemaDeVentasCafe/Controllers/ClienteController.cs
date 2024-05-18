using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using SistemaDeVentasCafe.Models;

namespace SistemaDeVentasCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        //Poder utilizar el contexto de la DB
        public readonly DbapiContext _dbcontext;

        public ClienteController(DbapiContext _context)
        {
            _dbcontext = _context;
        }

        //API Listar Clientes

        [HttpGet]

        //Ruta para llamar a la API listar

        [Route("Listar")]

        public IActionResult Listar()
        {
            List<Cliente> lista = new List<Cliente>();

            //Capturador de errores
            try
            {
                lista = _dbcontext.Clientes.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });   //Retorna la lista de objetos con un mensaje HTTP ok
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        //API Consultar Cliente

        [HttpGet]

        //Ruta para llamar a la API consultar

        [Route("Consultar/{idCliente:int}")]

        public IActionResult Consultar(int idCliente)
        {
            Cliente cliente = new Cliente();

            //realizo una busqueda del cliente

            cliente = _dbcontext.Clientes.Find(idCliente);

            if (cliente == null)
            {
                return BadRequest("Cliente no encontrado");
            }

            //Capturador de errores
            try
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = cliente });   //Retorna el cliente si es encontrado con un mensaje HTTP ok
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = cliente});
            }
        }

        //API Registrar Cliente

        [HttpPost]

        //Ruta para llamar a la API Registrar

        [Route("Registrar")]

        public IActionResult Registrar([FromBody] Cliente objeto)
        {
            try
            {
                _dbcontext.Clientes.Add(objeto);
                _dbcontext.SaveChanges();                    //Se almacena y guardan los cambios de la tabla

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });  //Enviamos un mensaje de que se guardo exitosamente
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        //API Modificar Cliente

        [HttpPut]

        //Ruta para llamar a la API Modificar

        [Route("Modificar")]

        public IActionResult Modificar([FromBody] Cliente objeto)
        {

            Cliente cliente = new Cliente();

            //realizo una busqueda del producto

            cliente = _dbcontext.Clientes.Find(objeto.IdCliente);

            if (cliente == null)
            {
                return BadRequest("Cliente no encontrado");
            }

            try
            {

                //Se realizan los cambios si no se dejo el campo en nulo

                cliente.Dni = objeto.Dni is null ? cliente.Dni : objeto.Dni;
                cliente.Nombre = objeto.Nombre is null ? cliente.Nombre : objeto.Nombre;
                cliente.Apellido = objeto.Apellido is null ? cliente.Apellido : objeto.Apellido;
                cliente.Telefono = objeto.Telefono is null ? cliente.Telefono : objeto.Telefono;
                cliente.CorreoElectronico = objeto.CorreoElectronico is null ? cliente.CorreoElectronico : objeto.CorreoElectronico;


                //Se almacena y guardan los cambios de la tabla

                _dbcontext.Clientes.Update(cliente);
                _dbcontext.SaveChanges();

                //Enviamos un mensaje de que se modifico exitosamente

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        //API Anular Cliente

        [HttpDelete]

        //Ruta para llamar la API Anular

        [Route("Anular/{idCliente:int}")]

        public IActionResult Anular(int idCliente)
        {
            Cliente cliente = new Cliente();

            //realizo una busqueda del cliente

            cliente = _dbcontext.Clientes.Find(idCliente);

            if (cliente == null)
            {
                return BadRequest("Cliente no encontrado");
            }

            try
            {

                //Se almacena y guardan los cambios de la tabla

                _dbcontext.Clientes.Remove(cliente);
                _dbcontext.SaveChanges();

                //Enviamos un mensaje de que se modifico exitosamente

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }


    }
}
