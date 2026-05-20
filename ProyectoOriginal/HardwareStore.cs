using System;
using System.Collections.Generic;
using System.IO;

class Minimercado
{
    // ============================================================
    // LISTAS PARALELAS - PRODUCTOS
    // ============================================================
    static List<int>    productoIds        = new List<int>();
    static List<string> productoNombres    = new List<string>();
    static List<string> productoCategorias = new List<string>();
    static List<double> productoPrecios    = new List<double>();
    static List<int>    productoStocks     = new List<int>();

    // ============================================================
    // LISTAS PARALELAS - USUARIOS
    // ============================================================
    static List<string> usuarioUsernames = new List<string>();
    static List<string> usuarioPasswords = new List<string>();
    static List<string> usuarioRoles     = new List<string>();
    static List<string> usuarioNombres   = new List<string>();

    // ============================================================
    // LISTAS PARALELAS - CARRITO
    // ============================================================
    static List<int>    carritoIds         = new List<int>();
    static List<string> carritoNombres     = new List<string>();
    static List<double> carritoPrecios     = new List<double>();
    static List<int>    carrritoCantidades = new List<int>();

    // ============================================================
    // LISTAS PARALELAS - REGISTRO DE VENTAS
    // Guardamos los productos de cada venta en strings separados por |
    // ============================================================
    static List<int>    ventaNumeros   = new List<int>();
    static List<string> ventaClientes  = new List<string>();
    static List<string> ventaFechas    = new List<string>();
    static List<double> ventaTotales   = new List<double>();
    static List<string> ventaMetodos   = new List<string>();
    static List<string> ventaDetalles  = new List<string>(); // guarda los productos de cada venta

    // Variables de sesion
    static string usuarioActual  = "";
    static string rolActual      = "";
    static string nombreActual   = "";
    static bool   sesionActiva   = false;
    static int    contadorVentas = 1;

    // ============================================================
    // MAIN
    // ============================================================
    static void Main(string[] args)
    {
        CargarDatosIniciales();
        Login();
        MostrarMenuSegunRol();
    }

    // ============================================================
    // FUNCIÓN: MOSTRAR MENÚ SEGÚN ROL
    // ============================================================
    static void MostrarMenuSegunRol()
    {
        if (rolActual == "admin")
        {
            MenuAdmin();
        }
        else if (rolActual == "contador")
        {
            MenuContador();
        }
        else if (rolActual == "cliente")
        {
            MenuCliente();
        }
    }

    // ============================================================
    // MENÚ: ADMINISTRADOR
    // ============================================================
    static void MenuAdmin()
    {
        bool salir = false;

        while (salir == false)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║        MINIMERCADO TECH              ║");
            Console.WriteLine("║       Panel Administrador            ║");
            Console.WriteLine($"║  Usuario: {nombreActual,-26}║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║  --- PRODUCTOS ---                   ║");
            Console.WriteLine("║  1.  Ver Catalogo                    ║");
            Console.WriteLine("║  2.  Agregar Producto                ║");
            Console.WriteLine("║  3.  Editar Producto                 ║");
            Console.WriteLine("║  4.  Eliminar Producto               ║");
            Console.WriteLine("║  5.  Buscar Producto                 ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║  --- VENTAS ---                      ║");
            Console.WriteLine("║  6.  Ver Registro de Ventas          ║");
            Console.WriteLine("║  7.  Estadisticas y Balance          ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║  --- USUARIOS ---                    ║");
            Console.WriteLine("║  8.  Ver Usuarios                    ║");
            Console.WriteLine("║  9.  Agregar Usuario                 ║");
            Console.WriteLine("║  10. Editar Usuario                  ║");
            Console.WriteLine("║  11. Eliminar Usuario                ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║  0.  Cerrar Sesion                   ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.Write("Seleccione una opcion: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":  MostrarCatalogo();        break;
                case "2":  AgregarProductoManual();  break;
                case "3":  EditarProducto();         break;
                case "4":  EliminarProducto();       break;
                case "5":  BuscarProducto();         break;
                case "6":  MostrarRegistroVentas();  break;
                case "7":  MostrarEstadisticas();    break;
                case "8":  MostrarUsuarios();        break;
                case "9":  AgregarUsuario();         break;
                case "10": EditarUsuario();          break;
                case "11": EliminarUsuario();        break;
                case "0":
                    CerrarSesion();
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opcion no valida.");
                    break;
            }

            if (salir == false)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
    }

    // ============================================================
    // MENÚ: CONTADOR
    // Solo reportes y estadísticas
    // ============================================================
    static void MenuContador()
    {
        bool salir = false;

        while (salir == false)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║        MINIMERCADO TECH              ║");
            Console.WriteLine("║         Panel Contador               ║");
            Console.WriteLine($"║  Usuario: {nombreActual,-26}║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║  1. Ver Catalogo e Inventario        ║");
            Console.WriteLine("║  2. Ver Registro de Ventas           ║");
            Console.WriteLine("║  3. Estadisticas y Balance           ║");
            Console.WriteLine("║  0. Cerrar Sesion                    ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.Write("Seleccione una opcion: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1": MostrarCatalogo();       break;
                case "2": MostrarRegistroVentas(); break;
                case "3": MostrarEstadisticas();   break;
                case "0":
                    CerrarSesion();
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opcion no valida.");
                    break;
            }

            if (salir == false)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
    }

