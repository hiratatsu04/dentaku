Public Class Form1

    Dim number1 As Integer = 0     '演算子入力前の数値
    Dim number2 As Integer = 0     '演算子入力後の数値

    '演算子の定義（列挙型）
    Private Enum OperatorType
        Plus = 0
        Minus = 1
        Times = 2
        Divide = 3
        None = 4
    End Enum

    Dim operatorValue As OperatorType = OperatorType.None     '演算子を格納する変数。上記の列挙型演算子を代入する

    Dim operatorTexts As New Dictionary(Of OperatorType, String)() From
        {
        {OperatorType.Plus, "＋"},
        {OperatorType.Minus, "－"},
        {OperatorType.Times, "×"},
        {OperatorType.Divide, "÷"}
        }

    'ボタンタイプの定義（列挙型）
    Private Enum ButtonType
        NumberButton = 10
        OperatorButton = 11
        EqualButton = 12
        ClearButton = 13
    End Enum

    Dim previousButton As Integer = ButtonType.ClearButton    '一つ前に押されたボタンを格納する。上記の列挙型演算子を代入する

    '数ボタンが押された時の動作
    Private Sub NumberButtonClick(sender As Object, e As EventArgs) Handles btn0.Click, btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click

        Dim buttonNumber = CType(sender, Button)   'senderをボタン型に変更
        Dim buttonText As String = ""      'ボタンの[NAME]を格納する変数
        Dim numberTemporary As Integer      '押されたボタンを一時的に格納するローカル変数

        '押されたボタンの判別。ボタンのNAMEから「btn」を除いて、数値に変換して、numTempに代入
        buttonText = buttonNumber.Name.Remove(0, 3)
        If Not Integer.TryParse(buttonText, numberTemporary) Then
            MessageBox.Show("ボタンフォームに数値以外の値が入っています")
        End If

        'Number1に数字が入っているか判別。入っていなければ、numberTemporaryを入れてプロシージャを抜ける
        If number1 = 0 Then
            number1 = numberTemporary
            txtShowResult.Text = number1.ToString()
            Return
        End If

        '演算子が格納されているか判別。入っていなければNumber1にnumberTemporaryを加えてプロシージャを抜ける
        If operatorValue = OperatorType.None Then
            Dim number1Text = number1.ToString() & numberTemporary.ToString()
            If Not Integer.TryParse(number1Text, number1) Then
                MessageBox.Show("Num1に数値以外が代入されました。")
            End If
            txtShowResult.Text = number1.ToString()
            Return
        End If

        'Number2に数字が格納されているか判別。数字が入っておらず、ひとつ前に押されたボタンが演算子であれば、Number2にnumberTemporaryを入れる。演算子ボタン以外であればNumber1にnumberTemporaryを加える。
        If number2 = 0 Then
            If previousButton = ButtonType.OperatorButton Then
                number2 = numberTemporary
                txtShowResult.Text = number2.ToString()
                Return
            Else
                Dim nNumber1Text = number1.ToString() & numberTemporary.ToString()
                If Not Integer.TryParse(nNumber1Text, number1) Then
                    MessageBox.Show("Num1に数値以外が代入されました。")
                End If
                txtShowResult.Text = number1.ToString()
                Return
            End If
        End If

        Dim number2Text = number2.ToString() & numberTemporary.ToString()
        If Not Integer.TryParse(number2Text, number2) Then
            MessageBox.Show("Num2に数値以外が代入されました。")
        End If
        txtShowResult.Text = number2.ToString()
        previousButton = ButtonType.NumberButton     'ボタンタイプに数ボタンをセット

    End Sub

    '演算子ボタンが押された時の動作
    Private Sub OperatorButtonClick(sender As Object, e As EventArgs) Handles btnTimes.Click, btnDivide.Click, btnPlus.Click, btnMinus.Click

        Dim buttonOperator = CType(sender, Button)  'senderをボタン型に変更
        Dim operatorTemporary As Integer      '演算子ボタンの種類を格納する変数

        Select Case buttonOperator.Name
            Case "btnPlus"
                operatorTemporary = OperatorType.Plus
            Case "btnMinus"
                operatorTemporary = OperatorType.Minus
            Case "btnTimes"
                operatorTemporary = OperatorType.Times
            Case "btnDivide"
                operatorTemporary = OperatorType.Divide
        End Select

        operatorValue = operatorTemporary
        txtShowOperator.Text = operatorTexts(operatorTemporary)
        previousButton = ButtonType.OperatorButton 'ボタンタイプに演算子ボタンをセット

    End Sub

    'イコールボタンの動作
    Private Sub EqualButtonClick(sender As Object, e As EventArgs) Handles btnEqual.Click

        Dim result = Equal()
        txtShowResult.Text = result.ToString()

    End Sub

    'クリアボタンの動作。全て表示、変数をリセット
    Private Sub ClearButtonClick(sender As Object, e As EventArgs) Handles btnClear.Click
        txtShowOperator.Text = ""
        txtShowResult.Text = ""
        number1 = 0
        number2 = 0
        operatorValue = OperatorType.None
        previousButton = ButtonType.ClearButton   'ボタンタイプにイコールボタンをセット
    End Sub

    Private Function Equal() As Double
        Dim calculateResult = Calculate(number1, number2, operatorValue)

        '全ての変数、演算子タイプをリセット
        number1 = 0
        number2 = 0
        operatorValue = OperatorType.None
        previousButton = ButtonType.EqualButton   'ボタンタイプにイコールボタンをセット

        Return calculateResult
    End Function

    Private Function Calculate(number1 As Integer, number2 As Integer, operatorValue As OperatorType) As Double

        Dim calculateResult As Double = 0

        '演算子ボタンの種類に応じて計算する
        Select Case operatorValue
            Case OperatorType.Plus
                calculateResult = number1 + number2
            Case OperatorType.Minus
                calculateResult = number1 - number2
            Case OperatorType.Times
                calculateResult = number1 * number2
            Case OperatorType.Divide
                calculateResult = number1 / number2
        End Select

        Return calculateResult

    End Function

End Class
