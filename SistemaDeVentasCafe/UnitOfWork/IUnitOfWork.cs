using SistemaDeVentasCafe.Models;
using SistemaDeVentasCafe.Repository.IRepository;

namespace SistemaDeVentasCafe.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IRepositoryGeneric<Cliente> repositoryCliente { get; } //inserto los depositorios con get para poder ingresar desde el service
        public IRepositoryGeneric<Producto> repositoryProducto { get; }
        Task Save();
    }
}
