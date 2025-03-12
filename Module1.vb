Imports System.Configuration
Imports System.Data.SqlClient


Module Module1

    Sub Main()
        Console.WriteLine("---------------- ADMINISTRADOR DE VENTAS ---------------- ")
        Console.WriteLine("--------------------------------------------------------- ")
        'ProductoBLL.BuscarProductos() ''
        Console.WriteLine("--------------------------------------------------------- ")
        Console.WriteLine("--------------------------------------------------------- ")
        'VentaBLL.BuscarVentas() 
        Console.WriteLine("--------------------------------------------------------- ")
        Console.WriteLine("--------------------------------------------------------- ")
        'VentaItemsBLL.BuscarVentasItems()


        'Console.Write("Cliente: ")
        'Dim cliente = Console.ReadLine()
        'Console.Write("Telefono: ")
        'Dim telefono = Console.ReadLine()
        'Console.Write("Correo: ")
        'Dim correo = Console.ReadLine()
        'ClienteBLL.AltaCliente(cliente, telefono, correo)

        'Console.Write("Nombre: ")
        'Dim nombre = Console.ReadLine()
        'Console.Write("Precio: ")
        'Dim precio = Console.ReadLine()
        'Console.Write("Categoria: ")
        'Dim categoria = Console.ReadLine()
        'ProductoBLL.AltaProducto(nombre, precio, categoria)

        'Console.Write("ID: ")
        'Dim id = Console.ReadLine()
        'Console.Write("Cliente: ")
        'Dim cliente = Console.ReadLine()
        'Console.Write("Telefono: ")
        'Dim telefono = Console.ReadLine()
        'Console.Write("Categoria: ")
        'Dim correo = Console.ReadLine()
        'ClienteBLL.BajaCliente(id, cliente, telefono, correo)

        'Console.Write("ID: ")
        'Dim id = Console.ReadLine()
        'Console.Write("Nombre: ")
        'Dim nombre = Console.ReadLine()
        'Console.Write("Precio: ")
        'Dim precio = Console.ReadLine()
        'If precio = "" Then
        '    precio = 0
        'End If
        'Console.Write("Categoria: ")
        'Dim categoria = Console.ReadLine()

        'ProductoBLL.BajaProducto(id, nombre, precio, categoria)

        'Console.Write("IDCliente: *7*")
        'Dim idCliente = Console.ReadLine()
        'Console.Write("Fecha: ")
        'Dim fecha = Console.ReadLine()
        'Console.Write("Total: ")
        'Dim total = Console.ReadLine()
        'If total = "" Then
        '    total = 0
        'End If

        'VentaBLL.CrearVenta(idCliente, fecha, total)

        'Console.Write("ID: ")
        'Dim id = Console.ReadLine()
        'Console.Write("IDCliente: ")
        'Dim idCliente = Console.ReadLine()
        'Console.Write("Fecha: ")
        'Dim fecha = Console.ReadLine()
        'Console.Write("Total: ")
        'Dim total = Console.ReadLine()
        'If total = "" Then
        '    total = 0
        'End If

        'VentaBLL.BajaVenta(id, idCliente, fecha, total)

        Console.WriteLine("ID: ")
        Dim id = Console.ReadLine()
        Console.WriteLine("Cliente: ")
        Dim cliente = Console.ReadLine()
        Console.WriteLine("Telefono: ")
        Dim telefono = Console.ReadLine()
        Console.WriteLine("Correo: ")
        Dim correo = Console.ReadLine()

        ClienteBLL.EditarCliente(id, cliente, telefono, correo)

        Console.ReadKey()
    End Sub


End Module
