using Microsoft.AspNetCore.Mvc;
using SistemaDeVentasCafe.CodigoRepetido;
using SistemaDeVentasCafe.DTOs;
using SistemaDeVentasCafe.Models;
using SistemaDeVentasCafe.Service.IService;
using SistemaDeVentasCafe.Service;

namespace SistemaDeVentasCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : Controller
    {
        private readonly IServiceGeneric<FacturaUpdateDto, FacturaCreateDto> _service;

        public FacturaController(IServiceGeneric<FacturaUpdateDto, FacturaCreateDto> service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Listar")]
        [ProducesResponseType(StatusCodes.Status200OK)] //documentación
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Listar()
        {
            var result = await _service.Listar();
            return Utilidades.AyudaControlador(result);
        }

        [HttpGet]
        [Route("Consultar/{idProducto:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<APIResponse>> Consultar(int idProducto)
        {
            var result = await _service.ObtenerPorId(idProducto);
            return Utilidades.AyudaControlador(result);
        }

        [HttpPost]
        [Route("Registrar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> Registrar([FromBody] FacturaCreateDto Factura)
        {
            var result = await _service.Crear(Factura);
            return Utilidades.AyudaControlador(result);
        }

        [HttpGet]
        [Route("Imprimir/{idProducto:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<APIResponse>> Imprimir(int idProducto)
        {
            var result = await ((ServiceFactura)_service).Imprimir(idProducto);
            return Utilidades.AyudaControlador(result);
        }
    }
}
