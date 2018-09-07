﻿#Region "Microsoft.VisualBasic::b04ab37bb47f073290401280e5a35560, Microsoft.VisualBasic.Core\ApplicationServices\Parallel\Threads\ParallelExtension.vb"

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

    '     Module ParallelExtension
    ' 
    '         Function: AsyncTask, RunTask
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports System.Threading

Namespace Parallel

    ''' <summary>
    ''' Parallel based on the threading
    ''' </summary>
    Public Module ParallelExtension

        ''' <summary>
        ''' Start a new thread and then returns the background thread task handle.
        ''' </summary>
        ''' <param name="start"></param>
        ''' <returns></returns>
        <Extension> Public Function RunTask(start As ThreadStart) As Thread
            Dim thread As New Thread(start)
            Call thread.Start()
            Return thread
        End Function

        ''' <summary>
        ''' 运行一个后台任务
        ''' </summary>
        ''' <param name="start"></param>
        ''' <returns></returns>
        Public Function AsyncTask(start As ThreadStart) As IAsyncResult
            Return start.BeginInvoke(Nothing, Nothing)
        End Function
    End Module
End Namespace
