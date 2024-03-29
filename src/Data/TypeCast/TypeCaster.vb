﻿#Region "Microsoft.VisualBasic::39082b16d13e7f3dd13ee54e0cb9c1ce, sciBASIC#\Microsoft.VisualBasic.Core\src\Data\TypeCast\TypeCaster.vb"

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

    '   Total Lines: 64
    '    Code Lines: 48
    ' Comment Lines: 0
    '   Blank Lines: 16
    '     File Size: 2.46 KB


    '     Module Extensions
    ' 
    '         Function: GetBytes, GetString, ParseObject, ToObject
    ' 
    '         Sub: Add
    ' 
    '     Interface ITypeCaster
    ' 
    '         Properties: type
    ' 
    '         Function: GetBytes, GetString, ParseObject, ToObject
    ' 
    '     Class TypeCaster
    ' 
    '         Properties: sizeOf, type
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Namespace ComponentModel.DataSourceModel.TypeCast

    <HideModuleName> Public Module Extensions

        ReadOnly typeCaster As New Dictionary(Of Type, ITypeCaster) From {
            New StringCaster, New IntegerCaster, New DoubleCaster, New DateCaster
        }

        <Extension>
        Private Sub Add(table As Dictionary(Of Type, ITypeCaster), caster As ITypeCaster)
            Call table.Add(caster.type, caster)
        End Sub

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Public Function GetBytes(type As Type) As Func(Of Object, Byte())
            Return AddressOf typeCaster(type).GetBytes
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Public Function GetString(type As Type) As Func(Of Object, String)
            Return AddressOf typeCaster(type).GetString
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Public Function ToObject(type As Type) As Func(Of Byte(), Object)
            Return AddressOf typeCaster(type).ToObject
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Public Function ParseObject(type As Type) As Func(Of String, Object)
            Return AddressOf typeCaster(type).ParseObject
        End Function
    End Module

    Public Interface ITypeCaster

        ReadOnly Property type As Type

        Function GetBytes(value As Object) As Byte()
        Function GetString(value As Object) As String
        Function ToObject(bytes As Byte()) As Object
        Function ParseObject(str As String) As Object
    End Interface

    Public MustInherit Class TypeCaster(Of T) : Implements ITypeCaster

        Public ReadOnly Property sizeOf As Integer = Marshal.SizeOf(type)
        Public ReadOnly Property type As Type = GetType(T) Implements ITypeCaster.type

        Public MustOverride Function GetBytes(value As Object) As Byte() Implements ITypeCaster.GetBytes
        Public MustOverride Function GetString(value As Object) As String Implements ITypeCaster.GetString
        Public MustOverride Function ToObject(bytes As Byte()) As Object Implements ITypeCaster.ToObject
        Public MustOverride Function ParseObject(str As String) As Object Implements ITypeCaster.ParseObject

    End Class

End Namespace
