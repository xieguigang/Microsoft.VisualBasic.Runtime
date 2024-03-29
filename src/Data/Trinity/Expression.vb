﻿#Region "Microsoft.VisualBasic::f099861e8528ac2b4d1e7c1e1778e320, sciBASIC#\Microsoft.VisualBasic.Core\src\Data\Trinity\Expression.vb"

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

    '   Total Lines: 53
    '    Code Lines: 34
    ' Comment Lines: 12
    '   Blank Lines: 7
    '     File Size: 2.05 KB


    '     Module Expression
    ' 
    '         Function: Concatenate
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq

Namespace Data.Trinity

    ''' <summary>
    ''' Natural expression builder for AI output
    ''' </summary>
    Public Module Expression

        ''' <summary>
        ''' If <paramref name="list"/> contains no elements or it is nothing, then this function will returns nothing
        ''' </summary>
        ''' <param name="list"></param>
        ''' <param name="comma$"></param>
        ''' <param name="andalso$"></param>
        ''' <param name="etc$"></param>
        ''' <param name="joinSpace"></param>
        ''' <returns></returns>
        <Extension>
        Public Function Concatenate(list As IEnumerable(Of String),
                                    Optional comma$ = ",",
                                    Optional andalso$ = "and",
                                    Optional etc$ = "etc",
                                    Optional joinSpace As Boolean = True,
                                    Optional enUS As Boolean = False) As String

            Dim space As String = "" Or " ".When(joinSpace)
            Dim dataArray As String() = list.SafeQuery.Where(Function(s) Not s.StringEmpty).ToArray

            If dataArray.Length = 0 Then
                Return Nothing
            End If

            With dataArray
                If .Length = 1 Then
                    Return .ByRef(0)
                ElseIf .Length < 8 Then
                    Return .Take(.Length - 1).JoinBy(comma & space) & $"{space}{[andalso]}{space}" & .Last
                Else
                    Dim str = .Take(7).JoinBy(comma & space) & $"{space}{[andalso]}{space}" & .ByRef(7)

                    If enUS Then
                        Return str & $"{comma}{space}{etc}"
                    Else
                        Return str & space & etc
                    End If
                End If
            End With
        End Function
    End Module
End Namespace
