using Microsoft.AspNetCore.Mvc;
using SistemaDeVentasCafe.Models;

namespace SistemaDeVentasCafe.Service.IService
{
    public interface IServiceGeneric<T, A> where T : class
    {
        public Task<APIResponse> Listar();
        public Task<APIResponse> ObtenerPorId(int id);
        public Task<APIResponse> Crear([FromBody] A objetoCreateDto);
        public Task<APIResponse> Actualizar([FromBody] T objetoUpdateDto);
        public Task<APIResponse> Eliminar(int id);
    }
}
