using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;   // Librería para Excel - se instala via NuGet

class Minimercado
{
    static List<int>    productoIds        = new List<int>();
    static List<string> productoNombres    = new List<string>();
    static List<string> productoCategorias = new List<string>();
    static List<double> productoPrecios    = new List<double>();
    static List<int>    productoStocks     = new List<int>();

    static List<string> usuarioUsernames = new List<string>();
    static List<string> usuarioPasswords = new List<string>();
    static List<string> usuarioRoles     = new List<string>();
    static List<string> usuarioNombres   = new List<string>();

    static List<int>    carritoIds         = new List<int>();
    static List<string> carritoNombres     = new List<string>();
    static List<double> carritoPrecios     = new List<double>();
    static List<int>    carrritoCantidades = new List<int>();

    static List<int>    ventaNumeros  = new List<int>();
    static List<string> ventaClientes = new List<string>();
    static List<string> ventaFechas   = new List<string>();
    static List<double> ventaTotales  = new List<double>();
    static List<string> ventaMetodos  = new List<string>();
    static List<string> ventaDetalles = new List<string>();

    static string usuarioActual  = "";
    static string rolActual      = "";
    static string nombreActual   = "";
    static bool   sesionActiva   = false;
    static int    contadorVentas = 1;

    static void Main(string[] args)
    {
        CargarDatosIniciales();

        while (true)
        {
            Login();
            MostrarMenuSegunRol();
        }
    }

    static void MostrarMenuSegunRol()
    {
        if (rolActual == "admin")        MenuAdmin();
        else if (rolActual == "contador") MenuContador();
        else if (rolActual == "cliente")  MenuCliente();
    }

    static void MenuAdmin()
    {
        bool salir = false;
        while (salir == false)
        {
            Console.Clear();
            Console.WriteLine("Panel Administrador");
            Console.WriteLine($"Usuario: {nombreActual}");
            Console.WriteLine();
            Console.WriteLine("--- PRODUCTOS ---");
            Console.WriteLine("1.  Ver Catalogo");
            Console.WriteLine("2.  Agregar Producto");
            Console.WriteLine("3.  Editar Producto");
            Console.WriteLine("4.  Eliminar Producto");
            Console.WriteLine("5.  Buscar Producto");
            Console.WriteLine();
            Console.WriteLine("--- VENTAS ---");
            Console.WriteLine("6.  Ver Registro de Ventas");
            Console.WriteLine("7.  Estadisticas y Balance");
            Console.WriteLine();
            Console.WriteLine("--- USUARIOS ---");
            Console.WriteLine("8.  Ver Usuarios");
            Console.WriteLine("9.  Agregar Usuario");
            Console.WriteLine("10. Editar Usuario");
            Console.WriteLine("11. Eliminar Usuario");
            Console.WriteLine();
            Console.WriteLine("--- EXCEL ---");
            Console.WriteLine("12. Exportar Inventario a Excel");
            Console.WriteLine("13. Importar Inventario desde Excel");
            Console.WriteLine();
            Console.WriteLine("0.  Cerrar Sesion");
            Console.WriteLine();
            Console.Write("Seleccione una opcion: ");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":  MostrarCatalogo();            break;
                case "2":  AgregarProductoManual();      break;
                case "3":  EditarProducto();             break;
                case "4":  EliminarProducto();           break;
                case "5":  BuscarProducto();             break;
                case "6":  MostrarRegistroVentas();      break;
                case "7":  MostrarEstadisticas();        break;
                case "8":  MostrarUsuarios();            break;
                case "9":  AgregarUsuario();             break;
                case "10": EditarUsuario();              break;
                case "11": EliminarUsuario();            break;
                case "12": ExportarInventarioExcel();    break;
                case "13": ImportarInventarioExcel();    break;
                case "0":  CerrarSesion(); salir = true; break;
                default:   Console.WriteLine("Opcion no valida."); break;
            }

            if (salir == false)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
    }

    static void MenuContador()
    {
        bool salir = false;
        while (salir == false)
        {
            Console.Clear();
            Console.WriteLine("Panel Contador");
            Console.WriteLine($"Usuario: {nombreActual}");
            Console.WriteLine();
            Console.WriteLine("1. Ver Catalogo e Inventario");
            Console.WriteLine("2. Ver Registro de Ventas");
            Console.WriteLine("3. Estadisticas y Balance");
            Console.WriteLine("4. Exportar Inventario a Excel");
            Console.WriteLine();
            Console.WriteLine("0. Cerrar Sesion");
            Console.WriteLine();
            Console.Write("Seleccione una opcion: ");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1": MostrarCatalogo();         break;
                case "2": MostrarRegistroVentas();   break;
                case "3": MostrarEstadisticas();     break;
                case "4": ExportarInventarioExcel(); break;
                case "0": CerrarSesion(); salir = true; break;
                default:  Console.WriteLine("Opcion no valida."); break;
            }

            if (salir == false)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
    }

