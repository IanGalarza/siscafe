using Microsoft.AspNetCore.Mvc;
using SistemaDeVentasCafe.CodigoRepetido;
using SistemaDeVentasCafe.DTOs;
using SistemaDeVentasCafe.Models;
using SistemaDeVentasCafe.Service.IService;

namespace SistemaDeVentasCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly IServiceGeneric<ClienteUpdateDto, ClienteCreateDto> _service;
        public ClienteController(IServiceGeneric<ClienteUpdateDto, ClienteCreateDto> service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] //documentación
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Listar()
        {
            var result = await _service.Listar();
            return Utilidades.AyudaControlador(result);
        }

        [HttpGet("{idCliente}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<APIResponse>> Consultar(int idCliente)
        {
            var result = await _service.ObtenerPorId(idCliente);
            return Utilidades.AyudaControlador(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIResponse>> Registrar([FromBody] ClienteCreateDto cliente)
        {
            var result = await _service.Crear(cliente);
            return Utilidades.AyudaControlador(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Modificar([FromBody] ClienteUpdateDto cliente)
        {
            var result = await _service.Actualizar(cliente);
            return Utilidades.AyudaControlador(result);
        }

        [HttpDelete("{idCliente}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Anular(int idCliente)
        {
            var result = await _service.Eliminar(idCliente);
            return Utilidades.AyudaControlador(result);
        }
    }
}
