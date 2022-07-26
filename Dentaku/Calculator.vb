Public Class Calculator

    Private numberBeforeOpetator As Double = 0                       '演算子入力前の数値
    Private numberAfterOperator As Double = 0                        '演算子入力後の数値
    Private operatorType As OperatorType = OperatorType.None         '演算子の種類を格納する
    Private previousAction As ActionType = ActionType.ClearAction    '一つ前の動作を格納する

    ''' <summary>
    ''' inputNumberを受けて表示される数を更新する
    ''' </summary>
    ''' <param name="inputNumber">ユーザーからの数入力</param>
    ''' <returns>画面に表示される数字(double型)</returns>
    Public Function Number(inputNumber As Integer) As Double

        If numberBeforeOpetator = 0 Then
            numberBeforeOpetator = inputNumber
            Return numberBeforeOpetator
        End If

        If operatorType = OperatorType.None Then
            Dim numberBeforeOpetatorText = numberBeforeOpetator.ToString() & inputNumber.ToString()
            If Not Integer.TryParse(numberBeforeOpetatorText, numberBeforeOpetator) Then
                Throw New FormatException($"{numberBeforeOpetatorText} を整数に変換できません")
            End If
            Return numberBeforeOpetator
        End If

        If numberAfterOperator = 0 Then
            If previousAction = ActionType.OperatorAction Then
                numberAfterOperator = inputNumber
                Return numberAfterOperator
            Else
                Dim numberBeforeOpetatorText = numberBeforeOpetator.ToString() & inputNumber.ToString()
                If Not Integer.TryParse(numberBeforeOpetatorText, numberBeforeOpetator) Then
                    Throw New FormatException($"{numberBeforeOpetatorText} を整数に変換できません")
                End If
                Return numberBeforeOpetator
            End If
        End If

        Dim numberAfterOperatorText = numberAfterOperator.ToString() & inputNumber.ToString()
        If Not Integer.TryParse(numberAfterOperatorText, numberAfterOperator) Then
            Throw New FormatException($"{numberAfterOperatorText} を整数に変換できません")
        End If

        Return numberAfterOperator

    End Function

    ''' <summary>
    ''' 数・演算子をクリアする
    ''' </summary>
    Public Sub Clear()

        ' 数・演算子・直前の動作をリセット
        numberBeforeOpetator = 0
        numberAfterOperator = 0
        operatorType = OperatorType.None
        previousAction = ActionType.ClearAction

    End Sub

    ''' <summary>
    ''' TODO：numberAfterOperatorが入っているときの動作が書けていない
    ''' 
    ''' </summary>
    ''' <param name="inputOperatorType"></param>
    Public Sub OperatorAction(inputOperatorType As OperatorType)

        previousAction = ActionType.OperatorAction
        operatorType = inputOperatorType

    End Sub

    ' イコールボタンが押されたときに動作する関数
    Public Function Equal() As Double

        Dim calculateResult As Double = Calculate()

        '全ての変数、演算子タイプをリセット
        numberBeforeOpetator = 0
        numberAfterOperator = 0
        operatorType = OperatorType.None
        previousAction = ActionType.EqualAction   'ボタンタイプにイコールボタンをセット

        Return calculateResult

    End Function

    ' 演算子に応じてnumberBeforeOpetatorとnumberAfterOperatorを使って計算を実行する関数
    Private Function Calculate() As Double

        Dim calculateResult As Double = 0

        '演算子ボタンの種類に応じて計算する
        Select Case operatorType
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
