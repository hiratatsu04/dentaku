Public Class Calculator

    Private numberBuffer As String = ""
    Private numberCurrent As String = ""
    Private operatorType As OperatorType = OperatorType.None        '演算子の種類を格納する
    Private previousAction As ActionType = ActionType.ClearAction   '一つ前の動作を格納する

    ''' <summary>
    ''' inputNumberを受けて表示される数を更新する
    ''' </summary>
    ''' <param name="inputNumber">ユーザーからの数入力</param>
    ''' <returns>画面に表示される数字(string型)</returns>
    Public Function Number(inputNumber As Integer) As String

        If previousAction = ActionType.EqualAction Then
            Clear()
        End If

        numberCurrent &= inputNumber

        Return numberCurrent

    End Function

    ''' <summary>
    ''' 小数点を付与する
    ''' </summary>
    ''' <returns></returns>
    Public Function Point() As String

        If Not numberCurrent.Contains(".") = True Then
            numberCurrent &= "."
        End If

        Return numberCurrent

    End Function

    ''' <summary>
    ''' 数・演算子（メンバ変数）をクリアする
    ''' </summary>
    Public Sub Clear()

        ' 数・演算子・直前の動作をリセット
        numberBuffer = ""
        numberCurrent = ""
        operatorType = OperatorType.None
        previousAction = ActionType.ClearAction

    End Sub

    ''' <summary>
    ''' TODO：numberAfterOperatorに数値が入っているときの動作が書けていない
    ''' numberAfterOperatorに数値が入っていれば、計算結果を返す
    ''' </summary>
    ''' <param name="inputOperatorType"></param>
    Public Function OperatorAction(inputOperatorType As OperatorType) As String

        If Not numberBuffer = "" Then
            numberCurrent = Calculate()
            numberBuffer = ""
        End If

        numberBuffer = numberCurrent
        numberCurrent = ""

        previousAction = ActionType.OperatorAction
        operatorType = inputOperatorType

        Return numberBuffer

    End Function

    ''' <summary>
    ''' 演算を実施後、変数・演算子の種類（メンバ変数）をリセット
    ''' </summary>
    ''' <returns></returns>
    Public Function Equal() As String

        numberCurrent = Calculate()

        numberBuffer = ""
        previousAction = ActionType.EqualAction

        Return numberCurrent

    End Function

    ''' <summary>
    ''' numberBeforeOpetatorとnumberAfterOpetatorを演算子に応じて計算する。
    ''' </summary>
    ''' <returns></returns>
    Private Function Calculate() As String

        Dim calculateResult As Double = 0
        Dim num1, num2 As Double

        If Not Double.TryParse(numberBuffer, num1) Then
            Throw New FormatException($"{numberBuffer} を整数に変換できません")
        End If
        If Not Double.TryParse(numberCurrent, num2) Then
            Throw New FormatException($"{numberCurrent} を整数に変換できません")
        End If

        '演算子ボタンの種類に応じて計算する
        Select Case operatorType
            Case OperatorType.Plus
                calculateResult = num1 + num2
            Case OperatorType.Minus
                calculateResult = num1 - num2
            Case OperatorType.Times
                calculateResult = num1 * num2
            Case OperatorType.Divide
                calculateResult = num1 / num2
        End Select

        Return calculateResult.ToString()

    End Function

End Class
