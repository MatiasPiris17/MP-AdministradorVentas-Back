Public Class VentaBLL

    Public Shared Function BuscarVentas() As List(Of Venta)

        Dim ventas As List(Of Venta) = VentaDAL.VerVentas

        If ventas IsNot Nothing AndAlso ventas.Count > 0 Then
            For Each venta As Venta In ventas

                Console.WriteLine($"ID: {venta.ID}, IDCliente: {venta.IDCliente}, Fecha: {venta.Fecha}, Venta: {venta.Total}")

            Next
        End If
        Return ventas

    End Function

    Public Shared Function CrearVenta(idCliente As Integer, fecha As Date, total As Double) As Venta
        Try
            If total < 0 Then
                Throw New Exception("El total tiene que ser positivo")
            End If


            Dim nuevaVenta = New Venta() With { ' agregar filtro para buscar cliente
                    .IDCliente = idCliente,
                    .Fecha = fecha,
                    .Total = total
                    }

            Dim ventaCreada = VentaDAL.CrearVenta(nuevaVenta)

            If ventaCreada.Estado Then
                Console.WriteLine("El producto se dio de alta con exito!")
                Console.WriteLine($"ID: {ventaCreada.Resultado.ID}, IDCliente: {ventaCreada.Resultado.IDCliente}, Fecha: {ventaCreada.Resultado.Fecha}, Total: {ventaCreada.Resultado.Total}")

                Return ventaCreada.Resultado
            Else
                Console.WriteLine($"ID: {nuevaVenta.ID}, IDCliente: {nuevaVenta.IDCliente}, Fecha: {nuevaVenta.Fecha}, Total: {nuevaVenta.Total}.")
                Throw New Exception("No se pudo dar de alta la venta")

            End If


        Catch err As Exception
            Console.WriteLine("ERROR => ", err.Message)
        End Try

    End Function

    Public Shared Function BajaVenta() As Venta

    End Function

End Class
