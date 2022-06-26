Public Class Form1

    Dim Num1 As Integer = 0
    Dim Num2 As Integer = 0
    Dim Ope As String = ""
    Dim PreBtn As String

    Private Sub NumButtonClick(sender As Object, e As EventArgs) Handles btn0.Click, btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click

        Dim btnNumber = CType(sender, Button)
        Dim btnText As String = ""
        Dim numTemp As Integer

        '押されたボタンの判別。ボタンのNAMEから「btn」を除いて、数値に変換して、numTempに代入
        btnText = btnNumber.Name.Remove(0, 3)
        numTemp = Integer.Parse(btnText)

        If Num1 = 0 Then
            Num1 = numTemp
            txtShowResult.Text = Num1.ToString()
            Return
        End If

        If Ope = "" Then
            Dim num1Text = Num1.ToString() & numTemp.ToString()
            Integer.TryParse(num1Text, Num1)    '文字列に変換後に足した後に数値に戻しているがこの動作不要？
            txtShowResult.Text = Num1.ToString()
            Return
        End If

        If Num2 = 0 Then
            If PreBtn = "Ope" Then
                Num2 = numTemp
                txtShowResult.Text = Num2.ToString()
                Return
            Else
                Dim nNum1Text = Num1.ToString() & numTemp.ToString()
                Integer.TryParse(nNum1Text, Num1)    '文字列に変換後に足した後に数値に戻しているがこの動作不要？
                txtShowResult.Text = Num1.ToString()
                Return
            End If
        End If

        Dim num2Text = Num2.ToString() & numTemp.ToString()
        Integer.TryParse(num2Text, Num2)    '文字列に変換後に足した後に数値に戻しているがこの動作不要？
        txtShowResult.Text = Num2.ToString()
        Return

        txtShowResult.Text = Num2.ToString()

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub OpeButton_Click(sender As Object, e As EventArgs) Handles btnTimes.Click, btnDevide.Click, btnPlus.Click, btnMinus.Click

        Dim btnOpe = CType(sender, Button)
        Dim opeTemp As String = ""

        Select Case btnOpe.Name
            Case "btnPlus"
                opeTemp = "+"
            Case "btnMinus"
                opeTemp = "-"
            Case "btnTimes"
                opeTemp = "*"
            Case "btnDevide"
                opeTemp = "/"
        End Select

        Ope = opeTemp
        txtShowOperator.Text = Ope

        PreBtn = "Ope"

    End Sub

    Private Sub BtnEqual_Click(sender As Object, e As EventArgs) Handles btnEqual.Click

        Dim calResult As Double = 0

        Select Case Ope
            Case "+"
                calResult = Num1 + Num2
            Case "-"
                calResult = Num1 - Num2
            Case "*"
                calResult = Num1 * Num2
            Case "/"
                calResult = Num1 / Num2
        End Select

        txtShowResult.Text = calResult.ToString()

        Num1 = 0
        Num2 = 0
        Ope = ""
        PreBtn = "Equal"

    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtShowOperator.Text = ""
        txtShowResult.Text = ""
        Num1 = 0
        Num2 = 0
        Ope = ""
        PreBtn = ""
    End Sub

End Class
