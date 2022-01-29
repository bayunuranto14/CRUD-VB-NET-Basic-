Public Class Dashboard

    Private Sub AboutThisApplicationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutThisApplicationToolStripMenuItem.Click
        MessageBox.Show("This is Application CRUD with MYSQL! . Thanks for using My Application!", "Hello! ", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

 
    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connect()
        kondisiawal()
        showdata()
    End Sub

    Sub showdata()
        da = New MySql.Data.MySqlClient.MySqlDataAdapter("select * from users order by id", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "users")
        Me.DataGridView1.DataSource = (ds.Tables("users"))
        no()
    End Sub

    Sub no()
        Dim dr As DataRow
        Dim x As String
        dr = SQLTable("select max(right(id,1)) as no from users").Rows(0)
        If dr.IsNull("no") Then
            x = "0"
        Else
            x = "0" & Format(dr("no") + 1, "0")
        End If
        TextBox1.Text = x
        TextBox1.Enabled = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'save data
        If Me.TextBox1.Text = "" Or Me.TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Sorry, Data is not completed")
        Else
            Dim save As String
            MsgBox("Your Data is Saved!")
            save = "Insert into users(id, firstname, lastname) values('" & Me.TextBox1.Text _
                & "','" & Me.TextBox2.Text & "','" & Me.TextBox3.Text & "')"
            cmd = New MySql.Data.MySqlClient.MySqlCommand(save, conn)
            cmd.ExecuteNonQuery()
            kondisiawal()
        End If
    End Sub

    Sub kondisiawal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        Button1.Enabled = True
        Button2.Enabled = False
        Button3.Enabled = False
        showdata()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'edit data
        If Me.TextBox1.Text = "" Or Me.TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Sorry, Data is not completed")
        Else
            Dim edit As String = "update users set id ='" & Me.TextBox1.Text _
                                 & "', firstname = '" & Me.TextBox2.Text _
                                 & "', lastname = '" & Me.TextBox3.Text _
                                 & "' where id = '" _
                                 & Me.TextBox1.Text & "'"

            cmd = New MySql.Data.MySqlClient.MySqlCommand(edit, conn)
            cmd.ExecuteNonQuery()
            MsgBox("edit successfully")
            kondisiawal()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Me.TextBox1.Text = "" Or Me.TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Sorry, Data is not completed")
            kondisiawal()
        Else
            If MessageBox.Show("are you sure you want to delete the data ??", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Dim delete As String = "delete from users where id = '" & Me.TextBox1.Text & "'"
                cmd = New MySql.Data.MySqlClient.MySqlCommand(delete, conn)
                cmd.ExecuteNonQuery()
                kondisiawal()
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        kondisiawal()
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        da = New MySql.Data.MySqlClient.MySqlDataAdapter("select * from users where firstname like '%" _
                                                          & Me.TextBox4.Text & "%'", conn)
        ds = New DataSet
        da.Fill(ds, "users")
        DataGridView1.DataSource = (ds.Tables("users"))
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        Button1.Enabled = False
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
    End Sub

    
    Private Sub ExitApplicationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitApplicationToolStripMenuItem.Click
        End
    End Sub
End Class