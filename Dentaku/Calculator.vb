﻿Public Class Calculator

    Private numberBeforeOperator As Double = 0                      '演算子入力前の数値
    Private numberAfterOperator As Double = 0                       '演算子入力後の数値
    Private numberBeforeOperatorText As String = "0"                '演算子入力前の数値のテキスト。1.02などの小数点以下に0が入ったときの処理用
    Private numberAfterOperatorText As String = "0"                 '演算子入力後の数値のテキスト。同上
    Private operatorType As OperatorType = OperatorType.None        '演算子の種類を格納する
    Private previousAction As ActionType = ActionType.ClearAction   '一つ前の動作を格納する
    Private addPoint As Boolean = False                             '小数点の要否判断に用いる
    Private addPointnumBefore As Boolean = False                    '演算子入力前の数値への小数点挿入判定
    Private addPointnumAfter As Boolean = False                     '演算子入力後の数値への小数点挿入判定

    ''' <summary>
    ''' inputNumberを受けて表示される数を更新する
    ''' </summary>
    ''' <param name="inputNumber">ユーザーからの数入力</param>
    ''' <returns>画面に表示される数字(double型)</returns>
    Public Function Number(inputNumber As Integer) As String

        If previousAction = ActionType.EqualAction Then
            Clear()
        End If

        ' 演算子が格納されていなければ、演算子入力前の数値(numberBeforeOperator)を操作する
        If operatorType = OperatorType.None Then

            ' 数値が0で小数点の操作もなければ、入力の数値を代入する
            If numberBeforeOperator = 0 And addPoint = False Then
                numberBeforeOperator = inputNumber
                numberBeforeOperatorText = numberBeforeOperator.ToString()

                ' 小数点のフラグがtrueであれば小数点を追加する
            ElseIf addPoint = True And addPointnumBefore = False Then
                numberBeforeOperatorText = numberBeforeOperatorText & "." & inputNumber.ToString()
                addPointnumBefore = True
                addPoint = False
                If Not Double.TryParse(numberBeforeOperatorText, numberBeforeOperator) Then
                    Throw New FormatException($"{numberBeforeOperatorText} を整数に変換できません")
                End If

                ' 上記2つ以外であれば、元の数値に入力された数値を付け加える
            Else
                numberBeforeOperatorText = numberBeforeOperatorText & inputNumber.ToString()
                If Not Double.TryParse(numberBeforeOperatorText, numberBeforeOperator) Then
                    Throw New FormatException($"{numberBeforeOperatorText} を整数に変換できません")
                End If
            End If

            Return numberBeforeOperatorText

            ' 演算子が格納されていれば、演算子入力後の数値(numberAfterOperator)を操作する
        Else

            ' 処理内容は上と同じ
            If numberAfterOperator = 0 And addPoint = False Then
                numberAfterOperator = inputNumber
                numberAfterOperatorText = numberAfterOperator.ToString()
            ElseIf addPoint = True And addPointnumAfter = False Then
                numberAfterOperatorText = numberAfterOperatorText & "." & inputNumber.ToString()
                addPointnumAfter = True
                addPoint = False
                If Not Double.TryParse(numberAfterOperatorText, numberAfterOperator) Then
                    Throw New FormatException($"{numberAfterOperatorText} を整数に変換できません")
                End If
            Else
                numberAfterOperatorText = numberAfterOperatorText & inputNumber.ToString()
                If Not Double.TryParse(numberAfterOperatorText, numberAfterOperator) Then
                    Throw New FormatException($"{numberAfterOperatorText} を整数に変換できません")
                End If
            End If

            Return numberAfterOperatorText

        End If

    End Function

    ''' <summary>
    ''' 小数点を付与する
    ''' </summary>
    ''' <returns></returns>
    Public Function Point() As String

        If previousAction = ActionType.EqualAction Then
            Clear()
        End If

        Dim currentNumber As String
        addPoint = True

        If Not operatorType = OperatorType.None Then
            currentNumber = numberAfterOperator.ToString() + "."
        Else
            currentNumber = numberBeforeOperator.ToString() + "."
        End If

        If addPointnumBefore = True And operatorType = OperatorType.None Then
            Return numberBeforeOperatorText
        End If

        If addPointnumAfter = True Then
            Return numberAfterOperatorText
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
        numberBeforeOperatorText = "0"
        numberAfterOperatorText = "0"
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
            numberBeforeOperatorText = numberBeforeOperator.ToString()
            numberAfterOperator = 0
            numberAfterOperatorText = numberAfterOperator.ToString()
        End If

        previousAction = ActionType.OperatorAction
        operatorType = inputOperatorType
        addPoint = False

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
        numberBeforeOperatorText = numberBeforeOperator.ToString()
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

        addPoint = False
        addPointnumAfter = False
        addPointnumBefore = False

        Return calculateResult

    End Function

End Class
