namespace BlazorLento.Web.Services;

/// <summary>
/// 5.000 pedidos y un catálogo pequeño, generados con semilla fija: la demo es igual en cada arranque.
/// </summary>
public sealed class DatosFalsos
{
    private static readonly string[] Nombres =
    [
        "Lucía", "Hugo", "Martina", "Mateo", "Sofía", "Daniel", "Paula", "Pablo", "Valeria", "Álvaro",
        "Carla", "Adrián", "Noa", "Diego", "Vega", "Marcos", "Julia", "Leo", "Emma", "Izan"
    ];

    private static readonly string[] Apellidos =
    [
        "García", "Martínez", "López", "Sánchez", "Pérez", "Gómez", "Hernández", "Díaz", "Moreno", "Ruiz",
        "Jiménez", "Torres", "Romero", "Navarro", "Suárez", "Montesdeoca", "Abreu", "Falcón", "Déniz", "Padrón"
    ];

    private static readonly (string Nombre, decimal Precio)[] Catalogo =
    [
        ("Teclado mecánico", 89.90m), ("Ratón inalámbrico", 34.50m), ("Monitor 27\"", 249.00m),
        ("Webcam 1080p", 59.00m), ("Auriculares con micro", 79.00m), ("Hub USB-C", 42.00m),
        ("Silla ergonómica", 319.00m), ("Lámpara de escritorio", 28.90m), ("Soporte portátil", 24.90m),
        ("Alfombrilla XL", 19.90m), ("SSD 1 TB", 99.00m), ("Cable HDMI 2 m", 9.90m)
    ];

    public IReadOnlyList<Pedido> Pedidos { get; }
    public IReadOnlyList<Producto> Productos { get; }

    public DatosFalsos()
    {
        var rnd = new Random(2026);

        Productos = Catalogo
            .Select((p, i) => new Producto(i + 1, p.Nombre, p.Precio, rnd.Next(0, 120)))
            .ToList();

        var inicio = new DateTime(2026, 1, 1);
        var pedidos = new List<Pedido>(5000);
        for (var i = 1; i <= 5000; i++)
        {
            var producto = Catalogo[rnd.Next(Catalogo.Length)];
            var cantidad = rnd.Next(1, 6);
            pedidos.Add(new Pedido
            {
                Id = i,
                Cliente = $"{Nombres[rnd.Next(Nombres.Length)]} {Apellidos[rnd.Next(Apellidos.Length)]} {Apellidos[rnd.Next(Apellidos.Length)]}",
                Producto = producto.Nombre,
                Cantidad = cantidad,
                Importe = producto.Precio * cantidad,
                Fecha = inicio.AddMinutes(rnd.Next(0, 60 * 24 * 280)),
                Estado = (EstadoPedido)rnd.Next(0, 5),
            });
        }

        Pedidos = pedidos;
    }
}
