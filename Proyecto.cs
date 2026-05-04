// ============================================================
//  MINIMERCADO TECH - Sistema de gestión
//  Materia: Estructura de Datos
//  Descripción: Sistema de minimercado temática computadoras
// ============================================================

// Estas líneas importan "cajas de herramientas" que trae C# por defecto
// No hay que instalar nada extra
using System;                        // Herramientas básicas: Console, Math, etc.
using System.Collections.Generic;   // Para usar List<> (listas dinámicas)
using System.IO;                     // Para leer y escribir archivos de texto

// ============================================================
// CLASES - Una clase es como un "molde" para crear objetos
// Piénsala como una ficha técnica con datos y acciones
// ============================================================

// --- CLASE PRODUCTO ---
// Representa cada artículo que vende el minimercado
class Producto
{
    // Propiedades: son los "campos" o datos que tiene cada producto
    public int    Id       { get; set; }  // Número único que identifica el producto
    public string Nombre   { get; set; }  // Nombre del producto, ej: "Teclado RGB"
    public string Categoria{ get; set; }  // Categoría: Periféricos, Hardware, etc.
    public double Precio   { get; set; }  // Precio en pesos
    public int    Stock    { get; set; }  // Cuántas unidades hay disponibles

    // Constructor: es el método que se llama cuando "creamos" un nuevo producto
    // Es como rellenar la ficha técnica al momento de crear el objeto
    public Producto(int id, string nombre, string categoria, double precio, int stock)
    {
        Id        = id;
        Nombre    = nombre;
        Categoria = categoria;
        Precio    = precio;
        Stock     = stock;
    }

    // Método ToString: define cómo se "imprime" un producto en consola
    // Se llama automáticamente cuando hacemos Console.WriteLine(producto)
    public override string ToString()
    {
        return $"[{Id}] {Nombre,-25} | {Categoria,-15} | ${Precio,10:N0} | Stock: {Stock}";
        // {Nombre,-25} significa: mostrar el nombre ocupando 25 caracteres (alineado izquierda)
        // ${Precio,10:N0} significa: mostrar el precio con separador de miles, ocupando 10 chars
    }
}

// --- CLASE ITEM DEL CARRITO ---
// Representa un producto que el cliente agregó al carrito
// Guarda el producto Y la cantidad que quiere comprar
class ItemCarrito
{
    public Producto Producto  { get; set; }  // El producto seleccionado
    public int      Cantidad  { get; set; }  // Cuántas unidades quiere

    // Propiedad calculada: el subtotal es precio × cantidad
    // No se guarda, se calcula cada vez que se necesita
    public double Subtotal => Producto.Precio * Cantidad;

    public ItemCarrito(Producto producto, int cantidad)
    {
        Producto = producto;
        Cantidad = cantidad;
    }
}

// --- CLASE USUARIO ---
// Representa a las personas que pueden iniciar sesión
class Usuario
{
    public string Username { get; set; }  // Nombre de usuario para login
    public string Password { get; set; }  // Contraseña
    public string Rol      { get; set; }  // "admin" o "cliente"

    public Usuario(string username, string password, string rol)
    {
        Username = username;
        Password = password;
        Rol      = rol;
    }
}

// --- CLASE VENTA ---
// Guarda el registro de una compra completa que se realizó
class Venta
{
    public int           NumeroVenta  { get; set; }  // Número de factura
    public string        Cliente      { get; set; }  // Username del cliente
    public DateTime      Fecha        { get; set; }  // Fecha y hora de la compra
    public List<ItemCarrito> Items    { get; set; }  // Lista de productos comprados
    public double        Total        { get; set; }  // Total pagado

    public Venta(int numero, string cliente, List<ItemCarrito> items)
    {
        NumeroVenta = numero;
        Cliente     = cliente;
        Fecha       = DateTime.Now;   // Toma la fecha y hora actual del sistema
        Items       = items;
        // Calculamos el total sumando todos los subtotales
        // Esto es como un foreach pero en una sola línea (LINQ básico)
        Total = 0;
        foreach (var item in items)
            Total += item.Subtotal;
    }
}

