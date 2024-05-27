using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaDeVentasCafe.CodigoRepetido;
using SistemaDeVentasCafe.DTOs;
using SistemaDeVentasCafe.Models;
using SistemaDeVentasCafe.Service.IService;
using SistemaDeVentasCafe.UnitOfWork;
using System.Net;
using System.Drawing.Printing;

namespace SistemaDeVentasCafe.Service
{
    public class ServiceFactura: IServiceGeneric<FacturaUpdateDto, FacturaCreateDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly APIResponse _apiresponse;
        private readonly ILogger<ServiceFactura> _logger;

        //De manera de texteo agrego una dbapicontext, corregir despues.

        public readonly DbapiContext _dbapi;
        public ServiceFactura(IMapper mapper, APIResponse apiresponse, ILogger<ServiceFactura> logger, IUnitOfWork unitOfWork, DbapiContext dbapi)
        {
            _mapper = mapper;
            _apiresponse = apiresponse;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _dbapi = dbapi;
        }

        public async Task<APIResponse> Listar()
        {
            try
            {
                List<Factura> lista = await _unitOfWork.repositoryFactura.ListarTodos();
                if (lista.Count == 0)
                {
                    _logger.LogError("La lista de facturas esta vacia.");
                    return Utilidades.NotFoundResponse(_apiresponse);
                }
                _logger.LogInformation("Lista de facturas traida con exito.");
                return Utilidades.ListOKResponse(lista, _apiresponse);
            }
            catch (Exception ex)
            {
                return Utilidades.ErrorHandling(ex, _apiresponse, _logger);
            }
        }

        public async Task<APIResponse> ObtenerPorId(int id)
        {
            try
            {
                var Factura = await _unitOfWork.repositoryFactura.ObtenerPorId(id);
                if (Factura == null)
                {
                    _logger.LogError("No existe factura con ese id.");
                    return Utilidades.NotFoundResponse(_apiresponse);
                }
                _logger.LogInformation("Factura traida con exito.");
                return Utilidades.OKResponse(Factura, _apiresponse);
            }
            catch (Exception ex)
            {
                return Utilidades.ErrorHandling(ex, _apiresponse, _logger);
            }
        }

        public async Task<APIResponse> Crear([FromBody] FacturaCreateDto facturaCreateDto)
        {
            try
            {
                var Factura = _mapper.Map<Factura>(facturaCreateDto);

                String listaprod = "";

                foreach (Facturaproducto prod in Factura.Lista_De_Productos)
                {

                    //Arreglar - Precio total y cantidadProductos muestra como null, ademas de que si esta este foreach no permite ingresar mas de 1 producto.

                    Producto producto = _dbapi.Productos.Find(prod.IdProducto); // Obtengo el producto y realizo los calculos del precio total + descripcion
                    Factura.PrecioTotal += producto.PrecioVenta; //Pongo el precio total
                    listaprod += producto.Descripcion + ", cantidad: " + prod.CantidadDelProducto;
                    listaprod += "\r\n";
                    Factura.CantidadProductos += prod.CantidadDelProducto;
                }

                Factura.Descripcion = listaprod;

                await _unitOfWork.repositoryFactura.Crear(Factura);
                await _unitOfWork.Save();
                                                    
                _logger.LogInformation("Factura creada con exito.");
                return Utilidades.CreatedResponse(_apiresponse);
            }
            catch (Exception ex)
            {
                return Utilidades.ErrorHandling(ex, _apiresponse, _logger);
            }
        }
        public async Task<APIResponse> Imprimir(int id)
        {
                try
                {
                    var Factura = await _unitOfWork.repositoryFactura.ObtenerPorId(id);
                    if (Factura == null)
                    {
                        _logger.LogError("No existe factura con ese id.");
                        return Utilidades.NotFoundResponse(_apiresponse);
                    }
                    _logger.LogInformation("Factura traida con exito.");

                    List<Facturaproducto> listaProductos = new List<Facturaproducto>();
                    
                    listaProductos = _dbapi.Facturaproductos.ToList();

                    string logpath = @"C:\Users\" + Environment.UserName + @"\Downloads\factura" + Factura.IdFactura.ToString() + ".txt";
                    if (!System.IO.File.Exists(logpath)){
                        FileStream fs = System.IO.File.Create(logpath);
                        fs.Close();
                       }    
                    System.IO.File.AppendAllText(logpath, Factura.ToString());
                    return Utilidades.OKResponse(Factura, _apiresponse);          
                    
                }
                catch (Exception ex)
                {
                    return Utilidades.ErrorHandling(ex, _apiresponse, _logger);
                }
         }

        public async Task<APIResponse> Actualizar([FromBody] FacturaUpdateDto facturaUpdateDto)         //FACTURA NO IMPLEMENTA
        {
            return null;
        }

        public async Task<APIResponse> Eliminar(int id)                                      //FACTURA NO IMPLEMENTA
        {
            return null;
        }
    }
}
