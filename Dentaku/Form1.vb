Public Class Form1

    Dim Num1 As Integer = 0
    Dim Num2 As Integer = 0
    Dim Operat As String = ""
    Dim PreBtn As Integer

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
            Integer.TryParse(Num1Text, Num1)    '文字列に変換後に足した後に数値に戻しているがこの動作不要？
            txtShowResult.Text = Num1.ToString()
            Return
        End If

        If Num2 = 0 Then
            If PreBtn = "Ope" Then
                Dim Num1Text = Num1.ToString() & NumTemp.ToString()
                Integer.TryParse(Num1Text, Num1)    '文字列に変換後に足した後に数値に戻しているがこの動作不要？
                txtShowResult.Text = Num1.ToString()
                Return
            Else
                Num2 = NumTemp
                txtShowResult.Text = Num2.ToString()
                Return
            End If
        End If

        Dim Num2Text = Num2.ToString() & NumTemp.ToString()
        Integer.TryParse(Num2Text, Num2)    '文字列に変換後に足した後に数値に戻しているがこの動作不要？
        txtShowResult.Text = Num2.ToString()
        Return

        txtShowResult.Text = Num2.ToString()

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
