// =====================================================================================
// BlazorLento — la app deliberadamente lenta de la charla
// "Los errores que convierten tu aplicación Blazor en una aplicación lenta"
//
// Blazor Web App con render interactivo en el servidor (InteractiveServer) para toda la app.
// Cada página muestra UN error con dos implementaciones, Lento 🐢 y Rápido ⚡, y un panel
// de diagnóstico con renders y llamadas a la API para medir el antes y el después.
//
// Las técnicas valen igual en WebAssembly; aquí usamos Server porque arranca en un segundo
// Los renders se calculan en el servidor; solo las actualizaciones necesarias del DOM viajan al navegador.
// =====================================================================================

using BlazorLento.Web.Components;
using BlazorLento.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Datos de ejemplo: 5.000 pedidos deterministas (misma semilla, mismos datos en cada arranque).
builder.Services.AddSingleton<DatosFalsos>();

// "API remota" simulada: latencia artificial + contador de llamadas. Scoped = una por circuito (por pestaña).
builder.Services.AddScoped<PedidosApi>();
builder.Services.AddScoped<PedidosApiCacheada>();

// Contadores de renderizados y llamadas que muestra PanelDiagnostico.
builder.Services.AddScoped<Diagnostico>();

// Estado de la demo 5: el mismo estado, dos formas de notificar cambios.
builder.Services.AddScoped<EstadoLento>();
builder.Services.AddScoped<EstadoRapido>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
