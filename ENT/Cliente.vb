Imports Examen.Utils

Public Class Cliente
    Public Property ID As Integer
    Public Property Cliente As String
    Public Property Telefono As String
    Public Property Correo As String

    Public Shared Widening Operator CType(v As RespuestaModel(Of Cliente)) As Cliente
        Throw New NotImplementedException()
    End Operator
End Class