    static void MenuCliente()
    {
        bool salir = false;
        while (salir == false)
        {
            Console.Clear();
            Console.WriteLine("Panel Cliente");
            Console.WriteLine($"Usuario: {nombreActual}");
            Console.WriteLine();
            Console.WriteLine("1. Ver Catalogo");
            Console.WriteLine("2. Buscar Producto");
            Console.WriteLine("3. Agregar al Carrito");
            Console.WriteLine("4. Ver Carrito");
            Console.WriteLine("5. Eliminar del Carrito");
            Console.WriteLine("6. Realizar Compra");
            Console.WriteLine();
            Console.WriteLine("0. Cerrar Sesion");
            Console.WriteLine();
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
                case "0": CerrarSesion(); salir = true; break;
                default:  Console.WriteLine("Opcion no valida."); break;
            }

            if (salir == false)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
    }

    static void CargarDatosIniciales()
    {
        AgregarUsuarioInterno("admin",    "admin123",  "admin",    "Administrador");
        AgregarUsuarioInterno("contador1","cont123",   "contador", "Maria Lopez");
        AgregarUsuarioInterno("cliente1", "pass123",   "cliente",  "Carlos Garcia");
        AgregarUsuarioInterno("carlos",   "carlos456", "cliente",  "Carlos Ramirez");

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

    static void Login()
    {
        Console.Clear();
        Console.WriteLine("Inicio de sesion:");

        int intentos    = 0;
        int maxIntentos = 3;
        sesionActiva    = false;

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
            Console.WriteLine("Demasiados intentos fallidos.");
            Console.WriteLine("Presione cualquier tecla para intentar de nuevo...");
            Console.ReadKey();
        }
    }

    static string LeerPasswordOculta()
    {
        string password = "";
        while (true)
        {
            ConsoleKeyInfo tecla = Console.ReadKey(true);
            if (tecla.Key == ConsoleKey.Enter) { Console.WriteLine(); break; }
            else if (tecla.Key == ConsoleKey.Backspace && password.Length > 0)
            { password = password.Substring(0, password.Length - 1); Console.Write("\b \b"); }
            else if (tecla.Key != ConsoleKey.Backspace)
            { password = password + tecla.KeyChar; Console.Write("*"); }
        }
        return password;
    }

    static void CerrarSesion()
    {
        sesionActiva = false;
        usuarioActual = "";
        rolActual = "";
        nombreActual = "";
        LimpiarCarrito();
    }

    static void MostrarCatalogo()
    {
        Console.WriteLine("Catalogo de Productos");
        Console.WriteLine($"{"ID",-5} {"Nombre",-28} {"Categoria",-15} {"Precio",12} {"Stock",6}");
        Console.WriteLine();

        for (int i = 0; i < productoIds.Count; i++)
        {
            string alerta = "";
            if (productoStocks[i] == 0)       alerta = " [AGOTADO]";
            else if (productoStocks[i] <= 3)  alerta = " [STOCK BAJO]";

            Console.WriteLine($"{productoIds[i],-5} {productoNombres[i],-28} {productoCategorias[i],-15} ${productoPrecios[i],11:N0} {productoStocks[i],6}{alerta}");
        }

        Console.WriteLine();
        Console.WriteLine($"Total de productos: {productoIds.Count}");
    }

    static void BuscarProducto()
    {
        Console.WriteLine("Buscador de Productos");
        Console.Write("Ingresa nombre o categoria a buscar: ");
        string termino    = Console.ReadLine().ToLower();
        bool encontroAlgo = false;

        Console.WriteLine();
        for (int i = 0; i < productoIds.Count; i++)
        {
            if (productoNombres[i].ToLower().Contains(termino) || productoCategorias[i].ToLower().Contains(termino))
            {
                Console.WriteLine($"{productoIds[i],-5} {productoNombres[i],-28} {productoCategorias[i],-15} ${productoPrecios[i],11:N0} Stock: {productoStocks[i]}");
                encontroAlgo = true;
            }
        }

        if (encontroAlgo == false) Console.WriteLine($"No se encontraron productos con '{termino}'");
        Console.WriteLine();
    }

    static int BuscarIndicePorId(int id)
    {
        for (int i = 0; i < productoIds.Count; i++)
            if (productoIds[i] == id) return i;
        return -1;
    }

    static void AgregarProductoManual()
    {
        Console.WriteLine("Agregar Nuevo Producto");
        MostrarCatalogo();

        Console.Write("\nNombre del producto: ");
        string nombre = Console.ReadLine();
        if (nombre == "") { Console.WriteLine("El nombre no puede estar vacio."); return; }

        Console.Write("Categoria (Hardware / Perifericos / Monitores): ");
        string categoria = Console.ReadLine();
        if (categoria == "") { Console.WriteLine("La categoria no puede estar vacia."); return; }

        Console.Write("Precio: ");
        if (double.TryParse(Console.ReadLine(), out double precio) == false || precio <= 0)
        { Console.WriteLine("Precio invalido."); return; }

        Console.Write("Stock inicial: ");
        if (int.TryParse(Console.ReadLine(), out int stock) == false || stock < 0)
        { Console.WriteLine("Stock invalido."); return; }

        int nuevoId = productoIds[productoIds.Count - 1] + 1;
        AgregarProducto(nuevoId, nombre, categoria, precio, stock);
        Console.WriteLine($"Producto '{nombre}' agregado con ID {nuevoId}.");
    }

