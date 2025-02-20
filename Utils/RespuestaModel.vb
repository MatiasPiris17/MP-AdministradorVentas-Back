Namespace Utils
    Public Class RespuestaModel(Of T)
        Public Property Estado As Boolean
        Public Property Resultado As T

        Public Sub New(estado As Boolean, resultado As T)
            Me.Estado = estado
            Me.Resultado = resultado
        End Sub
    End Class

End Namespace
