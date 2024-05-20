using SistemaDeVentasCafe.Models;
using SistemaDeVentasCafe.Repository.IRepository;

namespace SistemaDeVentasCafe.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbapiContext _context;
        public IRepositoryGeneric<Cliente> repositoryCliente { get; }
        public IRepositoryGeneric<Producto> repositoryProducto { get; }

        public UnitOfWork(DbapiContext context, IRepositoryGeneric<Cliente> _repositoryCliente, IRepositoryGeneric<Producto> _repositoryProducto)
        {
            _context = context;
            repositoryCliente = _repositoryCliente;
            repositoryProducto = _repositoryProducto;
        }
        public async Task Save() => await _context.SaveChangesAsync();
    }
}
