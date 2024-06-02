namespace SistemaDeVentasCafe.DTOs
{
    public record FacturaUpdateDto(int IdFactura,
                                   DateOnly FechaFactura,
                                   int CantidadProductos,
                                   string Descripcion,
                                   decimal PrecioTotal,
                                   int IdCliente,
                                   bool EstadoPago);
}

