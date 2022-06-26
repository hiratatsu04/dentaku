Public Class Form1

    Dim Num1 As Integer = 0     '演算子入力前の数値
    Dim Num2 As Integer = 0     '演算子入力後の数値

    '演算子の定義（列挙型）
    Private Enum Ope
        Plus = 0
        Minus = 1
        Times = 2
        Divide = 3
        None = 4
    End Enum

    Dim OpeValue As Integer = Ope.None     '演算子を格納する変数。上記の列挙型演算子を代入する

    'ボタンタイプの定義（列挙型）
    Private Enum BtnType
        NumBtn = 10
        OpeBtn = 11
        EqualBtn = 12
        ClearBtn = 13
    End Enum

    Dim PreBtn As Integer = BtnType.ClearBtn    '一つ前に押されたボタンを格納する。上記の列挙型演算子を代入する

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

        If OpeValue = Ope.None Then
            Dim num1Text = Num1.ToString() & numTemp.ToString()
            Integer.TryParse(num1Text, Num1)    '文字列に変換後に足した後に数値に戻しているがこの動作不要？
            txtShowResult.Text = Num1.ToString()
            Return
        End If

        If Num2 = 0 Then
            If PreBtn = BtnType.OpeBtn Then
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
        PreBtn = BtnType.NumBtn     'ボタンタイプに数ボタンをセット

    End Sub

    Private Sub OpeButton_Click(sender As Object, e As EventArgs) Handles btnTimes.Click, btnDivide.Click, btnPlus.Click, btnMinus.Click

        Dim btnOpe = CType(sender, Button)
        Dim opeTemp As Integer
        Dim opeText As String = ""

        Select Case btnOpe.Name
            Case "btnPlus"
                opeTemp = Ope.Plus
                opeText = "＋"
            Case "btnMinus"
                opeTemp = Ope.Minus
                opeText = "－"
            Case "btnTimes"
                opeTemp = Ope.Times
                opeText = "×"
            Case "btnDivide"
                opeTemp = Ope.Divide
                opeText = "÷"
        End Select

        OpeValue = opeTemp
        txtShowOperator.Text = opeText
        PreBtn = BtnType.OpeBtn 'ボタンタイプに演算子ボタンをセット

    End Sub

    Private Sub BtnEqual_Click(sender As Object, e As EventArgs) Handles btnEqual.Click

        Dim calResult As Double = 0

        Select Case OpeValue
            Case Ope.Plus
                calResult = Num1 + Num2
            Case Ope.Minus
                calResult = Num1 - Num2
            Case Ope.Times
                calResult = Num1 * Num2
            Case Ope.Divide
                calResult = Num1 / Num2
        End Select

        txtShowResult.Text = calResult.ToString()

        Num1 = 0
        Num2 = 0
        OpeValue = Ope.None
        PreBtn = BtnType.EqualBtn   'ボタンタイプにイコールボタンをセット

    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtShowOperator.Text = ""
        txtShowResult.Text = ""
        Num1 = 0
        Num2 = 0
        OpeValue = Ope.None
        PreBtn = BtnType.ClearBtn   'ボタンタイプにイコールボタンをセット
    End Sub

End Class
