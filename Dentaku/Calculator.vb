Public Class Calculator

    Private numberBeforeOperator As Double = 0                      '演算子入力前の数値
    Private numberAfterOperator As Double = 0                       '演算子入力後の数値
    Private operatorType As OperatorType = OperatorType.None        '演算子の種類を格納する
    Private previousAction As ActionType = ActionType.ClearAction   '一つ前の動作を格納する
    Private addPoint As Boolean = False                             '小数点の要否判断に用いる
    Private addPointnumBefore As Boolean = False
    Private addPointnumAfter As Boolean = False

    Dim numberBeforeOpetatorText As String = ""
    Dim numberAfterOpetatorText As String = ""


    ''' <summary>
    ''' inputNumberを受けて表示される数を更新する
    ''' </summary>
    ''' <param name="inputNumber">ユーザーからの数入力</param>
    ''' <returns>画面に表示される数字(double型)</returns>
    Public Function Number(inputNumber As Integer) As String

        Console.WriteLine(operatorType)
        If previousAction = ActionType.EqualAction Then
            Clear()
        End If

        If operatorType = OperatorType.None Then

            If numberBeforeOperator = 0 And addPoint = False Then
                numberBeforeOperator = inputNumber
                numberBeforeOpetatorText = numberBeforeOperator.ToString()
                Console.WriteLine("1-1")
            ElseIf addPoint = True And addPointnumBefore = False Then
                numberBeforeOpetatorText = numberBeforeOpetatorText & "." & inputNumber.ToString()
                addPointnumBefore = True
                addPoint = False
                If Not Double.TryParse(numberBeforeOpetatorText, numberBeforeOperator) Then
                    Throw New FormatException($"{numberBeforeOpetatorText} を整数に変換できません")
                End If
                Console.WriteLine("1-2")
            Else
                numberBeforeOpetatorText = numberBeforeOpetatorText & inputNumber.ToString()
                If Not Double.TryParse(numberBeforeOpetatorText, numberBeforeOperator) Then
                    Throw New FormatException($"{numberBeforeOpetatorText} を整数に変換できません")
                End If
                Console.WriteLine("1-3")
            End If

            Return numberBeforeOpetatorText

        Else

            If numberAfterOperator = 0 And addPoint = False Then
                numberAfterOperator = inputNumber
                numberAfterOpetatorText = numberAfterOperator.ToString()
                Console.WriteLine("2-1")
            ElseIf addPoint = True And addPointnumAfter = False Then
                numberAfterOpetatorText = numberAfterOpetatorText & "." & inputNumber.ToString()
                addPointnumAfter = True
                addPoint = False
                If Not Double.TryParse(numberAfterOpetatorText, numberAfterOperator) Then
                    Throw New FormatException($"{numberAfterOpetatorText} を整数に変換できません")
                End If
                Console.WriteLine("2-2")
            Else
                numberAfterOpetatorText = numberAfterOpetatorText & inputNumber.ToString()
                If Not Double.TryParse(numberAfterOpetatorText, numberAfterOperator) Then
                    Throw New FormatException($"{numberAfterOpetatorText} を整数に変換できません")
                End If
                Console.WriteLine("2-3")
            End If

            Return numberAfterOpetatorText

        End If

    End Function

    Public Function Point() As String

        Dim currentNumber As String
        addPoint = True

        If Not operatorType = OperatorType.None Then
            currentNumber = numberAfterOperator.ToString() + "."
            Debug.WriteLine("numberAfterOperator = " + currentNumber)
        Else
            currentNumber = numberBeforeOperator.ToString() + "."
            Debug.WriteLine("numberBeforeOpetator = " + currentNumber)
        End If

        If addPointnumBefore = True And operatorType = OperatorType.None Then
            Return numberBeforeOperator
        End If

        If addPointnumAfter = True Then
            Return numberAfterOperator
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
        numberBeforeOpetatorText = ""
        numberAfterOpetatorText = ""
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
        numberBeforeOpetatorText = numberBeforeOperator.ToString()
        previousAction = ActionType.EqualAction
        addPoint = False
        addPointnumAfter = False
        addPointnumBefore = False

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
