using BlazorLento.Web.Services;

namespace BlazorLento.Web.Components.Demos.Llamadas;

/// <summary>Qué calcula cada widget de la demo 4b a partir de la MISMA lista de productos.</summary>
public enum Metrica
{
    Productos,
    SinStock,
    MasCaro,
    ValorInventario,
}

public static class MetricaExtensiones
{
    public static string Titulo(this Metrica m) => m switch
    {
        Metrica.Productos => "Productos en catálogo",
        Metrica.SinStock => "Sin stock",
        Metrica.MasCaro => "Producto más caro",
        Metrica.ValorInventario => "Valor del inventario",
        _ => m.ToString(),
    };

    public static string Calcular(this Metrica m, IReadOnlyList<Producto> productos)
    {
        var es = System.Globalization.CultureInfo.GetCultureInfo("es-ES");
        return m switch
        {
            Metrica.Productos => productos.Count.ToString("N0", es),
            Metrica.SinStock => productos.Count(p => p.Stock == 0).ToString("N0", es),
            Metrica.MasCaro => productos.MaxBy(p => p.Precio)?.Nombre ?? "—",
            Metrica.ValorInventario => productos.Sum(p => p.Precio * p.Stock).ToString("C0", es),
            _ => "—",
        };
    }
}