    // ============================================================
    // MENÚ: CLIENTE
    // Puede comprar y también registrar ventas (hacer de cajero)
    // ============================================================
    static void MenuCliente()
    {
        bool salir = false;

        while (salir == false)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║        MINIMERCADO TECH              ║");
            Console.WriteLine("║         Panel Cliente                ║");
            Console.WriteLine($"║  Usuario: {nombreActual,-26}║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║  --- TIENDA ---                      ║");
            Console.WriteLine("║  1. Ver Catalogo                     ║");
            Console.WriteLine("║  2. Buscar Producto                  ║");
            Console.WriteLine("║  3. Agregar al Carrito               ║");
            Console.WriteLine("║  4. Ver Carrito                      ║");
            Console.WriteLine("║  5. Eliminar del Carrito             ║");
            Console.WriteLine("║  6. Realizar Compra                  ║");
            Console.WriteLine("║  0. Cerrar Sesion                    ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.Write("Seleccione una opcion: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1": MostrarCatalogo();    break;
                case "2": BuscarProducto();     break;
                case "3": AgregarAlCarrito();   break;
                case "4": MostrarCarrito();     break;
                case "5": EliminarDelCarrito(); break;
                case "6": RealizarCompra();     break;
                case "0":
                    CerrarSesion();
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opcion no valida.");
                    break;
            }

            if (salir == false)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
    }

    // ============================================================
    // FUNCIÓN: CARGAR DATOS INICIALES
    // ============================================================
    static void CargarDatosIniciales()
    {
        // Roles disponibles: admin, contador, cliente
        AgregarUsuarioInterno("admin",    "admin123", "admin",    "Administrador");
        AgregarUsuarioInterno("contador1","cont123",  "contador", "Maria Lopez");
        AgregarUsuarioInterno("cliente1", "pass123",  "cliente",  "Carlos Garcia");
        AgregarUsuarioInterno("carlos",   "carlos456","cliente",  "Carlos Ramirez");

        AgregarProducto(1,  "Teclado Mecanico RGB",     "Perifericos", 250000,  15);
        AgregarProducto(2,  "Mouse Gamer 16000 DPI",    "Perifericos", 180000,  20);
        AgregarProducto(3,  "Monitor 24 Full HD",       "Monitores",   850000,   8);
        AgregarProducto(4,  "RAM DDR4 16GB",            "Hardware",    320000,  25);
        AgregarProducto(5,  "SSD 500GB NVMe",           "Hardware",    280000,  18);
        AgregarProducto(6,  "Tarjeta Grafica RTX 3060", "Hardware",   1950000,   5);
        AgregarProducto(7,  "Audifonos Gaming 7.1",     "Perifericos", 220000,  12);
        AgregarProducto(8,  "Webcam Full HD 1080p",     "Perifericos", 150000,  10);
        AgregarProducto(9,  "Procesador Ryzen 5 5600",  "Hardware",    780000,   7);
        AgregarProducto(10, "Fuente de Poder 650W",     "Hardware",    340000,   9);
    }

    static void AgregarProducto(int id, string nombre, string categoria, double precio, int stock)
    {
        productoIds.Add(id);
        productoNombres.Add(nombre);
        productoCategorias.Add(categoria);
        productoPrecios.Add(precio);
        productoStocks.Add(stock);
    }

    static void AgregarUsuarioInterno(string username, string password, string rol, string nombre)
    {
        usuarioUsernames.Add(username);
        usuarioPasswords.Add(password);
        usuarioRoles.Add(rol);
        usuarioNombres.Add(nombre);
    }

    // ============================================================
    // FUNCIÓN: LOGIN
    // ============================================================
    static void Login()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════╗");
        Console.WriteLine("║     MINIMERCADO TECH  v1.0       ║");
        Console.WriteLine("║    Tu tienda de computadoras     ║");
        Console.WriteLine("╚══════════════════════════════════╝");
        Console.WriteLine("=== INICIO DE SESION ===");

        int intentos    = 0;
        int maxIntentos = 3;

        while (sesionActiva == false && intentos < maxIntentos)
        {
            Console.Write("Usuario: ");
            string username = Console.ReadLine();

            Console.Write("Contrasena: ");
            string password = LeerPasswordOculta();

            int indiceEncontrado = -1;

            for (int i = 0; i < usuarioUsernames.Count; i++)
            {
                if (usuarioUsernames[i].ToLower() == username.ToLower() && usuarioPasswords[i] == password)
                {
                    indiceEncontrado = i;
                    break;
                }
            }

            if (indiceEncontrado != -1)
            {
                sesionActiva  = true;
                usuarioActual = usuarioUsernames[indiceEncontrado];
                rolActual     = usuarioRoles[indiceEncontrado];
                nombreActual  = usuarioNombres[indiceEncontrado];
                Console.WriteLine($"\nBienvenido, {nombreActual}! (Rol: {rolActual})");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
            else
            {
                intentos++;
                Console.WriteLine($"Usuario o contrasena incorrectos. Intentos restantes: {maxIntentos - intentos}");
            }
        }

        if (sesionActiva == false)
        {
            Console.WriteLine("Demasiados intentos fallidos. El sistema se cerrara.");
            Environment.Exit(0);
        }
    }

