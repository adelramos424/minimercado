# MinimercadoTech 🖥️

Proyecto de la materia Estructura de Datos — Ingeniería de Software, semestre 3.

Es un sistema de minimercado en consola pero con temática de tecnología y computadoras, como si fuera una tienda tipo D1 o Ara pero vendiendo hardware, periféricos y monitores.

---

## Integrantes

- Luis Alejandro Toledo
- Mateo Gallon
- Adel Angel Ramos

---

## De qué va el proyecto

Básicamente es un sistema de caja registradora por consola. Tiene login, carrito de compras, facturación, registro de ventas y un catálogo de productos con su CRUD. Todo manejado con listas paralelas que es lo que hemos visto en clase.

También le metimos cosas extra que investigamos por nuestra cuenta como exportar e importar el inventario en Excel.

---

## Usuarios para probar

| Usuario    | Contraseña  | Rol        |
|------------|-------------|------------|
| admin      | admin123    | Admin      |
| contador1  | cont123     | Contador   |
| cliente1   | pass123     | Cliente    |
| carlos     | carlos456   | Cliente    |

---

## Qué puede hacer cada rol

**Admin** → tiene acceso a todo: productos, ventas, usuarios y Excel.

**Contador** → solo puede ver el inventario, el registro de ventas, las estadísticas y exportar a Excel. No puede modificar nada.

**Cliente** → puede ver el catálogo, buscar productos, armar su carrito y hacer la compra.

---

## Cómo correrlo

1. Clona el repo
2. Abre la carpeta en Visual Studio
3. Haz clic derecho en el proyecto → `Restore NuGet Packages` (esto instala ClosedXML que es la librería del Excel, se instala solo)
4. Dale F5 y listo

> Necesitas tener instalado .NET 8.0 o superior

---

## Lo que tiene el sistema

- Login con contraseña oculta y máximo 3 intentos
- Catálogo con avisos de stock bajo y productos agotados
- Carrito de compras con validación de stock en tiempo real
- 4 métodos de pago: efectivo con vuelto, crédito, débito y transferencia
- Factura en consola y guardada en archivo `.txt`
- Registro de ventas con detalle por compra
- Estadísticas: total vendido, promedio, venta más alta/baja, métodos de pago más usados
- CRUD de productos y gestión de usuarios
- Exportar inventario y ventas a Excel `.xlsx`
- Importar inventario desde Excel

---

## Estructuras de datos usadas

Todo el sistema usa **listas paralelas** (`List<>`) donde el índice de cada lista corresponde al mismo elemento. Por ejemplo `productoNombres[0]` y `productoPrecios[0]` son del mismo producto.

```
productoIds        → IDs de cada producto
productoNombres    → nombres
productoCategorias → categorías
productoPrecios    → precios
productoStocks     → stock disponible
```

El mismo esquema se usa para usuarios, carrito y registro de ventas.

---

## Librería externa

Usamos **ClosedXML** para todo lo de Excel. Está referenciada en el `.csproj` así que no hay que instalarla a mano, Visual Studio la descarga automáticamente al restaurar paquetes.
