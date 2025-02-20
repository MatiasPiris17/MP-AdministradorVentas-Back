Imports Examen.Utils

Public Class ProductoBLL

    Public Shared Function BuscarProductos() As List(Of Producto)
        Dim productos As List(Of Producto) = ProductoDAL.VerProductos

        If productos IsNot Nothing AndAlso productos.Count > 0 Then
            For Each producto As Producto In productos

                Console.WriteLine($"ID: {producto.ID}, Nombre: {producto.Nombre}, Precio: {producto.Precio}, Categoria: {producto.Categoria}")

            Next

        Else
            Console.WriteLine("No se encontraron clientes.")
        End If

        Return productos

    End Function

    Public Shared Function AltaProducto(nombre As String, precio As Double, categoria As String) As Producto
        Dim nuevoProducto = New Producto With {
            .Nombre = nombre,
            .Precio = precio,
            .Categoria = categoria
        }

        Dim productoCreado = ProductoDAL.CrearProducto(nuevoProducto)
        Console.WriteLine("El producto se dio de alta con exito!")
        Console.WriteLine($"ID: {productoCreado.ID}, Nombre: {productoCreado.Nombre}, Precio: {productoCreado.Precio}, Categoria: {productoCreado.Categoria}")
        Return productoCreado

    End Function

    Public Shared Function BajaProducto(id As Integer, nombre As String, precio As Double, categoria As String) As Producto
        Try
            If precio < 0 Then
                Throw New Exception("El precio tiene que ser positivo")
            End If

            Dim bajarProducto = New Producto With {
                .ID = id,
                .Nombre = nombre,
                .Precio = precio,
                .Categoria = categoria
                }

            Dim baja As RespuestaModel(Of Producto) = ProductoDAL.EliminarProducto(bajarProducto.ID)

            If baja.Estado Then
                Console.WriteLine("El producto se dio de baja con exito: ")
                Console.WriteLine($"Nombre: {baja.Resultado.Nombre}, Precio: {baja.Resultado.Precio}, Categoria: {baja.Resultado.Categoria}")
                Return baja.Resultado
            Else
                Console.WriteLine($"ID: {bajarProducto.ID}, Nombre: {bajarProducto.Nombre}, Precio: {bajarProducto.Precio}, Categoria: {bajarProducto.Categoria}.")
                Throw New Exception("Producto no encontrado con el ID proporcionado")
            End If

        Catch err As Exception
            Console.WriteLine(err.Message)
        End Try
    End Function

End Class

