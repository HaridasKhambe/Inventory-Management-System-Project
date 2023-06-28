Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (TextBox1.Text = "" And TextBox2.Text = "") Then
            MsgBox("Do Not Keep It Empty...!")
        Else
            If (TextBox1.Text = "a" And TextBox2.Text = "a") Then
                MsgBox("Successfully Login...")
                Dim obj As New Form2
                obj.Show()
                Me.Hide()
            Else
                MsgBox("Invalid Username And Password...!")
                TextBox1.Text = ""
                TextBox2.Text = ""
        End If
        End If
    End Sub


End Class