    static void EditarProducto()
    {
        Console.WriteLine("Editar Producto");
        MostrarCatalogo();

        Console.Write("\nIngresa el ID del producto a editar: ");
        if (int.TryParse(Console.ReadLine(), out int id) == false) { Console.WriteLine("ID invalido."); return; }

        int indice = BuscarIndicePorId(id);
        if (indice == -1) { Console.WriteLine("Producto no encontrado."); return; }

        Console.WriteLine($"\nEditando: {productoNombres[indice]}");
        Console.WriteLine("(Deja vacio y presiona Enter si no quieres cambiar ese campo)");

        Console.Write($"Nuevo nombre [{productoNombres[indice]}]: ");
        string nuevoNombre = Console.ReadLine();
        if (nuevoNombre != "") productoNombres[indice] = nuevoNombre;

        Console.Write($"Nueva categoria [{productoCategorias[indice]}]: ");
        string nuevaCategoria = Console.ReadLine();
        if (nuevaCategoria != "") productoCategorias[indice] = nuevaCategoria;

        Console.Write($"Nuevo precio [{productoPrecios[indice]:N0}]: ");
        string textoPrecio = Console.ReadLine();
        if (textoPrecio != "")
        {
            if (double.TryParse(textoPrecio, out double np) && np > 0) productoPrecios[indice] = np;
            else Console.WriteLine("Precio invalido, no se actualizo.");
        }

        Console.Write($"Nuevo stock [{productoStocks[indice]}]: ");
        string textoStock = Console.ReadLine();
        if (textoStock != "")
        {
            if (int.TryParse(textoStock, out int ns) && ns >= 0) productoStocks[indice] = ns;
            else Console.WriteLine("Stock invalido, no se actualizo.");
        }

        Console.WriteLine("Producto actualizado correctamente.");
    }

    static void EliminarProducto()
    {
        Console.WriteLine("Eliminar Producto");
        MostrarCatalogo();

        Console.Write("\nIngresa el ID del producto a eliminar: ");
        if (int.TryParse(Console.ReadLine(), out int id) == false) { Console.WriteLine("ID invalido."); return; }

        int indice = BuscarIndicePorId(id);
        if (indice == -1) { Console.WriteLine("Producto no encontrado."); return; }

        Console.Write($"Seguro que deseas eliminar '{productoNombres[indice]}'? (s/n): ");
        if (Console.ReadLine().ToLower() != "s") { Console.WriteLine("Eliminacion cancelada."); return; }

        string nombreEliminado = productoNombres[indice];
        productoIds.RemoveAt(indice); productoNombres.RemoveAt(indice);
        productoCategorias.RemoveAt(indice); productoPrecios.RemoveAt(indice); productoStocks.RemoveAt(indice);
        Console.WriteLine($"Producto '{nombreEliminado}' eliminado.");
    }

