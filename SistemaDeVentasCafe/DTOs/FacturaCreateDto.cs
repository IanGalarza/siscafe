using SistemaDeVentasCafe.Models;

namespace SistemaDeVentasCafe.DTOs
{
    public record FacturaCreateDto(DateOnly FechaFactura,
                                   int IdCliente,
                                   bool EstadoPago,
                                   ICollection<Facturaproducto> Lista_De_Productos);
}
