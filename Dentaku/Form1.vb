Public Class Form1

    ' 計算クラスのインスタンス生成
    Dim calculatorObject As New Calculator

    ' OperationTypeをキーに、表示する演算子文字列のディクショナリを定義
    Dim operatorTexts As New Dictionary(Of OperatorType, String)() From
        {
        {OperatorType.Plus, "＋"},
        {OperatorType.Minus, "－"},
        {OperatorType.Times, "×"},
        {OperatorType.Divide, "÷"}
        }

    '数ボタンが押された時の動作
    Private Sub NumberButtonClick(sender As Object, e As EventArgs) Handles btn0.Click, btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click

        Dim inputNumber As Integer      '押されたボタンを一時的に格納するローカル変数

        '押されたボタンの判別。ボタンのNAMEから「btn」を除いて、数値に変換して、numTempに代入
        Dim buttonNumber = CType(sender, Button)   'senderをボタン型に変更
        Dim buttonText As String = buttonNumber.Name.Remove(0, 3)
        If Not Integer.TryParse(buttonText, inputNumber) Then
            MessageBox.Show("ボタンフォームに数値以外の値が入っています")
            Return
        End If

        Try
            Dim number = calculatorObject.Number(inputNumber)
            txtShowResult.Text = number.ToString()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    '演算子ボタンが押された時の動作
    Private Sub OperatorButtonClick(sender As Object, e As EventArgs) Handles btnTimes.Click, btnDivide.Click, btnPlus.Click, btnMinus.Click

        Dim buttonOperator = CType(sender, Button)  'senderをボタン型に変更
        Dim operatorType As OperatorType      '演算子ボタンの種類を格納する変数

        Select Case buttonOperator.Name
            Case "btnPlus"
                operatorType = OperatorType.Plus
            Case "btnMinus"
                operatorType = OperatorType.Minus
            Case "btnTimes"
                operatorType = OperatorType.Times
            Case "btnDivide"
                operatorType = OperatorType.Divide
        End Select

        calculatorObject.OperatorAction(operatorType)
        txtShowOperator.Text = operatorTexts(operatorType)

    End Sub

    'イコールボタンの動作
    Private Sub EqualButtonClick(sender As Object, e As EventArgs) Handles btnEqual.Click

        Dim result = calculatorObject.Equal()

        txtShowResult.Text = result.ToString()

    End Sub

    'クリアボタンの動作。全て表示、変数をリセット
    Private Sub ClearButtonClick(sender As Object, e As EventArgs) Handles btnClear.Click

        calculatorObject.Clear()

        txtShowOperator.Text = ""
        txtShowResult.Text = ""

    End Sub

End Class
