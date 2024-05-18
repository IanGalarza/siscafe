using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using SistemaDeVentasCafe.Models;



namespace SistemaDeVentasCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        //Poder utilizar el contexto de la DB
        public readonly DbapiContext _dbcontext;

        public ProductoController(DbapiContext _context)
        {
            _dbcontext = _context;
        }

        //API Listar Productos

        [HttpGet]
       
        //Ruta para llamar a la API listar

        [Route("Listar")]

        public IActionResult Listar() {
            List<Producto> lista = new List<Producto>();

            //Capturador de errores
            try
            {
                lista = _dbcontext.Productos.ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });   //Retorna la lista de objetos con un mensaje HTTP ok
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });  
            }
        }

        //API Consultar Producto

        [HttpGet]

        //Ruta para llamar a la API consultar

        [Route("Consultar/{idProducto:int}")]

        public IActionResult Consultar(int idProducto)
        {
            Producto producto = new Producto();

            //realizo una busqueda del producto

            producto = _dbcontext.Productos.Find(idProducto);

            if (producto == null)
            {
                return BadRequest("Producto no encontrado");  
            }

            //Capturador de errores
            try
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = producto });   //Retorna el producto si es encontrado con un mensaje HTTP ok
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = producto });  
            }
        }

        //API Registrar Producto

        [HttpPost]

        //Ruta para llamar a la API Registrar

        [Route("Registrar")]

        public IActionResult Registrar([FromBody] Producto objeto)
        {
            try
            {
                _dbcontext.Productos.Add(objeto);
                _dbcontext.SaveChanges();                    //Se almacena y guardan los cambios de la tabla

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });  //Enviamos un mensaje de que se guardo exitosamente
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});
            }
        }

        //API Modificar Producto

        [HttpPut]

        //Ruta para llamar a la API Modificar

        [Route("Modificar")]

        public IActionResult Modificar([FromBody] Producto objeto)
        {

            Producto producto = new Producto();

            //realizo una busqueda del producto

            producto = _dbcontext.Productos.Find(objeto.IdProducto);

            if (producto == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                //Se realizan los cambios si no se dejo el campo en nulo

                producto.NumeroDeLote = objeto.NumeroDeLote is null ? producto.NumeroDeLote : objeto.NumeroDeLote; 
                producto.Descripcion = objeto.Descripcion is null ? producto.Descripcion : objeto.Descripcion;
                producto.PrecioVenta = objeto.PrecioVenta is null ? producto.PrecioVenta : objeto.PrecioVenta;
                producto.FechaVencimiento = objeto.FechaVencimiento is null ? producto.FechaVencimiento : objeto.FechaVencimiento;
                producto.StockActual = objeto.StockActual is null ? producto.StockActual : objeto.StockActual;


                //Se almacena y guardan los cambios de la tabla

                _dbcontext.Productos.Update(producto);
                _dbcontext.SaveChanges();

                //Enviamos un mensaje de que se modifico exitosamente

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });  
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        //API Anular Producto
       
        [HttpDelete]

        //Ruta para llamar la API Anular
      
        [Route("Anular/{idProducto:int}")]

        public IActionResult Anular(int idProducto)
        {
            Producto producto = new Producto();

            //realizo una busqueda del producto

            producto = _dbcontext.Productos.Find(idProducto);

            if (producto == null)
            {
                return BadRequest("Producto no encontrado");
            }
           
            try
            {
                //Se almacena y guardan los cambios de la tabla

                _dbcontext.Productos.Remove(producto);
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
