﻿
Imports System.Drawing

Namespace Imaging.Driver

    Public Delegate Function CreateGraphic(size As Size, fill As Color, dpi As Integer) As IGraphics

    Public Module DriverLoad

        Public libgdiplus_raster As CreateGraphic
        Public svg As CreateGraphic
        Public pdf As CreateGraphic

        Sub New()
        End Sub

        Public Function DefaultGraphicsDevice(Optional [default] As Drivers? = Nothing) As Drivers
            Static __default As Drivers = Drivers.GDI

            If Not [default] Is Nothing Then
                __default = [default]
            End If

            Return __default
        End Function

        Public Function CreateGraphicsDevice(size As Size,
                                             Optional fill As String = NameOf(Color.Transparent),
                                             Optional dpi As Integer = 100,
                                             Optional driver As Drivers = Drivers.Default) As IGraphics

            Dim fill_color As Color = fill.TranslateColor

            If driver = Drivers.Default Then
                driver = DefaultGraphicsDevice()
            End If

            Select Case driver
                Case Drivers.SVG : Return svg(size, fill_color, dpi)
                Case Drivers.PDF : Return pdf(size, fill_color, dpi)
                Case Drivers.GDI : Return libgdiplus_raster(size, fill_color, dpi)
                Case Else
                    Throw New NotImplementedException(driver.Description)
            End Select
        End Function
    End Module
End Namespace