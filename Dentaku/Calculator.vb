Public Class Calculator

    Private numberBeforeOpetator As Double = 0     '演算子入力前の数値
    Private numberAfterOperator As Double = 0     '演算子入力後の数値
    Private operatorValue As OperatorType = OperatorType.None     '演算子を格納する変数。
    Private previousOperation As OperationType = OperationType.ClearButton    '一つ前に押されたボタンを格納する。

    ' 数ボタンが押された時に動作するメソッド
    Public Function NumberAct(numberTemporary As Integer) As Double

        'Number1に数字が入っているか判別。入っていなければ、numberTemporaryを入れてプロシージャを抜ける
        If numberBeforeOpetator = 0 Then
            numberBeforeOpetator = numberTemporary
            Return numberBeforeOpetator
        End If

        '演算子が格納されているか判別。入っていなければNumber1にnumberTemporaryを加えてプロシージャを抜ける
        If operatorValue = OperatorType.None Then
            Dim numberBeforeOpetatorText = numberBeforeOpetator.ToString() & numberTemporary.ToString()
            If Not Integer.TryParse(numberBeforeOpetatorText, numberBeforeOpetator) Then
                Throw New FormatException($"{numberBeforeOpetatorText} を整数に変換できません")
            End If
            Return numberBeforeOpetator
        End If

        'Number2に数字が格納されているか判別。数字が入っておらず、ひとつ前に押されたボタンが演算子であれば、Number2にnumberTemporaryを入れる。演算子ボタン以外であればNumber1にnumberTemporaryを加える。
        If numberAfterOperator = 0 Then
            If previousOperation = OperationType.OperatorButton Then
                numberAfterOperator = numberTemporary
                Return numberAfterOperator
            Else
                Dim numberBeforeOpetatorText = numberBeforeOpetator.ToString() & numberTemporary.ToString()
                If Not Integer.TryParse(numberBeforeOpetatorText, numberBeforeOpetator) Then
                    Throw New FormatException($"{numberBeforeOpetatorText} を整数に変換できません")
                End If
                Return numberBeforeOpetator
            End If
        End If

        Dim numberAfterOperatorText = numberAfterOperator.ToString() & numberTemporary.ToString()
        If Not Integer.TryParse(numberAfterOperatorText, numberAfterOperator) Then
            Throw New FormatException($"{numberAfterOperatorText} を整数に変換できません")
        End If

        Return numberAfterOperator

    End Function

    ' クリアボタンが押された時に動作するメソッド
    Public Sub ClearAct()
        numberBeforeOpetator = 0
        numberAfterOperator = 0
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
        numberBeforeOpetator = 0
        numberAfterOperator = 0
        operatorValue = OperatorType.None
        previousOperation = OperationType.EqualButton   'ボタンタイプにイコールボタンをセット

        Return calculateResult

    End Function

    ' 演算子に応じてnumberBeforeOpetatorとnumberAfterOperatorを使って計算を実行する関数
    Private Function Calculate() As Double

        Dim calculateResult As Double = 0

        '演算子ボタンの種類に応じて計算する
        Select Case operatorValue
            Case OperatorType.Plus
                calculateResult = numberBeforeOpetator + numberAfterOperator
            Case OperatorType.Minus
                calculateResult = numberBeforeOpetator - numberAfterOperator
            Case OperatorType.Times
                calculateResult = numberBeforeOpetator * numberAfterOperator
            Case OperatorType.Divide
                calculateResult = numberBeforeOpetator / numberAfterOperator
        End Select

        Debug.WriteLine(numberBeforeOpetator.ToString)
        Debug.WriteLine(numberAfterOperator.ToString)

        Return calculateResult

    End Function

End Class
