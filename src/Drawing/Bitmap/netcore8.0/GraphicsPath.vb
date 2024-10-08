﻿Imports System.Drawing

Namespace Imaging

#If NET8_0_OR_GREATER Then

    Public Class PathData

        Public Property Points As PointF()

    End Class

    Public Class GraphicsPath

        Public ReadOnly Property PathData As PathData
            Get
                Throw New NotImplementedException
            End Get
        End Property

        Public ReadOnly Property PathPoints As PointF()
            Get
                Throw New NotImplementedException
            End Get
        End Property

        Sub New()
        End Sub

        Sub New(points As IEnumerable(Of PointF))
            Call AddPolygon(points.ToArray)
            Call CloseAllFigures()
        End Sub

        Public MustInherit Class op

        End Class

        Public Class Op_AddLine : Inherits op

            Public Property a As PointF
            Public Property b As PointF

            Sub New(a As PointF, b As PointF)
                _a = a
                _b = b
            End Sub

        End Class

        Public Class op_AddBezier : Inherits op

            Public Property pt1 As PointF
            Public Property pt2 As PointF
            Public Property pt3 As PointF
            Public Property pt4 As PointF

            Sub New(pt1 As PointF, pt2 As PointF, pt3 As PointF, pt4 As PointF)
                _pt1 = pt1
                _pt2 = pt2
                _pt3 = pt3
                _pt4 = pt4
            End Sub
        End Class

        Public Class op_AddCurve : Inherits op

            Public Property points As PointF()

        End Class

        Public Class op_AddLines : Inherits op

            Public Property points As PointF()

        End Class

        Public Class op_Reset : Inherits op
        End Class

        Public Class op_CloseAllFigures : Inherits op
        End Class

        Public Class op_CloseFigure : Inherits op
        End Class

        Public Class op_AddArc : Inherits op
            Public Property rect As RectangleF
            Public Property startAngle As Single
            Public Property sweepAngle As Single
        End Class

        Public Class op_AddRectangle : Inherits op
            Public Property rect As RectangleF
        End Class

        Public Class op_AddPolygon : Inherits op
            Public Property points As PointF()
        End Class

        Public Class op_AddEllipse : Inherits op
            Public Property x As Single
            Public Property y As Single
            Public Property r1 As Single
            Public Property r2 As Single
        End Class

        Dim opSet As New List(Of op)

        Public Sub AddEllipse(x As Single, y As Single, r1 As Single, r2 As Single)
            Call opSet.Add(New op_AddEllipse With {.x = x, .y = y, .r1 = r1, .r2 = r2})
        End Sub

        Public Sub AddPolygon(points As PointF())
            Call opSet.Add(New op_AddPolygon With {.points = points})
        End Sub

        Public Sub AddRectangle(rect As RectangleF)
            Call opSet.Add(New op_AddRectangle With {.rect = rect})
        End Sub

        Public Sub AddArc(rect As RectangleF, startAngle!, sweepAngle!)
            Call opSet.Add(New op_AddArc With {.rect = rect, .startAngle = startAngle, .sweepAngle = sweepAngle})
        End Sub

        Public Sub AddLine(a As PointF, b As PointF)
            Call opSet.Add(New Op_AddLine(a, b))
        End Sub

        Public Sub AddBezier(pt1 As PointF, pt2 As PointF, pt3 As PointF, pt4 As PointF)
            Call opSet.Add(New op_AddBezier(pt1, pt2, pt3, pt4))
        End Sub

        Public Sub AddCurve(ParamArray points As PointF())
            Call opSet.Add(New op_AddCurve With {.points = points})
        End Sub

        Public Sub AddLines(ParamArray points As PointF())
            Call opSet.Add(New op_AddLines With {.points = points})
        End Sub

        Public Sub Reset()
            Call opSet.Add(New op_Reset())
        End Sub

        Public Sub CloseAllFigures()
            Call opSet.Add(New op_CloseAllFigures())
        End Sub

        Public Sub CloseFigure()
            Call opSet.Add(New op_CloseFigure)
        End Sub
    End Class
#End If
End Namespace