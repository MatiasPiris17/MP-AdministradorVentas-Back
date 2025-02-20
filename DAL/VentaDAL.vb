Imports System.Data.SqlClient
Imports Examen.Utils

Public Class VentaDAL

    Private Shared connectionString As String = ConfigurationManager.ConnectionStrings("connection").ConnectionString

    Public Shared Function VerVentas() As List(Of Venta)
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

            Return ventas
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


    Public Shared Function EliminarVenta() As RespuestaModel(Of Venta)

    End Function

End Class
