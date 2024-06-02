namespace SistemaDeVentasCafe.DTOs
{
    public record CobranzaUpdateDto(int IdCobranza,
                                    string Descripcion,
                                    decimal importe,
                                    DateOnly FechaDeCobro,
                                    int NumeroFactura,
                                    int MedioDePago);
 
}
