﻿#Region "Microsoft.VisualBasic::57256a9215613a36d1a03cec97082c50, Microsoft.VisualBasic.Core\src\ComponentModel\File\XmlAssembly\DigitalSignature.vb"

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

    '     Class DigitalSignature
    ' 
    '         Function: GetModelInfo
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Scripting.MetaData

Namespace ComponentModel

    Public Class DigitalSignature

        ''' <summary>
        ''' parse xml assembly type reference which is generated by <see cref="XmlDataModel"/>
        ''' </summary>
        ''' <param name="file"></param>
        ''' <returns></returns>
        Public Shared Function GetModelInfo(file As String) As TypeInfo
            Dim modelType As String = Nothing
            Dim assembly As String = Nothing
            Dim doParse As Boolean = False
            Dim tag As NamedValue(Of String)

            For Each line As String In file.IterateAllLines
                line = Strings.Trim(line)

                If line = "<!--" Then
                    doParse = True
                ElseIf doParse Then
                    tag = line.GetTagValue(":", trim:=True)

                    If tag.Name = "model" Then
                        modelType = tag.Value
                    ElseIf tag.Name = "assembly" Then
                        assembly = tag.Value
                    End If

                    If (Not modelType.StringEmpty) AndAlso (Not assembly.StringEmpty) Then
                        Return New TypeInfo With {
                            .assembly = assembly.StringSplit(",\s+")(Scan0) & ".dll",
                            .fullName = modelType,
                            .reference = assembly
                        }
                    End If
                ElseIf line = "-->" Then
                    Exit For
                End If
            Next

            Return Nothing
        End Function
    End Class
End Namespace
