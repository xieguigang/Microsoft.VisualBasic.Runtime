#Region "Microsoft.VisualBasic::16030fa647567f089107450095d90f20, Microsoft.VisualBasic.Core\My\Log4VB.vb"

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

'     Module Log4VB
' 
' 
' 
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ApplicationServices.Debugging.Logging
Imports Microsoft.VisualBasic.Terminal

Namespace My

    ''' <summary>
    ''' VB.NET <see cref="Console"/> log framework.
    ''' </summary>
    Public Module Log4VB

        Friend ReadOnly logs As New List(Of LoggingDriver)

        ''' <summary>
        ''' ``<see cref="MSG_TYPES"/> -> <see cref="ConsoleColor"/>``
        ''' </summary>
        ReadOnly DebuggerTagColors As New Dictionary(Of Integer, ConsoleColor) From {
            {MSG_TYPES.DEBUG, ConsoleColor.DarkGreen},
            {MSG_TYPES.ERR, ConsoleColor.Red},
            {MSG_TYPES.INF, ConsoleColor.Blue},
            {MSG_TYPES.WRN, ConsoleColor.Yellow}
        }

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Private Function getColor(level As Integer) As ConsoleColor
            Return If(DebuggerTagColors.ContainsKey(level), DebuggerTagColors(level), CType(level, ConsoleColor))
        End Function

        ''' <summary>
        ''' 头部和消息字符串都是放在一个task之中进行输出的，<see cref="xConsole"/>的输出也是和内部的debugger输出使用的同一个消息线程
        ''' </summary>
        ''' <param name="header"></param>
        ''' <param name="msg"></param>
        ''' <param name="msgColor"></param>
        ''' <param name="level"><see cref="ConsoleColor"/> or <see cref="MSG_TYPES"/></param>
        Public Sub Print(header$, msg$, msgColor As ConsoleColor, level As Integer)
            If ForceSTDError Then
                Call Console.Error.WriteLine($"[{header}]{msg}")
            Else
                Dim cl As ConsoleColor = Console.ForegroundColor
                Dim headColor As ConsoleColor = getColor(level)

                If msgColor = headColor Then
                    Console.ForegroundColor = headColor
                    Console.WriteLine($"[{header}]{msg}")
                    Console.ForegroundColor = cl
                Else
                    Call Console.Write("[")
                    Console.ForegroundColor = headColor
                    Call Console.Write(header)
                    Console.ForegroundColor = cl
                    Call Console.Write("]")

                    Call WriteLine(msg, msgColor)
                End If
            End If

            For Each driver As LoggingDriver In VBDebugger.logs
                Call driver(header, msg, level)
            Next
        End Sub
    End Module
End Namespace
