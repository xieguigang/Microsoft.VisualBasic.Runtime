﻿#Region "Microsoft.VisualBasic::b3a106c39670fb151d62b3b32d984b29, My\LinuxRunHelper.vb"

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

    '     Module LinuxRunHelper
    ' 
    '         Function: BashRun, BashShell, MonoRun
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.CommandLine
Imports Microsoft.VisualBasic.Text

Namespace My

    ''' <summary>
    ''' mono shortcuts
    ''' </summary>
    Public Module LinuxRunHelper

        ''' <summary>
        ''' Run from bash shell
        ''' </summary>
        ''' <returns></returns>
        Public Function BashRun() As String
            Dim appName = App.AssemblyName
            Dim bash As String = Encodings.UTF8WithoutBOM _
                .CodePage _
                .GetString(My.Resources.bashRunner) _
                .Replace("{appName}", appName) _
                .LineTokens _
                .JoinBy(ASCII.LF)

            Return bash
        End Function

        ''' <summary>
        ''' 这里比perl脚本掉调用有一个缺点，在运行前还需要使用命令修改为可执行权限
        ''' 
        ''' ```
        ''' 'sudo chmod 777 cmd.sh'
        ''' ```
        ''' </summary>
        ''' <returns></returns>
        Public Function BashShell() As Integer
            Dim path As String = App.ExecutablePath.TrimSuffix
            Dim bash As String = BashRun()

            ' 在这里写入的bash脚本都是没有文件拓展名的
            '
            ' 同时写入man命令帮助脚本
            Call My.Resources.help.FlushStream(path.ParentPath & "/help")
            Call BashRun.SaveTo(path, Encodings.UTF8WithoutBOM.CodePage)

            Return 0
        End Function

        Public Function MonoRun(app As String, CLI As String) As ProcessEx
            Dim proc As New ProcessEx With {
                .Bin = "mono",
                .CLIArguments = app.GetFullPath.CLIPath & " " & CLI
            }
            Return proc
        End Function
    End Module
End Namespace
