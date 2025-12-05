Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository

Module qgramTestSearch

    Sub Run()
        Dim q As New QGramIndex(6)

        Call q.AddString("ATP")
        Call q.AddString("test acid")
        Call q.AddString("ATP")
        Call q.AddString("ATP+")
        Call q.AddString("Hello world")
        Call q.AddString("test")

        Dim find1 = q.FindSimilar("ATP").ToArray


        Pause()
    End Sub
End Module
