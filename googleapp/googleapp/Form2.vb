Imports System.Data.SqlClient
Public Class Form2

    Dim con As New SqlConnection("Data Source=LAPTOP-FA3FANOM\SQLEXPRESS;Initial Catalog=googledata;Integrated Security=True")
    Dim i As Integer
    Private Sub display()
        con.open()
        Dim cmd As New SqlCommand("select * from stock", con)
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        da.fill(ds, "stock")
        DataGridView1.DataSource = ds
        DataGridView1.DataMember = "stock"

        con.close()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        display()
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Missing Information..!! ID And Product Name Is Required...")
        Else
            Try
                con.Open()
                Dim cmd As New SqlCommand("insert into stock (product_id,idate,product_name,description,incoming_quantity,outgoing_quantity,avaible_quantity,price) values ('" + TextBox1.Text + "','" + idate.Value.ToString("yyyy-MM-dd") + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox7.Text + "')", con)
                cmd.ExecuteNonQuery()
                MsgBox("item added.")
                con.Close()
                display()
            Catch ex As Exception
                MsgBox(ex.Message)
                con.Close()
            End Try
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Then
            MsgBox("Please Enter ID Of Item....")
        Else
            Try
                con.Open()
                Dim cmd As New SqlCommand("delete from stock where product_id ='" + TextBox1.Text + "' ", con)
                If cmd.ExecuteNonQuery() Then
                    MsgBox("Item Deleted.")
                Else
                    MsgBox("Id Not Matched...!!")
                End If
                con.Close()
                display()
            Catch ex As Exception
                MsgBox(ex.Message)
                con.Close()
            End Try
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox1.Text = "" Then
            MsgBox("Please Enter ID And Data Of Product To Update Record...")
        Else
            Try
                con.Open()
                Dim cmd As New SqlCommand("update stock set product_id='" + TextBox1.Text + "' , idate='" + idate.Value.ToString("yyyy-MM-dd") + "' ,product_name='" + TextBox2.Text + "' ,description='" + TextBox3.Text + "' ,incoming_quantity='" + TextBox4.Text + "' ,outgoing_quantity='" + TextBox5.Text + "' ,avaible_quantity='" + TextBox6.Text + "' ,price='" + TextBox7.Text + "' where product_id='" + TextBox1.Text + "' ", con)
                If cmd.ExecuteNonQuery() Then
                    MsgBox("Item Updated.")
                Else
                    MsgBox("Id Not Matched...!!")
                End If
                con.Close()
                display()
            Catch ex As Exception
                MsgBox(ex.Message)
                con.Close()
            End Try
        End If
    End Sub



    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        If MsgBox("Are You Want To Delete All Items...?", MsgBoxStyle.YesNo, "Delete Message") = MsgBoxResult.Yes Then
            Try
                con.Open()
                Dim cmd As New SqlCommand("delete from stock", con)
                If cmd.ExecuteNonQuery() Then
                    MsgBox("All Items Deleted Successfully.")

                End If
                con.Close()
                display()
            Catch ex As Exception
                MsgBox(ex.Message)
                con.Close()
            End Try

        End If
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Dim obj As New Form3
        obj.Show()
        Me.Hide()

    End Sub


    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick



        Try
            con.Open()
            i = Convert.ToInt32(DataGridView1.SelectedCells.Item(0).Value.ToString())

            Dim cmd As New SqlCommand
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from stock where product_id = " & i & " "
            cmd.ExecuteNonQuery()
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Dim dr As SqlClient.SqlDataReader
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While dr.Read
                TextBox1.Text = dr.GetString(0).ToString()
                idate.Value = dr.GetDateTime(1).ToString()

                TextBox2.Text = dr.GetString(2).ToString()
                TextBox3.Text = dr.GetString(3).ToString()
                TextBox4.Text = dr.GetString(4).ToString()
                TextBox5.Text = dr.GetString(5).ToString()
                TextBox6.Text = dr.GetString(6).ToString()
                TextBox7.Text = dr.GetString(7).ToString()

            End While
            con.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            con.Close()
        End Try
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Dim obj As New Form4
        obj.Show()
        Me.Hide()

    End Sub
End Class