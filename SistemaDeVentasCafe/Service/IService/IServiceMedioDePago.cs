using Microsoft.AspNetCore.Mvc;
using SistemaDeVentasCafe.DTOs;
using SistemaDeVentasCafe.Models;

namespace SistemaDeVentasCafe.Service.IService
{
    public interface IServiceMedioDePago
    {
        public Task<Mediodepago> PagarConQR(int idCliente);
        public Task<Mediodepago> PagarConCredito([FromBody] MedioDePagoCreateDto tarjeta);
        public Task<Mediodepago> PagarConDebito([FromBody] MedioDePagoCreateDto trajeta);
    }
}
