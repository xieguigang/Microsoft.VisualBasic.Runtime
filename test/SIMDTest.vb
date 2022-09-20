Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Math

Module SIMDTest

    Sub Main()
        Dim a As Double() = 100000.Sequence.Select(Function(i) CDbl(i)).ToArray
        Dim b As Double() = a

        Dim c = SIMD.Add(a, b)

        Pause()
    End Sub
End Module
