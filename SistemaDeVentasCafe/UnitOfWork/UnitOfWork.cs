﻿using SistemaDeVentasCafe.Models;
using SistemaDeVentasCafe.Repository.IRepository;

namespace SistemaDeVentasCafe.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbapiContext _context;
        public IRepositoryGeneric<Cliente> repositoryCliente { get; }
        public IRepositoryGeneric<Producto> repositoryProducto { get; }
        public IRepositoryGeneric<Factura> repositoryFactura { get; }
        public IRepositoryFacturaProducto repositoryFacturaProducto { get; }

        public UnitOfWork(DbapiContext context, IRepositoryGeneric<Cliente> _repositoryCliente, IRepositoryGeneric<Producto> _repositoryProducto, IRepositoryGeneric<Factura> _repositoryFactura,
               IRepositoryFacturaProducto repositoryFacturaProducto)
        {
            _context = context;
            repositoryCliente = _repositoryCliente;
            repositoryProducto = _repositoryProducto;
            repositoryFactura = _repositoryFactura;
            this.repositoryFacturaProducto = repositoryFacturaProducto;
        }
        public async Task Save() => await _context.SaveChangesAsync();
    }
}
