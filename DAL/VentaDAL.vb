Imports System.Data.SqlClient

Public Class VentaDAL

    Private Shared connectionString As String = ConfigurationManager.ConnectionStrings("connection").ConnectionString

    Public Shared Function VerVentas() As RespuestaModel(Of List(Of Venta))
        Try
            Dim ventas As New List(Of Venta)

            Dim conn As New SqlConnection(connectionString)

            conn.Open()

            Dim query As String = "SELECT * FROM ventas"
            Dim cmd As New SqlCommand(query, conn)

            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()

                ventas.Add(New Venta() With {
                            .ID = reader(0).ToString,
                            .IDCliente = reader(1).ToString,
                            .Fecha = reader(2).ToString,
                            .Total = reader(3).ToString
                             })
            End While

            reader.Close()

            Return New RespuestaModel(Of List(Of Venta))(True, ventas)
        Catch err As Exception
            Console.WriteLine($"Error => ", err.Message)
        End Try

    End Function

    Public Shared Function CrearVenta(venta As Venta) As RespuestaModel(Of Venta)

        Try
            Dim nuevaVenta As Venta = Nothing
            Dim conn As New SqlConnection(connectionString)
            conn.Open()
            Dim query As String = "INSERT INTO ventas (IDCliente, Fecha, Total) OUTPUT INSERTED.ID VALUES (@IDCliente, @Fecha, @Total)"
            Dim cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@IDCliente", venta.IDCliente)
            cmd.Parameters.AddWithValue("@Fecha", venta.Fecha)
            cmd.Parameters.AddWithValue("@Total", venta.Total)

            Dim IDVenta As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            nuevaVenta = New Venta() With {
            .ID = IDVenta,
            .IDCliente = venta.IDCliente,
            .Fecha = venta.Fecha,
            .Total = venta.Total
            }

            Return New RespuestaModel(Of Venta)(True, nuevaVenta)

        Catch err As Exception
            Console.WriteLine($"Error => ", err.Message)
        End Try

    End Function


    Public Shared Function EliminarVenta(id As Integer) As RespuestaModel(Of Venta)
        Try
            Dim ventaEliminada As Venta = Nothing
            Dim conn As New SqlConnection(connectionString)
            conn.Open()
            Dim selectQuery As String = "SELECT ID, IDCliente, Fecha, Total FROM ventas WHERE ID = @ID"
            Dim selectCmd As New SqlCommand(selectQuery, conn)
            selectCmd.Parameters.AddWithValue("@ID", id)
            Dim reader As SqlDataReader = selectCmd.ExecuteReader()

            If reader.Read() Then
                ventaEliminada = New Venta() With {
                    .ID = reader("ID"),
                    .IDCliente = reader("IDCliente").ToString(),
                    .Fecha = reader("Fecha").ToString(),
                    .Total = reader("Total").ToString()
                }
            End If
            reader.Close()


            If ventaEliminada IsNot Nothing Then
                Dim deleteQuery As String = "DELETE FROM ventas WHERE ID = @ID"
                Dim deleteCmd As New SqlCommand(deleteQuery, conn)
                deleteCmd.Parameters.AddWithValue("@ID", id)
                deleteCmd.ExecuteNonQuery()

                Return New RespuestaModel(Of Venta)(True, ventaEliminada)
            Else
                Return New RespuestaModel(Of Venta)(False, ventaEliminada)

            End If

        Catch err As Exception
            Console.WriteLine($"Error => ", err.Message)
        End Try
    End Function

    Public Shared Function ModificarVenta(venta As Venta) As RespuestaModel(Of Venta)

        Try
            Dim filasAfectadas As Integer = 0
            Dim conn As New SqlConnection(connectionString)
            conn.Open()
            Dim query As String = "UPDATE ventas SET IDCliente = @IDCliente, Fecha = @Fecha, Total = @Total WHERE ID = @ID"
            Dim cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@ID", venta.ID)
            cmd.Parameters.AddWithValue("@IDCliente", venta.IDCliente)
            cmd.Parameters.AddWithValue("@Fecha", venta.Fecha)
            cmd.Parameters.AddWithValue("@Total", venta.Total)
            filasAfectadas = cmd.ExecuteNonQuery()

            If filasAfectadas > 0 Then
                Return New RespuestaModel(Of Venta)(True, venta)
            End If

            Return New RespuestaModel(Of Venta)(False, Nothing)

        Catch err As Exception
            Console.WriteLine("Error al modificar el producto: " & err.Message)
        End Try

    End Function



End Class
