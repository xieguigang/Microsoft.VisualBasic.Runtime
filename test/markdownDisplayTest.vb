﻿#Region "Microsoft.VisualBasic::28f67a3757cdc1b3252dfc5d6901a650, sciBASIC#\Microsoft.VisualBasic.Core\test\markdownDisplayTest.vb"

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

    '   Total Lines: 24
    '    Code Lines: 16
    ' Comment Lines: 0
    '   Blank Lines: 8
    '     File Size: 413 B


    ' Module markdownDisplayTest
    ' 
    '     Sub: Main1
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ApplicationServices.Terminal

Module markdownDisplayTest
    Sub Main1()
        Call MarkdownRender.Print("# title

This is a inline ``code`` span. **bold** font style test.

> quote
> test
> block
>
> A ``code span`` in this block quot

A new ``paragraph``.

A url test: http://test.url/a/b/c/xxxx.txt


", indent:=10)

        Pause()
    End Sub
End Module
