Public Class Calculator

    Private number1 As Double = 0     '演算子入力前の数値
    Private number2 As Double = 0     '演算子入力後の数値
    Private operatorValue As OperatorType = OperatorType.None     '演算子を格納する変数。
    Private previousOperation As OperationType = OperationType.ClearButton    '一つ前に押されたボタンを格納する。

    ' 数ボタンが押された時に動作するメソッド
    Public Function NumberAct(numberTemporary As Integer) As Double

        'Number1に数字が入っているか判別。入っていなければ、numberTemporaryを入れてプロシージャを抜ける
        If number1 = 0 Then
            number1 = numberTemporary
            Return number1
        End If

        '演算子が格納されているか判別。入っていなければNumber1にnumberTemporaryを加えてプロシージャを抜ける
        If operatorValue = OperatorType.None Then
            Dim number1Text = number1.ToString() & numberTemporary.ToString()
            If Not Integer.TryParse(number1Text, number1) Then
                Throw New FormatException($"{number1Text} を整数に変換できません")
            End If
            Return number1
        End If

        'Number2に数字が格納されているか判別。数字が入っておらず、ひとつ前に押されたボタンが演算子であれば、Number2にnumberTemporaryを入れる。演算子ボタン以外であればNumber1にnumberTemporaryを加える。
        If number2 = 0 Then
            If previousOperation = OperationType.OperatorButton Then
                number2 = numberTemporary
                Return number2
            Else
                Dim number1Text = number1.ToString() & numberTemporary.ToString()
                If Not Integer.TryParse(number1Text, number1) Then
                    Throw New FormatException($"{number1Text} を整数に変換できません")
                End If
                Return number1
            End If
        End If

        Dim number2Text = number2.ToString() & numberTemporary.ToString()
        If Not Integer.TryParse(number2Text, number2) Then
            Throw New FormatException($"{number2Text} を整数に変換できません")
        End If

        Return number2

    End Function

    ' クリアボタンが押された時に動作するメソッド
    Public Sub ClearAct()
        number1 = 0
        number2 = 0
        operatorValue = OperatorType.None
        previousOperation = OperationType.ClearButton   '直前の動作(previousOperation)にイコール動作をセット
    End Sub

    ' 演算子ボタンが押されたときに動作する関数
    Public Sub OperatorAct(operatorTemporary As OperatorType)

        previousOperation = OperationType.OperatorButton '直前の動作(previousOperation)に演算動作ンをセット
        operatorValue = operatorTemporary

    End Sub

    ' イコールボタンが押されたときに動作する関数
    Public Function EqualAct() As Double

        Dim calculateResult = Calculate()

        '全ての変数、演算子タイプをリセット
        number1 = 0
        number2 = 0
        operatorValue = OperatorType.None
        previousOperation = OperationType.EqualButton   'ボタンタイプにイコールボタンをセット

        Return calculateResult

    End Function

    ' 演算子に応じてnumber1とnumber2を使って計算を実行する関数
    Private Function Calculate() As Double

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

        Debug.WriteLine(number1.ToString)
        Debug.WriteLine(number2.ToString)

        Return calculateResult

    End Function

End Class
