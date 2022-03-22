﻿#Region "Microsoft.VisualBasic::594b717f3ebc338843d4093cb8d89034, sciBASIC#\Microsoft.VisualBasic.Core\src\My\Framework\MemoryLoads.vb"

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

    '   Total Lines: 14
    '    Code Lines: 7
    ' Comment Lines: 6
    '   Blank Lines: 1
    '     File Size: 272.00 B


    '     Enum MemoryLoads
    ' 
    '         Heavy, Light, Max
    ' 
    '  
    ' 
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace My.FrameworkInternal

    Public Enum MemoryLoads As Byte
        ''' <summary>
        ''' lazy load
        ''' </summary>
        Light
        ''' <summary>
        ''' less than 2GB
        ''' </summary>
        Heavy
        Max
    End Enum
End Namespace
