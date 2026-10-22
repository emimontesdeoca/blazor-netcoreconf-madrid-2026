---
marp: true
theme: default
paginate: true
size: 16:9
lang: es
title: "Los errores que convierten tu aplicación Blazor en una aplicación lenta"
description: "Cinco errores habituales de rendimiento en Blazor y cómo corregirlos. NetCoreConf Madrid 2026."
header: "**&lt;NetCoreConf/&gt;**  \nMadrid ’26"
footer: "Emiliano Montesdeoca · Rendimiento en Blazor"
style: |
  section {
    font-family: "Segoe UI", "Inter", system-ui, sans-serif;
    font-size: 30px;
    line-height: 1.4;
    padding: 116px 72px 76px;
    background: #ffffff url('assets/netcoreconf-claro.png') center / cover no-repeat;
    color: #242331;
  }
  section h1 { font-size: 1.8em; line-height: 1.14; color: #72278b; letter-spacing: -0.025em; }
  section h2 { font-size: 1.42em; line-height: 1.2; color: #72278b; margin-bottom: 0.6em; }
  section strong, section a { color: #72278b; }
  section li { margin: 0.4em 0; }
  section code { font-size: 0.82em; background: #f3edf7; padding: 0.1em 0.25em; border-radius: 4px; }
  section pre { font-size: 0.75em; line-height: 1.4; background: #f7f4fa; border-left: 4px solid #72278b; padding: 0.65em 0.85em; }
  section pre code { font-size: 1em; background: transparent; padding: 0; }
  section header { top: 57px; right: 116px; left: auto; width: 190px; text-align: right; font-size: 13px; line-height: 1.45; color: #72278b; }
  section header strong { font-size: 16px; }
  section footer { left: 72px; bottom: 18px; font-size: 14px; color: #6d6676; }
  section::after { right: 72px; bottom: 18px; font-size: 14px; color: #6d6676; }
  section.lead { background-image: url('assets/netcoreconf-fondo.png'); color: #fff; }
  section.lead h1, section.lead h2, section.lead strong, section.lead a { color: #fff; }
  section.lead p { color: #eee4f6; }
  section.lead header, section.lead header strong { color: #fff; }
  section.lead footer, section.lead::after { color: #e1d4ed; }
  section.sponsor {
    position: relative;
    overflow: hidden;
    padding: 0;
    background: #160b4b;
  }
  section.sponsor p { margin: 0; }
  section.sponsor img {
    position: absolute;
    inset: 0;
    width: 100%;
    height: 100%;
    object-fit: cover;
  }
  section.sponsor header, section.sponsor footer, section.sponsor::after { display: none; }
  section.demo h2::before { content: "DEMO / "; font-size: 0.55em; letter-spacing: 0.08em; }
  section.transform-before h2::before {
    content: "ANTES / ";
    color: #b83e0b;
    font-size: 0.55em;
    letter-spacing: 0.08em;
  }
  section.transform-after h2::before {
    content: "DESPUÉS / ";
    color: #086b35;
    font-size: 0.55em;
    letter-spacing: 0.08em;
  }
  section.transform-before pre, section.transform-after pre {
    font-size: 0.62em;
    line-height: 1.25;
  }
  section.transform-after.detalle pre { font-size: 0.5em; line-height: 1.12; }
  section.transform-before pre { background: #fff5f0; border-left-color: #b83e0b; }
  section.transform-after pre { background: #eff8f1; border-left-color: #086b35; }
  section.prompt h2::before {
    content: "PARA TU IA / ";
    color: #72278b;
    font-size: 0.55em;
    letter-spacing: 0.08em;
  }
  section.prompt blockquote {
    background: #f7f4fa;
    padding: 0.75em 1em;
    font-size: 0.86em;
    line-height: 1.35;
  }
  section.tip {
    display: grid;
    grid-template-columns: minmax(0, 0.9fr) minmax(0, 1.1fr);
    grid-template-rows: auto auto 1fr;
    align-content: center;
    column-gap: 1.3em;
    row-gap: 0.4em;
  }
  section.tip h2 {
    grid-column: 1 / -1;
    grid-row: 1;
    font-size: 1.25em;
    margin-bottom: 0.2em;
  }
  section.tip h2::before {
    content: "ONE MORE TIP / ";
    color: #72278b;
    font-size: 0.55em;
    letter-spacing: 0.08em;
  }
  section.tip p { grid-column: 1; grid-row: 2; margin: 0.2em 0; font-size: 0.9em; }
  section.tip ul { grid-column: 1; grid-row: 3; margin: 0.2em 0; padding-left: 1.1em; }
  section.tip li { font-size: 0.84em; margin: 0.3em 0; }
  section.tip pre {
    grid-column: 2;
    grid-row: 2 / span 2;
    align-self: center;
    margin: 0;
    font-size: 0.65em;
    line-height: 1.22;
    background: #f7f4fa;
  }
  section.bio .bio-layout {
    display: grid;
    grid-template-columns: minmax(0, 1.15fr) minmax(0, 0.85fr);
    align-items: center;
    gap: 1.2em;
  }
  section.bio h2 { margin-top: 0.45em; margin-bottom: 0.25em; }
  section.bio .bio-copy { align-self: start; font-size: 0.82em; line-height: 1.3; }
  section.bio .bio-copy ul { margin: 0.5em 0 0.8em; padding-left: 1.1em; }
  section.bio .bio-copy li { margin: 0.45em 0; line-height: 1.28; }
  section.bio .bio-copy strong { color: #72278b; }
  section.bio .bio-badges {
    display: flex;
    align-items: center;
    gap: 0.6em;
    margin-top: 0.6em;
  }
  section.bio .bio-badges img {
    display: block;
    width: auto;
    height: 96px;
    object-fit: contain;
  }
  section.bio .bio-badges img.mvp {
    height: 50px;
  }
  section.bio .bio-photo {
    display: block;
    width: 100%;
    height: 420px;
    border-radius: 10px;
    object-fit: cover;
    object-position: center 32%;
  }
  section table { font-size: 0.8em; width: 100%; }
  section th { background: #72278b; color: #fff; }
  section blockquote { border-left: 5px solid #72278b; padding-left: 0.7em; color: #51465c; }
  .metric { font-size: 1.7em; font-weight: 700; line-height: 1.2; color: #72278b; }
  .lento { color: #b83e0b; font-weight: 600; }
  .rapido { color: #086b35; font-weight: 600; }
---

<!-- _class: lead -->
<!-- _paginate: false -->

# Los errores que convierten tu aplicación Blazor en una aplicación lenta

## Y cómo arreglarlos midiendo cada cambio

**Emiliano Montesdeoca del Puerto**
NetCoreConf Madrid 2026 · 23 de octubre

<!--
TIEMPO: 0:00 – 0:15

32 diapositivas: 30 de contenido y las dos de patrocinadores.
Objetivo: 35 minutos de contenido, 5 de preguntas y 5 de margen para las
transiciones y los imprevistos de una sesión de 45 minutos.
Confirmar la duración con la organización antes del último ensayo.

Vamos a ver cinco errores en una aplicación preparada para la charla.
Compararemos el comportamiento antes y después de corregir cada uno.
La demo usa .NET 10; no es una charla sobre las novedades de .NET 11.
Las diapositivas de patrocinadores reutilizan el material del PPTX de NetCoreConf.
-->

---

<!-- _class: sponsor -->
<!-- _paginate: false -->

![Patrocinadores de NetCoreConf Madrid 2026](assets/netcoreconf-sponsors.png)

<!--
TIEMPO: 0:15 – 0:20

Agradecer a los patrocinadores antes de empezar la charla.
-->

---

<!-- _class: bio -->

## Hola, soy Emi

<div class="bio-layout">
  <div class="bio-copy">
    <ul>
      <li>No he estado nunca en la cárcel</li>
      <li>Papá y felizmente casado</li>
      <li>Cloud Solutions Team Lead · Intelequia</li>
      <li>Microsoft MVP en Developer Technologies desde 2022</li>
      <li>Blogger / intento de influencer</li>
    </ul>
    <div class="bio-badges">
      <img src="assets/most-active-speaker-2025.svg" alt="Most Active Speaker 2025">
      <img src="assets/most-active-speaker-2024.svg" alt="Most Active Speaker 2024">
      <img src="assets/most-active-speaker-2023.svg" alt="Most Active Speaker 2023">
      <img class="mvp" src="assets/microsoft-mvp.png" alt="Microsoft MVP">
    </div>
  </div>
  <img class="bio-photo" src="assets/profile.jpeg" alt="Emi con su familia">
</div>

<!--
TIEMPO: 0:20 – 0:45

Presentación breve: trayectoria, áreas de trabajo y comunidad.
-->

---

## ¿Qué es Blazor?

Un framework de **ASP.NET Core** para crear interfaces web
con **componentes Razor: HTML y C#**.

```razor
<button @onclick="Incrementar">Clics: @clics</button>

@code {
    private int clics;
    private void Incrementar() => clics++;
}
```

**Cada componente tiene su interfaz, su estado y sus eventos.**

<!--
TIEMPO: 0:45 – 2:00

Un componente puede ser un botón, una ficha de producto o una tabla.
Podemos combinar varios componentes para construir una página.
En el ejemplo, al pulsar el botón se ejecuta un método de C# y cambia el contador.
Para que responda al clic necesita un modo interactivo, como el de nuestra demo.

El navegador sigue mostrando HTML y CSS. Blazor utiliza JavaScript
internamente y también permite llamar a bibliotecas de JavaScript.
Con esta idea de componente basta para seguir la charla.
Referencia: https://learn.microsoft.com/aspnet/core/blazor/
-->

---

## ¿Dónde se ejecuta una aplicación Blazor?

- **Static SSR:** el servidor genera HTML por petición.
- **Interactive Server:** C# y el renderizado, en el servidor.
  Los eventos y las actualizaciones del DOM viajan por SignalR.
- **Interactive WebAssembly:** C# y el renderizado, en el navegador.

**Un componente puede renderizarse sin que cambie el DOM.**

<!--
TIEMPO: 2:00 – 3:00

Al renderizar, Blazor ejecuta el marcado del componente, compara el resultado
con el anterior y aplica al DOM solo los cambios necesarios.
El DOM es la estructura de la página que mantiene el navegador.
Renderizar no significa generar de nuevo todo el HTML de la pantalla.

La demo usa Interactive Server en toda la aplicación. El prerender está
desactivado para medir una sola ejecución interactiva.
Las técnicas también sirven para WebAssembly, pero el coste del renderizado
se produce en el navegador, no en el servidor.
Static SSR también puede repetir consultas o generar demasiado HTML,
pero no tiene estos eventos interactivos.

Si preguntan por Auto: puede usar Server al principio y WebAssembly en
visitas posteriores. No cambia el modo de un componente que ya está abierto.
-->

---

## La aplicación que vamos a usar

**BlazorLento:** una aplicación de pedidos con un buscador y un carrito.

1. Las filas **se renderizan aunque sus datos no cambien**.
2. El buscador llama a la API **con cada tecla**.
3. La tabla carga **miles de filas que no se ven**.
4. Varios componentes **repiten la misma consulta**.
5. Añadir al carrito **hace que la tabla se vuelva a renderizar**.

<!--
TIEMPO: 3:00 – 4:00

Enseñar el menú, el selector Lento/Rápido y el panel de diagnóstico.
Explicar que cada página muestra un problema y dos implementaciones.
Los datos son ficticios y la API está simulada dentro del servidor.
Las llamadas que cuenta el panel no son peticiones HTTP del navegador.

El panel cuenta renderizados y llamadas. No mide imágenes por segundo.
En cada demo haremos la misma acción en ambos modos y compararemos los números.
Para la versión de 30 minutos, seguir los tiempos de PREPARACION.md.
-->

---

<!-- _class: transform-before -->

## 1. Las filas se renderizan sin haber cambiado

```razor
@code {
    [Parameter, EditorRequired] public Pedido Pedido { get; set; } = default!;
    private int _renders;
}
```

El padre se renderiza cada segundo y pasa el mismo `Pedido`, una **clase mutable**.
Esta fila no evita renderizarse aunque los datos que muestra sigan igual.

**Cada segundo se renderizan las 200 filas, aunque sus datos sigan igual.**

<!--
TIEMPO: 4:00 – 5:00

La página tiene un reloj que llama a StateHasChanged cada segundo.
Al renderizarse el padre, Blazor vuelve a pasar el objeto Pedido a cada fila.
Pasar objetos no es un error por sí mismo. Aquí el problema es repetir
el renderizado de 200 componentes cuyos datos no han cambiado.

Si todos los parámetros son tipos inmutables conocidos y siguen teniendo
los mismos valores, Blazor puede omitir su actualización.
Un record definido por nosotros no se considera automáticamente uno de esos tipos.

Los eventos y los cambios de estado también pueden solicitar renderizados.
Un manejador asíncrono que queda pendiente suele solicitar uno al devolver
el control y otro al terminar. Eso no garantiza exactamente dos renderizados.
Referencia: https://learn.microsoft.com/aspnet/core/blazor/components/rendering
-->

---

<!-- _class: transform-after -->

## La fila se actualiza solo si cambia el pedido

<p class="metric">200 renders/s → 0 · pedido modificado → 1</p>

```csharp
private int _idRenderizado = -1;
private int _versionRenderizada = -1;

protected override bool ShouldRender()
    => Pedido.Id != _idRenderizado || Pedido.Version != _versionRenderizada;

protected override void OnAfterRender(bool firstRender)
{
    _idRenderizado = Pedido.Id;
    _versionRenderizada = Pedido.Version;
}
```

<!--
TIEMPO: 5:00 – 8:00

Mostrar el código ANTES y avanzar para transformar el bloque en DESPUÉS.
Ruta: /renderizados. Aumentar el zoom para que se lean el panel y los contadores.
Validación del 21/10: 400 renderizados en dos actualizaciones del reloj;
0 en Rápido después de reiniciar. Al marcar el pedido, el contador de la
primera fila pasa de 1 a 2 y los de las otras 199 siguen en 1.

Al cambiar de implementación, las 200 filas tienen que renderizarse una vez.
Esperar a que aparezcan antes de reiniciar el panel.
El panel cuenta los componentes que hemos instrumentado, no todos los de la app,
y se actualiza como máximo cuatro veces por segundo.
Si falla la demo, explicar el resultado esperado con este código.
-->

---

## Qué garantiza esta transformación

- Guarda `Id` y `Version` tras el primer renderizado; por eso la carga inicial se conserva.
- Incrementa `Version` cuando cambia un dato visible de la fila.
- También puedes pasar **parámetros primitivos** que Blazor compare.

**No usar `ShouldRender => false` como receta general.**

<!--
TIEMPO: 8:00 – 8:40

Esto no impide que el padre pase parámetros ni evita todo el ciclo de vida.
Evita reconstruir el árbol de renderizado de una fila sin cambios.
Si no puedes garantizar que Version cubra todos los campos visibles,
compara esos campos o pasa parámetros primitivos.
-->

---

<!-- _class: prompt -->

## Copia este prompt

> Revisa la fila de esta app Blazor: recibe un `Pedido` mutable y su padre se actualiza periódicamente. Evita renderizar las filas cuyos datos visibles no cambian, pero conserva la actualización cuando sí cambian. No uses `ShouldRender => false` como atajo. Inspecciona cómo se modifican los pedidos, implementa la opción más sencilla y verifica ambos casos.

<!--
TIEMPO: 8:40 – 9:00

Leer el prompt como siguiente paso que la audiencia puede aplicar a su propio repo.
-->

---

<!-- _class: transform-before -->

## 2. Una llamada a la API por cada tecla

```csharp
private async Task Buscar(ChangeEventArgs e)
{
    _texto = e.Value?.ToString() ?? "";
    var texto = _texto;
    _resultados = await Api.BuscarAsync(texto);
    _textoResultados = texto;
}
```

`@oninput` + **martinez** = **8 llamadas**.

Una búsqueda anterior puede terminar después y sustituir los resultados actuales.

<!--
TIEMPO: 9:00 – 10:00

Hay dos problemas: se hacen llamadas que no necesitamos y pueden mostrarse
resultados que ya no corresponden al texto del buscador.
La API no consulta si el texto está vacío. Por eso, vaciar el campo al iniciar
la simulación no añade una novena llamada.

Cada búsqueda tarda 250 ms, salvo "mart", que añade 800 ms de retraso.
Así podemos reproducir una respuesta desactualizada sin depender del azar.
La latencia es artificial; no estamos midiendo una API externa.
-->

---

<!-- _class: transform-after -->

## La búsqueda espera y cancela la anterior

<p class="metric">8 llamadas → 1 · resultados actuales</p>

```csharp
var texto = _texto;
_cts?.Cancel();
_cts?.Dispose();
_cts = new CancellationTokenSource();
var ct = _cts.Token;

try
{
    await Task.Delay(DebounceMs, ct);
    var resultados = await Api.BuscarAsync(texto, ct);
    ct.ThrowIfCancellationRequested();
    _resultados = resultados;
    _textoResultados = texto;
}
catch (OperationCanceledException) when (ct.IsCancellationRequested) { }
```

<!--
TIEMPO: 10:00 – 14:00

Mostrar el código ANTES y avanzar para transformar el bloque en DESPUÉS.
Ruta: /eventos. Pulsar el botón «Escribir martinez» en ambos modos.
Antes de comparar, esperar a que el contador de llamadas en curso llegue a 0.
Cambiar a Rápido, reiniciar y repetir.
Validación del 21/10: 8 llamadas y resultados de "mart" en Lento;
1 llamada y resultados de "martinez" en Rápido.
El botón simula una tecla cada 90 ms.

Para mostrar la cancelación, escribir "mart", esperar a que empiece la llamada
y escribir "martinez". El panel cuenta cancelaciones de llamadas iniciadas,
no las esperas de Task.Delay que se cancelan antes de consultar.

Mantener visible la pestaña durante la demo. El navegador puede ralentizar
los temporizadores de las pestañas que están en segundo plano.
-->

---

## Debounce y cancelación

- **Debounce:** espera 300 ms desde la última tecla antes de buscar.
- **Cancelación:** detiene la espera o la llamada anterior cuando cambia el texto.
- **Protección:** no muestra una respuesta si ya hay una búsqueda más reciente.
- Al salir de la página, `Dispose` cancela la operación pendiente.

<!--
TIEMPO: 14:00 – 14:45

La cancelación es cooperativa: hay que pasar el token a HttpClient o EF Core
cuando lo admitan. Comprobarlo antes de asignar el resultado evita mostrar
una respuesta desactualizada si la operación no se canceló a tiempo.
Si la API no acepta un token, se puede descartar la respuesta con un identificador
de solicitud. Eso evita mostrarla, pero no reduce el trabajo de la API.
-->

---

<!-- _class: prompt -->

## Copia este prompt

> Analiza el buscador de esta app Blazor, que llama a la API en cada `oninput`. Añade debounce y cancelación de la búsqueda anterior; pasa el token a la API y evita que una respuesta antigua reemplace los resultados actuales. Cancela y libera el token al salir del componente, conserva la experiencia existente y verifica la escritura rápida, la pausa y la cancelación. No ocultes errores distintos de una cancelación esperada.

<!--
TIEMPO: 14:45 – 15:00

Insistir en que la IA revise la firma real de la API y el ciclo de vida del componente.
-->

---

<!-- _class: transform-before -->

## 3. Cargar 5.000 filas para mostrar unas pocas

```razor
@foreach (var p in _pedidos)
{
    <tr data-pedido="@p.Id">
        <td>@p.Id</td>
        <td>@p.Cliente</td>
        <td>@p.Producto</td>
        <td class="num">@p.Cantidad</td>
        <td class="num">@p.Importe</td>
        <td>@p.Estado</td>
    </tr>
}
```

**5.000 filas × 6 celdas = 30.000 celdas.**

El contenedor solo muestra unas **15 filas** a la vez.

<!--
TIEMPO: 15:00 – 16:00

Las 30.000 celdas no son todos los nodos del DOM: también hay filas y otros elementos.
El foreach genera todas las filas y vuelve a recorrerlas en cada renderizado.
En Interactive Server, los cambios necesarios del DOM viajan por SignalR.
Esto no significa que el primer renderizado siempre llegue por SignalR:
el prerender puede haber enviado el HTML en la respuesta HTTP inicial.
-->

---

<!-- _class: transform-after -->

## La transformación: virtualizar la tabla

```razor
<Virtualize TItem="Pedido" Items="_pedidos" ItemSize="38"
            OverscanCount="5" SpacerElement="tr" Context="p">
    <tr data-pedido="@p.Id">
        <td>@p.Id</td>
        <td>@p.Cliente</td>
        <td>@p.Producto</td>
        <td class="num">@p.Cantidad</td>
        <td class="num">@p.Importe.ToString("C", CultureInfo.GetCultureInfo("es-ES"))</td>
        <td>@p.Estado</td>
    </tr>
</Virtualize>
```

<p class="metric">5.000 filas → visibles + margen</p>

<!--
TIEMPO: 16:00 – 21:00

Mostrar el código ANTES y avanzar para transformar el bloque en DESPUÉS.
Ruta: /listas. Comparar Lento y Rápido. Después activar «Cargar bajo demanda»
y desplazarse hasta el final de la lista.
Items recibe una ICollection<Pedido>; no admite directamente un IReadOnlyList.

Validación del 21/10 con una ventana de 1600×1000: 5.000 filas en Lento
y 27 en Rápido. Con ItemsProvider se solicitaron 27 pedidos con skip=0&top=27.
Al llegar al pedido 5000 seguía habiendo unas decenas de filas en el DOM.
El número depende del tamaño de la ventana, la altura de las filas y las
filas adicionales de margen que configura OverscanCount.
El contador no incluye espaciadores ni filas de carga.

El tiempo se mide desde la creación del componente hasta recibir del navegador
un número de filas positivo y estable durante dos fotogramas.
Incluye carga de datos, renderizado y comunicación.
No es el tiempo de CPU del renderizado, ni el tiempo exacto que tarda
el navegador en dibujar la interfaz, ni el tiempo total de navegación.
Comparar sobre todo el número de filas; no prometer un porcentaje de mejora.
-->

---

## Virtualizar no es paginar los datos

| Implementación | Filas en el DOM | Datos solicitados |
|---|---|---|
| `foreach` | 5.000 | 5.000 |
| `Virtualize` + `Items` | Visibles + margen | 5.000 |
| `Virtualize` + `ItemsProvider` | Visibles + margen | Solo los necesarios |

**Renderizar menos filas** y **cargar menos datos** son mejoras distintas.

<!--
TIEMPO: 21:00 – 21:45

La variante bajo demanda no carga los 5.000 pedidos en el componente.
La API simulada sí conserva todos sus datos en el servidor. No estamos
demostrando una consulta paginada a una base de datos ni reduciendo esa memoria.

ItemsProvider recibe StartIndex, Count y CancellationToken.
Hay que pasar el token a la API para cancelar las solicitudes de filas
que ya no se necesitan.
La demo usa filas de 38 px y un contenedor de 520 px con desplazamiento vertical.
También se puede virtualizar usando el desplazamiento de toda la página.

SpacerElement="tr" permite usar espaciadores válidos dentro de tbody.
Ya existía antes de .NET 9. Las filas que no están en el DOM no aparecen
en la búsqueda del navegador ni al imprimir.
Dejar estos detalles para las preguntas si no hace falta explicarlos.
-->

---

<!-- _class: prompt -->

## Copia este prompt

> Revisa esta lista Blazor que genera miles de filas con `foreach`. Sustitúyela por `Virtualize` y conserva el marcado válido dentro de la tabla. Usa `Items` si los datos ya están cargados; si la lista debe ser grande también en memoria, propone `ItemsProvider` con paginación y cancelación. Comprueba la altura real de las filas, el número visible en el DOM y que se puede recorrer hasta el final. No confundas virtualización con paginación.

<!--
TIEMPO: 21:45 – 22:00

Recordar: el proveedor solo reduce los datos solicitados si el backend ofrece paginación.
-->

---

<!-- _class: transform-before -->

## 4. Volver a cargar el mismo pedido

```razor
<DetallePedidoLento PedidoId="_pedidoId">
    Vista actualizada a las @_hora.ToString("HH:mm:ss")
</DetallePedidoLento>
```

El reloj cambia cada segundo, pero el **Id del pedido sigue igual**.
Al pasar el reloj dentro del detalle, se repite la consulta.

<!--
TIEMPO: 22:00 – 23:00

El componente lento recibe también el texto del reloj, que cambia cada segundo.
Por eso vuelve a ejecutar la carga aunque PedidoId sea el mismo.
-->

---

<!-- _class: transform-after -->

## El reloj fuera; el Id dentro

<p class="metric">mismo Id → 0 llamadas · Id nuevo → 1</p>

```razor
<p class="small muted">
    Vista actualizada a las @_hora.ToString("HH:mm:ss")
</p>
<DetallePedidoRapido PedidoId="_pedidoId" />
```

<!--
TIEMPO: 23:00 – 26:45

Mostrar el código ANTES y avanzar para transformar el bloque en DESPUÉS.
Ruta: /llamadas. En Lento, esperar dos cambios del reloj y observar las llamadas.
Cambiar a Rápido, reiniciar el panel: la hora sigue avanzando, pero el contador
no aumenta. Cambiar el pedido: se hace una llamada para el nuevo Id.

La hora ahora se representa fuera del componente que carga el pedido.
El detalle recibe solo el Id, un parámetro primitivo que no cambia con el reloj.
No abrir ni explicar la implementación interna de cancelación salvo que pregunten.
-->

---

<!-- _class: prompt -->

## Copia este prompt

> Revisa esta página Blazor: un texto del reloj cambia cada segundo dentro de un componente que carga un pedido. Separa ese texto del detalle para que el componente reciba solo el Id. Verifica que los cambios del reloj no vuelvan a llamar a la API y que cambiar el Id sí cargue el pedido nuevo. Haz el cambio mínimo y conserva la cancelación si se cambia de Id durante una carga.

<!--
TIEMPO: 26:45 – 27:00
-->

---

<!-- _class: transform-before -->

## 5. La tabla se renderiza al cambiar el carrito

```csharp
public event Action? OnChange;

public void AñadirAlCarrito(int pedidoId)
{
    Carrito.Add(pedidoId);
    OnChange?.Invoke();
}
```

Un evento general avisa a todos cuando cambia cualquier dato.
Al añadir un producto, la tabla se renderiza aunque sus pedidos no cambien.

<!--
TIEMPO: 27:00 – 28:00

Un evento general puede ser suficiente en una aplicación pequeña.
El problema aparece cuando muchos componentes se suscriben y se renderizan
con cada cambio, aunque no utilicen el dato que ha cambiado.
Eso no significa que todo estado centralizado sea lento ni que haga falta
añadir una biblioteca para resolverlo.
-->

---

<!-- _class: transform-after -->

## Notificar solo el cambio que necesita cada componente

<p class="metric">Añadir: tabla 1 render → 0</p>

```csharp
public event Action? CarritoCambiado;
public event Action? FiltroCambiado;

CarritoCambiado?.Invoke(); // Al añadir al carrito
FiltroCambiado?.Invoke(); // Al cambiar el filtro
```

La tabla solo escucha a `FiltroCambiado`.

<!--
TIEMPO: 28:00 – 32:40

Mostrar el código ANTES y avanzar para transformar el bloque en DESPUÉS.
Ruta: /estado. Esperar a que aparezcan los componentes antes de reiniciar
el panel. Hacerlo en ambos modos para comparar la misma acción.
Validación del 21/10: Añadir en Lento suma un renderizado a cada componente.
En Rápido suma uno al carrito y otro a las ofertas, pero no a la tabla ni al filtro.
Al cambiar el filtro, la tabla sí se actualiza.
Evitar detalles sobre el caché del filtro, lambdas e `InvokeAsync` salvo que pregunten.
-->

---

<!-- _class: prompt -->

## Copia este prompt

> Al añadir un producto al carrito, la tabla de pedidos también se renderiza, aunque no usa el carrito. Cambia lo mínimo para que carrito y filtro tengan notificaciones separadas y la tabla escuche solo al filtro. Conserva la desuscripción en `Dispose` y comprueba que Añadir no actualiza la tabla, pero cambiar el filtro sí.

<!--
TIEMPO: 32:40 – 33:00
-->

---

## Cinco preguntas para revisar tu aplicación

| Pregunta | Qué revisar | Qué medir |
|---|---|---|
| ¿Se renderiza sin cambios? | Parámetros y `ShouldRender` | Renderizados |
| ¿Busco con cada tecla? | Debounce y cancelación | Llamadas |
| ¿Renderizo filas que no se ven? | `Virtualize` y paginación | Filas y lotes |
| ¿Vuelve a cambiar el Id? | Parámetros estables | Llamadas repetidas |
| ¿Qué componentes deben actualizarse? | Suscripciones de estado | Componentes afectados |

**No hace falta optimizarlo todo, sino corregir lo que está causando la lentitud.**

<!--
TIEMPO: 33:00 – 35:00

Resumir cada fila sin repetir las demos. Esta es la diapositiva que pueden fotografiar.
Las medidas ayudan a decidir qué merece la pena cambiar.
No recomendar ShouldRender para todos los componentes ni presentar los bucles
como la única causa de lentitud.
El panel también tiene un coste; por eso limita sus propias actualizaciones.

Terminar el contenido principal en el minuto 35. Si queda tiempo, usar los cinco
tips opcionales; si no, saltarlos y pasar directamente a preguntas.
-->

---

<!-- _class: tip -->

## 1 · Agrupa llamadas relacionadas

**Idea:** si la pantalla siempre necesita varios datos juntos, pídelos a un único endpoint que devuelva un DTO.

- Reduce viajes de red, no solo tiempo de espera.
- `Task.WhenAll` paraleliza, pero no reduce el número de solicitudes.

```csharp
var panel = await http.GetFromJsonAsync<PanelDto>(
    $"/api/pedidos/{id}/panel", ct);
```

<!--
TIEMPO OPCIONAL: hasta 1 minuto

El backend agrega los datos necesarios. No combinar solicitudes independientes
si las pantallas no siempre necesitan los mismos datos.
-->

---

<!-- _class: tip -->

## 2 · Cachea lecturas que cambian poco

**Idea:** evita volver a leer datos que siguen vigentes.

- Define una duración y cuándo invalidar.
- No compartas datos de usuario entre sesiones.

```csharp
if (cache.TryGetValue("productos",
        out IReadOnlyList<Producto>? productos)
    && productos is not null)
    return productos;

productos = await api.GetProductosAsync(ct);
cache.Set("productos", productos, TimeSpan.FromMinutes(5));
return productos;
```

<!--
TIEMPO OPCIONAL: hasta 1 minuto

Explicar que una caché necesita una política de expiración e invalidación.
-->

---

<!-- _class: tip -->

## 3 · Trae solo los campos que muestras

**Idea:** para una pantalla de lectura, proyecta los campos necesarios.

- `AsNoTracking` evita seguimiento si solo vas a leer.
- Menos columnas y objetos reducen memoria y tráfico.

```csharp
var filas = await db.Pedidos
    .AsNoTracking()
    .Where(p => p.Estado == estado)
    .Select(p => new PedidoFila(p.Id, p.Cliente, p.Importe))
    .ToListAsync(ct);
```

<!--
TIEMPO OPCIONAL: hasta 1 minuto

El ejemplo aplica a EF Core. PedidoFila es un DTO de lectura.
-->

---

<!-- _class: tip -->

## 4 · Pagina desde el servidor

**Idea:** en colecciones grandes, solicita una página y pide la siguiente al desplazarte.

- Usa un orden estable y limita el tamaño de página.
- Para desplazamientos muy profundos, valora paginación por cursor.

```csharp
var pagina = await db.Pedidos
    .OrderBy(p => p.Id)
    .Skip(offset)
    .Take(pageSize)
    .Select(p => new PedidoFila(p.Id, p.Cliente, p.Importe))
    .ToListAsync(ct);
```

<!--
TIEMPO OPCIONAL: hasta 1 minuto

Relacionar la paginación del servidor con ItemsProvider de la demo 3.
-->

---

<!-- _class: tip -->

## 5 · Mide antes y después

**Idea:** compara la misma operación con los mismos datos y condiciones.

- Mide lo que le importa al usuario y registra el volumen de datos.
- No conviertas una medición aislada en una promesa universal.

```csharp
var inicio = Stopwatch.GetTimestamp();
var pedidos = await api.GetPedidosAsync(200, ct);
logger.LogInformation(
    "Pedidos={Count}; {ElapsedMs} ms",
    pedidos.Count,
    Stopwatch.GetElapsedTime(inicio).TotalMilliseconds);
```

<!--
TIEMPO OPCIONAL: hasta 1 minuto

Estas cinco diapositivas son material de reserva: si vas justo, salta directamente
a preguntas. Si las presentas, reservar hasta cinco minutos para el bloque.
-->

---

<!-- _class: sponsor -->
<!-- _paginate: false -->

![Patrocinadores de NetCoreConf Madrid 2026](assets/netcoreconf-sponsors.png)

<!--
TIEMPO: Penúltima diapositiva

Agradecer a los patrocinadores antes de abrir las preguntas.
-->

---

<!-- _class: lead -->
<!-- _paginate: false -->

# Gracias

## ¿Preguntas?

**Emiliano Montesdeoca**
linkedin.com/in/emimontesdeoca · @emimontesdeocaa

<!--
TIEMPO: 35:00 – 40:00 si se omiten los tips; 40:00 – 45:00 si se presentan.

Reservar cinco minutos para preguntas.
Si se omiten los tips, usar el tiempo restante como margen para preguntas y transiciones.
Mantener esta diapositiva al terminar.

Preguntas previsibles:
- WebAssembly: las técnicas también sirven, pero cambian los costes y dónde se miden.
- Static SSR: puede repetir consultas o generar demasiado HTML, pero no tiene
  estos eventos interactivos. Auto no cambia el modo de un componente ya abierto.
- Records: no se consideran automáticamente inmutables por ser records.
- QuickGrid: ofrece paginación y virtualización; elegir según el caso.
- Alturas variables en .NET 10: esta tabla mantiene alturas estables.
  ItemSize es una estimación; si las alturas varían, puede haber saltos al desplazarse.
- Cancelación: es cooperativa. Cancelar la petición del cliente no deshace
  los cambios que el servidor remoto ya haya realizado.
- Caché: los tips opcionales muestran una caché simple. En producción hay que definir
  duración, invalidación, aislamiento entre usuarios y límites de memoria.

Si no sabes una respuesta, anotarla y ofrecer continuar la conversación después.
-->
