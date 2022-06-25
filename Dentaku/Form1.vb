Public Class Form1

    Dim Num1 As Integer = 0
    Dim Num2 As Integer = 0
    Dim Operat As String

    Private Sub NumButtonClick(sender As Object, e As EventArgs) Handles btn0.Click, btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click

        Dim BtnNumber = CType(sender, Button)
        Dim NumTemp As Integer

        Select Case BtnNumber.Name
            Case "btn0"
                NumTemp = 0
            Case "btn1"
                NumTemp = 1
            Case "btn2"
                NumTemp = 2
            Case "btn3"
                NumTemp = 3
            Case "btn4"
                NumTemp = 4
            Case "btn5"
                NumTemp = 5
            Case "btn6"
                NumTemp = 6
            Case "btn7"
                NumTemp = 7
            Case "btn8"
                NumTemp = 8
            Case "btn9"
                NumTemp = 9
        End Select

        If Num1 = 0 Then
            Num1 = NumTemp
            txtShowResult.Text = Num1.ToString()
            Return
        End If

        If Operat = "" Then
            Dim Num1Text = Num1.ToString() & NumTemp.ToString()
            Integer.TryParse(Num1Text, Num1)
            txtShowResult.Text = Num1.ToString()
            Return
        End If

        txtShowResult.Text = NumTemp.ToString()

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
