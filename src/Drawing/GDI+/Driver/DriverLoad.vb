﻿#Region "Microsoft.VisualBasic::3fde03fee4dd2d2c804d1c7dce2f54da, Microsoft.VisualBasic.Core\src\Drawing\GDI+\Driver\DriverLoad.vb"

    ' Author:
    ' 
    '       asuka (amethyst.asuka@gcmodeller.org)
    '       xie (genetics@smrucc.org)
    '       xieguigang (xie.guigang@live.com)
    ' 
    ' Copyright (c) 2018 GPL3 Licensed
    ' 
    ' 
    ' GNU GENERAL PUBLIC LICENSE (GPL3)
    ' 
    ' 
    ' This program is free software: you can redistribute it and/or modify
    ' it under the terms of the GNU General Public License as published by
    ' the Free Software Foundation, either version 3 of the License, or
    ' (at your option) any later version.
    ' 
    ' This program is distributed in the hope that it will be useful,
    ' but WITHOUT ANY WARRANTY; without even the implied warranty of
    ' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    ' GNU General Public License for more details.
    ' 
    ' You should have received a copy of the GNU General Public License
    ' along with this program. If not, see <http://www.gnu.org/licenses/>.



    ' /********************************************************************************/

    ' Summaries:


    ' Code Statistics:

    '   Total Lines: 87
    '    Code Lines: 65 (74.71%)
    ' Comment Lines: 4 (4.60%)
    '    - Xml Docs: 100.00%
    ' 
    '   Blank Lines: 18 (20.69%)
    '     File Size: 3.46 KB


    '     Module DriverLoad
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Function: (+3 Overloads) CreateGraphicsDevice, DefaultGraphicsDevice, UseGraphicsDevice
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports System.Runtime.CompilerServices

#If NET48 Then
#Else
Imports Bitmap = Microsoft.VisualBasic.Imaging.Bitmap
#End If

Namespace Imaging.Driver

    Public Module DriverLoad

        Public libgdiplus_raster As DeviceInterop
        Public svg As DeviceInterop
        Public pdf As DeviceInterop

        Sub New()
        End Sub

        ''' <summary>
        ''' 用户所指定的图形引擎驱动程序类型，但是这个值会被开发人员设定的驱动程序类型的值所覆盖，
        ''' 通常情况下，默认引擎选用的是``gdi+``引擎
        ''' </summary>
        Public Function DefaultGraphicsDevice(Optional [default] As Drivers? = Nothing) As Drivers
            Static __default As Drivers = Drivers.GDI

            If Not [default] Is Nothing Then
                __default = [default]
            End If

            Return __default
        End Function

        Public Function CreateGraphicsDevice(background As Bitmap,
                                             Optional direct_access As Boolean = True,
                                             Optional driver As Drivers = Drivers.Default) As IGraphics
            If driver = Drivers.Default Then
                driver = DefaultGraphicsDevice()
            End If

            Select Case driver
                Case Drivers.GDI : Return libgdiplus_raster.CreateCanvas2D(background, direct_access)
                Case Drivers.PDF : Return pdf.CreateCanvas2D(background, direct_access)
                Case Drivers.SVG : Return svg.CreateCanvas2D(background, direct_access)
                Case Else
                    Throw New NotImplementedException(driver.Description)
            End Select
        End Function

        Public Function UseGraphicsDevice(driver As Drivers) As DeviceInterop
            If driver = Drivers.Default Then
                driver = DefaultGraphicsDevice()
            End If

            If svg Is Nothing OrElse
                pdf Is Nothing OrElse
                libgdiplus_raster Is Nothing Then

            End If

            Select Case driver
                Case Drivers.SVG : Return svg
                Case Drivers.PDF : Return pdf
                Case Drivers.GDI : Return libgdiplus_raster
                Case Else
                    Throw New NotImplementedException(driver.Description)
            End Select
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function CreateDefaultRasterGraphics(size As Size, fill_color As Color, Optional dpi As Integer = 100) As IGraphics
            Return UseGraphicsDevice(Drivers.GDI).CreateGraphic(size, fill_color, dpi)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function CreateGraphicsDevice(size As Size, fill_color As Color,
                                             Optional dpi As Integer = 100,
                                             Optional driver As Drivers = Drivers.Default) As IGraphics

            Return UseGraphicsDevice(driver).CreateGraphic(size, fill_color, dpi)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function CreateGraphicsDevice(size As Size,
                                             Optional fill As String = NameOf(Color.Transparent),
                                             Optional dpi As Integer = 100,
                                             Optional driver As Drivers = Drivers.Default) As IGraphics

            Return CreateGraphicsDevice(size, fill.TranslateColor, dpi, driver:=driver)
        End Function
    End Module
End Namespace
