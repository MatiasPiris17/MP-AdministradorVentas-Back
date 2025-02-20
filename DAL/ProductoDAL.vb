Imports System.Data.SqlClient
Imports Examen.Utils

Public Class ProductoDAL

    Private Shared connectionString As String = ConfigurationManager.ConnectionStrings("connection").ConnectionString

    Public Shared Function VerProductos() As List(Of Producto)
        Try
            Dim productos As New List(Of Producto)

            Dim conn As New SqlConnection(connectionString)

            conn.Open()

            Dim query As String = "SELECT * FROM productos"
            Dim cmd As New SqlCommand(query, conn)

            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()

                productos.Add(New Producto() With {
                            .ID = reader(0).ToString,
                            .Nombre = reader(1).ToString,
                            .Precio = reader(2).ToString,
                            .Categoria = reader(3).ToString
                             })
            End While

            reader.Close()

            Return productos

        Catch err As Exception
            Console.WriteLine($"Error => ", err.Message)
        End Try

    End Function

    Public Shared Function CrearProducto(producto As Producto) As Producto
        Try
            Dim nuevoProducto As Producto
            Dim conn As New SqlConnection(connectionString)
            conn.Open()
            Dim query As String = "INSERT INTO productos (Nombre, Precio, Categoria) OUTPUT INSERTED.ID VALUES (@Nombre, @Precio, @Categoria)"
            Dim cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@Nombre", producto.Nombre)
            cmd.Parameters.AddWithValue("@Precio", producto.Precio)
            cmd.Parameters.AddWithValue("@Categoria", producto.Categoria)

            Dim IDProducto As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            nuevoProducto = New Producto() With {
            .ID = IDProducto,
            .Nombre = producto.Nombre,
            .Precio = producto.Precio,
            .Categoria = producto.Categoria
            }

            Return nuevoProducto

        Catch err As Exception
            Console.WriteLine($"Error => ", err.Message)
        End Try

    End Function

    Public Shared Function EliminarProducto(id As Integer) As RespuestaModel(Of Producto)

        Try

            Dim conn As New SqlConnection(connectionString)
            Dim productoEliminado As Producto = Nothing
            conn.Open()
            Dim selectQuery As String = "SELECT ID, Nombre, Precio, Categoria FROM productos WHERE ID = @ID"
            Dim selectCmd As New SqlCommand(selectQuery, conn)
            selectCmd.Parameters.AddWithValue("@ID", id)
            Dim reader As SqlDataReader = selectCmd.ExecuteReader()

            If reader.Read() Then
                productoEliminado = New Producto() With {
                    .ID = reader("ID"),
                    .Nombre = reader("Nombre").ToString(),
                    .Precio = reader("Precio").ToString(),
                    .Categoria = reader("Categoria").ToString()
                }
            End If
            reader.Close()

            If productoEliminado IsNot Nothing Then
                Dim deleteQuery As String = "DELETE FROM productos WHERE ID = @ID"
                Dim deleteCmd As New SqlCommand(deleteQuery, conn)
                deleteCmd.Parameters.AddWithValue("@ID", id)
                deleteCmd.ExecuteNonQuery()

                Return New RespuestaModel(Of Producto)(True, productoEliminado)
            Else
                Return New RespuestaModel(Of Producto)(False, productoEliminado)
            End If

        Catch err As Exception
            Console.WriteLine($"Error => ", err.Message)

        End Try

    End Function


End Class
