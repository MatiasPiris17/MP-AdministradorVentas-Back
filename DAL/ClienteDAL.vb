Imports System.Configuration
Imports System.Data.SqlClient
Imports Examen.Utils

Public Class ClienteDAL

    Private Shared connectionString As String = ConfigurationManager.ConnectionStrings("connection").ConnectionString

    Public Shared Function VerClientes() As List(Of Cliente)

        Try
            Dim clientes As New List(Of Cliente)

            Dim conn As New SqlConnection(connectionString)

            conn.Open()

            'Console.WriteLine("Conexión exitosa a SQL Server!")

            Dim query As String = "SELECT * FROM clientes"
            Dim cmd As New SqlCommand(query, conn)

            Dim reader As SqlDataReader = cmd.ExecuteReader()

            While reader.Read()

                clientes.Add(New Cliente() With {
                            .ID = reader(0).ToString,
                            .Cliente = reader(1).ToString,
                            .Telefono = reader(2).ToString,
                            .Correo = reader(3).ToString
                             })
            End While

            reader.Close()

            Return clientes

        Catch err As Exception
            Console.WriteLine($"Error => ", err.Message)
        End Try

    End Function

    Public Shared Function CrearCliente(cliente As Cliente) As Cliente
        Try
            Dim nuevoCliente As Cliente = Nothing
            Dim conn As New SqlConnection(connectionString)
            conn.Open()
            Dim query As String = "INSERT INTO clientes (Cliente, Telefono, Correo) OUTPUT INSERTED.ID VALUES (@Cliente, @Telefono, @Correo)"
            Dim cmd As New SqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@Cliente", cliente.Cliente)
            cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono)
            cmd.Parameters.AddWithValue("@Correo", cliente.Correo)

            Dim IDCliente As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            nuevoCliente = New Cliente() With {
            .ID = IDCliente,
            .Cliente = cliente.Cliente,
            .Telefono = cliente.Telefono,
            .Correo = cliente.Correo
            }

            Return nuevoCliente

        Catch err As Exception
            Console.WriteLine($"Error => ", err.Message)
        End Try

    End Function


    Public Shared Function EliminarCliente(id As Integer) As RespuestaModel(Of Cliente)
        Try

            Dim conn As New SqlConnection(connectionString)
            Dim clienteEliminado As Cliente = Nothing
            conn.Open()
            Dim selectQuery As String = "SELECT ID, Cliente, Telefono, Correo FROM clientes WHERE ID = @ID"
            Dim selectCmd As New SqlCommand(selectQuery, conn)
            selectCmd.Parameters.AddWithValue("@ID", id)
            Dim reader As SqlDataReader = selectCmd.ExecuteReader()

            If reader.Read() Then
                clienteEliminado = New Cliente() With {
                    .ID = reader("ID"),
                    .Cliente = reader("Cliente").ToString(),
                    .Telefono = reader("Telefono").ToString(),
                    .Correo = reader("Correo").ToString()
                }
            End If

            reader.Close()

            If clienteEliminado IsNot Nothing Then
                Dim deleteQuery As String = "DELETE FROM clientes WHERE ID = @ID"
                Dim deleteCmd As New SqlCommand(deleteQuery, conn)
                deleteCmd.Parameters.AddWithValue("@ID", id)
                deleteCmd.ExecuteNonQuery()

                Return New RespuestaModel(Of Cliente)(True, clienteEliminado)
            Else
                Return New RespuestaModel(Of Cliente)(False, clienteEliminado)
            End If

        Catch err As Exception
            Console.WriteLine($"Error => ", err.Message)
        End Try
    End Function



End Class
