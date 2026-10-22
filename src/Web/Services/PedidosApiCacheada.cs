namespace BlazorLento.Web.Services;

/// <summary>
/// Demo 4b: varios componentes necesitan el mismo dato a la vez. En vez de que cada uno llame a la API,
/// comparten la MISMA tarea: la primera llamada la crea y las demás esperan a esa ("single-flight").
///
/// Scoped: una caché por circuito. En una app real pondrías una expiración (IMemoryCache, HybridCache)
/// y la invalidarías cuando el dato cambie.
/// </summary>
public sealed class PedidosApiCacheada(PedidosApi api)
{
    private Task<IReadOnlyList<Producto>>? _productos;
    private readonly Dictionary<int, Task<Pedido?>> _pedidos = new();

    public Task<IReadOnlyList<Producto>> GetProductosAsync(CancellationToken ct = default)
    {
        // Si la tarea falla, no la dejamos cacheada: el siguiente lo vuelve a intentar.
        if (_productos is null || _productos.IsFaulted || _productos.IsCanceled)
            _productos = api.GetProductosAsync();

        // El token cancela MI espera, no la petición compartida: otro componente puede seguir necesitándola.
        return _productos.WaitAsync(ct);
    }

    public Task<Pedido?> GetPedidoAsync(int id, CancellationToken ct = default)
    {
        if (!_pedidos.TryGetValue(id, out var tarea) || tarea.IsFaulted || tarea.IsCanceled)
            _pedidos[id] = tarea = api.GetPedidoAsync(id);

        return tarea.WaitAsync(ct);
    }

    public void Invalidar()
    {
        _productos = null;
        _pedidos.Clear();
    }
}
