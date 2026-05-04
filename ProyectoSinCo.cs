using System;
using System.Collections.Generic;
using System.IO;

class Producto
{
    public int    Id        { get; set; }
    public string Nombre    { get; set; }
    public string Categoria { get; set; }
    public double Precio    { get; set; }
    public int    Stock     { get; set; }

    public Producto(int id, string nombre, string categoria, double precio, int stock)
    {
        Id        = id;
        Nombre    = nombre;
        Categoria = categoria;
        Precio    = precio;
        Stock     = stock;
    }

    public override string ToString()
    {
        return $"[{Id}] {Nombre,-25} | {Categoria,-15} | ${Precio,10:N0} | Stock: {Stock}";
    }
}

class ItemCarrito
{
    public Producto Producto { get; set; }
    public int      Cantidad { get; set; }
    public double   Subtotal => Producto.Precio * Cantidad;

    public ItemCarrito(Producto producto, int cantidad)
    {
        Producto = producto;
        Cantidad = cantidad;
    }
}

class Usuario
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Rol      { get; set; }

    public Usuario(string username, string password, string rol)
    {
        Username = username;
        Password = password;
        Rol      = rol;
    }
}

class Venta
{
    public int               NumeroVenta { get; set; }
    public string            Cliente     { get; set; }
    public DateTime          Fecha       { get; set; }
    public List<ItemCarrito> Items       { get; set; }
    public double            Total       { get; set; }

    public Venta(int numero, string cliente, List<ItemCarrito> items)
    {
        NumeroVenta = numero;
        Cliente     = cliente;
        Fecha       = DateTime.Now;
        Items       = items;
        Total       = 0;
        foreach (var item in items)
            Total += item.Subtotal;
    }
}

class Minimercado
{
    static List<Producto>    catalogo      = new List<Producto>();
    static List<Usuario>     usuarios      = new List<Usuario>();
    static List<ItemCarrito> carrito       = new List<ItemCarrito>();
    static List<Venta>       registroVentas = new List<Venta>();

    static string usuarioActual = "";
    static string rolActual     = "";
    static bool   sesionActiva  = false;
    static int    contadorVentas = 1;

    static void Main(string[] args)
    {
        CargarDatosIniciales();

        Console.WriteLine("╔══════════════════════════════════╗");
        Console.WriteLine("║     MINIMERCADO TECH  v1.0       ║");
        Console.WriteLine("║    Tu tienda de computadoras     ║");
        Console.WriteLine("╚══════════════════════════════════╝");
        Console.WriteLine();

        Login();

        Console.WriteLine("\n--- DEMO DEL SISTEMA ---\n");

        MostrarCatalogo();
        Console.WriteLine();

        BuscarProducto();
        Console.WriteLine();

        AgregarAlCarrito();
        Console.WriteLine();

        MostrarCarrito();
        Console.WriteLine();

        RealizarCompra();
        Console.WriteLine();

        MostrarRegistroVentas();
    }

    static void CargarDatosIniciales()
    {
        usuarios.Add(new Usuario("admin",    "admin123",  "admin"));
        usuarios.Add(new Usuario("cliente1", "pass123",   "cliente"));
        usuarios.Add(new Usuario("carlos",   "carlos456", "cliente"));

        catalogo.Add(new Producto(1,  "Teclado Mecánico RGB",     "Periféricos", 250000,  15));
        catalogo.Add(new Producto(2,  "Mouse Gamer 16000 DPI",    "Periféricos", 180000,  20));
        catalogo.Add(new Producto(3,  "Monitor 24\" Full HD",     "Monitores",   850000,   8));
        catalogo.Add(new Producto(4,  "RAM DDR4 16GB",            "Hardware",    320000,  25));
        catalogo.Add(new Producto(5,  "SSD 500GB NVMe",           "Hardware",    280000,  18));
        catalogo.Add(new Producto(6,  "Tarjeta Gráfica RTX 3060", "Hardware",   1950000,   5));
        catalogo.Add(new Producto(7,  "Audífonos Gaming 7.1",     "Periféricos", 220000,  12));
        catalogo.Add(new Producto(8,  "Webcam Full HD 1080p",     "Periféricos", 150000,  10));
        catalogo.Add(new Producto(9,  "Procesador Ryzen 5 5600",  "Hardware",    780000,   7));
        catalogo.Add(new Producto(10, "Fuente de Poder 650W",     "Hardware",    340000,   9));
    }

