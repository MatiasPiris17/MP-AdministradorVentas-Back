Imports System.Data.SqlClient

Public Class VentaItemsDAL

    Private Shared connectionString As String = ConfigurationManager.ConnectionStrings("connection").ConnectionString

    Public Shared Function VerVentaItems() As List(Of VentaItems)
        Try
            Dim ventaItems As New List(Of VentaItems)

            Dim conn As New SqlConnection(connectionString)
            conn.Open()
            Dim query As String = "SELECT * FROM ventasItems"
            Dim cmd As New SqlCommand(query, conn)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()
                ventaItems.Add(New VentaItems() With {
                               .ID = reader(0).ToString,
                               .IDVenta = reader(1).ToString,
                               .IDProducto = reader(2).ToString,
                               .PrecioUnitario = reader(3).ToString,
                               .Cantidad = reader(4).ToString,
                               .PrecioTotal = reader(5).ToString
                })
            End While

            Return ventaItems

        Catch err As Exception
            Console.WriteLine($"Error => ", err.Message)
        End Try

    End Function


    Public Shared Function CrearVentaitem(item As VentaItems) As RespuestaModel(Of VentaItems)
        Try

            Dim nuevaVentaItem As VentaItems = Nothing
            Dim conn As New SqlConnection(connectionString)
            conn.Open()
            Dim query As String = "INSERT INTO ventasitems (IDVenta, IDProducto, PrecioUnitario, Cantidad, PrecioTotal) OUTPUT INSERTED.ID VALUES (@IDVenta, @IDProducto, @PrecioUnitario, @Cantidad, @PrecioTotal)"
            Dim cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@IDVenta", item.IDVenta)
            cmd.Parameters.AddWithValue("@IDProducto", item.IDProducto)
            cmd.Parameters.AddWithValue("@PrecioUnitario", item.PrecioUnitario)
            cmd.Parameters.AddWithValue("@Cantidad", item.Cantidad)
            cmd.Parameters.AddWithValue("@PrecioTotal", item.PrecioTotal)

            Dim IDVentaItem As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            nuevaVentaItem = New VentaItems() With {
            .ID = IDVentaItem,
            .IDVenta = item.IDVenta,
            .IDProducto = item.IDProducto,
            .PrecioUnitario = item.PrecioUnitario,
            .Cantidad = item.Cantidad,
            .PrecioTotal = item.PrecioTotal
            }

            Return New RespuestaModel(Of VentaItems)(True, nuevaVentaItem)

        Catch err As Exception
            Console.WriteLine($"Error => ", err.Message)
        End Try
    End Function

    Public Shared Function EliminarVentaItem(id As Integer) As RespuestaModel(Of VentaItems)
        Try
            Dim ventaItemEliminada As VentaItems = Nothing
            Dim conn As New SqlConnection(connectionString)
            conn.Open()
            Dim selectQuery As String = "SELECT ID, IDVenta, IDProducto, PrecioUnitario, Cantidad, PrecioTotal FROM ventasitems WHERE ID = @ID"
            Dim selectCmd As New SqlCommand(selectQuery, conn)
            selectCmd.Parameters.AddWithValue("@ID", id)
            Dim reader As SqlDataReader = selectCmd.ExecuteReader()

            If reader.Read() Then
                ventaItemEliminada = New VentaItems() With {
                    .ID = reader("ID").ToString,
                    .IDVenta = reader("IDVenta").ToString,
                    .IDProducto = reader("IDProducto").ToString(),
                    .PrecioUnitario = reader("PrecioUnitario").ToString(),
                    .Cantidad = reader("Cantidad").ToString(),
                    .PrecioTotal = reader("PrecioTotal").ToString
                }
            End If
            reader.Close()

            If ventaItemEliminada IsNot Nothing Then
                Dim deleteQuery As String = "DELETE FROM ventas WHERE ID = @ID"
                Dim deleteCmd As New SqlCommand(deleteQuery, conn)
                deleteCmd.Parameters.AddWithValue("@ID", id)
                deleteCmd.ExecuteNonQuery()

                Return New RespuestaModel(Of VentaItems)(True, ventaItemEliminada)
            Else
                Return New RespuestaModel(Of VentaItems)(False, ventaItemEliminada)

            End If

        Catch err As Exception
            Console.WriteLine($"Error => ", err.Message)
        End Try
    End Function

    Public Shared Function ModificarVentaItem(ventaItem As VentaItems) As RespuestaModel(Of VentaItems)
        Try
            Dim filasAfectadas As Integer = 0

            Dim conn As New SqlConnection(connectionString)
            conn.Open()
            Dim query As String = "UPDATE ventas SET IDVenta = @IDVenta, IDProducto = @IDProducto, PrecioUnitario = @PrecioUnitario, Cantidad = @Cantidad, PrecioTotal = @PrecioTotal WHERE ID = @ID"
            Dim cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@ID", ventaItem.ID)
            cmd.Parameters.AddWithValue("@IDCliente", ventaItem.IDVenta)
            cmd.Parameters.AddWithValue("@Fecha", ventaItem.IDProducto)
            cmd.Parameters.AddWithValue("@PrecioUnitario", ventaItem.PrecioUnitario)
            cmd.Parameters.AddWithValue("@Cantidad", ventaItem.Cantidad)
            cmd.Parameters.AddWithValue("@PrecioTotal", ventaItem.PrecioTotal)

            filasAfectadas = cmd.ExecuteNonQuery()

            If filasAfectadas > 0 Then
                Return New RespuestaModel(Of VentaItems)(True, ventaItem)
            End If

            Return New RespuestaModel(Of VentaItems)(False, Nothing)
        Catch err As Exception
            Console.WriteLine("Error al modificar el item de la venta: " & err.Message)
        End Try
    End Function

End Class
