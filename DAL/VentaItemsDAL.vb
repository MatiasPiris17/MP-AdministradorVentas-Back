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

End Class
