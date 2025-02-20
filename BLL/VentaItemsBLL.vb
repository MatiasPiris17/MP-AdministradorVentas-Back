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

End Class