    static void Login()
    {
        Console.WriteLine("=== INICIO DE SESIÓN ===");

        int intentos    = 0;
        int maxIntentos = 3;

        while (!sesionActiva && intentos < maxIntentos)
        {
            Console.Write("Usuario: ");
            string username = Console.ReadLine();

            Console.Write("Contraseña: ");
            string password = LeerPasswordOculta();

            Usuario encontrado = null;

            foreach (Usuario u in usuarios)
            {
                if (u.Username.ToLower() == username.ToLower() && u.Password == password)
                {
                    encontrado = u;
                    break;
                }
            }

            if (encontrado != null)
            {
                sesionActiva  = true;
                usuarioActual = encontrado.Username;
                rolActual     = encontrado.Rol;
                Console.WriteLine($"\n✓ Bienvenido, {usuarioActual}! (Rol: {rolActual})");
            }
            else
            {
                intentos++;
                int restantes = maxIntentos - intentos;
                Console.WriteLine($"✗ Usuario o contraseña incorrectos. Intentos restantes: {restantes}");
            }
        }

        if (!sesionActiva)
        {
            Console.WriteLine("Demasiados intentos fallidos. El sistema se cerrará.");
            Environment.Exit(0);
        }
    }

    static string LeerPasswordOculta()
    {
        string password = "";

        while (true)
        {
            ConsoleKeyInfo tecla = Console.ReadKey(true);

            if (tecla.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }

            if (tecla.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b");
            }
            else if (tecla.Key != ConsoleKey.Backspace)
            {
                password += tecla.KeyChar;
                Console.Write("*");
            }
        }

        return password;
    }

    static void CerrarSesion()
    {
        Console.WriteLine($"\nCerrando sesión de {usuarioActual}...");
        sesionActiva  = false;
        usuarioActual = "";
        rolActual     = "";
        carrito.Clear();
        Console.WriteLine("Sesión cerrada. ¡Hasta pronto!");
    }

    static void MostrarCatalogo()
    {
        Console.WriteLine("=== CATÁLOGO DE PRODUCTOS ===");
        Console.WriteLine($"{"ID",-4} {"Nombre",-25} {"Categoría",-15} {"Precio",12} {"Stock",8}");
        Console.WriteLine(new string('-', 70));

        foreach (Producto p in catalogo)
            Console.WriteLine(p);

        Console.WriteLine(new string('-', 70));
        Console.WriteLine($"Total de productos: {catalogo.Count}");
    }

    static void BuscarProducto()
    {
        Console.WriteLine("=== BUSCADOR DE PRODUCTOS ===");
        Console.Write("Ingresa término de búsqueda (nombre o categoría): ");
        string termino = Console.ReadLine().ToLower();

        List<Producto> resultados = new List<Producto>();

        foreach (Producto p in catalogo)
        {
            if (p.Nombre.ToLower().Contains(termino) || p.Categoria.ToLower().Contains(termino))
                resultados.Add(p);
        }

        if (resultados.Count == 0)
        {
            Console.WriteLine($"No se encontraron productos con '{termino}'");
        }
        else
        {
            Console.WriteLine($"\nSe encontraron {resultados.Count} resultado(s):");
            Console.WriteLine(new string('-', 70));
            foreach (Producto p in resultados)
                Console.WriteLine(p);
        }
    }

    static Producto BuscarPorId(int id)
    {
        foreach (Producto p in catalogo)
        {
            if (p.Id == id)
                return p;
        }
        return null;
    }

    static void AgregarAlCarrito()
    {
        Console.WriteLine("=== AGREGAR AL CARRITO ===");
        MostrarCatalogo();

        Console.Write("\nIngresa el ID del producto que deseas agregar: ");
        if (!int.TryParse(Console.ReadLine(), out int idProducto))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Producto producto = BuscarPorId(idProducto);

        if (producto == null)
        {
            Console.WriteLine("Producto no encontrado.");
            return;
        }

        if (producto.Stock == 0)
        {
            Console.WriteLine($"Lo sentimos, '{producto.Nombre}' está agotado.");
            return;
        }

        Console.Write($"Cantidad (disponibles: {producto.Stock}): ");
        if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)
        {
            Console.WriteLine("Cantidad inválida.");
            return;
        }

