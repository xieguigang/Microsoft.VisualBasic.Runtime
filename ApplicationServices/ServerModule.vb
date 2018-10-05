Imports Microsoft.VisualBasic.Net.Protocols.Reflection
Imports Microsoft.VisualBasic.Net.Tcp

Namespace ApplicationServices

    ''' <summary>
    ''' The Tcp socket server abstract
    ''' </summary>
    Public MustInherit Class ServerModule : Implements IDisposable

        ''' <summary>
        ''' Tcp socket
        ''' </summary>
        Protected socket As TcpServicesSocket

        ''' <summary>
        ''' Create a new server module based on a tcp server socket.
        ''' </summary>
        ''' <param name="port">The listen port of the tcp socket.</param>
        Sub New(port As Integer)
            socket = New TcpServicesSocket(port, AddressOf LogException) With {
                .Responsehandler = ProtocolHandler()
            }
        End Sub

        Protected MustOverride Sub LogException(ex As Exception)
        ''' <summary>
        ''' Generally, using a <see cref="Protocol"/> attribute using reflection way is recommended.
        ''' </summary>
        ''' <returns></returns>
        Protected MustOverride Function ProtocolHandler() As ProtocolHandler

        Public Overridable Function Run() As Integer
            Return socket.Run
        End Function

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    Call socket.Dispose()
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            ' TODO: uncomment the following line if Finalize() is overridden above.
            ' GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class
End Namespace