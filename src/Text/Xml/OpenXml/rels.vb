﻿#Region "Microsoft.VisualBasic::a6bdca451132b9a048d2ba72ff789965, sciBASIC#\mime\application%vnd.openxmlformats-officedocument.spreadsheetml.sheet\Excel\IO\_rels\rels.vb"

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

    '   Total Lines: 52
    '    Code Lines: 42
    ' Comment Lines: 0
    '   Blank Lines: 10
    '     File Size: 1.73 KB


    '     Class rels
    ' 
    '         Properties: Relationships, Target
    ' 
    '         Function: filePath, toXml
    ' 
    '     Class Relationship
    ' 
    '         Properties: Id, Target, TargetMode, Type
    ' 
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository

Namespace Text.Xml.OpenXml

    <XmlRoot("Relationships", [Namespace]:="http://schemas.openxmlformats.org/package/2006/relationships")>
    Public Class rels

        <XmlElement("Relationship")>
        Public Property Relationships As Relationship()
            Get
                Return relTable.Values.ToArray
            End Get
            Set(value As Relationship())
                relTable = value.ToDictionary
            End Set
        End Property

        Dim relTable As Dictionary(Of Relationship)

        Public Property Target(Id As String) As Relationship
            Get
                Return relTable(Id)
            End Get
            Set(value As Relationship)
                relTable(Id) = value
            End Set
        End Property
    End Class

    Public Class Relationship : Implements INamedValue

        <XmlAttribute> Public Property Id As String Implements IKeyedEntity(Of String).Key
        <XmlAttribute> Public Property Type As String
        <XmlAttribute> Public Property Target As String
        <XmlAttribute> Public Property TargetMode As String

        Public Overrides Function ToString() As String
            Return Target
        End Function
    End Class
End Namespace
