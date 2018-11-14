﻿#Region "Microsoft.VisualBasic::344118a21e8f68da07729150ae19620d, Microsoft.VisualBasic.Core\Extensions\WebServices\Http\DataURI.vb"

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

    '     Class DataURI
    ' 
    '         Properties: base64, chartSet, mime
    ' 
    '         Constructor: (+3 Overloads) Sub New
    '         Function: FromFile, SVGImage, ToStream, ToString, URIParser
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.Net.Protocols
Imports Microsoft.VisualBasic.Text

Namespace Net.Http

    ''' <summary>
    ''' Data URI scheme
    ''' </summary>
    Public Class DataURI

        ''' <summary>
        ''' File mime type
        ''' </summary>
        Public ReadOnly Property mime As String
        ''' <summary>
        ''' The base64 string
        ''' </summary>
        Public ReadOnly Property base64 As String
        Public ReadOnly Property chartSet As String

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="file"></param>
        ''' <param name="codepage$">
        ''' The chartset codepage name, by default is ``ASCII``.
        ''' </param>
        Sub New(file$, Optional codepage$ = Nothing)
            mime = Strings.LCase(file.FileMimeType.MIMEType)
            base64 = file.ReadBinary.ToBase64String
            codepage = codepage
        End Sub

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Sub New(image As Image)
            Call Me.New(image.ToBase64String, ContentTypes.MIME.Png, Nothing)
        End Sub

        Public Sub New(base64$, mine$, Optional charset$ = Nothing)
            Me.base64 = base64
            Me.mime = mime
            Me.chartSet = charset
        End Sub

        ''' <summary>
        ''' <see cref="Convert.FromBase64String"/>
        ''' </summary>
        ''' <returns></returns>
        Public Function ToStream() As Stream
            Return New MemoryStream(Convert.FromBase64String(base64))
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Function FromFile(file As String) As DataURI
            Return New DataURI(file)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Function SVGImage(svg As String) As DataURI
            Return New DataURI(base64:=TextEncodings.UTF8WithoutBOM.GetBytes(svg).ToBase64String, mine:="image/svg+xml")
        End Function

        Public Shared Function URIParser(uri As String) As DataURI
            Dim t = uri.Split(";"c) _
                .Select(Function(p) p.StringSplit("[:=,]")) _
                .ToDictionary(Function(k) k(0).ToLower,
                              Function(value) value(1))

            Return New DataURI(
                base64:=t.TryGetValue("base64"),
                charset:=t.TryGetValue("charset"),
                mine:=t.TryGetValue("data")
            )
        End Function

        ''' <summary>
        ''' Output this data uri string text
        ''' </summary>
        ''' <returns></returns>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Overrides Function ToString() As String
            If chartSet.StringEmpty Then
                Return $"data:{mime};base64,{base64}"
            Else
                Return $"data:{mime};charset={chartSet};base64,{base64}"
            End If
        End Function
    End Class
End Namespace
