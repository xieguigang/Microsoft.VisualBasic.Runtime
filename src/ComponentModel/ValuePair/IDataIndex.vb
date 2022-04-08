﻿#Region "Microsoft.VisualBasic::df22c1f73b5ac0d88ccf7969720d9252, sciBASIC#\Microsoft.VisualBasic.Core\src\ComponentModel\ValuePair\IDataIndex.vb"

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
    '    Code Lines: 6
    ' Comment Lines: 0
    '   Blank Lines: 3
    '     File Size: 232.00 B


    '     Interface IDataIndex
    ' 
    '         Function: GetByIndex
    ' 
    '         Sub: SetByIndex
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace ComponentModel

    Public Interface IDataIndex

        Sub SetByIndex(index As String, value As Object)
        Function GetByIndex(index As String, [default] As Object) As Object

    End Interface
End Namespace
