namespace BlazorLento.Web.Services;

// =====================================================================================
// Demo 5: gestión del estado. El mismo estado (carrito + filtro) con dos formas de avisar.
// =====================================================================================

/// <summary>
/// Lento 🐢: un único evento <see cref="OnChange"/> notifica cualquier cambio a todos los componentes
/// suscritos, incluida la tabla de 200 filas que no utiliza los datos del carrito.
/// </summary>
public sealed class EstadoLento
{
    public List<int> Carrito { get; } = new();
    public EstadoPedido? Filtro { get; private set; }

    public event Action? OnChange;

    public void AñadirAlCarrito(int pedidoId)
    {
        Carrito.Add(pedidoId);
        OnChange?.Invoke();
    }

    public void CambiarFiltro(EstadoPedido? filtro)
    {
        Filtro = filtro;
        OnChange?.Invoke();
    }
}

/// <summary>
/// Rápido ⚡: notificaciones separadas para el carrito y el filtro.
/// Cada componente se suscribe solo a los cambios de los datos que utiliza.
/// </summary>
public sealed class EstadoRapido
{
    private readonly List<int> _carrito = new();

    public IReadOnlyList<int> Carrito => _carrito;
    public EstadoPedido? Filtro { get; private set; }

    public event Action? CarritoCambiado;
    public event Action? FiltroCambiado;

    public void AñadirAlCarrito(int pedidoId)
    {
        _carrito.Add(pedidoId);
        CarritoCambiado?.Invoke();
    }

    public void CambiarFiltro(EstadoPedido? filtro)
    {
        if (filtro == Filtro) return;          // mismo valor: no hay nada que notificar
        Filtro = filtro;
        FiltroCambiado?.Invoke();
    }
}
