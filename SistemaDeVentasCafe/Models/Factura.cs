using System;
using System.Collections.Generic;

namespace SistemaDeVentasCafe.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public DateOnly? FechaFactura { get; set; }

    public int? CantidadProductos { get; set; }

    public string? Descripcion { get; set; }

    public decimal? PrecioTotal { get; set; }

    public int? IdCliente { get; set; }

    public bool? EstadoPago { get; set; }

    public virtual ICollection<Facturaproducto> Lista_De_Productos { get; set; } = new List<Facturaproducto>();

    public virtual Cliente? fCliente { get; set; }

    public override string ToString()
    {
        string factura = "Numero de Factura:" + this.IdFactura.ToString();
        factura += "\r\n" + "Fecha de Facturacion:" + this.FechaFactura.ToString();
        factura += "\r\n" + "Cantidad de productos totales: " + this.CantidadProductos.ToString();
        factura += "\r\n" + "Descripcion de la factura: " + this.Descripcion;
        factura += "\r\n" + "Precio Total: " + this.PrecioTotal.ToString();
        factura += "\r\n" + "ID del Cliente: " + this.PrecioTotal.ToString();
        string pago = "Sin Pagar";
        if (this.EstadoPago == true)
        {
            pago = "Pagado";
        }
        factura += "\r\n" + "Estado de pago: " + pago;

        factura += "\r\n" + "Lista de Productos: ";

        foreach (Facturaproducto f in Lista_De_Productos)
        {
            factura += "\r\n" + "Id Producto: " + f.IdProducto + ", Cantidad: " + f.CantidadDelProducto;
        }

        return factura;
    }
}
