Imports System.Text

Namespace Math.Information

    ''' <summary>
    ''' 
    ''' </summary>
    Public Class LempelZivComplexity

        ''' <summary>
        ''' 计算二进制序列的Lempel-Ziv复杂度
        ''' </summary>
        ''' <param name="sequence">二进制序列（由'0'和'1'组成的字符串）</param>
        ''' <returns>返回复杂度值c(n)和归一化复杂度C(n)</returns>
        Public Shared Function ComputeLZC(sequence As String) As (Complexity As Integer, NormalizedComplexity As Double)
            If String.IsNullOrEmpty(sequence) Then
                Return (0, 0)
            End If

            Dim n As Integer = sequence.Length
            Dim i As Integer = 0
            Dim j As Integer = 1
            Dim c As Integer = 0
            Dim dictionary As New HashSet(Of String)

            While i < n
                Dim currentSubstring As String = sequence.Substring(i, j - i)
                If dictionary.Contains(currentSubstring) Then
                    j += 1
                    If j > n Then
                        c += 1
                        Exit While
                    End If
                Else
                    dictionary.Add(currentSubstring)
                    c += 1
                    i = j
                    j = i + 1
                    If j > n Then
                        Exit While
                    End If
                End If
            End While

            ' 计算归一化复杂度 [5](@ref)
            Dim b_n As Double = n / Math.Log(n, 2)
            Dim normalizedC As Double = c / b_n

            Return (c, normalizedC)
        End Function

        ''' <summary>
        ''' 将数值序列转换为二进制序列（基于中值）用于LZC计算 [5](@ref)
        ''' </summary>
        ''' <param name="data">输入数值序列</param>
        ''' <returns>二进制序列字符串</returns>
        Public Shared Function ConvertToBinarySequence(data As Double()) As String
            If data Is Nothing OrElse data.Length = 0 Then
                Return String.Empty
            End If

            ' 计算中值作为阈值
            Dim sortedData = data.OrderBy(Function(x) x).ToArray()
            Dim median As Double
            If sortedData.Length Mod 2 = 0 Then
                median = (sortedData(sortedData.Length \ 2 - 1) + sortedData(sortedData.Length \  ￣2)) / 2.0
            Else
                median = sortedData(sortedData.Length \ 2)
            End If

            Dim binarySeq As New StringBuilder()
            For Each value In data
                If value >= median Then
                    binarySeq.Append("1")
                Else
                    binarySeq.Append("0")
                End If
            Next

            Return binarySeq.ToString()
        End Function

    End Class
End Namespace