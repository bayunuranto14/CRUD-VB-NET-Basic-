Imports MySql.Data.MySqlClient

Module connection

    Public conn As MySqlConnection
    Public cmd As MySqlCommand
    Public rd As MySqlDataReader
    Public da As MySqlDataAdapter
    Public ds As DataSet
    Public str As String

    Sub testconnect()

        Try
            Dim str As String = "Server=localhost;user id=root;password=;database=db_crudvbnet"
            conn = New MySqlConnection(str)
            If conn.State = ConnectionState.Closed Then

                conn.Open()
                MsgBox("Connected Succesfully", MsgBoxStyle.Information, "Information")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Sub connect()
        Try
            Dim str As String = "Server=localhost;user id=root;password=;database=db_crudvbnet"
            conn = New MySqlConnection(str)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try
    End Sub

    Public Function SQLTable(ByVal Source As String) As DataTable
        Try
            Dim adp As New MySqlDataAdapter(Source, conn)
            Dim DT As New DataTable
            adp.Fill(DT)
            SQLTable = DT
        Catch ex As Exception
            MsgBox(ex.Message)
            SQLTable = Nothing
        End Try
    End Function
End Module
