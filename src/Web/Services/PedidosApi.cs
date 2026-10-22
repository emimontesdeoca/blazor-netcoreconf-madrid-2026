using System.Globalization;

namespace BlazorLento.Web.Services;

/// <summary>
/// La "API remota" de la demo. No hay HTTP: es una clase con latencia artificial y contador de llamadas,
/// para que la demo no dependa de levantar otro proceso y para que las llamadas se vean en pantalla.
///
/// Todo lo que hacen los componentes (cancelar, no repetir, cachear) sería idéntico con un HttpClient:
/// cambia <c>Task.Delay(latencia, ct)</c> por <c>http.GetFromJsonAsync(..., ct)</c>.
/// </summary>
public sealed class PedidosApi(DatosFalsos datos, Diagnostico diag)
{
    /// <summary>Latencia base de cada llamada.</summary>
    public TimeSpan Latencia { get; set; } = TimeSpan.FromMilliseconds(250);

    /// <summary>La búsqueda "mart" tarda más para reproducir respuestas desordenadas sin depender del azar.</summary>
    public TimeSpan RetrasoBusquedaParcial { get; set; } = TimeSpan.FromMilliseconds(800);

    public async Task<IReadOnlyList<Pedido>> GetPedidosAsync(int cantidad, CancellationToken ct = default)
    {
        await SimularLlamadaAsync($"GET /pedidos?top={cantidad}", ct);
        return datos.Pedidos.Take(cantidad).ToList();
    }

    public async Task<(IReadOnlyList<Pedido> Items, int Total)> GetPaginaAsync(int inicio, int cantidad, CancellationToken ct = default)
    {
        await SimularLlamadaAsync($"GET /pedidos?skip={inicio}&top={cantidad}", ct);
        return (datos.Pedidos.Skip(inicio).Take(cantidad).ToList(), datos.Pedidos.Count);
    }

    public async Task<Pedido?> GetPedidoAsync(int id, CancellationToken ct = default)
    {
        await SimularLlamadaAsync($"GET /pedidos/{id}", ct);
        return datos.Pedidos.FirstOrDefault(p => p.Id == id);
    }

    public async Task<IReadOnlyList<Producto>> GetProductosAsync(CancellationToken ct = default)
    {
        await SimularLlamadaAsync("GET /productos", ct);
        return datos.Productos;
    }

    /// <summary>Búsqueda por cliente o producto, sin distinguir mayúsculas ni tildes ("martinez" encuentra "Martínez").</summary>
    public async Task<IReadOnlyList<Pedido>> BuscarAsync(string texto, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return [];

        var retraso = string.Equals(texto, "mart", StringComparison.OrdinalIgnoreCase)
            ? RetrasoBusquedaParcial
            : TimeSpan.Zero;
        await SimularLlamadaAsync($"GET /pedidos?q={texto}", ct, retraso);

        var comparador = CultureInfo.InvariantCulture.CompareInfo;
        const CompareOptions opciones = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace;

        return datos.Pedidos
            .Where(p => comparador.IndexOf(p.Cliente, texto, opciones) >= 0
                     || comparador.IndexOf(p.Producto, texto, opciones) >= 0)
            .Take(50)
            .ToList();
    }

    private async Task SimularLlamadaAsync(string ruta, CancellationToken ct, TimeSpan retrasoAdicional = default)
    {
        diag.LlamadaIniciada(ruta);
        var cancelada = false;
        try
        {
            await Task.Delay(Latencia + retrasoAdicional, ct);
        }
        catch (OperationCanceledException)
        {
            cancelada = true;
            throw;
        }
        finally
        {
            diag.LlamadaTerminada(cancelada);
        }
    }
}