// ============================================================
// CLASE PRINCIPAL DEL PROGRAMA
// Aquí van todas las funciones del sistema
// ============================================================
class Minimercado
{
    // --------------------------------------------------------
    // ESTRUCTURAS DE DATOS GLOBALES
    // Son las "bases de datos" en memoria del programa
    // Usamos List<> porque es una lista dinámica (crece y reduce sola)
    // --------------------------------------------------------

    // Lista de productos disponibles en el catálogo
    static List<Producto> catalogo = new List<Producto>();

    // Lista de usuarios registrados en el sistema
    static List<Usuario> usuarios = new List<Usuario>();

    // Lista que representa el carrito de compras actual
    // Se limpia cada vez que se hace una compra
    static List<ItemCarrito> carrito = new List<ItemCarrito>();

    // Lista de todas las ventas realizadas (registro histórico)
    static List<Venta> registroVentas = new List<Venta>();

    // Variables de sesión: guardan quién está conectado en este momento
    static string usuarioActual = "";   // Username del usuario logueado
    static string rolActual     = "";   // Rol del usuario ("admin" o "cliente")
    static bool   sesionActiva  = false; // true si alguien está logueado

    // Contador para los números de venta (empieza en 1 y va aumentando)
    static int contadorVentas = 1;

    // ============================================================
    // MÉTODO MAIN - El punto de entrada del programa
    // C# siempre empieza ejecutando este método
    // ============================================================
    static void Main(string[] args)
    {
        // Cargamos datos de prueba para que el sistema no esté vacío
        CargarDatosIniciales();

        Console.WriteLine("╔══════════════════════════════════╗");
        Console.WriteLine("║     MINIMERCADO TECH  v1.0       ║");
        Console.WriteLine("║    Tu tienda de computadoras     ║");
        Console.WriteLine("╚══════════════════════════════════╝");
        Console.WriteLine();

        // Pedimos login antes de dejar hacer cualquier cosa
        Login();

        // Aquí llamamos las funciones para demostrar que funcionan
        // (Cuando hagas el menú, desde el menú llamarás estas funciones)
        Console.WriteLine("\n--- DEMO DEL SISTEMA ---");
        Console.WriteLine("(Estas funciones las conectarás al menú que hagas tú)\n");

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

    // ============================================================
    // FUNCIÓN 1: CARGAR DATOS INICIALES
    // Crea usuarios y productos de prueba para que el sistema
    // no esté vacío cuando lo ejecutemos
    // ============================================================
    static void CargarDatosIniciales()
    {
        // --- Usuarios de prueba ---
        // Usamos .Add() para agregar elementos a la lista
        usuarios.Add(new Usuario("admin",    "admin123",  "admin"));
        usuarios.Add(new Usuario("cliente1", "pass123",   "cliente"));
        usuarios.Add(new Usuario("carlos",   "carlos456", "cliente"));

        // --- Productos de prueba ---
        // Formato: Id, Nombre, Categoría, Precio, Stock
        catalogo.Add(new Producto(1, "Teclado Mecánico RGB",    "Periféricos", 250000, 15));
        catalogo.Add(new Producto(2, "Mouse Gamer 16000 DPI",   "Periféricos", 180000, 20));
        catalogo.Add(new Producto(3, "Monitor 24\" Full HD",    "Monitores",   850000, 8));
        catalogo.Add(new Producto(4, "RAM DDR4 16GB",           "Hardware",    320000, 25));
        catalogo.Add(new Producto(5, "SSD 500GB NVMe",          "Hardware",    280000, 18));
        catalogo.Add(new Producto(6, "Tarjeta Gráfica RTX 3060","Hardware",   1950000, 5));
        catalogo.Add(new Producto(7, "Audífonos Gaming 7.1",    "Periféricos", 220000, 12));
        catalogo.Add(new Producto(8, "Webcam Full HD 1080p",    "Periféricos", 150000, 10));
        catalogo.Add(new Producto(9, "Procesador Ryzen 5 5600", "Hardware",    780000, 7));
        catalogo.Add(new Producto(10,"Fuente de Poder 650W",    "Hardware",    340000, 9));
    }

    // ============================================================
    // FUNCIÓN 2: LOGIN
    // Verifica que el usuario y contraseña existan en la lista
    // de usuarios. Da máximo 3 intentos.
    // ============================================================
    static void Login()
    {
        Console.WriteLine("=== INICIO DE SESIÓN ===");

        int intentos = 0;          // Contador de intentos fallidos
        int maxIntentos = 3;       // Máximo de intentos permitidos

        // Mientras no haya sesión activa Y no hayamos agotado intentos
        while (!sesionActiva && intentos < maxIntentos)
        {
            Console.Write("Usuario: ");
            string username = Console.ReadLine();

            Console.Write("Contraseña: ");
            // LeerPasswordOculta() muestra asteriscos en vez de letras
            string password = LeerPasswordOculta();

            // Buscamos en la lista de usuarios uno que coincida
            // con el username Y password ingresados
            Usuario encontrado = null;  // null = "no encontrado todavía"

            // Recorremos la lista con foreach (como un for pero para listas)
            foreach (Usuario u in usuarios)
            {
                // Comparamos ignorando mayúsculas/minúsculas en el username
                if (u.Username.ToLower() == username.ToLower() && u.Password == password)
                {
                    encontrado = u;  // Encontramos el usuario
                    break;           // Salimos del foreach, ya no necesitamos seguir buscando
                }
            }

            if (encontrado != null)  // Si encontramos un usuario válido
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

        // Si agotó los intentos sin lograrse loguear, cerramos el programa
        if (!sesionActiva)
        {
            Console.WriteLine("Demasiados intentos fallidos. El sistema se cerrará.");
            Environment.Exit(0);  // Cierra el programa
        }
    }

    // ============================================================
    // FUNCIÓN AUXILIAR: LEER PASSWORD OCULTA
    // Lee caracteres uno por uno y muestra '*' en pantalla
    // para que nadie vea la contraseña
    // ============================================================
    static string LeerPasswordOculta()
    {
        string password = "";      // Aquí vamos acumulando los caracteres

        // Leemos tecla por tecla
        while (true)
        {
            // ReadKey(true): lee una tecla sin mostrarla en pantalla
            ConsoleKeyInfo tecla = Console.ReadKey(true);

            // Si presionaron Enter, terminamos de leer
            if (tecla.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();  // Bajamos una línea
                break;
            }

            // Si presionaron Backspace y hay caracteres, borramos el último
            if (tecla.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b");  // Borra el '*' de la pantalla
            }
            else if (tecla.Key != ConsoleKey.Backspace)
            {
                // Agregamos el carácter a la contraseña y mostramos '*'
                password += tecla.KeyChar;
                Console.Write("*");
            }
        }

        return password;
    }

    // ============================================================
    // FUNCIÓN 3: CERRAR SESIÓN
    // Limpia los datos de la sesión actual
    // ============================================================
    static void CerrarSesion()
    {
        Console.WriteLine($"\nCerrando sesión de {usuarioActual}...");

        // Limpiamos las variables de sesión
        sesionActiva  = false;
        usuarioActual = "";
        rolActual     = "";

        // También limpiamos el carrito al cerrar sesión
        carrito.Clear();  // .Clear() vacía la lista completamente

        Console.WriteLine("Sesión cerrada. ¡Hasta pronto!");
    }

    // ============================================================
    // FUNCIÓN 4: MOSTRAR CATÁLOGO
    // Muestra todos los productos disponibles en pantalla
    // ============================================================
    static void MostrarCatalogo()
    {
        Console.WriteLine("=== CATÁLOGO DE PRODUCTOS ===");
        Console.WriteLine($"{"ID",-4} {"Nombre",-25} {"Categoría",-15} {"Precio",12} {"Stock",8}");
        Console.WriteLine(new string('-', 70));  // Línea separadora de 70 guiones

        // Recorremos la lista de productos y mostramos cada uno
        foreach (Producto p in catalogo)
        {
            // Usamos el método ToString() que definimos en la clase Producto
            Console.WriteLine(p);
        }

        Console.WriteLine(new string('-', 70));
        Console.WriteLine($"Total de productos: {catalogo.Count}");
        // .Count es una propiedad de List<> que dice cuántos elementos tiene
    }

    // ============================================================
    // FUNCIÓN 5: BARRA DE BÚSQUEDA
    // Permite buscar productos por nombre o categoría
    // Recorre la lista y filtra los que coincidan con el texto
    // ============================================================
    static void BuscarProducto()
    {
        Console.WriteLine("=== BUSCADOR DE PRODUCTOS ===");
        Console.Write("Ingresa término de búsqueda (nombre o categoría): ");
        string termino = Console.ReadLine().ToLower();  // Convertimos a minúsculas para comparar

        // Lista donde guardaremos los resultados encontrados
        // Usamos una lista local (solo existe dentro de esta función)
        List<Producto> resultados = new List<Producto>();

        // Recorremos el catálogo buscando coincidencias
        foreach (Producto p in catalogo)
        {
            // .ToLower() convierte a minúsculas para que la búsqueda no sea case-sensitive
            // .Contains() verifica si el texto contiene el término buscado
            bool nombreCoincide    = p.Nombre.ToLower().Contains(termino);
            bool categoriaCoincide = p.Categoria.ToLower().Contains(termino);

            if (nombreCoincide || categoriaCoincide)
            {
                resultados.Add(p);  // Agregamos a la lista de resultados
            }
        }

        // Mostramos los resultados
        if (resultados.Count == 0)  // Si la lista de resultados está vacía
        {
            Console.WriteLine($"No se encontraron productos con '{termino}'");
        }
        else
        {
            Console.WriteLine($"\nSe encontraron {resultados.Count} resultado(s):");
            Console.WriteLine(new string('-', 70));

            foreach (Producto p in resultados)
            {
                Console.WriteLine(p);
            }
        }
    }

    // ============================================================
    // FUNCIÓN AUXILIAR: BUSCAR PRODUCTO POR ID
    // Recibe un ID y devuelve el Producto que tenga ese ID
    // Devuelve null si no existe
    // Esta función la usan otras funciones internamente
    // ============================================================
    static Producto BuscarPorId(int id)
    {
        foreach (Producto p in catalogo)
        {
            if (p.Id == id)
                return p;  // Devolvemos el producto encontrado
        }
        return null;  // Si llegamos aquí, no se encontró
    }

    // ============================================================
    // FUNCIÓN 6: AGREGAR AL CARRITO
    // Permite al usuario seleccionar un producto y cantidad
    // Valida que exista el producto y haya suficiente stock
    // ============================================================
    static void AgregarAlCarrito()
    {
        Console.WriteLine("=== AGREGAR AL CARRITO ===");
        MostrarCatalogo();  // Mostramos el catálogo para que el usuario vea las opciones

        Console.Write("\nIngresa el ID del producto que deseas agregar: ");

        // int.TryParse intenta convertir el texto a número
        // Si falla (el usuario escribió letras), devuelve false y no crashea el programa
        if (!int.TryParse(Console.ReadLine(), out int idProducto))
        {
            Console.WriteLine("ID inválido.");
            return;  // Salimos de la función
        }

        // Buscamos el producto por ID
        Producto producto = BuscarPorId(idProducto);

        if (producto == null)
        {
            Console.WriteLine("Producto no encontrado.");
            return;
        }

        // Validamos que haya stock disponible
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

        // Verificamos que no pida más de lo que hay en stock
        if (cantidad > producto.Stock)
        {
            Console.WriteLine($"Stock insuficiente. Solo hay {producto.Stock} unidades.");
            return;
        }

        // Verificamos si el producto ya estaba en el carrito
        // Si ya está, simplemente aumentamos la cantidad
        bool yaEnCarrito = false;
        foreach (ItemCarrito item in carrito)
        {
            if (item.Producto.Id == idProducto)
            {
                // Verificamos que la nueva cantidad total no supere el stock
                if (item.Cantidad + cantidad > producto.Stock)
                {
                    Console.WriteLine($"No puedes agregar más. Ya tienes {item.Cantidad} en el carrito y solo hay {producto.Stock} en stock.");
                    yaEnCarrito = true;
                    break;
                }

                item.Cantidad += cantidad;  // Sumamos la cantidad
                Console.WriteLine($"✓ Cantidad actualizada: {item.Cantidad}x {producto.Nombre}");
                yaEnCarrito = true;
                break;
            }
        }

        // Si no estaba en el carrito, lo agregamos como nuevo item
        if (!yaEnCarrito)
        {
            carrito.Add(new ItemCarrito(producto, cantidad));
            Console.WriteLine($"✓ Agregado al carrito: {cantidad}x {producto.Nombre} - ${producto.Precio * cantidad:N0}");
        }
    }

    // ============================================================
    // FUNCIÓN 7: MOSTRAR CARRITO
    // Muestra todos los productos que el usuario tiene en el carrito
    // con sus cantidades y subtotales
    // ============================================================
    static void MostrarCarrito()
    {
        Console.WriteLine("=== CARRITO DE COMPRAS ===");

        // Si el carrito está vacío
        if (carrito.Count == 0)
        {
            Console.WriteLine("El carrito está vacío.");
            return;
        }

        Console.WriteLine($"{"#",-4} {"Producto",-25} {"Precio",12} {"Cant",6} {"Subtotal",14}");
        Console.WriteLine(new string('-', 65));

        double total = 0;  // Variable para acumular el total

        // Recorremos los items del carrito
        // i es el índice (posición en la lista), empieza en 0
        for (int i = 0; i < carrito.Count; i++)
        {
            ItemCarrito item = carrito[i];  // Accedemos al item por su posición

            Console.WriteLine($"{i + 1,-4} {item.Producto.Nombre,-25} ${item.Producto.Precio,11:N0} {item.Cantidad,6} ${item.Subtotal,13:N0}");
            total += item.Subtotal;  // Acumulamos el total
        }

        Console.WriteLine(new string('-', 65));
        Console.WriteLine($"{"TOTAL:",-48} ${total,13:N0}");
        Console.WriteLine($"Artículos en carrito: {carrito.Count}");
    }

    // ============================================================
    // FUNCIÓN 8: ELIMINAR DEL CARRITO
    // Permite quitar un producto del carrito por su posición
    // ============================================================
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

        if (numero == 0) return;  // El usuario canceló

        // Las listas usan índice 0, pero mostramos desde 1 al usuario
        // Por eso restamos 1 para obtener el índice real
        int indice = numero - 1;
        string nombreProducto = carrito[indice].Producto.Nombre;

        carrito.RemoveAt(indice);  // .RemoveAt() elimina el elemento en esa posición
        Console.WriteLine($"✓ '{nombreProducto}' eliminado del carrito.");
    }

    // ============================================================
    // FUNCIÓN 9: REALIZAR COMPRA / FACTURACIÓN
    // Procesa el carrito: genera la factura, descuenta el stock,
    // guarda la venta en el registro y limpia el carrito
    // ============================================================
    static void RealizarCompra()
    {
        Console.WriteLine("=== PROCESO DE COMPRA ===");

        if (carrito.Count == 0)
        {
            Console.WriteLine("El carrito está vacío. Agrega productos antes de comprar.");
            return;
        }

        // Mostramos el resumen antes de confirmar
        MostrarCarrito();

        Console.Write("\n¿Confirmar compra? (s/n): ");
        string confirmacion = Console.ReadLine().ToLower();

        if (confirmacion != "s")
        {
            Console.WriteLine("Compra cancelada.");
            return;
        }

        // Creamos una nueva venta con los datos actuales
        // Hacemos una COPIA de la lista del carrito para guardarla en la venta
        // Si no copiamos, cuando limpiemos el carrito, la venta también quedaría vacía
        List<ItemCarrito> itemsVenta = new List<ItemCarrito>(carrito);

        Venta nuevaVenta = new Venta(contadorVentas, usuarioActual, itemsVenta);
        contadorVentas++;  // Incrementamos el contador para la próxima venta

        // Descontamos el stock de cada producto comprado
        foreach (ItemCarrito item in carrito)
        {
            // Buscamos el producto en el catálogo y le descontamos el stock
            Producto prod = BuscarPorId(item.Producto.Id);
            if (prod != null)
            {
                prod.Stock -= item.Cantidad;  // -= es igual a: prod.Stock = prod.Stock - item.Cantidad
            }
        }

        // Guardamos la venta en el registro histórico
        registroVentas.Add(nuevaVenta);

        // Limpiamos el carrito para la próxima compra
        carrito.Clear();

        // Generamos e imprimimos la factura
        ImprimirFactura(nuevaVenta);

        // Guardamos la factura en un archivo .txt
        GuardarFacturaEnArchivo(nuevaVenta);
    }

    // ============================================================
    // FUNCIÓN 10: IMPRIMIR FACTURA
    // Genera y muestra la factura de una venta en consola
    // Recibe un objeto Venta y lo formatea bonito
    // ============================================================
    static void ImprimirFactura(Venta venta)
    {
        Console.WriteLine();
        Console.WriteLine("╔══════════════════════════════════════════════════╗");
        Console.WriteLine("║              MINIMERCADO TECH                   ║");
        Console.WriteLine("║          NIT: 900.123.456-7                     ║");
        Console.WriteLine("║       Calle 45 #23-10, Medellín                 ║");
        Console.WriteLine("╠══════════════════════════════════════════════════╣");
        Console.WriteLine($"║  Factura N°: {venta.NumeroVenta:D6}                            ║");
        // :D6 formatea el número con mínimo 6 dígitos (ej: 000001)
        Console.WriteLine($"║  Fecha: {venta.Fecha:dd/MM/yyyy HH:mm:ss}               ║");
        Console.WriteLine($"║  Cliente: {venta.Cliente,-38}║");
        Console.WriteLine("╠══════════════════════════════════════════════════╣");
        Console.WriteLine($"║  {"Producto",-24} {"Cant",4} {"P.Unit",10} {"Subtotal",10} ║");
        Console.WriteLine("╠══════════════════════════════════════════════════╣");

        foreach (ItemCarrito item in venta.Items)
        {
            // Si el nombre es muy largo, lo cortamos para que quepa en la factura
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

    // ============================================================
    // FUNCIÓN 11: GUARDAR FACTURA EN ARCHIVO
    // Guarda la factura en un archivo .txt en el disco
    // Usa System.IO (ya está importado arriba)
    // ============================================================
    static void GuardarFacturaEnArchivo(Venta venta)
    {
        // Creamos el nombre del archivo con el número de venta
        string nombreArchivo = $"factura_{venta.NumeroVenta:D6}.txt";

        // StreamWriter es una "pluma" para escribir en archivos de texto
        // El 'using' asegura que el archivo se cierre correctamente al terminar
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
            {
                escritor.WriteLine($"{item.Producto.Nombre,-25} {item.Cantidad,4} ${item.Subtotal,11:N0}");
            }

            escritor.WriteLine(new string('-', 50));
            escritor.WriteLine($"TOTAL: ${venta.Total:N0}");
        }
        // Al salir del 'using', el archivo se guarda y cierra automáticamente

        Console.WriteLine($"\n✓ Factura guardada en: {nombreArchivo}");
    }

    // ============================================================
    // FUNCIÓN 12: MOSTRAR REGISTRO DE VENTAS
    // Muestra el historial de todas las ventas realizadas
    // ============================================================
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

        double totalGeneral = 0;  // Para calcular el acumulado de todas las ventas

        foreach (Venta v in registroVentas)
        {
            Console.WriteLine($"{v.NumeroVenta:D6,-10} {v.Cliente,-15} {v.Fecha:dd/MM/yyyy HH:mm,-20} {v.Items.Count,10} ${v.Total,13:N0}");
            totalGeneral += v.Total;
        }

        Console.WriteLine(new string('-', 75));
        Console.WriteLine($"Total de ventas: {registroVentas.Count}");
        Console.WriteLine($"Recaudo total:   ${totalGeneral:N0}");
    }

    // ============================================================
    // FUNCIÓN 13: VER DETALLE DE UNA VENTA
    // Permite ver el detalle completo (factura) de una venta pasada
    // ============================================================
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

        // Buscamos la venta con ese número en el registro
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

        // Mostramos la factura de esa venta
        ImprimirFactura(ventaEncontrada);
    }
}
