﻿#Region "Microsoft.VisualBasic::3862a2a41cbd62c044aef25cc0745ae6, Microsoft.VisualBasic.Core\src\ApplicationServices\Terminal\Utility\ProgressBar\ShellProgressBar\StringExtensions.vb"

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

    '   Total Lines: 8
    '    Code Lines: 8 (100.00%)
    ' Comment Lines: 0 (0.00%)
    '    - Xml Docs: 0.00%
    ' 
    '   Blank Lines: 0 (0.00%)
    '     File Size: 404 B


    '     Module StringExtensions
    ' 
    '         Function: Excerpt
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace ApplicationServices.Terminal.ProgressBar.ShellProgressBar
    Friend Module StringExtensions
        Public Function Excerpt(phrase As String, Optional length As Integer = 60) As String
            If String.IsNullOrEmpty(phrase) OrElse phrase.Length < length Then Return phrase
            Return phrase.Substring(0, length - 3) & "..."
        End Function
    End Module
End Namespace
