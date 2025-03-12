Imports Examen.Utils

Public Class VentaBLL

    Public Shared Function BuscarVentas() As List(Of Venta)

        Dim ventas As RespuestaModel(Of List(Of Venta)) = VentaDAL.VerVentas()

        If ventas IsNot Nothing AndAlso ventas.Resultado.Count > 0 Then
            For Each venta As Venta In ventas.Resultado
                Console.WriteLine($"ID: {venta.ID}, IDCliente: {venta.IDCliente}, Fecha: {venta.Fecha}, Venta: {venta.Total}")
            Next
        End If

        Return ventas.Resultado

    End Function

    Public Shared Function BuscarVentaID(id As Integer) As Venta
        Dim ventas As RespuestaModel(Of List(Of Venta)) = VentaDAL.VerVentas()
        Return ventas.Resultado.FirstOrDefault(Function(c) c.ID = id)
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

    Public Shared Function BajaVenta(id As Integer, idCliente As Integer, fecha As Date, total As Double) As Venta
        Try

            If total < 0 Then
                Throw New Exception("El precio tiene que ser positivo")
            End If

            Dim bajarVenta = New Venta() With {
                .ID = id,
                .IDCliente = idCliente,
                .Fecha = fecha,
                .Total = total
            }
            Console.WriteLine($"ID: {bajarVenta.ID}, IDCliente: {bajarVenta.IDCliente}, Fecha: {bajarVenta.Fecha}, Total: {bajarVenta.Total}.")

            Dim baja As RespuestaModel(Of Venta) = VentaDAL.EliminarVenta(bajarVenta.ID)

            If baja.Estado Then
                Console.WriteLine("La venta se dio de baja con exito: ")
                Console.WriteLine($"ID: {baja.Resultado.ID}, IDCLiente: {baja.Resultado.IDCliente}, Fecha: {baja.Resultado.Fecha}, Total: {baja.Resultado.Total}")
                Return baja.Resultado
            Else
                Throw New Exception("Venta no encontrado con el ID proporcionado.")
            End If


        Catch err As Exception
            Console.WriteLine(err.Message)

        End Try

    End Function


    Public Shared Function EditarVenta(id As Integer, idCliente As Integer, fecha As Date, total As Double)
        Try
            Dim editar = New Venta() With {
            .ID = id,
            .IDCliente = idCliente,
            .Fecha = fecha,
            .Total = total
            }

            Dim modificacionVenta As RespuestaModel(Of Venta) = VentaDAL.ModificarVenta(editar)

            If modificacionVenta.Estado Then
                Dim ventaEditada = BuscarVentas(modificacionVenta.Resultado.ID)

                Console.Write($"Venta editada con exito.")
                Console.Write($" ID: {ventaEditada.ID}, IDCliente: {ventaEditada.IDCliente}, Fecha: {ventaEditada.Fecha}, Total: {ventaEditada.Total}")
                Return ventaEditada
            Else
                Throw New Exception("El cliente no se pudo editar")
            End If

        Catch err As Exception
            Console.WriteLine(err.Message)
        End Try
    End Function

End Class
