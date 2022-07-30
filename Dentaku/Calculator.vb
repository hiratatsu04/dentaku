Public Class Calculator

    Private numberBeforeOperator As Double = 0                      '演算子入力前の数値
    Private numberAfterOperator As Double = 0                       '演算子入力後の数値
    Private operatorType As OperatorType = OperatorType.None        '演算子の種類を格納する
    Private previousAction As ActionType = ActionType.ClearAction   '一つ前の動作を格納する
    Private addPoint As Boolean = False                             '小数点の要否判断に用いる
    Private addPointnumBefore As Boolean = False
    Private addPointnumAfter As Boolean = False


    ''' <summary>
    ''' inputNumberを受けて表示される数を更新する
    ''' </summary>
    ''' <param name="inputNumber">ユーザーからの数入力</param>
    ''' <returns>画面に表示される数字(double型)</returns>
    Public Function Number(inputNumber As Integer) As Double

        If numberBeforeOperator = 0 Then
            numberBeforeOperator = inputNumber
            Return numberBeforeOperator
        End If

        If operatorType = OperatorType.None Then
            Dim numberBeforeOpetatorText = numberBeforeOperator.ToString() & inputNumber.ToString()

            If addPointnumBefore = False Then
                numberBeforeOpetatorText = numberBeforeOperator.ToString() & "." & inputNumber.ToString()
                addPointnumBefore = True
            End If

            If Not Double.TryParse(numberBeforeOpetatorText, numberBeforeOperator) Then
                Throw New FormatException($"{numberBeforeOpetatorText} を整数に変換できません")
            End If

            Return numberBeforeOperator
        End If

        If numberAfterOperator = 0 Then
            If previousAction = ActionType.OperatorAction Then
                numberAfterOperator = inputNumber
                Return numberAfterOperator
            Else
                Dim numberBeforeOpetatorText = numberBeforeOperator.ToString() & inputNumber.ToString()

                If addPointnumBefore = False Then
                    numberBeforeOpetatorText = numberBeforeOperator.ToString() & "." & inputNumber.ToString()
                    addPointnumBefore = True
                End If

                If Not Double.TryParse(numberBeforeOpetatorText, numberBeforeOperator) Then
                    Throw New FormatException($"{numberBeforeOpetatorText} を整数に変換できません")
                End If

                Return numberBeforeOperator
            End If
        End If

        Dim numberAfterOperatorText = numberAfterOperator.ToString() & inputNumber.ToString()

        If addPointnumAfter = False Then
            numberAfterOperatorText = numberAfterOperator.ToString() & "." & inputNumber.ToString()
            addPointnumAfter = True
        End If

        If Not Double.TryParse(numberAfterOperatorText, numberAfterOperator) Then
            Throw New FormatException($"{numberAfterOperatorText} を整数に変換できません")
        End If

        Return numberAfterOperator

    End Function

    Public Function Point() As String

        If addPointnumBefore = True And addPoint = True Then
            Return numberBeforeOperator
        End If

        If addPointnumAfter = True Then
            Return numberAfterOperator
        End If

        addPoint = True

        Dim currentNumber As String

        If Not operatorType = OperatorType.None Then
            currentNumber = numberAfterOperator.ToString() + "."
            Debug.WriteLine("numberAfterOperator" + currentNumber)
        Else
            currentNumber = numberBeforeOperator.ToString() + "."
            Debug.WriteLine("numberBeforeOpetator" + currentNumber)
        End If

        Return currentNumber

    End Function

    ''' <summary>
    ''' 数・演算子（メンバ変数）をクリアする
    ''' </summary>
    Public Sub Clear()

        ' 数・演算子・直前の動作をリセット
        numberBeforeOperator = 0
        numberAfterOperator = 0
        operatorType = OperatorType.None
        previousAction = ActionType.ClearAction
        addPoint = False
        addPointnumAfter = False
        addPointnumBefore = False

    End Sub

    ''' <summary>
    ''' TODO：numberAfterOperatorに数値が入っているときの動作が書けていない
    ''' numberAfterOperatorに数値が入っていれば、計算結果を返す
    ''' </summary>
    ''' <param name="inputOperatorType"></param>
    Public Function OperatorAction(inputOperatorType As OperatorType) As Double

        If previousAction = ActionType.EqualAction Then
            numberAfterOperator = 0
        End If

        If Not numberAfterOperator = 0 Then
            Dim calculateResult = Calculate()
            numberBeforeOperator = calculateResult
            numberAfterOperator = 0
        End If

        previousAction = ActionType.OperatorAction
        operatorType = inputOperatorType

        Return numberBeforeOperator

    End Function

    ''' <summary>
    ''' 演算を実施後、変数・演算子の種類（メンバ変数）をリセット
    ''' </summary>
    ''' <returns></returns>
    Public Function Equal() As Double

        Dim calculateResult As Double = Calculate()

        '全ての変数・演算子の種類をリセット
        numberBeforeOperator = calculateResult
        previousAction = ActionType.EqualAction

        Return calculateResult

    End Function

    ''' <summary>
    ''' numberBeforeOpetatorとnumberAfterOpetatorを演算子に応じて計算する。
    ''' </summary>
    ''' <returns></returns>
    Private Function Calculate() As Double

        Dim calculateResult As Double = 0

        '演算子ボタンの種類に応じて計算する
        Select Case operatorType
            Case OperatorType.Plus
                calculateResult = numberBeforeOperator + numberAfterOperator
            Case OperatorType.Minus
                calculateResult = numberBeforeOperator - numberAfterOperator
            Case OperatorType.Times
                calculateResult = numberBeforeOperator * numberAfterOperator
            Case OperatorType.Divide
                calculateResult = numberBeforeOperator / numberAfterOperator
        End Select

        Debug.WriteLine(numberBeforeOperator.ToString)
        Debug.WriteLine(numberAfterOperator.ToString)

        Return calculateResult

    End Function

End Class
