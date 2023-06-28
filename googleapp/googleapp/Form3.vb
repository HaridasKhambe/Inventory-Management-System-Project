Imports System.Data.SqlClient
Public Class Form3

    Dim con As New SqlConnection("Data Source=LAPTOP-FA3FANOM\SQLEXPRESS;Initial Catalog=googledata;Integrated Security=True")
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MsgBox("Please Enter The Customer Name...")
        Else
            Try
                con.Open()
                Dim cmd As New SqlCommand("select * from stock where product_name='" + TextBox1.Text + "'", con)
                Dim da As New SqlDataAdapter(cmd)
                Dim ds As New DataSet
                da.Fill(ds, "stock")
                DataGridView1.DataSource = ds
                DataGridView1.DataMember = "stock"
                con.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
                con.Close()
            End Try

        End If





    End Sub

    Private Sub display()
        con.Open()
        Dim cmd As New SqlCommand("select * from stock", con)
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        da.Fill(ds, "stock")
        DataGridView1.DataSource = ds
        DataGridView1.DataMember = "stock"

        con.Close()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        display()
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        display()
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click
        Dim obj As New Form2
        obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Dim obj As New Form4
        obj.Show()
        Me.Hide()
    End Sub
End Class