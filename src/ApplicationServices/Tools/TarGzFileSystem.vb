Imports System.Formats.Tar
Imports System.IO
Imports System.IO.Compression
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Language

Namespace ApplicationServices

#If NET8_0_OR_GREATER Then

    Public Class TarGzFileSystem : Implements IFileSystemEnvironment

        ReadOnly gz As GZipStream
        ReadOnly tar As TarReader
        ReadOnly tree As FileSystemTree

        Public ReadOnly Property [readonly] As Boolean Implements IFileSystemEnvironment.readonly
            Get
                Return True
            End Get
        End Property

        Sub New(targz As String)
            Dim file As Stream = targz.Open(FileMode.Open, doClear:=False, [readOnly]:=True)

            gz = New GZipStream(file, CompressionMode.Decompress)
            tar = New TarReader(gz)

            ' load files
            Dim entry As New Value(Of TarEntry)
            Dim filenames As New List(Of String)

            Do While (entry = tar.GetNextEntry) IsNot Nothing
                If CheckVirtualEntry(entry) Then
                    Continue Do
                End If

                Call filenames.Add("/" & CType(entry, TarEntry).Name)
            Loop

            tree = FileSystemTree.BuildTree(filenames)
        End Sub

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Private Shared Function CheckVirtualEntry(entry As TarEntry) As Boolean
            Return entry.EntryType.HasFlag(TarEntryType.SymbolicLink) OrElse
                entry.EntryType.HasFlag(TarEntryType.HardLink) OrElse
                entry.EntryType.HasFlag(TarEntryType.GlobalExtendedAttributes)
        End Function

        Public Sub Close() Implements IFileSystemEnvironment.Close
            Call tar.Dispose()
            Call gz.Dispose()
        End Sub

        Public Sub Flush() Implements IFileSystemEnvironment.Flush
        End Sub

        Public Function OpenFile(path As String,
                                 Optional mode As FileMode = FileMode.OpenOrCreate,
                                 Optional access As FileAccess = FileAccess.Read) As Stream Implements IFileSystemEnvironment.OpenFile

            Dim entry As TarEntry = tar.
        End Function

        Public Function DeleteFile(path As String) As Boolean Implements IFileSystemEnvironment.DeleteFile
            Throw New NotSupportedException("Readonly stream!")
        End Function

        Public Function FileExists(path As String, Optional ZERO_Nonexists As Boolean = False) As Boolean Implements IFileSystemEnvironment.FileExists
            Throw New NotImplementedException()
        End Function

        Public Function FileSize(path As String) As Long Implements IFileSystemEnvironment.FileSize
            Throw New NotImplementedException()
        End Function

        Public Function FileModifyTime(path As String) As Date Implements IFileSystemEnvironment.FileModifyTime
            Throw New NotImplementedException()
        End Function

        Public Function GetFullPath(filename As String) As String Implements IFileSystemEnvironment.GetFullPath
            Throw New NotImplementedException()
        End Function

        Public Function WriteText(text As String, path As String) As Boolean Implements IFileSystemEnvironment.WriteText
            Throw New NotSupportedException("Readonly stream!")
        End Function

        Public Function ReadAllText(path As String) As String Implements IFileSystemEnvironment.ReadAllText
            Throw New NotImplementedException()
        End Function

        Public Function GetFiles() As IEnumerable(Of String) Implements IFileSystemEnvironment.GetFiles
            Throw New NotImplementedException()
        End Function
    End Class
#End If

End Namespace