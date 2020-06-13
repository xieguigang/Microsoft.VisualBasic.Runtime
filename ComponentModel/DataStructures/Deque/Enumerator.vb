Namespace ComponentModel.Collection.Deque

    Friend Class Enumerator(Of S)
        Implements IEnumerator(Of S)
        'initialize with -1 to ensure that InvalidOperationException is thrown when Current is called befor the first call of MoveNext
        Private Property curIndex As Integer = -1
        ''' <summary>
        ''' version of Deque(Of T) this Enumerator is enumerating from the moment this enumerator has been created
        ''' </summary>
        ''' 
        Private Property version As Long
        ''' <summary>
        ''' Deque(Of T) this enumerator is enumerating
        ''' </summary>
        Private Property Que As Deque(Of S)

        Public Sub New(ByVal que As Deque(Of S), ByVal version As Long)
            Me.version = version
            Me.Que = que
        End Sub

        Public ReadOnly Property p_Current As S Implements IEnumerator(Of S).Current
            Get

                If curIndex < 0 OrElse curIndex >= Que.Count OrElse version <> Que.version Then
                    Throw New InvalidOperationException()
                Else
                    Return Que(curIndex)
                End If
            End Get
        End Property

        Private ReadOnly Property Current As Object Implements IEnumerator.Current
            Get
                Return p_Current
            End Get
        End Property

        Public Sub Dispose() Implements IDisposable.Dispose
            Que = Nothing
            curIndex = Nothing
            version = Nothing
        End Sub

        Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
            If version <> Que.version Then
                Throw New InvalidOperationException()
            End If

            curIndex += 1

            If curIndex >= Que.Count Then
                Return False
            End If

            Return True
        End Function

        Public Sub Reset() Implements IEnumerator.Reset
            curIndex = 0
        End Sub
    End Class
End Namespace