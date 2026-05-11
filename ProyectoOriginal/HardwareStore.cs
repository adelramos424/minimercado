using System;
using System.Collections.Generic;
using System.IO;

class Minimercado
{

    // LISTAS PARALELAS - PRODUCTOS

    static List<int> productoIds = new List<int>();
    static List<string> productoNombres = new List<string>();
    static List<string> productoCategorias = new List<string>();
    static List<double> productoPrecios = new List<double>();
    static List<int> productoStocks = new List<int>();


    // LISTAS PARALELAS - USUARIOS

    static List<string> usuarioUsernames = new List<string>();
    static List<string> usuarioPasswords = new List<string>();
    static List<string> usuarioRoles = new List<string>();


    // LISTAS PARALELAS - CARRITO

    static List<int> carritoIds = new List<int>();
    static List<string> carritoNombres = new List<string>();
    static List<double> carritoPrecios = new List<double>();
    static List<int> carrritoCantidades = new List<int>();


    // LISTAS PARALELAS - REGISTRO DE VENTAS

    static List<int> ventaNumeros = new List<int>();
    static List<string> ventaClientes = new List<string>();
    static List<string> ventaFechas = new List<string>();
    static List<double> ventaTotales = new List<double>();

    // Variables de sesion
    static string usuarioActual = "";
    static string rolActual = "";
    static bool sesionActiva = false;
    static int contadorVentas = 1;


    // MAIN

    static void Main(string[] args)
    {
        CargarDatosIniciales();

        Console.WriteLine("╔══════════════════════════════════╗");
        Console.WriteLine("║       MINIMERCADO TECH           ║");
        Console.WriteLine("║    Tu tienda de computadoras     ║");
        Console.WriteLine("╚══════════════════════════════════╝");
        Console.WriteLine();

        Login();

        Console.WriteLine("\n--- Entrando al sistema ---\n");

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


    // FUNCIÓN: CARGAR DATOS INICIALES

    static void CargarDatosIniciales()
    {
        usuarioUsernames.Add("admin"); usuarioPasswords.Add("admin123"); usuarioRoles.Add("admin");
        usuarioUsernames.Add("cliente1"); usuarioPasswords.Add("pass123"); usuarioRoles.Add("cliente");
        usuarioUsernames.Add("carlos"); usuarioPasswords.Add("carlos456"); usuarioRoles.Add("cliente");

        AgregarProducto(1, "Teclado Mecanico RGB", "Perifericos", 250000, 15);
        AgregarProducto(2, "Mouse Gamer 16000 DPI", "Perifericos", 180000, 20);
        AgregarProducto(3, "Monitor 24 Full HD", "Monitores", 850000, 8);
        AgregarProducto(4, "RAM DDR4 16GB", "Hardware", 320000, 25);
        AgregarProducto(5, "SSD 500GB NVMe", "Hardware", 280000, 18);
        AgregarProducto(6, "Tarjeta Grafica RTX 3060", "Hardware", 1950000, 5);
        AgregarProducto(7, "Audifonos Gaming 7.1", "Perifericos", 220000, 12);
        AgregarProducto(8, "Webcam Full HD 1080p", "Perifericos", 150000, 10);
        AgregarProducto(9, "Procesador Ryzen 5 5600", "Hardware", 780000, 7);
        AgregarProducto(10, "Fuente de Poder 650W", "Hardware", 340000, 9);
    }

    static void AgregarProducto(int id, string nombre, string categoria, double precio, int stock)
    {
        productoIds.Add(id);
        productoNombres.Add(nombre);
        productoCategorias.Add(categoria);
        productoPrecios.Add(precio);
        productoStocks.Add(stock);
    }


    // FUNCIÓN: LOGIN

    static void Login()
    {
        Console.WriteLine("=== INICIO DE SESIÓN ===");

        int intentos = 0;
        int maxIntentos = 3;

        while (!sesionActiva && intentos < maxIntentos)
        {
            Console.Write("Usuario: ");
            string username = Console.ReadLine();

            Console.Write("Contraseña: ");
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
                sesionActiva = true;
                usuarioActual = usuarioUsernames[indiceEncontrado];
                rolActual = usuarioRoles[indiceEncontrado];
                Console.WriteLine($"\n✓ Bienvenido, {usuarioActual}! (Rol: {rolActual})");
            }
            else
            {
                intentos++;
                Console.WriteLine($"✗ Usuario o contraseña incorrectos. Intentos restantes: {maxIntentos - intentos}");
            }
        }

        if (!sesionActiva)
        {
            Console.WriteLine("Demasiados intentos fallidos. El sistema se cerrara.");
            Environment.Exit(0);
        }
    }


