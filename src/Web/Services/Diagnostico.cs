namespace BlazorLento.Web.Services;

/// <summary>
/// Lo que medimos en la charla: cuántas veces se renderiza cada componente y cuántas llamadas
/// se realizan a la API. Los muestra <c>PanelDiagnostico</c>.
///
/// Importante: el evento <see cref="Cambio"/> se dispara DURANTE los renders de otros componentes,
/// así que quien lo escuche no debe llamar a StateHasChanged directamente (ver PanelDiagnostico).
/// </summary>
public sealed class Diagnostico
{
    private readonly Dictionary<string, int> _renders = new();
    private readonly List<string> _ultimasLlamadas = new();
    private readonly object _lock = new();

    public int LlamadasApi { get; private set; }
    public int LlamadasCanceladas { get; private set; }
    public int EnVuelo { get; private set; }

    public event Action? Cambio;

    public int RendersTotales
    {
        get { lock (_lock) return _renders.Values.Sum(); }
    }

    public IReadOnlyList<KeyValuePair<string, int>> RendersPorComponente
    {
        get { lock (_lock) return _renders.OrderByDescending(kv => kv.Value).ToList(); }
    }

    public IReadOnlyList<string> UltimasLlamadas
    {
        get { lock (_lock) return _ultimasLlamadas.TakeLast(8).Reverse().ToList(); }
    }

    /// <summary>Llámalo desde el markup del componente (<c>@{ Diag.Render(nameof(X)); }</c>): se ejecuta en cada render.</summary>
    public void Render(string componente)
    {
        lock (_lock)
        {
            _renders[componente] = _renders.GetValueOrDefault(componente) + 1;
        }
        Cambio?.Invoke();
    }

    public void LlamadaIniciada(string ruta)
    {
        lock (_lock)
        {
            LlamadasApi++;
            EnVuelo++;
            _ultimasLlamadas.Add($"{DateTime.Now:HH:mm:ss.fff}  {ruta}");
        }
        Cambio?.Invoke();
    }

    public void LlamadaTerminada(bool cancelada)
    {
        lock (_lock)
        {
            EnVuelo--;
            if (cancelada) LlamadasCanceladas++;
        }
        Cambio?.Invoke();
    }

    public void Reiniciar()
    {
        lock (_lock)
        {
            _renders.Clear();
            _ultimasLlamadas.Clear();
            LlamadasApi = 0;
            LlamadasCanceladas = 0;
        }
        Cambio?.Invoke();
    }
}
