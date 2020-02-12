﻿#Region "Microsoft.VisualBasic::ed5e4aad317a781d9036cb0f5efbd183, Microsoft.VisualBasic.Core\ApplicationServices\Debugger\Exception\StackFrame.vb"

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

    '     Class StackFrame
    ' 
    '         Properties: File, Line, Method
    ' 
    '         Function: Parser, parserImpl, ToString
    ' 
    '     Class Method
    ' 
    '         Properties: [Module], [Namespace], Method
    ' 
    '         Constructor: (+2 Overloads) Sub New
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Linq

Namespace ApplicationServices.Debugging.Diagnostics

    Public Class StackFrame

        ''' <summary>
        ''' Method call
        ''' </summary>
        ''' <returns></returns>
        Public Property Method As Method
        ''' <summary>
        ''' The file path of the source file
        ''' </summary>
        ''' <returns></returns>
        Public Property File As String
        ''' <summary>
        ''' The line number in current source <see cref="File"/>.
        ''' </summary>
        ''' <returns></returns>
        Public Property Line As String

        Sub New()
        End Sub

        ''' <summary>
        ''' make value copy
        ''' </summary>
        ''' <param name="clone"></param>
        Sub New(clone As StackFrame)
            File = clone.File
            Line = clone.Line
            Method = New Method With {
                .Method = clone.Method.Method,
                .[Module] = clone.Method.Module,
                .[Namespace] = clone.Method.Namespace
            }
        End Sub

        Public Overrides Function ToString() As String
            Return $"{Method} at {File}:line {Line}"
        End Function

        Public Shared Function Parser(line As String) As StackFrame
            With line.Replace("位置", "in").Replace("行号", "line")
                Return .StringSplit(" in ").DoCall(AddressOf parserImpl)
            End With
        End Function

        Private Shared Function parserImpl(t As String()) As StackFrame
            Dim method As String = t(0)
            Dim location As String = t.ElementAtOrDefault(1)
            Dim file$, lineNumber$

            If Not location.StringEmpty Then
                t = location.StringSplit("[:]line ")

                If t.Length = 1 Then
                    ' on mono environment
                    file = "Unknown"
                    lineNumber = 0
                Else
                    file = t(0)
                    lineNumber = t(1)
                End If
            Else
                file = "Unknown"
                lineNumber = 0
            End If

            Return New StackFrame With {
                .Method = New Method(method),
                .File = file,
                .Line = lineNumber
            }
        End Function
    End Class

    Public Class Method

        Public Property [Namespace] As String
        Public Property [Module] As String
        Public Property Method As String

        Sub New()
        End Sub

        Sub New(s As String)
            Dim t = s.Split("."c).AsList

            Method = t(-1)
            [Module] = t(-2)
            [Namespace] = t.Take(t.Count - 2).JoinBy(".")
        End Sub

        Public Overrides Function ToString() As String
            Return $"{[Namespace]}.{[Module]}.{Method}"
        End Function
    End Class
End Namespace
