using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaDeVentasCafe.CodigoRepetido;
using SistemaDeVentasCafe.DTOs;
using SistemaDeVentasCafe.Models;
using SistemaDeVentasCafe.Service.IService;
using SistemaDeVentasCafe.UnitOfWork;
using System.Reflection.Metadata.Ecma335;

namespace SistemaDeVentasCafe.Service
{
    public class ServiceMedioDePago : IServiceMedioDePago
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly APIResponse _apiresponse;
        private readonly ILogger<ServiceMedioDePago> _logger;

        public ServiceMedioDePago(IMapper mapper, APIResponse apiresponse, ILogger<ServiceMedioDePago> logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _apiresponse = apiresponse;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Mediodepago> PagarConCredito([FromBody] MedioDePagoCreateDto tarjeta)
        {
            try
            {
                var cod = _mapper.Map<Mediodepago>(tarjeta);
                cod.Descripcion = "Pago Realizado con Tarjeta De Credito.";
                await _unitOfWork.repositoryMedioDePago.Crear(cod);
                await _unitOfWork.Save();
                return cod;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Mediodepago> PagarConDebito([FromBody] MedioDePagoCreateDto tarjeta)
        {
            try
            {
                var cod = _mapper.Map<Mediodepago>(tarjeta);
                cod.Descripcion = "Pago Realizado con Tarjeta De Debito.";
                await _unitOfWork.repositoryMedioDePago.Crear(cod);
                await _unitOfWork.Save();
                return cod;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Mediodepago> PagarConQR(int idCliente)
        {
            try
            {
                var cliente = await _unitOfWork.repositoryCliente.ObtenerPorId(idCliente);
                if (cliente == null)
                {
                    _logger.LogError("No existe cliente con ese id.");
                    return null;
                }
                Mediodepago cod = new();
                cod.Nombre = cliente.Nombre;
                cod.Apellido = cliente.Apellido;
                cod.Telefono = cliente.Telefono;
                cod.Descripcion = "Pago Realizado con Codigo QR.";
                await _unitOfWork.repositoryMedioDePago.Crear(cod);
                await _unitOfWork.Save();
                return cod;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
