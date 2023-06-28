Imports System.Data.SqlClient
Public Class Form4
    Dim con As New SqlConnection("Data Source=LAPTOP-FA3FANOM\SQLEXPRESS;Initial Catalog=googledata;Integrated Security=True")
    Dim i As Integer
    Private Sub display()
        con.Open()
        Dim cmd As New SqlCommand("select * from customers", con)
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        da.Fill(ds, "customers")
        DataGridView1.DataSource = ds
        DataGridView1.DataMember = "customers"

        con.Close()
    End Sub
    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        Dim obj As New Form2
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Dim obj As New Form3
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        display()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Missing Information..!! Enter ID And Name...")
        Else
            Try
                con.Open()
                Dim cmd As New SqlCommand("insert into customers (customer_id,cdate,customer_name,mobile,model,problem,solution,price) values ('" + TextBox1.Text + "','" + ccdate.Value.ToString("yyyy-MM-dd") + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + TextBox7.Text + "')", con)
                cmd.ExecuteNonQuery()
                MsgBox("Customer added.")
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
            MsgBox("Please Enter ID Of Customer....")
        Else
            Try
                con.Open()
                Dim cmd As New SqlCommand("delete from customers where customer_id = '" + TextBox1.Text + "' ", con)
                If cmd.ExecuteNonQuery() Then
                    MsgBox("Customer Deleted.")
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
            MsgBox("Please Enter ID And Data Of Customer To Update Record...")
        Else
            Try
                con.Open()
                Dim cmd As New SqlCommand("update customers set customer_id='" + TextBox1.Text + "' , cdate='" + ccdate.Value.ToString("yyyy-MM-dd") + "' ,customer_name='" + TextBox2.Text + "' ,mobile='" + TextBox3.Text + "' ,model='" + TextBox4.Text + "' ,problem='" + TextBox5.Text + "' ,solution='" + TextBox6.Text + "' ,price='" + TextBox7.Text + "' where customer_id='" + TextBox1.Text + "' ", con)
                If cmd.ExecuteNonQuery() Then
                    MsgBox("Customer Updated.")
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If MsgBox("Are You Want To Delete All Customers...?", MsgBoxStyle.YesNo, "Delete Message") = MsgBoxResult.Yes Then
            Try
                con.Open()
                Dim cmd As New SqlCommand("delete from customers", con)
                If cmd.ExecuteNonQuery() Then
                    MsgBox("All Customers Deleted Successfully.")

                End If
                con.Close()
                display()
            Catch ex As Exception
                MsgBox(ex.Message)
                con.Close()
            End Try

        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        display()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If TextBox8.Text = "" Then
            MsgBox("Please Enter The Customer Name...")
        Else
            Try
                con.Open()
                Dim cmd As New SqlCommand("select * from customers where customer_name='" + TextBox8.Text + "'", con)
                Dim da As New SqlDataAdapter(cmd)
                Dim ds As New DataSet
                da.Fill(ds, "customers")
                DataGridView1.DataSource = ds
                DataGridView1.DataMember = "customers"
                con.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
                con.Close()
            End Try
        End If


    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        Try
            con.Open()
            i = Convert.ToInt32(DataGridView1.SelectedCells.Item(0).Value.ToString())

            Dim cmd As New SqlCommand
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from customers where customer_id = " & i & " "
            cmd.ExecuteNonQuery()
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Dim dr As SqlClient.SqlDataReader
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While dr.Read
                TextBox1.Text = dr.GetString(0).ToString()
                ccdate.Value = dr.GetDateTime(1).ToString()
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
End Class