        if (cantidad > producto.Stock)
        {
            Console.WriteLine($"Stock insuficiente. Solo hay {producto.Stock} unidades.");
            return;
        }

        bool yaEnCarrito = false;
        foreach (ItemCarrito item in carrito)
        {
            if (item.Producto.Id == idProducto)
            {
                if (item.Cantidad + cantidad > producto.Stock)
                {
                    Console.WriteLine($"No puedes agregar más. Ya tienes {item.Cantidad} en el carrito.");
                    yaEnCarrito = true;
                    break;
                }
                item.Cantidad += cantidad;
                Console.WriteLine($"✓ Cantidad actualizada: {item.Cantidad}x {producto.Nombre}");
                yaEnCarrito = true;
                break;
            }
        }

        if (!yaEnCarrito)
        {
            carrito.Add(new ItemCarrito(producto, cantidad));
            Console.WriteLine($"✓ Agregado: {cantidad}x {producto.Nombre} - ${producto.Precio * cantidad:N0}");
        }
    }

    static void MostrarCarrito()
    {
        Console.WriteLine("=== CARRITO DE COMPRAS ===");

        if (carrito.Count == 0)
        {
            Console.WriteLine("El carrito está vacío.");
            return;
        }

        Console.WriteLine($"{"#",-4} {"Producto",-25} {"Precio",12} {"Cant",6} {"Subtotal",14}");
        Console.WriteLine(new string('-', 65));

        double total = 0;

        for (int i = 0; i < carrito.Count; i++)
        {
            ItemCarrito item = carrito[i];
            Console.WriteLine($"{i + 1,-4} {item.Producto.Nombre,-25} ${item.Producto.Precio,11:N0} {item.Cantidad,6} ${item.Subtotal,13:N0}");
            total += item.Subtotal;
        }

        Console.WriteLine(new string('-', 65));
        Console.WriteLine($"{"TOTAL:",-48} ${total,13:N0}");
        Console.WriteLine($"Artículos en carrito: {carrito.Count}");
    }

    static void EliminarDelCarrito()
    {
        if (carrito.Count == 0)
        {
            Console.WriteLine("El carrito está vacío.");
            return;
        }

        MostrarCarrito();
        Console.Write("\nIngresa el número del producto a eliminar (0 para cancelar): ");

        if (!int.TryParse(Console.ReadLine(), out int numero) || numero < 0 || numero > carrito.Count)
        {
            Console.WriteLine("Número inválido.");
            return;
        }

        if (numero == 0) return;

        int indice = numero - 1;
        string nombreProducto = carrito[indice].Producto.Nombre;
        carrito.RemoveAt(indice);
        Console.WriteLine($"✓ '{nombreProducto}' eliminado del carrito.");
    }

    static void RealizarCompra()
    {
        Console.WriteLine("=== PROCESO DE COMPRA ===");

        if (carrito.Count == 0)
        {
            Console.WriteLine("El carrito está vacío.");
            return;
        }

        MostrarCarrito();

        Console.Write("\n¿Confirmar compra? (s/n): ");
        string confirmacion = Console.ReadLine().ToLower();

        if (confirmacion != "s")
        {
            Console.WriteLine("Compra cancelada.");
            return;
        }

        List<ItemCarrito> itemsVenta = new List<ItemCarrito>(carrito);
        Venta nuevaVenta = new Venta(contadorVentas, usuarioActual, itemsVenta);
        contadorVentas++;

        foreach (ItemCarrito item in carrito)
        {
            Producto prod = BuscarPorId(item.Producto.Id);
            if (prod != null)
                prod.Stock -= item.Cantidad;
        }

        registroVentas.Add(nuevaVenta);
        carrito.Clear();

        ImprimirFactura(nuevaVenta);
        GuardarFacturaEnArchivo(nuevaVenta);
    }

    static void ImprimirFactura(Venta venta)
    {
        Console.WriteLine();
        Console.WriteLine("╔══════════════════════════════════════════════════╗");
        Console.WriteLine("║              MINIMERCADO TECH                   ║");
        Console.WriteLine("║          NIT: 900.123.456-7                     ║");
        Console.WriteLine("║       Calle 45 #23-10, Medellín                 ║");
        Console.WriteLine("╠══════════════════════════════════════════════════╣");
        Console.WriteLine($"║  Factura N°: {venta.NumeroVenta:D6}                            ║");
        Console.WriteLine($"║  Fecha: {venta.Fecha:dd/MM/yyyy HH:mm:ss}               ║");
        Console.WriteLine($"║  Cliente: {venta.Cliente,-38}║");
        Console.WriteLine("╠══════════════════════════════════════════════════╣");
        Console.WriteLine($"║  {"Producto",-24} {"Cant",4} {"P.Unit",10} {"Subtotal",10} ║");
        Console.WriteLine("╠══════════════════════════════════════════════════╣");

        foreach (ItemCarrito item in venta.Items)
        {
            string nombre = item.Producto.Nombre.Length > 24
                ? item.Producto.Nombre.Substring(0, 21) + "..."
                : item.Producto.Nombre;

            Console.WriteLine($"║  {nombre,-24} {item.Cantidad,4} {item.Producto.Precio,9:N0} {item.Subtotal,9:N0} ║");
        }

        Console.WriteLine("╠══════════════════════════════════════════════════╣");
        Console.WriteLine($"║  {"TOTAL A PAGAR:",-35} ${venta.Total,10:N0} ║");
        Console.WriteLine("╚══════════════════════════════════════════════════╝");
        Console.WriteLine("     ¡Gracias por su compra en MinimercadoTech!    ");
    }

    static void GuardarFacturaEnArchivo(Venta venta)
    {
        string nombreArchivo = $"factura_{venta.NumeroVenta:D6}.txt";

        using (StreamWriter escritor = new StreamWriter(nombreArchivo))
        {
            escritor.WriteLine("MINIMERCADO TECH");
            escritor.WriteLine($"Factura N°: {venta.NumeroVenta:D6}");
            escritor.WriteLine($"Fecha: {venta.Fecha:dd/MM/yyyy HH:mm:ss}");
            escritor.WriteLine($"Cliente: {venta.Cliente}");
            escritor.WriteLine(new string('-', 50));
            escritor.WriteLine($"{"Producto",-25} {"Cant",4} {"Subtotal",12}");
            escritor.WriteLine(new string('-', 50));

            foreach (ItemCarrito item in venta.Items)
                escritor.WriteLine($"{item.Producto.Nombre,-25} {item.Cantidad,4} ${item.Subtotal,11:N0}");

            escritor.WriteLine(new string('-', 50));
            escritor.WriteLine($"TOTAL: ${venta.Total:N0}");
        }

        Console.WriteLine($"\n✓ Factura guardada en: {nombreArchivo}");
    }

    static void MostrarRegistroVentas()
    {
        Console.WriteLine("=== REGISTRO DE VENTAS ===");

        if (registroVentas.Count == 0)
        {
            Console.WriteLine("No hay ventas registradas.");
            return;
        }

        Console.WriteLine($"{"N° Venta",-10} {"Cliente",-15} {"Fecha",-20} {"Artículos",10} {"Total",14}");
        Console.WriteLine(new string('-', 75));

        double totalGeneral = 0;

        foreach (Venta v in registroVentas)
        {
            Console.WriteLine($"{v.NumeroVenta:D6,-10} {v.Cliente,-15} {v.Fecha:dd/MM/yyyy HH:mm,-20} {v.Items.Count,10} ${v.Total,13:N0}");
            totalGeneral += v.Total;
        }

        Console.WriteLine(new string('-', 75));
        Console.WriteLine($"Total de ventas: {registroVentas.Count}");
        Console.WriteLine($"Recaudo total:   ${totalGeneral:N0}");
    }

    static void VerDetalleVenta()
    {
        MostrarRegistroVentas();

        if (registroVentas.Count == 0) return;

        Console.Write("\nIngresa el número de venta a ver (ej: 1): ");

        if (!int.TryParse(Console.ReadLine(), out int numVenta))
        {
            Console.WriteLine("Número inválido.");
            return;
        }

        Venta ventaEncontrada = null;
        foreach (Venta v in registroVentas)
        {
            if (v.NumeroVenta == numVenta)
            {
                ventaEncontrada = v;
                break;
            }
        }

        if (ventaEncontrada == null)
        {
            Console.WriteLine($"No existe la venta N° {numVenta}");
            return;
        }

        ImprimirFactura(ventaEncontrada);
    }
}
