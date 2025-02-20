Imports Examen.Utils

Public Class ClienteBLL

    'BuscarClientes
    Public Shared Function BuscarClientes() As List(Of Cliente)

        Dim clientes As List(Of Cliente) = ClienteDAL.VerClientes()
        If clientes IsNot Nothing AndAlso clientes.Count > 0 Then
            For Each cliente As Cliente In clientes
                Console.WriteLine("ID: " & cliente.ID &
                ", Cliente: " & cliente.Cliente &
                ", Teléfono: " & cliente.Telefono &
                ", Correo: " & cliente.Correo)
            Next
        Else
            Console.WriteLine("No se encontraron clientes.")
        End If

        Return clientes

    End Function

    Public Shared Function AltaCliente(cliente As String, telefono As String, correo As String) As Cliente
        Dim nuevoCliente = New Cliente With {
            .Cliente = cliente,
            .Telefono = telefono,
            .Correo = correo
        }

        Dim clienteCreado = ClienteDAL.CrearCliente(nuevoCliente)
        Console.WriteLine("El cliente se dio de alta con exito!")
        Console.WriteLine($"ID: {clienteCreado.ID}, Cliente: {clienteCreado.Cliente}, Telefono: {clienteCreado.Telefono}, Correo: {clienteCreado.Correo}")
        Return nuevoCliente
    End Function

    Public Shared Function BajaCliente(id As Integer, cliente As String, telefono As String, correo As String) As Cliente
        Try
            Dim bajarCliente = New Cliente With {
            .ID = id,
            .Cliente = cliente,
            .Telefono = telefono,
            .Correo = correo
        }

            Dim baja As RespuestaModel(Of Cliente) = ClienteDAL.EliminarCliente(bajarCliente.ID)

            If baja.Estado Then
                Console.Write("El cliente se dio de baja con exito: ")
                Console.Write($"Cliente: {baja.Resultado.Cliente}, Telefono: {baja.Resultado.Telefono}, Correo: {baja.Resultado.Correo}")
                Return baja.Resultado
            Else
                Console.WriteLine($"ID: {BajaCliente.ID}, Cliente: {bajarCliente.Cliente}, Telefono: {bajarCliente.Telefono}, Correo: {bajarCliente.Correo}.")
                Throw New Exception("Cliente no encontrado con el ID proporcionado")
            End If


        Catch err As Exception
            Console.WriteLine(err.Message)
        End Try


    End Function
End Class
