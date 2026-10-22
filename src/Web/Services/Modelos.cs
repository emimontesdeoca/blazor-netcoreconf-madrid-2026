namespace BlazorLento.Web.Services;

public enum EstadoPedido { Pendiente, Pagado, Enviado, Entregado, Cancelado }

/// <summary>
/// Un pedido. Es una clase mutable a propósito: es lo que suele haber en una app real
/// y es justo lo que hace que Blazor no pueda saber si un parámetro "ha cambiado".
/// </summary>
public sealed class Pedido
{
    public int Id { get; init; }
    public string Cliente { get; init; } = "";
    public string Producto { get; init; } = "";
    public int Cantidad { get; init; }
    public decimal Importe { get; init; }
    public DateTime Fecha { get; init; }
    public EstadoPedido Estado { get; set; }

    /// <summary>Sube cada vez que el pedido cambia. Es lo que compara ShouldRender en la demo 1.</summary>
    public int Version { get; set; }
}

public sealed record Producto(int Id, string Nombre, decimal Precio, int Stock);
