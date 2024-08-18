﻿#Region "Microsoft.VisualBasic::51226cc199be686a619232eef1c0baa4, Microsoft.VisualBasic.Core\src\ComponentModel\ValuePair\TagData\TimeSignal.vb"

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

    '   Total Lines: 9
    '    Code Lines: 6 (66.67%)
    ' Comment Lines: 0 (0.00%)
    '    - Xml Docs: 0.00%
    ' 
    '   Blank Lines: 3 (33.33%)
    '     File Size: 195 B


    '     Interface ITimeSignal
    ' 
    '         Properties: intensity, time
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace ComponentModel.TagData

    Public Interface ITimeSignal

        ReadOnly Property time As Double
        ReadOnly Property intensity As Double

    End Interface
End Namespace