    // FUNCIÓN: LEER PASSWORD OCULTA

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
                password += tecla.KeyChar;
                Console.Write("*");
            }
        }

        return password;
    }


    // FUNCIÓN: CERRAR SESIÓN

    static void CerrarSesion()
    {
        Console.WriteLine($"Cerrando sesion de {usuarioActual}...");
        sesionActiva = false;
        usuarioActual = "";
        rolActual = "";
        LimpiarCarrito();
        Console.WriteLine("Sesion cerrada. Hasta pronto!");
    }


    // FUNCIÓN: MOSTRAR CATÁLOGO

    static void MostrarCatalogo()
    {
        Console.WriteLine("=== CATALOGO DE PRODUCTOS ===");
        Console.WriteLine($"{"ID",-5} {"Nombre",-28} {"Categoria",-15} {"Precio",12} {"Stock",6}");
        Console.WriteLine(new string('-', 70));

        for (int i = 0; i < productoIds.Count; i++)
        {
            Console.WriteLine($"{productoIds[i],-5} {productoNombres[i],-28} {productoCategorias[i],-15} ${productoPrecios[i],11:N0} {productoStocks[i],6}");
        }

        Console.WriteLine(new string('-', 70));
        Console.WriteLine($"Total de productos: {productoIds.Count}");
    }


    // FUNCIÓN: BUSCAR PRODUCTO

    static void BuscarProducto()
    {
        Console.WriteLine("=== BUSCADOR DE PRODUCTOS ===");
        Console.Write("Ingresa nombre o categoria a buscar: ");
        string termino = Console.ReadLine().ToLower();

        bool encontroAlgo = false;

        Console.WriteLine(new string('-', 70));

        for (int i = 0; i < productoIds.Count; i++)
        {
            bool coincideNombre = productoNombres[i].ToLower().Contains(termino);
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

    // FUNCIÓN AUXILIAR: BUSCAR ÍNDICE DE PRODUCTO POR ID

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


    // FUNCIÓN: AGREGAR AL CARRITO

    static void AgregarAlCarrito()
    {
        Console.WriteLine("=== AGREGAR AL CARRITO ===");
        MostrarCatalogo();

        Console.Write("\nID del producto a agregar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
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

        if (productoStocks[indice] == 0)
        {
            Console.WriteLine($"'{productoNombres[indice]}' esta agotado.");
            return;
        }

        Console.Write($"Cantidad (disponibles: {productoStocks[indice]}): ");
        if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)
        {
            Console.WriteLine("Cantidad invalida.");
            return;
        }

        if (cantidad > productoStocks[indice])
        {
            Console.WriteLine($"Stock insuficiente. Solo hay {productoStocks[indice]} unidades.");
            return;
        }

        bool yaEstaba = false;

        for (int i = 0; i < carritoIds.Count; i++)
        {
            if (carritoIds[i] == id)
            {
                if (carrritoCantidades[i] + cantidad > productoStocks[indice])
                {
                    Console.WriteLine($"No puedes agregar mas. Ya tienes {carrritoCantidades[i]} en el carrito.");
                }
                else
                {
                    carrritoCantidades[i] = carrritoCantidades[i] + cantidad;
                    Console.WriteLine($"✓ Cantidad actualizada: {carrritoCantidades[i]}x {carritoNombres[i]}");
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
            Console.WriteLine($"✓ Agregado: {cantidad}x {productoNombres[indice]} - ${productoPrecios[indice] * cantidad:N0}");
        }
    }


    // FUNCIÓN: MOSTRAR CARRITO

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


    // FUNCIÓN: ELIMINAR DEL CARRITO

    static void EliminarDelCarrito()
    {
        if (carritoIds.Count == 0)
        {
            Console.WriteLine("El carrito esta vacio.");
            return;
        }

        MostrarCarrito();
        Console.Write("\nNumero del producto a eliminar (0 para cancelar): ");

        if (!int.TryParse(Console.ReadLine(), out int numero) || numero < 0 || numero > carritoIds.Count)
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

        Console.WriteLine($"✓ '{nombre}' eliminado del carrito.");
    }


    // FUNCIÓN AUXILIAR: CALCULAR TOTAL DEL CARRITO

    static double CalcularTotalCarrito()
    {
        double total = 0;

        for (int i = 0; i < carritoIds.Count; i++)
        {
            total = total + (carritoPrecios[i] * carrritoCantidades[i]);
        }

        return total;
    }


    // FUNCIÓN AUXILIAR: LIMPIAR CARRITO

    static void LimpiarCarrito()
    {
        carritoIds.Clear();
        carritoNombres.Clear();
        carritoPrecios.Clear();
        carrritoCantidades.Clear();
    }


    // FUNCIÓN: REALIZAR COMPRA

    static void RealizarCompra()
    {
        Console.WriteLine("=== PROCESO DE COMPRA ===");

        if (carritoIds.Count == 0)
        {
            Console.WriteLine("El carrito esta vacio.");
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

        double total = CalcularTotalCarrito();
        string fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        ventaNumeros.Add(contadorVentas);
        ventaClientes.Add(usuarioActual);
        ventaFechas.Add(fecha);
        ventaTotales.Add(total);

        for (int i = 0; i < carritoIds.Count; i++)
        {
            int indiceProducto = BuscarIndicePorId(carritoIds[i]);

            if (indiceProducto != -1)
            {
                productoStocks[indiceProducto] = productoStocks[indiceProducto] - carrritoCantidades[i];
            }
        }

        ImprimirFactura(contadorVentas, usuarioActual, fecha, total);
        GuardarFacturaEnArchivo(contadorVentas, usuarioActual, fecha, total);

        contadorVentas = contadorVentas + 1;
        LimpiarCarrito();
    }


    // FUNCIÓN: IMPRIMIR FACTURA EN CONSOLA

    static void ImprimirFactura(int numero, string cliente, string fecha, double total)
    {
        Console.WriteLine();
        Console.WriteLine("╔══════════════════════════════════════════════════╗");
        Console.WriteLine("║              MINIMERCADO TECH                   ║");
        Console.WriteLine("║          NIT: 900.123.456-7                     ║");
        Console.WriteLine("║       Calle 45 #23-10, Medellin                 ║");
        Console.WriteLine("╠══════════════════════════════════════════════════╣");
        Console.WriteLine($"║  Factura N°: {numero:D6}                            ║");
        Console.WriteLine($"║  Fecha: {fecha}               ║");
        Console.WriteLine($"║  Cliente: {cliente,-38}║");
        Console.WriteLine("╠══════════════════════════════════════════════════╣");
        Console.WriteLine($"║  {"Producto",-24} {"Cant",4} {"P.Unit",10} {"Subtotal",9} ║");
        Console.WriteLine("╠══════════════════════════════════════════════════╣");

        for (int i = 0; i < carritoIds.Count; i++)
        {
            // Antes era con ? : (ternario), ahora es if/else normal
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
        Console.WriteLine("     Gracias por su compra en MinimercadoTech!    ");
    }


    // FUNCIÓN: GUARDAR FACTURA EN ARCHIVO .TXT

    static void GuardarFacturaEnArchivo(int numero, string cliente, string fecha, double total)
    {
        string nombreArchivo = $"factura_{numero:D6}.txt";

        using (StreamWriter escritor = new StreamWriter(nombreArchivo))
        {
            escritor.WriteLine("MINIMERCADO TECH");
            escritor.WriteLine($"Factura N: {numero:D6}");
            escritor.WriteLine($"Fecha: {fecha}");
            escritor.WriteLine($"Cliente: {cliente}");
            escritor.WriteLine(new string('-', 50));

            for (int i = 0; i < carritoIds.Count; i++)
            {
                double subtotal = carritoPrecios[i] * carrritoCantidades[i];
                escritor.WriteLine($"{carritoNombres[i],-25} x{carrritoCantidades[i]}  ${subtotal:N0}");
            }

            escritor.WriteLine(new string('-', 50));
            escritor.WriteLine($"TOTAL: ${total:N0}");
        }

        Console.WriteLine($"\n✓ Factura guardada en: {nombreArchivo}");
    }


    // FUNCIÓN: MOSTRAR REGISTRO DE VENTAS

    static void MostrarRegistroVentas()
    {
        Console.WriteLine("=== REGISTRO DE VENTAS ===");

        if (ventaNumeros.Count == 0)
        {
            Console.WriteLine("No hay ventas registradas.");
            return;
        }

        Console.WriteLine($"{"N° Venta",-10} {"Cliente",-15} {"Fecha",-22} {"Total",14}");
        Console.WriteLine(new string('-', 65));

        double totalGeneral = 0;

        for (int i = 0; i < ventaNumeros.Count; i++)
        {
            Console.WriteLine($"{ventaNumeros[i]:D6,-10} {ventaClientes[i],-15} {ventaFechas[i],-22} ${ventaTotales[i],13:N0}");
            totalGeneral = totalGeneral + ventaTotales[i];
        }

        Console.WriteLine(new string('-', 65));
        Console.WriteLine($"Total de ventas: {ventaNumeros.Count}");
        Console.WriteLine($"Recaudo total:   ${totalGeneral:N0}");
    }
}
