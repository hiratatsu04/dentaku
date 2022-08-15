Imports System.Text.RegularExpressions

Public Class Calculator

    ''' <summary>
    ''' 計算機動作の列挙型
    ''' </summary>
    Public Enum ActionType
        ''' <summary>
        ''' 数ボタン押下時の動作
        ''' </summary>
        NumberAction
        ''' <summary>
        ''' 演算子ボタン押下時の動作
        ''' </summary>
        OperatorAction
        ''' <summary>
        ''' イコールボタン押下時の動作
        ''' </summary>
        EqualAction
        ''' <summary>
        ''' クリアボタン押下時の動作
        ''' </summary>
        ClearAction
    End Enum

    ''' <summary>
    ''' 表示値として表示する値を表します。
    ''' </summary>
    Public Enum DisplayOperand
        ''' <summary>
        ''' 被演算子 1 を表示値にします。
        ''' </summary>
        Operand1 = 1
        ''' <summary>
        ''' 被演算子 2 を表示値にします。
        ''' </summary>
        Operand2 = 2
    End Enum

    Private Const DefaultNumber = "0"

    Private _Operand2Text As String = DefaultNumber
    Private _Operand1Text = String.Empty
    Private _DisplayOperand = DisplayOperand.Operand2
    Private _OperatorType As OperatorType = OperatorType.None        '演算子の種類を格納する
    Private _PreviousAction As ActionType = ActionType.ClearAction   '一つ前の動作を格納する

    ''' <summary>
    ''' 表示値を取得します。
    ''' </summary>
    ''' <returns>表示値の文字列</returns>
    Public ReadOnly Property DisplayNumberText
        Get
            Return If(_DisplayOperand = DisplayOperand.Operand1, _Operand1Text, _Operand2Text)
        End Get
    End Property

    ''' <summary>
    ''' 入力された文字を被演算子 2 と表示値に追加します。
    ''' </summary>
    ''' <param name="key"></param>
    Private Sub AddChar(key As Char)

        If _Operand2Text = "0" Then
            If key = "0"c Then
                ' 0 のときに 0 は追加しない
                Return
            ElseIf key >= "1"c AndAlso key <= "9"c Then
                _Operand2Text = ""
            End If
        End If
        _Operand2Text &= key
        _DisplayOperand = DisplayOperand.Operand2

    End Sub

    ''' <summary>
    ''' 表示値を被演算子 1 に移し、被演算子 2 と表示値をクリアします。
    ''' </summary>
    Private Sub ShiftOperand()
        _Operand1Text = DisplayNumberText
        _Operand2Text = DefaultNumber
        _DisplayOperand = DisplayOperand.Operand2
    End Sub

    ''' <summary>
    ''' 数字入力の処理を実行します。
    ''' </summary>
    ''' <param name="inputNumber">ユーザーからの数入力</param>
    Public Sub Number(inputNumber As Integer)

        If inputNumber < 0 OrElse inputNumber > 9 Then
            Throw New ArgumentOutOfRangeException(NameOf(inputNumber))
        End If

        If _PreviousAction = ActionType.EqualAction Then
            Clear()
        ElseIf _PreviousAction = ActionType.OperatorAction Then
            ShiftOperand()
        End If

        AddChar($"{inputNumber}"(0))

        _PreviousAction = ActionType.NumberAction

    End Sub

    ''' <summary>
    ''' 小数点の処理を実行します。
    ''' </summary>
    Public Sub Point()

        If _PreviousAction = ActionType.EqualAction Then
            Clear()
        ElseIf _PreviousAction = ActionType.OperatorAction Then
            ShiftOperand()
        End If

        AddChar("."c)

        _PreviousAction = ActionType.NumberAction

    End Sub

    ''' <summary>
    ''' 状態をクリアします。
    ''' </summary>
    Public Sub Clear()

        _Operand2Text = DefaultNumber
        _Operand1Text = String.Empty
        _DisplayOperand = DisplayOperand.Operand2
        _OperatorType = OperatorType.None
        _PreviousAction = ActionType.ClearAction

    End Sub

    ''' <summary>
    ''' 演算子の処理を実行します。
    ''' </summary>
    ''' <param name="inputOperatorType">演算子</param>
    Public Sub OperatorAction(inputOperatorType As OperatorType)

        If _PreviousAction = ActionType.NumberAction Then
            If Not String.IsNullOrEmpty(_Operand1Text) Then
                ' 既にバッファーに値があれば先に計算
                Calculate()
            End If
        End If

        _OperatorType = inputOperatorType
        _PreviousAction = ActionType.OperatorAction

    End Sub

    ''' <summary>
    ''' イコールの処理を実行します。
    ''' </summary>
    Public Sub Equal()

        If _PreviousAction = ActionType.NumberAction OrElse _PreviousAction = ActionType.EqualAction Then
            If Not String.IsNullOrEmpty(_Operand1Text) Then
                ' 既にバッファーに値があれば先に計算
                Calculate()
            End If
        End If

        _PreviousAction = ActionType.EqualAction

    End Sub

    ''' <summary>
    ''' 計算を実行します。
    ''' </summary>
    Private Sub Calculate()

        Dim op1 = ConvertToDecimal(_Operand1Text)
        Dim op2 = ConvertToDecimal(_Operand2Text)

        Dim result = Calculate(_OperatorType, op1, op2)
        _Operand1Text = CleanDisplayNumber(result.ToString())
        _DisplayOperand = DisplayOperand.Operand1

    End Sub

    ''' <summary>
    ''' 表示用文字列から不要な文字を取り除きます。
    ''' </summary>
    ''' <param name="str">表示文字列</param>
    ''' <returns>不要な文字を取り除いた表示文字列</returns>
    Private Shared Function CleanDisplayNumber(str As String) As String

        If str.Contains(".") Then
            ' 末尾の 0 を削除
            str = Regex.Replace(str, "0+$", "")
        End If
        ' 最後に小数点があれば削除
        str = Regex.Replace(str, "\.$", "")
        Return str

    End Function

    ''' <summary>
    ''' 数値文字列を Decimal 型に変換します。
    ''' </summary>
    ''' <param name="number"></param>
    ''' <returns></returns>
    Private Shared Function ConvertToDecimal(number As String)

        Dim value As Decimal
        Return If(Decimal.TryParse(number, value), value, 0)

    End Function

    ''' <summary>
    ''' op1 と op2 を被演算子として、演算子に応じた計算結果を取得します。
    ''' </summary>
    ''' <param name="operatorType">演算子</param>
    ''' <param name="op1">被演算子 1</param>
    ''' <param name="op2">被演算子 2</param>
    ''' <returns>計算結果</returns>
    Private Shared Function Calculate(operatorType As OperatorType, op1 As Decimal, op2 As Decimal) As Decimal
        '演算子ボタンの種類に応じて計算する
        Select Case operatorType
            Case OperatorType.Plus
                Return op1 + op2
            Case OperatorType.Minus
                Return op1 - op2
            Case OperatorType.Times
                Return op1 * op2
            Case OperatorType.Divide
                Return op1 / op2
        End Select
        Return 0
    End Function

End Class
