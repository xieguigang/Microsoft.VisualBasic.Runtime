﻿#Region "Microsoft.VisualBasic::28afd7cd739855385762d45bb8d0dd98, Microsoft.VisualBasic.Core\src\ApplicationServices\Tools\Zip\ZipStream.vb"

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

    '   Total Lines: 303
    '    Code Lines: 194 (64.03%)
    ' Comment Lines: 57 (18.81%)
    '    - Xml Docs: 63.16%
    ' 
    '   Blank Lines: 52 (17.16%)
    '     File Size: 11.79 KB


    '     Class ZipStream
    ' 
    '         Properties: [readonly], filepath, zip
    ' 
    '         Constructor: (+2 Overloads) Sub New
    ' 
    '         Function: DeleteFile, EnumerateFiles, FileExists, FileModifyTime, FileSize
    '                   GetFileEntry, (+2 Overloads) GetFiles, GetFullPath, OpenFile, ReadAllText
    '                   ReadLines, ToString, WriteText
    ' 
    '         Sub: Close, (+2 Overloads) Dispose, Flush, WriteLines
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.IO
Imports System.IO.Compression
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Language.UnixBash

Namespace ApplicationServices.Zip

    ''' <summary>
    ''' using a zip archive file as a virtual filesystem
    ''' </summary>
    Public Class ZipStream : Implements IFileSystemEnvironment, IDisposable

        Dim disposedValue As Boolean
        Dim virtual_fs As FileSystemTree
        Dim s As Stream

        Public ReadOnly Property [readonly] As Boolean Implements IFileSystemEnvironment.readonly
        Public ReadOnly Property zip As ZipArchive

        ''' <summary>
        ''' Gets the absolute path of the file opened in the FileStream.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property filepath As String
            Get
                If TypeOf s Is FileStream Then
                    Return DirectCast(s, FileStream).Name
                Else
                    ' maybe in-memory stream, no file name
                    Return Nothing
                End If
            End Get
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="file"></param>
        ''' <param name="is_readonly">
        ''' readonly mode just allows for read data from the zip archive file, and 
        ''' set this parameter value to FALSE will enable for create entry inside 
        ''' the file.
        ''' </param>
        Sub New(file As Stream, Optional is_readonly As Boolean = False)
            s = file
            [readonly] = is_readonly

            If is_readonly Then
                zip = New ZipArchive(file, ZipArchiveMode.Read)
            Else
                zip = New ZipArchive(file, ZipArchiveMode.Update)
            End If

            virtual_fs = FileSystemTree.BuildTree(GetFiles)
        End Sub

        Sub New(filepath As String, Optional is_readonly As Boolean = False)
            Call Me.New(filepath.Open(FileMode.OpenOrCreate, doClear:=False, [readOnly]:=is_readonly), is_readonly)
        End Sub

        Public Sub Close() Implements IFileSystemEnvironment.Close
            Call zip.Dispose()
        End Sub

        Public Sub Flush() Implements IFileSystemEnvironment.Flush
            ' do nothing
        End Sub

        ''' <summary>
        ''' debug view of the stream source
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Function ToString() As String
            If TypeOf s Is FileStream Then
                Return DirectCast(s, FileStream).Name
            Else
                ' maybe in-memory stream, no file name
                Return $"buffer_stream://&H_{GetHashCode.ToHexString}"
            End If
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="path"></param>
        ''' <returns>
        ''' return nothing if the required archive entry could not be found
        ''' </returns>
        Public Function GetFileEntry(path As String, allow_new As Boolean) As ZipArchiveEntry
            Dim ref As FileSystemTree = FileSystemTree.GetFile(virtual_fs, path)

            If Not ref Is Nothing Then
                Return zip.GetEntry(CStr(ref.data))
            End If

            If [readonly] Then
                Return Nothing
            ElseIf Not allow_new Then
                ' entry is missing, andalso not allow create 
                ' then return nothing if the required archive entry could not be found
                Return Nothing
            Else
                ' create new?
                Return zip.CreateEntry(path)
            End If
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="path"></param>
        ''' <param name="mode"></param>
        ''' <param name="access"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' due to the reason of zip deflate stream object does not supports the seek operation,
        ''' so this function convert the zip deflate stream to a memory stream at first in this
        ''' function for avoid the downstream operation error when call the stream seek method.
        ''' </remarks>
        Public Function OpenFile(path As String, Optional mode As FileMode = FileMode.OpenOrCreate, Optional access As FileAccess = FileAccess.Read) As Stream Implements IFileSystemEnvironment.OpenFile
            Dim file As ZipArchiveEntry = GetFileEntry(path, allow_new:=mode = FileMode.OpenOrCreate OrElse mode = FileMode.CreateNew)

            If file Is Nothing Then
                Return Nothing
            Else
                Dim ms As New MemoryStream
                Dim zip_buf = file.Open

                ' 20240410 convert zip stream to memory stream 
                ' for avoid the seek error
                Call zip_buf.CopyTo(ms)
                Call ms.Seek(Scan0, SeekOrigin.Begin)

                Return ms
            End If
        End Function

        Public Function DeleteFile(path As String) As Boolean Implements IFileSystemEnvironment.DeleteFile
            Dim node = FileSystemTree.DeleteFile(virtual_fs, path)

            If Not node Is Nothing Then
                Call zip.GetEntry(CStr(node.data)).Delete()
            End If

            Return True
        End Function

        Public Function FileExists(path As String, Optional ZERO_Nonexists As Boolean = False) As Boolean Implements IFileSystemEnvironment.FileExists
            Dim file As ZipArchiveEntry = GetFileEntry(path, allow_new:=False)

            If file Is Nothing Then
                Return False
            ElseIf file.Length = 0 Then
                Return ZERO_Nonexists
            Else
                Return True
            End If
        End Function

        Public Function FileSize(path As String) As Long Implements IFileSystemEnvironment.FileSize
            Dim file As ZipArchiveEntry = GetFileEntry(path, allow_new:=False)

            If file Is Nothing Then
                Return -1
            Else
                Return file.Length
            End If
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function GetFullPath(filename As String) As String Implements IFileSystemEnvironment.GetFullPath
            Return ("/" & filename.Replace("\"c, "/"c)).StringReplace("/{2,}", "/")
        End Function

        Public Function WriteText(text As String, path As String) As Boolean Implements IFileSystemEnvironment.WriteText
            Dim file As ZipArchiveEntry = GetFileEntry(path, allow_new:=True)

            If file Is Nothing Then
                Throw New NotImplementedException
            End If

            Using s As New StreamWriter(file.Open)
                Call s.WriteLine(text)
                Call s.Flush()
            End Using

            Return True
        End Function

        Public Sub WriteLines(str As IEnumerable(Of String), path As String)
            Dim file As ZipArchiveEntry = GetFileEntry(path, allow_new:=True)

            If file Is Nothing Then
                Throw New NotImplementedException
            End If

            Using s As New StreamWriter(file.Open)
                For Each line As String In str
                    Call s.WriteLine(line)
                Next

                Call s.Flush()
            End Using
        End Sub

        Public Iterator Function ReadLines(path As String) As IEnumerable(Of String)
            Dim file As ZipArchiveEntry = GetFileEntry(path, allow_new:=True)

            If file Is Nothing Then
                Return
            End If

            Using s As New StreamReader(file.Open)
                Dim line As Value(Of String) = ""

                Do While Not (line = s.ReadLine) Is Nothing
                    Yield CStr(line)
                Loop
            End Using
        End Function

        Public Function ReadAllText(path As String) As String Implements IFileSystemEnvironment.ReadAllText
            Dim file As ZipArchiveEntry = GetFileEntry(path, allow_new:=False)

            If file Is Nothing Then
                Return Nothing
            Else
                Using s As New StreamReader(file.Open)
                    Return s.ReadToEnd
                End Using
            End If
        End Function

        Public Function GetFiles() As IEnumerable(Of String) Implements IFileSystemEnvironment.GetFiles
            Return zip.Entries.Select(Function(f) f.FullName)
        End Function

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: 释放托管状态(托管对象)
                    Call Flush()
                    Call Close()
                End If

                ' TODO: 释放未托管的资源(未托管的对象)并重写终结器
                ' TODO: 将大型字段设置为 null
                disposedValue = True
            End If
        End Sub

        ' ' TODO: 仅当“Dispose(disposing As Boolean)”拥有用于释放未托管资源的代码时才替代终结器
        ' Protected Overrides Sub Finalize()
        '     ' 不要更改此代码。请将清理代码放入“Dispose(disposing As Boolean)”方法中
        '     Dispose(disposing:=False)
        '     MyBase.Finalize()
        ' End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            ' 不要更改此代码。请将清理代码放入“Dispose(disposing As Boolean)”方法中
            Dispose(disposing:=True)
            GC.SuppressFinalize(Me)
        End Sub

        Public Function FileModifyTime(path As String) As Date Implements IFileSystemEnvironment.FileModifyTime
            Return Nothing
        End Function

        Public Function GetFiles(subdir As String, ParamArray exts() As String) As IEnumerable(Of String) Implements IFileSystemEnvironment.GetFiles
            With ls - ShellSyntax.wildcards(exts)
                Dim filter As Func(Of String, Boolean) = .MakeFilter
                Dim fullpath As String = GetFullPath(subdir)
                Dim subset As IEnumerable(Of String) = GetFiles() _
                    .Select(Function(file) GetFullPath(file)) _
                    .Where(Function(file) file.StartsWith(fullpath)) _
                    .Where(filter)

                Return subset
            End With
        End Function

        Public Function EnumerateFiles(subdir As String, ParamArray exts() As String) As IEnumerable(Of String) Implements IFileSystemEnvironment.EnumerateFiles
            Dim fullpath As String = GetFullPath(subdir)
            Dim nsize As Integer = fullpath.Split("/"c).Length
            Dim folderFiles = GetFiles _
                .Select(Function(file) GetFullPath(file)) _
                .Where(Function(file) file.StartsWith(fullpath)) _
                .Where(Function(file)
                           Dim nsize2 As Integer = file.Split("/"c).Length
                           Dim test As Boolean = nsize2 - 1 = nsize
                           Return test
                       End Function)

            With ls - ShellSyntax.wildcards(exts)
                Dim filter As Func(Of String, Boolean) = .MakeFilter
                Dim subset As IEnumerable(Of String) = folderFiles.Where(filter)

                Return subset
            End With
        End Function
    End Class
End Namespace
