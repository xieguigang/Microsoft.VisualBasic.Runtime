﻿#Region "Microsoft.VisualBasic::fa256efcdb8b080b4bf8bef21b08fe77, Microsoft.VisualBasic.Core\src\Serialization\BEncoding\BString.vb"

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

    '     Class BString
    ' 
    '         Properties: Value
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Function: CompareTo, Equals, GetHashCode, (+2 Overloads) ToBencodedString, ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Text

Namespace Serialization.Bencoding

    ''' <summary>
    ''' A bencode string.
    ''' </summary>
    Public Class BString
        Implements BElement, IComparable(Of BString)

        ''' <summary>
        ''' The value of the bencoded integer.
        ''' </summary>
        Public Property Value As String

        ''' <summary>
        ''' The main constructor.
        ''' </summary>
        ''' <param name="value"></param>
        Public Sub New(value As String)
            Me.Value = value
        End Sub

        ''' <summary>
        ''' Generates the bencoded equivalent of the string.
        ''' </summary>
        ''' <returns>The bencoded equivalent of the string.</returns>
        Public Function ToBencodedString() As String Implements BElement.ToBencodedString
            Return ToBencodedString(New StringBuilder()).ToString()
        End Function

        ''' <summary>
        ''' Generates the bencoded equivalent of the string.
        ''' </summary>
        ''' <param name="u">The StringBuilder to append to.</param>
        ''' <returns>The bencoded equivalent of the string.</returns>
        Public Function ToBencodedString(u As StringBuilder) As StringBuilder Implements BElement.ToBencodedString
            If u Is Nothing Then
                u = New StringBuilder(Value.Length)
            Else
                u.Append(Value.Length)
            End If

            Return u.Append(":"c).Append(Value)
        End Function

        ''' <see cref="Object.GetHashCode()"/>
        Public Overrides Function GetHashCode() As Integer
            Return Value.GetHashCode()
        End Function

        ''' <summary>
        ''' String.Equals(object)
        ''' </summary>
        Public Overrides Function Equals(obj As Object) As Boolean
            Try
                Return Value.Equals(CType(obj, BString).Value)
            Catch
                Return False
            End Try
        End Function

        ''' <see cref="Object.ToString()"/>
        Public Overrides Function ToString() As String
            Return Value.ToString()
        End Function

        ''' <see cref="IComparable.CompareTo(Object)"/>
        Public Function CompareTo(other As BString) As Integer Implements IComparable(Of BString).CompareTo
            Return Value.CompareTo(other.Value)
        End Function

        ''' <summary>
        ''' Allows you to set a string to a BString.
        ''' </summary>
        ''' <param name="s"></param>
        ''' <returns></returns>
        Public Overloads Shared Widening Operator CType(s As String) As BString
            Return New BString(value:=s)
        End Operator
    End Class
End Namespace