    // ============================================================
    // FUNCIÓN: LEER PASSWORD OCULTA
    // ============================================================
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
            else if (tecla.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b");
            }
            else if (tecla.Key != ConsoleKey.Backspace)
            {
                password = password + tecla.KeyChar;
                Console.Write("*");
            }
        }

        return password;
    }

    // ============================================================
    // FUNCIÓN: CERRAR SESIÓN
    // ============================================================
    static void CerrarSesion()
    {
        Console.WriteLine($"Cerrando sesion de {nombreActual}...");
        sesionActiva  = false;
        usuarioActual = "";
        rolActual     = "";
        nombreActual  = "";
        LimpiarCarrito();
        Console.WriteLine("Sesion cerrada. Hasta pronto!");
    }

    // ============================================================
    // FUNCIÓN: MOSTRAR CATÁLOGO
    // ============================================================
    static void MostrarCatalogo()
    {
        Console.WriteLine("=== CATALOGO DE PRODUCTOS ===");
        Console.WriteLine($"{"ID",-5} {"Nombre",-28} {"Categoria",-15} {"Precio",12} {"Stock",6}");
        Console.WriteLine(new string('-', 70));

        for (int i = 0; i < productoIds.Count; i++)
        {
            string alertaStock = "";

            if (productoStocks[i] == 0)
            {
                alertaStock = " [AGOTADO]";
            }
            else if (productoStocks[i] <= 3)
            {
                alertaStock = " [STOCK BAJO]";
            }

            Console.WriteLine($"{productoIds[i],-5} {productoNombres[i],-28} {productoCategorias[i],-15} ${productoPrecios[i],11:N0} {productoStocks[i],6}{alertaStock}");
        }

        Console.WriteLine(new string('-', 70));
        Console.WriteLine($"Total de productos: {productoIds.Count}");
    }

    // ============================================================
    // FUNCIÓN: BUSCAR PRODUCTO
    // ============================================================
    static void BuscarProducto()
    {
        Console.WriteLine("=== BUSCADOR DE PRODUCTOS ===");
        Console.Write("Ingresa nombre o categoria a buscar: ");
        string termino = Console.ReadLine().ToLower();

        bool encontroAlgo = false;

        Console.WriteLine(new string('-', 70));

        for (int i = 0; i < productoIds.Count; i++)
        {
            bool coincideNombre    = productoNombres[i].ToLower().Contains(termino);
            bool coincideCategoria = productoCategorias[i].ToLower().Contains(termino);

            if (coincideNombre || coincideCategoria)
            {
                Console.WriteLine($"{productoIds[i],-5} {productoNombres[i],-28} {productoCategorias[i],-15} ${productoPrecios[i],11:N0} Stock: {productoStocks[i]}");
                encontroAlgo = true;
            }
        }

        if (encontroAlgo == false)
        {
            Console.WriteLine($"No se encontraron productos con '{termino}'");
        }

        Console.WriteLine(new string('-', 70));
    }

    // ============================================================
    // FUNCIÓN AUXILIAR: BUSCAR ÍNDICE POR ID
    // ============================================================
    static int BuscarIndicePorId(int id)
    {
        for (int i = 0; i < productoIds.Count; i++)
        {
            if (productoIds[i] == id)
            {
                return i;
            }
        }
        return -1;
    }

    // ============================================================
    // CRUD - AGREGAR PRODUCTO
    // ============================================================
    static void AgregarProductoManual()
    {
        Console.WriteLine("=== AGREGAR NUEVO PRODUCTO ===");
        MostrarCatalogo();

        Console.Write("\nNombre del producto: ");
        string nombre = Console.ReadLine();

        if (nombre == "")
        {
            Console.WriteLine("El nombre no puede estar vacio.");
            return;
        }

        Console.Write("Categoria (Hardware / Perifericos / Monitores): ");
        string categoria = Console.ReadLine();

        if (categoria == "")
        {
            Console.WriteLine("La categoria no puede estar vacia.");
            return;
        }

        Console.Write("Precio: ");
        if (double.TryParse(Console.ReadLine(), out double precio) == false || precio <= 0)
        {
            Console.WriteLine("Precio invalido.");
            return;
        }

        Console.Write("Stock inicial: ");
        if (int.TryParse(Console.ReadLine(), out int stock) == false || stock < 0)
        {
            Console.WriteLine("Stock invalido.");
            return;
        }

        int nuevoId = productoIds[productoIds.Count - 1] + 1;
        AgregarProducto(nuevoId, nombre, categoria, precio, stock);
        Console.WriteLine($"Producto '{nombre}' agregado con ID {nuevoId}.");
    }

    // ============================================================
    // CRUD - EDITAR PRODUCTO
    // ============================================================
    static void EditarProducto()
    {
        Console.WriteLine("=== EDITAR PRODUCTO ===");
        MostrarCatalogo();

        Console.Write("\nIngresa el ID del producto a editar: ");
        if (int.TryParse(Console.ReadLine(), out int id) == false)
        {
            Console.WriteLine("ID invalido.");
            return;
        }

        int indice = BuscarIndicePorId(id);

        if (indice == -1)
        {
            Console.WriteLine("Producto no encontrado.");
            return;
        }

        Console.WriteLine($"\nEditando: {productoNombres[indice]}");
        Console.WriteLine("(Deja vacio y presiona Enter si no quieres cambiar ese campo)");

        Console.Write($"Nuevo nombre [{productoNombres[indice]}]: ");
        string nuevoNombre = Console.ReadLine();
        if (nuevoNombre != "")
        {
            productoNombres[indice] = nuevoNombre;
        }

        Console.Write($"Nueva categoria [{productoCategorias[indice]}]: ");
        string nuevaCategoria = Console.ReadLine();
        if (nuevaCategoria != "")
        {
            productoCategorias[indice] = nuevaCategoria;
        }

        Console.Write($"Nuevo precio [{productoPrecios[indice]:N0}]: ");
        string textoPrecio = Console.ReadLine();
        if (textoPrecio != "")
        {
            if (double.TryParse(textoPrecio, out double nuevoPrecio) == false || nuevoPrecio <= 0)
            {
                Console.WriteLine("Precio invalido, no se actualizo.");
            }
            else
            {
                productoPrecios[indice] = nuevoPrecio;
            }
        }

        Console.Write($"Nuevo stock [{productoStocks[indice]}]: ");
        string textoStock = Console.ReadLine();
        if (textoStock != "")
        {
            if (int.TryParse(textoStock, out int nuevoStock) == false || nuevoStock < 0)
            {
                Console.WriteLine("Stock invalido, no se actualizo.");
            }
            else
            {
                productoStocks[indice] = nuevoStock;
            }
        }

        Console.WriteLine("Producto actualizado correctamente.");
    }

    // ============================================================
    // CRUD - ELIMINAR PRODUCTO
    // ============================================================
    static void EliminarProducto()
    {
        Console.WriteLine("=== ELIMINAR PRODUCTO ===");
        MostrarCatalogo();

        Console.Write("\nIngresa el ID del producto a eliminar: ");
        if (int.TryParse(Console.ReadLine(), out int id) == false)
        {
            Console.WriteLine("ID invalido.");
            return;
        }

        int indice = BuscarIndicePorId(id);

        if (indice == -1)
        {
            Console.WriteLine("Producto no encontrado.");
            return;
        }

        Console.Write($"Seguro que deseas eliminar '{productoNombres[indice]}'? (s/n): ");
        string confirmacion = Console.ReadLine().ToLower();

        if (confirmacion != "s")
        {
            Console.WriteLine("Eliminacion cancelada.");
            return;
        }

        string nombreEliminado = productoNombres[indice];

        productoIds.RemoveAt(indice);
        productoNombres.RemoveAt(indice);
        productoCategorias.RemoveAt(indice);
        productoPrecios.RemoveAt(indice);
        productoStocks.RemoveAt(indice);

        Console.WriteLine($"Producto '{nombreEliminado}' eliminado.");
    }

    // ============================================================
    // GESTIÓN DE USUARIOS - VER USUARIOS
    // ============================================================
    static void MostrarUsuarios()
    {
        Console.WriteLine("=== LISTA DE USUARIOS ===");
        Console.WriteLine($"{"#",-4} {"Username",-15} {"Nombre",-20} {"Rol",-12}");
        Console.WriteLine(new string('-', 55));

        for (int i = 0; i < usuarioUsernames.Count; i++)
        {
            Console.WriteLine($"{i + 1,-4} {usuarioUsernames[i],-15} {usuarioNombres[i],-20} {usuarioRoles[i],-12}");
        }

        Console.WriteLine(new string('-', 55));
        Console.WriteLine($"Total usuarios: {usuarioUsernames.Count}");
    }

    // ============================================================
    // GESTIÓN DE USUARIOS - AGREGAR USUARIO
    // ============================================================
    static void AgregarUsuario()
    {
        Console.WriteLine("=== AGREGAR USUARIO ===");

        Console.Write("Username: ");
        string username = Console.ReadLine();

        if (username == "")
        {
            Console.WriteLine("El username no puede estar vacio.");
            return;
        }

        for (int i = 0; i < usuarioUsernames.Count; i++)
        {
            if (usuarioUsernames[i].ToLower() == username.ToLower())
            {
                Console.WriteLine("Ese username ya existe.");
                return;
            }
        }

        Console.Write("Nombre completo: ");
        string nombre = Console.ReadLine();

        if (nombre == "")
        {
            Console.WriteLine("El nombre no puede estar vacio.");
            return;
        }

        Console.WriteLine("Roles disponibles: admin / contador / cliente");
        Console.Write("Rol: ");
        string rol = Console.ReadLine().ToLower();

        if (rol != "admin" && rol != "contador" && rol != "cliente")
        {
            Console.WriteLine("Rol invalido.");
            return;
        }

        Console.Write("Contrasena: ");
        string password = LeerPasswordOculta();

        if (password == "")
        {
            Console.WriteLine("La contrasena no puede estar vacia.");
            return;
        }

        AgregarUsuarioInterno(username, password, rol, nombre);
        Console.WriteLine($"Usuario '{username}' agregado con rol '{rol}'.");
    }

    // ============================================================
    // GESTIÓN DE USUARIOS - EDITAR USUARIO
    // ============================================================
    static void EditarUsuario()
    {
        Console.WriteLine("=== EDITAR USUARIO ===");
        MostrarUsuarios();

        Console.Write("\nIngresa el username a editar: ");
        string username = Console.ReadLine();

        int indice = -1;
        for (int i = 0; i < usuarioUsernames.Count; i++)
        {
            if (usuarioUsernames[i].ToLower() == username.ToLower())
            {
                indice = i;
                break;
            }
        }

        if (indice == -1)
        {
            Console.WriteLine("Usuario no encontrado.");
            return;
        }

        Console.WriteLine($"\nEditando usuario: {usuarioUsernames[indice]}");
        Console.WriteLine("(Deja vacio y presiona Enter si no quieres cambiar ese campo)");

        // Nombre completo
        Console.Write($"Nuevo nombre completo [{usuarioNombres[indice]}]: ");
        string nuevoNombre = Console.ReadLine();
        if (nuevoNombre != "")
        {
            usuarioNombres[indice] = nuevoNombre;
        }

        // Rol
        Console.Write($"Nuevo rol [{usuarioRoles[indice]}] (admin / contador / cliente): ");
        string nuevoRol = Console.ReadLine().ToLower();
        if (nuevoRol != "")
        {
            if (nuevoRol != "admin" && nuevoRol != "contador" && nuevoRol != "cliente")
            {
                Console.WriteLine("Rol invalido, no se actualizo.");
            }
            else
            {
                // No se puede cambiar el rol del usuario activo
                if (usuarioUsernames[indice].ToLower() == usuarioActual.ToLower())
                {
                    Console.WriteLine("No puedes cambiar tu propio rol.");
                }
                else
                {
                    usuarioRoles[indice] = nuevoRol;
                }
            }
        }

        // Contraseña
        Console.Write("Nueva contrasena (Enter para no cambiar): ");
        string nuevaPassword = LeerPasswordOculta();
        if (nuevaPassword != "")
        {
            usuarioPasswords[indice] = nuevaPassword;
        }

        Console.WriteLine("Usuario actualizado correctamente.");
    }

    // ============================================================
    // GESTIÓN DE USUARIOS - ELIMINAR USUARIO
    // ============================================================
    static void EliminarUsuario()
    {
        Console.WriteLine("=== ELIMINAR USUARIO ===");
        MostrarUsuarios();

        Console.Write("\nIngresa el username a eliminar: ");
        string username = Console.ReadLine();

        if (username.ToLower() == usuarioActual.ToLower())
        {
            Console.WriteLine("No puedes eliminar el usuario con el que estas logueado.");
            return;
        }

        int indice = -1;
        for (int i = 0; i < usuarioUsernames.Count; i++)
        {
            if (usuarioUsernames[i].ToLower() == username.ToLower())
            {
                indice = i;
                break;
            }
        }

        if (indice == -1)
        {
            Console.WriteLine("Usuario no encontrado.");
            return;
        }

        Console.Write($"Seguro que deseas eliminar al usuario '{username}'? (s/n): ");
        string confirmacion = Console.ReadLine().ToLower();

        if (confirmacion != "s")
        {
            Console.WriteLine("Eliminacion cancelada.");
            return;
        }

        usuarioUsernames.RemoveAt(indice);
        usuarioPasswords.RemoveAt(indice);
        usuarioRoles.RemoveAt(indice);
        usuarioNombres.RemoveAt(indice);

        Console.WriteLine($"Usuario '{username}' eliminado.");
    }

    // ============================================================
    // FUNCIÓN: AGREGAR AL CARRITO
    // CORRECCIÓN: ahora pregunta si quiere seguir agregando
    // ============================================================
    static void AgregarAlCarrito()
    {
        bool seguirAgregando = true;

        while (seguirAgregando == true)
        {
            Console.WriteLine("=== AGREGAR AL CARRITO ===");
            MostrarCatalogo();

            Console.Write("\nID del producto a agregar (0 para cancelar): ");
            if (int.TryParse(Console.ReadLine(), out int id) == false)
            {
                Console.WriteLine("ID invalido.");
                break;
            }

            if (id == 0)
            {
                break;
            }

            int indice = BuscarIndicePorId(id);

            if (indice == -1)
            {
                Console.WriteLine("Producto no encontrado.");
            }
            else if (productoStocks[indice] == 0)
            {
                Console.WriteLine($"'{productoNombres[indice]}' esta agotado.");
            }
            else
            {
                Console.Write($"Cantidad (disponibles: {productoStocks[indice]}): ");
                if (int.TryParse(Console.ReadLine(), out int cantidad) == false || cantidad <= 0)
                {
                    Console.WriteLine("Cantidad invalida.");
                }
                else if (cantidad > productoStocks[indice])
                {
                    Console.WriteLine($"Stock insuficiente. Solo hay {productoStocks[indice]} unidades.");
                }
                else
                {
                    bool yaEstaba = false;

                    for (int i = 0; i < carritoIds.Count; i++)
                    {
                        if (carritoIds[i] == id)
                        {
                            int totalSiAgrega = carrritoCantidades[i] + cantidad;

                            if (totalSiAgrega > productoStocks[indice])
                            {
                                Console.WriteLine($"No puedes agregar {cantidad} mas. Ya tienes {carrritoCantidades[i]} en el carrito y solo hay {productoStocks[indice]} en stock.");
                            }
                            else
                            {
                                carrritoCantidades[i] = carrritoCantidades[i] + cantidad;
                                Console.WriteLine($"Cantidad actualizada: {carrritoCantidades[i]}x {carritoNombres[i]}");
                            }

                            yaEstaba = true;
                            break;
                        }
                    }

                    if (yaEstaba == false)
                    {
                        carritoIds.Add(productoIds[indice]);
                        carritoNombres.Add(productoNombres[indice]);
                        carritoPrecios.Add(productoPrecios[indice]);
                        carrritoCantidades.Add(cantidad);
                        Console.WriteLine($"Agregado: {cantidad}x {productoNombres[indice]} - ${productoPrecios[indice] * cantidad:N0}");
                    }
                }
            }

            // Mostramos el carrito actual
            Console.WriteLine();
            MostrarCarrito();
            Console.WriteLine();

            // Preguntamos si quiere seguir agregando
            Console.Write("Deseas agregar otro producto? (s/n): ");
            string respuesta = Console.ReadLine().ToLower();

            if (respuesta != "s")
            {
                seguirAgregando = false;
            }
        }
    }

    // ============================================================
    // FUNCIÓN: MOSTRAR CARRITO
    // ============================================================
    static void MostrarCarrito()
    {
        Console.WriteLine("=== CARRITO DE COMPRAS ===");

        if (carritoIds.Count == 0)
        {
            Console.WriteLine("El carrito esta vacio.");
            return;
        }

        Console.WriteLine($"{"#",-4} {"Producto",-28} {"Precio",12} {"Cant",6} {"Subtotal",14}");
        Console.WriteLine(new string('-', 68));

        double total = 0;

        for (int i = 0; i < carritoIds.Count; i++)
        {
            double subtotal = carritoPrecios[i] * carrritoCantidades[i];
            Console.WriteLine($"{i + 1,-4} {carritoNombres[i],-28} ${carritoPrecios[i],11:N0} {carrritoCantidades[i],6} ${subtotal,13:N0}");
            total = total + subtotal;
        }

        Console.WriteLine(new string('-', 68));
        Console.WriteLine($"{"TOTAL:",-52} ${total,13:N0}");
    }

    // ============================================================
    // FUNCIÓN: ELIMINAR DEL CARRITO
    // ============================================================
    static void EliminarDelCarrito()
    {
        if (carritoIds.Count == 0)
        {
            Console.WriteLine("El carrito esta vacio.");
            return;
        }

        MostrarCarrito();
        Console.Write("\nNumero del producto a eliminar (0 para cancelar): ");

        if (int.TryParse(Console.ReadLine(), out int numero) == false || numero < 0 || numero > carritoIds.Count)
        {
            Console.WriteLine("Numero invalido.");
            return;
        }

        if (numero == 0)
        {
            return;
        }

        int indice = numero - 1;
        string nombre = carritoNombres[indice];

        carritoIds.RemoveAt(indice);
        carritoNombres.RemoveAt(indice);
        carritoPrecios.RemoveAt(indice);
        carrritoCantidades.RemoveAt(indice);

        Console.WriteLine($"'{nombre}' eliminado del carrito.");
    }

    // ============================================================
    // FUNCIÓN AUXILIAR: CALCULAR TOTAL DEL CARRITO
    // ============================================================
    static double CalcularTotalCarrito()
    {
        double total = 0;

        for (int i = 0; i < carritoIds.Count; i++)
        {
            total = total + (carritoPrecios[i] * carrritoCantidades[i]);
        }

        return total;
    }

    // ============================================================
    // FUNCIÓN AUXILIAR: LIMPIAR CARRITO
    // ============================================================
    static void LimpiarCarrito()
    {
        carritoIds.Clear();
        carritoNombres.Clear();
        carritoPrecios.Clear();
        carrritoCantidades.Clear();
    }

    // ============================================================
    // FUNCIÓN: MÉTODO DE PAGO
    // ============================================================
    static string SeleccionarMetodoPago(double total)
    {
        Console.WriteLine("\n=== METODO DE PAGO ===");
        Console.WriteLine($"Total a pagar: ${total:N0}");
        Console.WriteLine("1. Efectivo");
        Console.WriteLine("2. Tarjeta de Credito");
        Console.WriteLine("3. Tarjeta de Debito");
        Console.WriteLine("4. Transferencia Bancaria");
        Console.Write("Seleccione metodo de pago: ");

        string opcion = Console.ReadLine();
        string metodo = "";

        if (opcion == "1")
        {
            metodo = "Efectivo";

            Console.Write("Ingrese el valor recibido: $");
            if (double.TryParse(Console.ReadLine(), out double valorRecibido) == false || valorRecibido < total)
            {
                Console.WriteLine("Valor insuficiente. Se cancela el pago.");
                return "";
            }

            double cambio = valorRecibido - total;
            Console.WriteLine($"Cambio a devolver: ${cambio:N0}");
        }
        else if (opcion == "2")
        {
            metodo = "Tarjeta de Credito";
            Console.WriteLine("Pago con tarjeta de credito procesado.");
        }
        else if (opcion == "3")
        {
            metodo = "Tarjeta de Debito";
            Console.WriteLine("Pago con tarjeta de debito procesado.");
        }
        else if (opcion == "4")
        {
            metodo = "Transferencia Bancaria";
            Console.WriteLine("Cuenta: 123-456789-00  Banco: Bancolombia");
            Console.WriteLine("Transferencia registrada.");
        }
        else
        {
            Console.WriteLine("Opcion invalida.");
            return "";
        }

        return metodo;
    }

    // ============================================================
    // FUNCIÓN: REALIZAR COMPRA
    // CORRECCIÓN: guardamos el detalle de los productos ANTES
    // de limpiar el carrito, así el registro queda completo
    // ============================================================
    static void RealizarCompra()
    {
        Console.WriteLine("=== PROCESO DE COMPRA ===");

        if (carritoIds.Count == 0)
        {
            Console.WriteLine("El carrito esta vacio.");
            return;
        }

        MostrarCarrito();

        Console.Write("\nDeseas confirmar la compra? (s/n): ");
        string confirmacion = Console.ReadLine().ToLower();

        if (confirmacion != "s")
        {
            Console.WriteLine("Compra cancelada.");
            return;
        }

        double total  = CalcularTotalCarrito();
        string metodo = SeleccionarMetodoPago(total);

        if (metodo == "")
        {
            return;
        }

        string fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        // CORRECCIÓN: construimos el detalle de la venta ANTES de limpiar el carrito
        // Guardamos cada producto como "nombre|cantidad|precio" separados por coma
        // Así podemos recuperar los datos después sin necesidad del carrito
        string detalle = "";
        for (int i = 0; i < carritoIds.Count; i++)
        {
            if (i > 0)
            {
                detalle = detalle + ",";
            }
            detalle = detalle + carritoNombres[i] + "|" + carrritoCantidades[i] + "|" + carritoPrecios[i];
        }

        // Guardamos la venta en el registro
        ventaNumeros.Add(contadorVentas);
        ventaClientes.Add(usuarioActual);
        ventaFechas.Add(fecha);
        ventaTotales.Add(total);
        ventaMetodos.Add(metodo);
        ventaDetalles.Add(detalle);

        // Descontamos el stock
        for (int i = 0; i < carritoIds.Count; i++)
        {
            int indiceProducto = BuscarIndicePorId(carritoIds[i]);

            if (indiceProducto != -1)
            {
                productoStocks[indiceProducto] = productoStocks[indiceProducto] - carrritoCantidades[i];
            }
        }

        // Imprimimos y guardamos la factura ANTES de limpiar el carrito
        ImprimirFactura(contadorVentas, usuarioActual, fecha, total, metodo);
        GuardarFacturaEnArchivo(contadorVentas, usuarioActual, fecha, total, metodo);

        contadorVentas = contadorVentas + 1;

        // Limpiamos el carrito al final
        LimpiarCarrito();
    }

    // ============================================================
    // FUNCIÓN: IMPRIMIR FACTURA
    // ============================================================
    static void ImprimirFactura(int numero, string cliente, string fecha, double total, string metodo)
    {
        Console.WriteLine();
        Console.WriteLine("╔══════════════════════════════════════════════════╗");
        Console.WriteLine("║              MINIMERCADO TECH                   ║");
        Console.WriteLine("║          NIT: 900.123.456-7                     ║");
        Console.WriteLine("║       Calle 45 #23-10, Medellin                 ║");
        Console.WriteLine("╠══════════════════════════════════════════════════╣");
        Console.WriteLine($"║  Factura N: {numero:D6}                             ║");
        Console.WriteLine($"║  Fecha: {fecha}                      ║");
        Console.WriteLine($"║  Cliente: {cliente,-38}║");
        Console.WriteLine($"║  Pago: {metodo,-41}║");
        Console.WriteLine("╠══════════════════════════════════════════════════╣");
        Console.WriteLine($"║  {"Producto",-24} {"Cant",4} {"P.Unit",10} {"Subtotal",9} ║");
        Console.WriteLine("╠══════════════════════════════════════════════════╣");

        // Usamos el carrito directamente porque aún no se ha limpiado
        for (int i = 0; i < carritoIds.Count; i++)
        {
            string nombre;

            if (carritoNombres[i].Length > 24)
            {
                nombre = carritoNombres[i].Substring(0, 21) + "...";
            }
            else
            {
                nombre = carritoNombres[i];
            }

            double subtotal = carritoPrecios[i] * carrritoCantidades[i];
            Console.WriteLine($"║  {nombre,-24} {carrritoCantidades[i],4} {carritoPrecios[i],9:N0} {subtotal,9:N0} ║");
        }

        Console.WriteLine("╠══════════════════════════════════════════════════╣");
        Console.WriteLine($"║  {"TOTAL A PAGAR:",-35} ${total,10:N0} ║");
        Console.WriteLine("╚══════════════════════════════════════════════════╝");
        Console.WriteLine("     Gracias por su compra en MinimercadoTech!     ");
    }

    // ============================================================
    // FUNCIÓN: GUARDAR FACTURA EN ARCHIVO
    // ============================================================
    static void GuardarFacturaEnArchivo(int numero, string cliente, string fecha, double total, string metodo)
    {
        string nombreArchivo = $"factura_{numero:D6}.txt";

        using (StreamWriter escritor = new StreamWriter(nombreArchivo))
        {
            escritor.WriteLine("MINIMERCADO TECH");
            escritor.WriteLine($"Factura N: {numero:D6}");
            escritor.WriteLine($"Fecha: {fecha}");
            escritor.WriteLine($"Cliente: {cliente}");
            escritor.WriteLine($"Metodo de pago: {metodo}");
            escritor.WriteLine(new string('-', 50));

            for (int i = 0; i < carritoIds.Count; i++)
            {
                double subtotal = carritoPrecios[i] * carrritoCantidades[i];
                escritor.WriteLine($"{carritoNombres[i],-25} x{carrritoCantidades[i]}  ${subtotal:N0}");
            }

            escritor.WriteLine(new string('-', 50));
            escritor.WriteLine($"TOTAL: ${total:N0}");
        }

        Console.WriteLine($"Factura guardada en: {nombreArchivo}");
    }

    // ============================================================
    // FUNCIÓN: MOSTRAR REGISTRO DE VENTAS
    // CORRECCIÓN: ahora usa ventaDetalles que se guarda antes
    // de limpiar el carrito, así siempre muestra datos correctos
    // ============================================================
    static void MostrarRegistroVentas()
    {
        Console.WriteLine("=== REGISTRO DE VENTAS ===");

        if (ventaNumeros.Count == 0)
        {
            Console.WriteLine("No hay ventas registradas.");
            return;
        }

        Console.WriteLine($"{"N° Venta",-10} {"Cliente",-15} {"Fecha",-22} {"Metodo",-22} {"Total",14}");
        Console.WriteLine(new string('-', 87));

        double totalGeneral = 0;

        for (int i = 0; i < ventaNumeros.Count; i++)
        {
            Console.WriteLine($"{ventaNumeros[i]:D6,-10} {ventaClientes[i],-15} {ventaFechas[i],-22} {ventaMetodos[i],-22} ${ventaTotales[i],13:N0}");
            totalGeneral = totalGeneral + ventaTotales[i];
        }

        Console.WriteLine(new string('-', 87));
        Console.WriteLine($"Total de ventas: {ventaNumeros.Count}");
        Console.WriteLine($"Recaudo total:   ${totalGeneral:N0}");

        // Opcion de ver detalle de una venta
        Console.Write("\nDeseas ver el detalle de una venta? (s/n): ");
        string respuesta = Console.ReadLine().ToLower();

        if (respuesta == "s")
        {
            Console.Write("Ingresa el numero de venta: ");
            if (int.TryParse(Console.ReadLine(), out int numBuscado))
            {
                int indiceVenta = -1;
                for (int i = 0; i < ventaNumeros.Count; i++)
                {
                    if (ventaNumeros[i] == numBuscado)
                    {
                        indiceVenta = i;
                        break;
                    }
                }

                if (indiceVenta == -1)
                {
                    Console.WriteLine("Venta no encontrada.");
                }
                else
                {
                    MostrarDetalleVenta(indiceVenta);
                }
            }
        }
    }

    // ============================================================
    // FUNCIÓN AUXILIAR: MOSTRAR DETALLE DE UNA VENTA
    // Reconstruye la factura usando ventaDetalles
    // ============================================================
    static void MostrarDetalleVenta(int indiceVenta)
    {
        Console.WriteLine();
        Console.WriteLine($"--- Detalle Venta N° {ventaNumeros[indiceVenta]:D6} ---");
        Console.WriteLine($"Cliente : {ventaClientes[indiceVenta]}");
        Console.WriteLine($"Fecha   : {ventaFechas[indiceVenta]}");
        Console.WriteLine($"Pago    : {ventaMetodos[indiceVenta]}");
        Console.WriteLine(new string('-', 55));
        Console.WriteLine($"{"Producto",-25} {"Cant",5} {"P.Unit",12} {"Subtotal",12}");
        Console.WriteLine(new string('-', 55));

        // El detalle está guardado como "nombre|cant|precio,nombre|cant|precio,..."
        // Primero separamos por coma para obtener cada producto
        string detalle = ventaDetalles[indiceVenta];
        string[] productos = detalle.Split(',');

        for (int i = 0; i < productos.Length; i++)
        {
            // Luego separamos cada producto por | para obtener sus datos
            string[] partes = productos[i].Split('|');

            string nombreProd = partes[0];
            int    cantProd   = int.Parse(partes[1]);
            double precioProd = double.Parse(partes[2]);
            double subtotal   = cantProd * precioProd;

            Console.WriteLine($"{nombreProd,-25} {cantProd,5} ${precioProd,11:N0} ${subtotal,11:N0}");
        }

        Console.WriteLine(new string('-', 55));
        Console.WriteLine($"{"TOTAL:",-44} ${ventaTotales[indiceVenta],11:N0}");
    }

    // ============================================================
    // FUNCIÓN: ESTADÍSTICAS Y BALANCE
    // CORRECCIÓN: usa ventaTotales y ventaMetodos que persisten
    // durante toda la sesión sin depender del carrito
    // ============================================================
    static void MostrarEstadisticas()
    {
        Console.WriteLine("=== ESTADISTICAS Y BALANCE ===");

        if (ventaNumeros.Count == 0)
        {
            Console.WriteLine("No hay ventas registradas aun.");
            return;
        }

        double totalVendido  = 0;
        double ventaMayor    = 0;
        double ventaMenor    = ventaTotales[0];
        string clienteTop    = "";
        double ventaMayorVal = 0;

        for (int i = 0; i < ventaTotales.Count; i++)
        {
            totalVendido = totalVendido + ventaTotales[i];

            if (ventaTotales[i] > ventaMayor)
            {
                ventaMayor    = ventaTotales[i];
                clienteTop    = ventaClientes[i];
                ventaMayorVal = ventaTotales[i];
            }

            if (ventaTotales[i] < ventaMenor)
            {
                ventaMenor = ventaTotales[i];
            }
        }

        double promedioVenta = totalVendido / ventaNumeros.Count;

        int cantEfectivo      = 0;
        int cantCredito       = 0;
        int cantDebito        = 0;
        int cantTransferencia = 0;

        for (int i = 0; i < ventaMetodos.Count; i++)
        {
            if (ventaMetodos[i] == "Efectivo")
            {
                cantEfectivo = cantEfectivo + 1;
            }
            else if (ventaMetodos[i] == "Tarjeta de Credito")
            {
                cantCredito = cantCredito + 1;
            }
            else if (ventaMetodos[i] == "Tarjeta de Debito")
            {
                cantDebito = cantDebito + 1;
            }
            else if (ventaMetodos[i] == "Transferencia Bancaria")
            {
                cantTransferencia = cantTransferencia + 1;
            }
        }

        int productosAgotados  = 0;
        int productosStockBajo = 0;

        for (int i = 0; i < productoStocks.Count; i++)
        {
            if (productoStocks[i] == 0)
            {
                productosAgotados = productosAgotados + 1;
            }
            else if (productoStocks[i] <= 3)
            {
                productosStockBajo = productosStockBajo + 1;
            }
        }

        Console.WriteLine(new string('=', 45));
        Console.WriteLine("  RESUMEN DE VENTAS");
        Console.WriteLine(new string('=', 45));
        Console.WriteLine($"  Total de ventas realizadas : {ventaNumeros.Count}");
        Console.WriteLine($"  Total recaudado            : ${totalVendido:N0}");
        Console.WriteLine($"  Promedio por venta         : ${promedioVenta:N0}");
        Console.WriteLine($"  Venta mas alta             : ${ventaMayor:N0}");
        Console.WriteLine($"  Venta mas baja             : ${ventaMenor:N0}");
        Console.WriteLine($"  Cliente con mayor compra   : {clienteTop} (${ventaMayorVal:N0})");
        Console.WriteLine(new string('-', 45));
        Console.WriteLine("  METODOS DE PAGO");
        Console.WriteLine(new string('-', 45));
        Console.WriteLine($"  Efectivo                   : {cantEfectivo} ventas");
        Console.WriteLine($"  Tarjeta de Credito         : {cantCredito} ventas");
        Console.WriteLine($"  Tarjeta de Debito          : {cantDebito} ventas");
        Console.WriteLine($"  Transferencia Bancaria     : {cantTransferencia} ventas");
        Console.WriteLine(new string('-', 45));
        Console.WriteLine("  ESTADO DEL INVENTARIO");
        Console.WriteLine(new string('-', 45));
        Console.WriteLine($"  Total de productos         : {productoIds.Count}");
        Console.WriteLine($"  Productos agotados         : {productosAgotados}");
        Console.WriteLine($"  Productos con stock bajo   : {productosStockBajo}");
        Console.WriteLine(new string('=', 45));
    }
}
