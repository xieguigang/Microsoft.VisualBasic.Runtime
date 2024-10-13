﻿#Region "Microsoft.VisualBasic::ccbd76adfd3c8ee5f274de67230d60a7, Microsoft.VisualBasic.Core\src\Drawing\GDI+\GdiRasterGraphics.vb"

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

    '   Total Lines: 79
    '    Code Lines: 40 (50.63%)
    ' Comment Lines: 26 (32.91%)
    '    - Xml Docs: 88.46%
    ' 
    '   Blank Lines: 13 (16.46%)
    '     File Size: 2.49 KB


    '     Interface GdiRasterGraphics
    ' 
    '         Properties: ImageResource
    ' 
    '     Class IGraphicsData
    ' 
    '         Properties: content_type
    ' 
    '         Function: Save
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.IO
Imports Microsoft.VisualBasic.Net.Http

#If NET48 Then
Imports System.Drawing
#Else
Imports Microsoft.VisualBasic.Imaging
#End If

Namespace Imaging.Driver

    ''' <summary>
    ''' 
    ''' </summary>
    Public Interface GdiRasterGraphics

        ReadOnly Property ImageResource As Image

    End Interface

    Public MustInherit Class IGraphicsData

        ''' <summary>
        ''' The graphics engine driver type indicator, 
        ''' 
        ''' + for <see cref="Drivers.GDI"/> -> <see cref="ImageData"/>(<see cref="Drawing.Image"/>, <see cref="Bitmap"/>)
        ''' + for <see cref="Drivers.SVG"/> -> <see cref="SVGData"/>(<see cref="SVGDocument"/>)
        ''' 
        ''' (驱动程序的类型)
        ''' </summary>
        ''' <returns></returns>
        Public MustOverride ReadOnly Property Driver As Drivers

        ''' <summary>
        ''' http content type
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property content_type As String
            Get
                Select Case Driver
                    Case Drivers.GDI
                        Return "image/png"
                    Case Drivers.PS
                        Return "application/postscript"
                    Case Drivers.SVG
                        Return "image/svg+xml"
                    Case Drivers.WMF
                        Return "application/x-wmf"
                    Case Else
                        Return "application/octet-stream"
                End Select
            End Get
        End Property

        Public MustOverride ReadOnly Property Width As Integer
        Public MustOverride ReadOnly Property Height As Integer

        Public MustOverride Function GetDataURI() As DataURI

        ''' <summary>
        ''' Save the image graphics to file
        ''' </summary>
        ''' <param name="path$"></param>
        ''' <returns></returns>
        Public Overridable Function Save(path$) As Boolean
            Using s As Stream = path.Open(FileMode.OpenOrCreate, doClear:=True)
                Return Save(s)
            End Using
        End Function

        ''' <summary>
        ''' Save the image graphics to a specific output stream
        ''' </summary>
        ''' <param name="out"></param>
        ''' <returns></returns>
        Public MustOverride Function Save(out As Stream) As Boolean

    End Class
End Namespace