    static void MostrarUsuarios()
    {
        Console.WriteLine("Lista de Usuarios");
        Console.WriteLine($"{"#",-4} {"Username",-15} {"Nombre",-20} {"Rol",-12}");
        Console.WriteLine();
        for (int i = 0; i < usuarioUsernames.Count; i++)
            Console.WriteLine($"{i + 1,-4} {usuarioUsernames[i],-15} {usuarioNombres[i],-20} {usuarioRoles[i],-12}");
        Console.WriteLine();
        Console.WriteLine($"Total usuarios: {usuarioUsernames.Count}");
    }

    static void AgregarUsuario()
    {
        Console.WriteLine("Agregar Usuario");

        Console.Write("Username: ");
        string username = Console.ReadLine();
        if (username == "") { Console.WriteLine("El username no puede estar vacio."); return; }

        for (int i = 0; i < usuarioUsernames.Count; i++)
            if (usuarioUsernames[i].ToLower() == username.ToLower()) { Console.WriteLine("Ese username ya existe."); return; }

        Console.Write("Nombre completo: ");
        string nombre = Console.ReadLine();
        if (nombre == "") { Console.WriteLine("El nombre no puede estar vacio."); return; }

        Console.WriteLine("Roles disponibles: admin / contador / cliente");
        Console.Write("Rol: ");
        string rol = Console.ReadLine().ToLower();
        if (rol != "admin" && rol != "contador" && rol != "cliente") { Console.WriteLine("Rol invalido."); return; }

        Console.Write("Contrasena: ");
        string password = LeerPasswordOculta();
        if (password == "") { Console.WriteLine("La contrasena no puede estar vacia."); return; }

        AgregarUsuarioInterno(username, password, rol, nombre);
        Console.WriteLine($"Usuario '{username}' agregado con rol '{rol}'.");
    }

    static void EditarUsuario()
    {
        Console.WriteLine("Editar Usuario");
        MostrarUsuarios();

        Console.Write("\nIngresa el username a editar: ");
        string username = Console.ReadLine();

        int indice = -1;
        for (int i = 0; i < usuarioUsernames.Count; i++)
            if (usuarioUsernames[i].ToLower() == username.ToLower()) { indice = i; break; }

        if (indice == -1) { Console.WriteLine("Usuario no encontrado."); return; }

        Console.WriteLine($"\nEditando usuario: {usuarioUsernames[indice]}");
        Console.WriteLine("(Deja vacio y presiona Enter si no quieres cambiar ese campo)");

        Console.Write($"Nuevo nombre completo [{usuarioNombres[indice]}]: ");
        string nuevoNombre = Console.ReadLine();
        if (nuevoNombre != "") usuarioNombres[indice] = nuevoNombre;

        Console.Write($"Nuevo rol [{usuarioRoles[indice]}] (admin / contador / cliente): ");
        string nuevoRol = Console.ReadLine().ToLower();
        if (nuevoRol != "")
        {
            if (nuevoRol != "admin" && nuevoRol != "contador" && nuevoRol != "cliente")
                Console.WriteLine("Rol invalido, no se actualizo.");
            else if (usuarioUsernames[indice].ToLower() == usuarioActual.ToLower())
                Console.WriteLine("No puedes cambiar tu propio rol.");
            else
                usuarioRoles[indice] = nuevoRol;
        }

        Console.Write("Nueva contrasena (Enter para no cambiar): ");
        string nuevaPassword = LeerPasswordOculta();
        if (nuevaPassword != "") usuarioPasswords[indice] = nuevaPassword;

        Console.WriteLine("Usuario actualizado correctamente.");
    }

    static void EliminarUsuario()
    {
        Console.WriteLine("Eliminar Usuario");
        MostrarUsuarios();

        Console.Write("\nIngresa el username a eliminar: ");
        string username = Console.ReadLine();

        if (username.ToLower() == usuarioActual.ToLower())
        { Console.WriteLine("No puedes eliminar el usuario con el que estas logueado."); return; }

        int indice = -1;
        for (int i = 0; i < usuarioUsernames.Count; i++)
            if (usuarioUsernames[i].ToLower() == username.ToLower()) { indice = i; break; }

        if (indice == -1) { Console.WriteLine("Usuario no encontrado."); return; }

        Console.Write($"Seguro que deseas eliminar al usuario '{username}'? (s/n): ");
        if (Console.ReadLine().ToLower() != "s") { Console.WriteLine("Eliminacion cancelada."); return; }

        usuarioUsernames.RemoveAt(indice); usuarioPasswords.RemoveAt(indice);
        usuarioRoles.RemoveAt(indice); usuarioNombres.RemoveAt(indice);
        Console.WriteLine($"Usuario '{username}' eliminado.");
    }

    static void AgregarAlCarrito()
    {
        bool seguirAgregando = true;
        while (seguirAgregando == true)
        {
            Console.WriteLine("Agregar al Carrito");
            MostrarCatalogo();

            Console.Write("\nID del producto a agregar (0 para salir): ");
            if (int.TryParse(Console.ReadLine(), out int id) == false) { Console.WriteLine("ID invalido."); break; }
            if (id == 0) break;

            int indice = BuscarIndicePorId(id);

            if (indice == -1)
                Console.WriteLine("Producto no encontrado.");
            else if (productoStocks[indice] == 0)
                Console.WriteLine($"'{productoNombres[indice]}' esta agotado.");
            else
            {
                Console.Write($"Cantidad (disponibles: {productoStocks[indice]}): ");
                if (int.TryParse(Console.ReadLine(), out int cantidad) == false || cantidad <= 0)
                    Console.WriteLine("Cantidad invalida.");
                else if (cantidad > productoStocks[indice])
                    Console.WriteLine($"Stock insuficiente. Solo hay {productoStocks[indice]} unidades.");
                else
                {
                    bool yaEstaba = false;
                    for (int i = 0; i < carritoIds.Count; i++)
                    {
                        if (carritoIds[i] == id)
                        {
                            int totalSiAgrega = carrritoCantidades[i] + cantidad;
                            if (totalSiAgrega > productoStocks[indice])
                                Console.WriteLine($"No puedes agregar {cantidad} mas. Ya tienes {carrritoCantidades[i]} en el carrito.");
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

            Console.WriteLine();
            MostrarCarrito();
            Console.WriteLine();
            Console.Write("Deseas agregar otro producto? (s/n): ");
            if (Console.ReadLine().ToLower() != "s") seguirAgregando = false;
        }
    }

    static void MostrarCarrito()
    {
        Console.WriteLine("Carrito de Compras");
        if (carritoIds.Count == 0) { Console.WriteLine("El carrito esta vacio."); return; }

        Console.WriteLine($"{"#",-4} {"Producto",-28} {"Precio",12} {"Cant",6} {"Subtotal",14}");
        Console.WriteLine();

        double total = 0;
        for (int i = 0; i < carritoIds.Count; i++)
        {
            double subtotal = carritoPrecios[i] * carrritoCantidades[i];
            Console.WriteLine($"{i + 1,-4} {carritoNombres[i],-28} ${carritoPrecios[i],11:N0} {carrritoCantidades[i],6} ${subtotal,13:N0}");
            total = total + subtotal;
        }

        Console.WriteLine();
        Console.WriteLine($"{"TOTAL:",-52} ${total,13:N0}");
    }

    static void EliminarDelCarrito()
    {
        if (carritoIds.Count == 0) { Console.WriteLine("El carrito esta vacio."); return; }
        MostrarCarrito();

        Console.Write("\nNumero del producto a eliminar (0 para cancelar): ");
        if (int.TryParse(Console.ReadLine(), out int numero) == false || numero < 0 || numero > carritoIds.Count)
        { Console.WriteLine("Numero invalido."); return; }
        if (numero == 0) return;

        int indice = numero - 1;
        string nombre = carritoNombres[indice];
        carritoIds.RemoveAt(indice); carritoNombres.RemoveAt(indice);
        carritoPrecios.RemoveAt(indice); carrritoCantidades.RemoveAt(indice);
        Console.WriteLine($"'{nombre}' eliminado del carrito.");
    }

    static double CalcularTotalCarrito()
    {
        double total = 0;
        for (int i = 0; i < carritoIds.Count; i++)
            total = total + (carritoPrecios[i] * carrritoCantidades[i]);
        return total;
    }

    static void LimpiarCarrito()
    {
        carritoIds.Clear(); carritoNombres.Clear();
        carritoPrecios.Clear(); carrritoCantidades.Clear();
    }

    static string SeleccionarMetodoPago(double total)
    {
        Console.WriteLine("Metodo de Pago");
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
            { Console.WriteLine("Valor insuficiente."); return ""; }
            Console.WriteLine($"Cambio a devolver: ${valorRecibido - total:N0}");
        }
        else if (opcion == "2") { metodo = "Tarjeta de Credito";      Console.WriteLine("Pago procesado."); }
        else if (opcion == "3") { metodo = "Tarjeta de Debito";        Console.WriteLine("Pago procesado."); }
        else if (opcion == "4") { metodo = "Transferencia Bancaria";   Console.WriteLine("Cuenta: 123-456789-00  Banco: Bancolombia"); }
        else { Console.WriteLine("Opcion invalida."); return ""; }

        return metodo;
    }

    static void RealizarCompra()
    {
        Console.WriteLine("Proceso de Compra");
        if (carritoIds.Count == 0) { Console.WriteLine("El carrito esta vacio."); return; }

        MostrarCarrito();
        Console.Write("\nDeseas confirmar la compra? (s/n): ");
        if (Console.ReadLine().ToLower() != "s") { Console.WriteLine("Compra cancelada."); return; }

        double total  = CalcularTotalCarrito();
        string metodo = SeleccionarMetodoPago(total);
        if (metodo == "") return;

        string fecha   = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        string detalle = "";

        for (int i = 0; i < carritoIds.Count; i++)
        {
            if (i > 0) detalle = detalle + ",";
            detalle = detalle + carritoNombres[i] + "|" + carrritoCantidades[i] + "|" + carritoPrecios[i];
        }

        ventaNumeros.Add(contadorVentas); ventaClientes.Add(usuarioActual);
        ventaFechas.Add(fecha); ventaTotales.Add(total);
        ventaMetodos.Add(metodo); ventaDetalles.Add(detalle);

        for (int i = 0; i < carritoIds.Count; i++)
        {
            int ip = BuscarIndicePorId(carritoIds[i]);
            if (ip != -1) productoStocks[ip] = productoStocks[ip] - carrritoCantidades[i];
        }

        ImprimirFactura(contadorVentas, usuarioActual, fecha, total, metodo);
        GuardarFacturaEnArchivo(contadorVentas, usuarioActual, fecha, total, metodo);
        contadorVentas = contadorVentas + 1;
        LimpiarCarrito();
    }

    static void ImprimirFactura(int numero, string cliente, string fecha, double total, string metodo)
    {
        Console.WriteLine();
        Console.WriteLine("MINIMERCADO TECH");
        Console.WriteLine("NIT: 900.123.456-7");
        Console.WriteLine("Calle 45 #23-10, Medellin");
        Console.WriteLine();
        Console.WriteLine($"Factura N: {numero:D6}");
        Console.WriteLine($"Fecha: {fecha}");
        Console.WriteLine($"Cliente: {cliente}");
        Console.WriteLine($"Pago: {metodo}");
        Console.WriteLine();
        Console.WriteLine($"{"Producto",-24} {"Cant",4} {"P.Unit",10} {"Subtotal",9}");

        for (int i = 0; i < carritoIds.Count; i++)
        {
            string nombre;
            if (carritoNombres[i].Length > 24) nombre = carritoNombres[i].Substring(0, 21) + "...";
            else nombre = carritoNombres[i];

            double subtotal = carritoPrecios[i] * carrritoCantidades[i];
            Console.WriteLine($"  {nombre,-24} {carrritoCantidades[i],4} {carritoPrecios[i],9:N0} {subtotal,9:N0}");
        }

        Console.WriteLine();
        Console.WriteLine($"TOTAL A PAGAR: ${total:N0}");
        Console.WriteLine();
        Console.WriteLine("Gracias por su compra en MinimercadoTech!");
    }

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

    static void MostrarRegistroVentas()
    {
        Console.WriteLine("Registro de Ventas");
        if (ventaNumeros.Count == 0) { Console.WriteLine("No hay ventas registradas."); return; }

        Console.WriteLine($"{"N° Venta",-10} {"Cliente",-15} {"Fecha",-22} {"Metodo",-22} {"Total",14}");
        Console.WriteLine();

        double totalGeneral = 0;
        for (int i = 0; i < ventaNumeros.Count; i++)
        {
            Console.WriteLine($"{ventaNumeros[i]:D6,-10} {ventaClientes[i],-15} {ventaFechas[i],-22} {ventaMetodos[i],-22} ${ventaTotales[i],13:N0}");
            totalGeneral = totalGeneral + ventaTotales[i];
        }

        Console.WriteLine();
        Console.WriteLine($"Total de ventas: {ventaNumeros.Count}");
        Console.WriteLine($"Recaudo total:   ${totalGeneral:N0}");

        Console.Write("\nDeseas ver el detalle de una venta? (s/n): ");
        if (Console.ReadLine().ToLower() == "s")
        {
            Console.Write("Ingresa el numero de venta: ");
            if (int.TryParse(Console.ReadLine(), out int numBuscado))
            {
                int iv = -1;
                for (int i = 0; i < ventaNumeros.Count; i++)
                    if (ventaNumeros[i] == numBuscado) { iv = i; break; }
                if (iv == -1) Console.WriteLine("Venta no encontrada.");
                else MostrarDetalleVenta(iv);
            }
        }
    }

    static void MostrarDetalleVenta(int iv)
    {
        Console.WriteLine($"\n--- Detalle Venta N° {ventaNumeros[iv]:D6} ---");
        Console.WriteLine($"Cliente : {ventaClientes[iv]}");
        Console.WriteLine($"Fecha   : {ventaFechas[iv]}");
        Console.WriteLine($"Pago    : {ventaMetodos[iv]}");
        Console.WriteLine();
        Console.WriteLine($"{"Producto",-25} {"Cant",5} {"P.Unit",12} {"Subtotal",12}");
        Console.WriteLine();

        string[] productos = ventaDetalles[iv].Split(',');
        for (int i = 0; i < productos.Length; i++)
        {
            string[] partes    = productos[i].Split('|');
            string nombreProd  = partes[0];
            int    cantProd    = int.Parse(partes[1]);
            double precioProd  = double.Parse(partes[2]);
            double subtotal    = cantProd * precioProd;
            Console.WriteLine($"{nombreProd,-25} {cantProd,5} ${precioProd,11:N0} ${subtotal,11:N0}");
        }

        Console.WriteLine();
        Console.WriteLine($"{"TOTAL:",-44} ${ventaTotales[iv],11:N0}");
    }

    static void MostrarEstadisticas()
    {
        Console.WriteLine("Estadisticas y Balance");
        if (ventaNumeros.Count == 0) { Console.WriteLine("No hay ventas registradas aun."); return; }

        double totalVendido = 0; double ventaMayor = 0; double ventaMenor = ventaTotales[0];
        string clienteTop = ""; double ventaMayorVal = 0;

        for (int i = 0; i < ventaTotales.Count; i++)
        {
            totalVendido = totalVendido + ventaTotales[i];
            if (ventaTotales[i] > ventaMayor) { ventaMayor = ventaTotales[i]; clienteTop = ventaClientes[i]; ventaMayorVal = ventaTotales[i]; }
            if (ventaTotales[i] < ventaMenor)   ventaMenor = ventaTotales[i];
        }

        double promedioVenta = totalVendido / ventaNumeros.Count;
        int cantEfectivo = 0; int cantCredito = 0; int cantDebito = 0; int cantTransferencia = 0;

        for (int i = 0; i < ventaMetodos.Count; i++)
        {
            if      (ventaMetodos[i] == "Efectivo")             cantEfectivo      = cantEfectivo + 1;
            else if (ventaMetodos[i] == "Tarjeta de Credito")   cantCredito       = cantCredito + 1;
            else if (ventaMetodos[i] == "Tarjeta de Debito")    cantDebito        = cantDebito + 1;
            else if (ventaMetodos[i] == "Transferencia Bancaria") cantTransferencia = cantTransferencia + 1;
        }

        int productosAgotados = 0; int productosStockBajo = 0;
        for (int i = 0; i < productoStocks.Count; i++)
        {
            if      (productoStocks[i] == 0)  productosAgotados  = productosAgotados + 1;
            else if (productoStocks[i] <= 3)  productosStockBajo = productosStockBajo + 1;
        }

        Console.WriteLine("RESUMEN DE VENTAS");
        Console.WriteLine($"  Total de ventas realizadas : {ventaNumeros.Count}");
        Console.WriteLine($"  Total recaudado            : ${totalVendido:N0}");
        Console.WriteLine($"  Promedio por venta         : ${promedioVenta:N0}");
        Console.WriteLine($"  Venta mas alta             : ${ventaMayor:N0}");
        Console.WriteLine($"  Venta mas baja             : ${ventaMenor:N0}");
        Console.WriteLine($"  Cliente con mayor compra   : {clienteTop} (${ventaMayorVal:N0})");
        Console.WriteLine("METODOS DE PAGO");
        Console.WriteLine($"  Efectivo                   : {cantEfectivo} ventas");
        Console.WriteLine($"  Tarjeta de Credito         : {cantCredito} ventas");
        Console.WriteLine($"  Tarjeta de Debito          : {cantDebito} ventas");
        Console.WriteLine($"  Transferencia Bancaria     : {cantTransferencia} ventas");
        Console.WriteLine("ESTADO DEL INVENTARIO");
        Console.WriteLine($"  Total de productos         : {productoIds.Count}");
        Console.WriteLine($"  Productos agotados         : {productosAgotados}");
        Console.WriteLine($"  Productos con stock bajo   : {productosStockBajo}");
        Console.WriteLine();
    }

    static void ExportarInventarioExcel()
    {
        Console.WriteLine("Exportar a Excel");

        XLWorkbook libro = new XLWorkbook();

        IXLWorksheet hojaInventario = libro.Worksheets.Add("Inventario");

        hojaInventario.Cell(1, 1).Value = "ID";
        hojaInventario.Cell(1, 2).Value = "Nombre";
        hojaInventario.Cell(1, 3).Value = "Categoria";
        hojaInventario.Cell(1, 4).Value = "Precio";
        hojaInventario.Cell(1, 5).Value = "Stock";
        hojaInventario.Cell(1, 6).Value = "Estado";

        IXLRange encabezadoInventario = hojaInventario.Range("A1", "F1");
        encabezadoInventario.Style.Fill.BackgroundColor      = XLColor.FromHtml("#1F4E79");
        encabezadoInventario.Style.Font.FontColor            = XLColor.White;
        encabezadoInventario.Style.Font.Bold                 = true;
        encabezadoInventario.Style.Alignment.Horizontal      = XLAlignmentHorizontalValues.Center;

        for (int i = 0; i < productoIds.Count; i++)
        {
            int fila = i + 2; // fila 2, 3, 4...

            hojaInventario.Cell(fila, 1).Value = productoIds[i];
            hojaInventario.Cell(fila, 2).Value = productoNombres[i];
            hojaInventario.Cell(fila, 3).Value = productoCategorias[i];
            hojaInventario.Cell(fila, 4).Value = productoPrecios[i];
            hojaInventario.Cell(fila, 5).Value = productoStocks[i];

            string estado = "Disponible";
            if (productoStocks[i] == 0)       estado = "AGOTADO";
            else if (productoStocks[i] <= 3)  estado = "STOCK BAJO";

            hojaInventario.Cell(fila, 6).Value = estado;

            if (i % 2 == 0)
            {
                hojaInventario.Range(fila, 1, fila, 6).Style.Fill.BackgroundColor = XLColor.FromHtml("#EBF3FB");
            }

            if (productoStocks[i] == 0)
                hojaInventario.Range(fila, 1, fila, 6).Style.Fill.BackgroundColor = XLColor.FromHtml("#FCE4D6");
            else if (productoStocks[i] <= 3)
                hojaInventario.Range(fila, 1, fila, 6).Style.Fill.BackgroundColor = XLColor.FromHtml("#FFF2CC");

            hojaInventario.Cell(fila, 4).Style.NumberFormat.Format = "$#,##0";
        }

        hojaInventario.Columns().AdjustToContents();

        hojaInventario.SheetView.FreezeRows(1);

        IXLWorksheet hojaVentas = libro.Worksheets.Add("Ventas");

        hojaVentas.Cell(1, 1).Value = "N Venta";
        hojaVentas.Cell(1, 2).Value = "Cliente";
        hojaVentas.Cell(1, 3).Value = "Fecha";
        hojaVentas.Cell(1, 4).Value = "Metodo de Pago";
        hojaVentas.Cell(1, 5).Value = "Productos";
        hojaVentas.Cell(1, 6).Value = "Total";

        IXLRange encabezadoVentas = hojaVentas.Range("A1", "F1");
        encabezadoVentas.Style.Fill.BackgroundColor = XLColor.FromHtml("#1D6033");
        encabezadoVentas.Style.Font.FontColor       = XLColor.White;
        encabezadoVentas.Style.Font.Bold            = true;
        encabezadoVentas.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        if (ventaNumeros.Count == 0)
        {
            hojaVentas.Cell(2, 1).Value = "No hay ventas registradas";
        }
        else
        {
            for (int i = 0; i < ventaNumeros.Count; i++)
            {
                int fila = i + 2;

                string[] items          = ventaDetalles[i].Split(',');
                string   resumenItems   = "";
                for (int j = 0; j < items.Length; j++)
                {
                    string[] partes = items[j].Split('|');
                    if (j > 0) resumenItems = resumenItems + ", ";
                    resumenItems = resumenItems + partes[0] + " x" + partes[1];
                }

                hojaVentas.Cell(fila, 1).Value = ventaNumeros[i].ToString("D6");
                hojaVentas.Cell(fila, 2).Value = ventaClientes[i];
                hojaVentas.Cell(fila, 3).Value = ventaFechas[i];
                hojaVentas.Cell(fila, 4).Value = ventaMetodos[i];
                hojaVentas.Cell(fila, 5).Value = resumenItems;
                hojaVentas.Cell(fila, 6).Value = ventaTotales[i];

                hojaVentas.Cell(fila, 6).Style.NumberFormat.Format = "$#,##0";

                if (i % 2 == 0)
                    hojaVentas.Range(fila, 1, fila, 6).Style.Fill.BackgroundColor = XLColor.FromHtml("#E8F5E9");
            }
        }

        hojaVentas.Columns().AdjustToContents();
        hojaVentas.SheetView.FreezeRows(1);

        string nombreArchivo = $"inventario_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

        libro.SaveAs(nombreArchivo);

        Console.WriteLine($"Archivo exportado: {nombreArchivo}");
        Console.WriteLine($"Ubicacion: {Directory.GetCurrentDirectory()}\\{nombreArchivo}");
    }

    static void ImportarInventarioExcel()
    {
        Console.WriteLine("Importar desde Excel");
        Console.WriteLine("IMPORTANTE: El archivo debe tener el mismo formato que el exportado.");
        Console.WriteLine("Columnas requeridas: ID | Nombre | Categoria | Precio | Stock");
        Console.WriteLine();

        Console.Write("Nombre del archivo a importar (ej: inventario.xlsx): ");
        string nombreArchivo = Console.ReadLine();

        if (File.Exists(nombreArchivo) == false)
        {
            Console.WriteLine($"El archivo '{nombreArchivo}' no se encontro.");
            Console.WriteLine($"Asegurate de que este en la carpeta: {Directory.GetCurrentDirectory()}");
            return;
        }

        Console.Write("Esto reemplazara el catalogo actual. Deseas continuar? (s/n): ");
        if (Console.ReadLine().ToLower() != "s") { Console.WriteLine("Importacion cancelada."); return; }

        List<int>    idsTemp        = new List<int>();
        List<string> nombresTemp    = new List<string>();
        List<string> categoriasTemp = new List<string>();
        List<double> preciosTemp    = new List<double>();
        List<int>    stocksTemp     = new List<int>();

        int productosLeidos = 0;
        int errores         = 0;

        XLWorkbook libro = new XLWorkbook(nombreArchivo);

        IXLWorksheet hoja = libro.Worksheet(1);

        foreach (IXLRow fila in hoja.RowsUsed().Skip(1))
        {
            string celdaId       = fila.Cell(1).GetValue<string>().Trim();
            string celdaNombre   = fila.Cell(2).GetValue<string>().Trim();
            string celdaCateg    = fila.Cell(3).GetValue<string>().Trim();
            string celdaPrecio   = fila.Cell(4).GetValue<string>().Trim();
            string celdaStock    = fila.Cell(5).GetValue<string>().Trim();

            if (int.TryParse(celdaId, out int id) == false)
            { Console.WriteLine($"Fila {fila.RowNumber()}: ID invalido '{celdaId}', se omite."); errores = errores + 1; continue; }

            if (celdaNombre == "")
            { Console.WriteLine($"Fila {fila.RowNumber()}: Nombre vacio, se omite."); errores = errores + 1; continue; }

            if (celdaCateg == "")
            { Console.WriteLine($"Fila {fila.RowNumber()}: Categoria vacia, se omite."); errores = errores + 1; continue; }

            if (double.TryParse(celdaPrecio, out double precio) == false || precio <= 0)
            { Console.WriteLine($"Fila {fila.RowNumber()}: Precio invalido '{celdaPrecio}', se omite."); errores = errores + 1; continue; }

            if (int.TryParse(celdaStock, out int stock) == false || stock < 0)
            { Console.WriteLine($"Fila {fila.RowNumber()}: Stock invalido '{celdaStock}', se omite."); errores = errores + 1; continue; }

            idsTemp.Add(id);
            nombresTemp.Add(celdaNombre);
            categoriasTemp.Add(celdaCateg);
            preciosTemp.Add(precio);
            stocksTemp.Add(stock);
            productosLeidos = productosLeidos + 1;
        }

        if (productosLeidos == 0)
        {
            Console.WriteLine("No se encontraron productos validos en el archivo. El catalogo no fue modificado.");
            return;
        }

        productoIds.Clear();
        productoNombres.Clear();
        productoCategorias.Clear();
        productoPrecios.Clear();
        productoStocks.Clear();

        for (int i = 0; i < idsTemp.Count; i++)
        {
            productoIds.Add(idsTemp[i]);
            productoNombres.Add(nombresTemp[i]);
            productoCategorias.Add(categoriasTemp[i]);
            productoPrecios.Add(preciosTemp[i]);
            productoStocks.Add(stocksTemp[i]);
        }

        Console.WriteLine($"\nImportacion completada:");
        Console.WriteLine($"  Productos importados : {productosLeidos}");
        Console.WriteLine($"  Filas con errores    : {errores}");
        Console.WriteLine("\nCatalogo actualizado. Usa 'Ver Catalogo' para verificar.");
    }
}
