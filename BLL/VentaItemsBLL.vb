Imports System.Xml.Schema

Public Class VentaItemsBLL

    Public Shared Function BuscarVentasItems() As List(Of VentaItems)

        Dim ventaItems As List(Of VentaItems) = VentaItemsDAL.VerVentaItems()
        If ventaItems IsNot Nothing AndAlso ventaItems.Count > 0 Then
            For Each item As VentaItems In ventaItems
                Console.WriteLine($"ID: {item.IDVenta}, IDVenta: {item.IDVenta}, IDProducto: {item.IDProducto}, PrecioUnitario: {item.PrecioUnitario}, Cantidad: {item.Cantidad}, PrecioTotal: {item.PrecioTotal}")
            Next
        End If

        Return ventaItems
    End Function

    Public Shared Function BuscarVentasItemID(id As Integer) As VentaItems
        Dim items As List(Of VentaItems) = VentaItemsDAL.VerVentaItems()
        Return items.FirstOrDefault(Function(c) c.ID = id)
    End Function

    Public Shared Function AltaVentaItem(idVenta As Integer, idProducto As Integer, precioUnitario As Double, cantidad As Double, precioTotal As Double)
        Try
            Dim nuevaVentaItem = New VentaItems() With {
            .IDVenta = idVenta,
            .IDProducto = idProducto,
            .PrecioUnitario = precioUnitario,
            .Cantidad = cantidad,
            .PrecioTotal = precioTotal
            }
            Console.WriteLine($"IDVenta: {nuevaVentaItem.IDVenta}, IDProducto: {nuevaVentaItem.IDProducto}, PrecioUnitario: {nuevaVentaItem.PrecioUnitario}, Cantidad: {nuevaVentaItem.Cantidad}, PrecioTotal: {nuevaVentaItem.PrecioTotal}")

            Dim ventaItemCreado = VentaItemsDAL.CrearVentaitem(nuevaVentaItem)

            If ventaItemCreado.Estado Then
                Console.WriteLine($"El itemn/producto se dio de alta en la venta {ventaItemCreado.Resultado.ID} con exito!")
                Console.WriteLine($"ID: {ventaItemCreado.Resultado.ID}, IDVenta: {nuevaVentaItem.IDVenta}, IDProducto: {nuevaVentaItem.IDProducto}, PrecioUnitario: {nuevaVentaItem.PrecioUnitario}, Cantidad: {nuevaVentaItem.Cantidad}, PrecioTotal: {nuevaVentaItem.PrecioTotal}")

                Return ventaItemCreado.Resultado
            Else
                Throw New Exception("No se pudo dar de alta el item/producto de la venta")
            End If

        Catch err As Exception
            Console.WriteLine("ERROR => ", err.Message)
        End Try
    End Function

    Public Shared Function BajaVentaItem() As VentaItems

    End Function

    Public Shared Function EditarVentaItem(id As Integer, idVenta As Integer, idProducto As Integer, precioUnitario As Double, cantidad As Integer, precioTotal As Double) As VentaItems
        Try
            Dim editar = New VentaItems() With {
                        .ID = id,
                        .IDVenta = idVenta,
                        .IDProducto = idProducto,
                        .PrecioUnitario = precioUnitario,
                        .Cantidad = cantidad,
                        .PrecioTotal = precioTotal,
                        }

            Dim modificacionVenta As RespuestaModel(Of VentaItems) = VentaItemsDAL.ModificarVentaItem(editar)

            If modificacionVenta.Estado Then
                Dim ventaEditada = BuscarVentasItemID(modificacionVenta.Resultado.ID)

                Console.WriteLine("Item editado con exito")
                Return ventaEditada
            End If

        Catch err As Exception
            Console.WriteLine(err.Message)
        End Try

    End Function

End Class
