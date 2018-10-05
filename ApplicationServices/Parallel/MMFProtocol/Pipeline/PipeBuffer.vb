﻿#Region "Microsoft.VisualBasic::e0bef81df8c24e1c13abddaa2e5327ac, Microsoft.VisualBasic.Core\ApplicationServices\Parallel\MMFProtocol\Pipeline\PipeBuffer.vb"

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

    '     Class PipeBuffer
    ' 
    '         Properties: byteData, Name
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Function: Serialize
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Net.Protocols
Imports Microsoft.VisualBasic.Text

Namespace Parallel.MMFProtocol.Pipeline

    Public Class PipeBuffer : Inherits RawStream

        Public Property Name As String
        Public Property byteData As Byte()

        Sub New(raw As Byte())
            Dim nameLen As Byte() = New Byte(INT32 - 1) {}
            Dim p As int = Scan0
            Call Array.ConstrainedCopy(raw, p + INT32, nameLen, Scan0, INT32)

            Dim len As Integer = BitConverter.ToInt32(nameLen, Scan0)
            Dim name As Byte() = New Byte(len - 1) {}
            Call Array.ConstrainedCopy(raw, p + name.Length, name, Scan0, len)
            Me.Name = System.Text.Encoding.UTF8.GetString(name)

            byteData = New Byte(raw.Length - INT32 - len - 1) {}
            Call Array.ConstrainedCopy(raw, p, byteData, Scan0, byteData.Length)
        End Sub

        Public Overrides Function Serialize() As Byte()
            Dim nameBuf As Byte() = UTF8WithoutBOM.GetBytes(Name)
            Dim buffer As Byte() = New Byte(INT32 + nameBuf.Length + byteData.Length - 1) {}
            Dim nameLen As Byte() = BitConverter.GetBytes(nameBuf.Length)
            Dim p As int = Scan0

            Call Array.ConstrainedCopy(nameLen, Scan0, buffer, p + nameLen.Length, nameLen.Length)
            Call Array.ConstrainedCopy(nameBuf, Scan0, buffer, p + nameBuf.Length, nameBuf.Length)
            Call Array.ConstrainedCopy(byteData, Scan0, buffer, p + byteData.Length, byteData.Length)

            Return buffer
        End Function
    End Class
End Namespace
