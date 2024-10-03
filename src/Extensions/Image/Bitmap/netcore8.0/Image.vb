﻿Imports System.Drawing
Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Imaging.BitmapImage

Namespace Imaging

#If NET8_0_OR_GREATER Then

    ''' <summary>
    ''' the abstract image data model, example as gdi+ raster image bitmap, svg image, pdf image, etc
    ''' </summary>
    Public MustInherit Class Image : Implements IDisposable

        Private disposedValue As Boolean
        Public MustOverride ReadOnly Property Size As Size

        Public ReadOnly Property Width As Integer
            Get
                Return Size.Width
            End Get
        End Property

        Public ReadOnly Property Height As Integer
            Get
                Return Size.Height
            End Get
        End Property

        Public MustOverride Sub Save(s As Stream, format As ImageFormats)

        Public Shared Function FromStream(s As Stream) As Bitmap
            Throw New NotImplementedException
        End Function

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: 释放托管状态(托管对象)
                End If

                ' TODO: 释放未托管的资源(未托管的对象)并重写终结器
                ' TODO: 将大型字段设置为 null
                disposedValue = True
            End If
        End Sub

        ' ' TODO: 仅当“Dispose(disposing As Boolean)”拥有用于释放未托管资源的代码时才替代终结器
        ' Protected Overrides Sub Finalize()
        '     ' 不要更改此代码。请将清理代码放入“Dispose(disposing As Boolean)”方法中
        '     Dispose(disposing:=False)
        '     MyBase.Finalize()
        ' End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            ' 不要更改此代码。请将清理代码放入“Dispose(disposing As Boolean)”方法中
            Dispose(disposing:=True)
            GC.SuppressFinalize(Me)
        End Sub
    End Class

    ''' <summary>
    ''' the gdi+ raster image data in memory
    ''' </summary>
    Public Class Bitmap : Inherits Image

        Public Overrides ReadOnly Property Size As Size
            Get
                Return memoryBuffer.Size
            End Get
        End Property

        ReadOnly memoryBuffer As BitmapBuffer

        Sub New(data As BitmapBuffer)
            memoryBuffer = data
        End Sub

        Sub New(copy As Image)
            Throw New NotImplementedException
        End Sub

        Sub New(size As Size)
            Call Me.New(size.Width, size.Height)
        End Sub

        Sub New(width As Integer, height As Integer)
            Throw New NotImplementedException
        End Sub

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Sub SetPixel(x As Integer, y As Integer, pixel As Color)
            Call memoryBuffer.SetPixel(x, y, pixel)
        End Sub

        Public Function Resize(newWidth As Integer, newHeight As Integer) As Bitmap
            Dim pixels = memoryBuffer.GetARGB
            pixels = BitmapResizer.ResizeImage(pixels, memoryBuffer.Width, memoryBuffer.Height, newWidth, newHeight)
            ' construct bitmap data based on pixels matrix
            Dim sizedBitmap As New BitmapBuffer(pixels, New Size(newWidth, newHeight))
            Dim bitmap As New Bitmap(sizedBitmap)
            Return bitmap
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function GetPixel(X As Integer, Y As Integer) As Color
            Return memoryBuffer.GetPixel(X, Y)
        End Function

        Public Overrides Sub Save(s As Stream, format As ImageFormats)
            Throw New NotImplementedException()
        End Sub
    End Class
#End If
End Namespace