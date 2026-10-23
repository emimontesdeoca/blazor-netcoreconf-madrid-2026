# BlazorLento

Aplicación de demostración para explorar cinco problemas habituales de rendimiento en Blazor y comparar una versión lenta con una optimizada.

[Ver las slides en GitHub Pages](https://emimontesdeoca.github.io/blazor-netcoreconf-madrid-2026/) · [Descargar el PDF](slides/deck.pdf)

El sitio estará disponible cuando GitHub Pages esté habilitado y el repositorio sea público.

## Qué puedes probar

| Ruta | Escenario |
|---|---|
| `/renderizados` | Evitar que filas sin cambios vuelvan a renderizarse |
| `/eventos` | Reducir búsquedas con debounce y cancelación |
| `/listas` | Mostrar listas extensas con virtualización y carga bajo demanda |
| `/llamadas` | Cambiar la vista sin repetir la consulta del pedido |
| `/estado` | Evitar que el carrito vuelva a renderizar la tabla |

La aplicación usa datos ficticios y una API simulada; no necesita base de datos ni servicios externos.

## Ejecutar localmente

Requisito: **.NET 10 SDK**.

```powershell
dotnet run --project .\src\Web --launch-profile http
```

Abre http://localhost:5100.

## Slides y GitHub Pages

El workflow [deploy-slides.yml](.github/workflows/deploy-slides.yml) publica `slides/deck.html` y sus assets en la raíz de la rama `pages`.

1. En **Settings → Pages**, selecciona **Deploy from a branch**, rama `pages` y carpeta `/(root)`.
2. Genera el HTML de Pages y el PDF de descarga desde `slides`:

   ```powershell
   Set-Location .\slides
   npx @marp-team/marp-cli deck.md -o deck.html
   npx @marp-team/marp-cli deck.md --pdf --allow-local-files -o deck.pdf
   ```

   `--allow-local-files` permite incluir las imágenes locales de `assets/` en el PDF.

3. Publica los cambios en `master`; el workflow creará o actualizará la rama `pages`.

El enlace público requiere que el repositorio sea público y que se complete el primer despliegue.
