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
    Private Enum DisplayOperand
        ''' <summary>
        ''' 被演算子Bufferを表示値にします。
        ''' </summary>
        OperandBuffer = 1
        ''' <summary>
        ''' 被演算子Currentを表示値にします。
        ''' </summary>
        OperandCurrent = 2
    End Enum

    Private Const DefaultNumber As String = "0"

<<<<<<< HEAD
    Private _OperandBufferText As String = String.Empty
    Private _OperandCurrentText As String = DefaultNumber
=======
    Private _OperandBufferText As String = DefaultNumber
    Private _OperandCurrentText As String = String.Empty
>>>>>>> c370003cb60423c9c0b5950d172fa816fffdf5ec
    Private _DisplayOperand = DisplayOperand.OperandCurrent
    Private _OperatorType As OperatorType = OperatorType.None        '演算子の種類を格納する
    Private _PreviousAction As ActionType = ActionType.ClearAction   '一つ前の動作を格納する

    ''' <summary>
    ''' 表示値を取得します。
    ''' </summary>
    ''' <returns>表示値の文字列</returns>
    Public ReadOnly Property DisplayNumberText
        Get
<<<<<<< HEAD
            Debug.WriteLine($"Buffer = {_OperandBufferText}")
            Debug.WriteLine($"Current = {_OperandCurrentText}")
            Debug.WriteLine($"Display = {_DisplayOperand}")
=======
>>>>>>> c370003cb60423c9c0b5950d172fa816fffdf5ec
            Return If(_DisplayOperand = DisplayOperand.OperandCurrent, _OperandCurrentText, _OperandBufferText)
        End Get
    End Property

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

        ' inputNumberをChar型に変換
        AddChar(inputNumber.ToString()(0))

        _PreviousAction = ActionType.NumberAction

    End Sub

    Private Sub ShiftOperand()
        _OperandBufferText = _OperandCurrentText
        _OperandCurrentText = DefaultNumber
        _DisplayOperand = DisplayOperand.OperandCurrent
    End Sub

    Private Sub AddChar(key As Char)

        If _OperandCurrentText = "0" Then
            If key = "0" Then
                ' 0のときに0は追加しない
                Return
            ElseIf key >= "1" AndAlso key <= "9" Then
                _OperandCurrentText = ""
            End If
        End If

        _OperandCurrentText &= key
        _DisplayOperand = DisplayOperand.OperandCurrent

    End Sub

    ''' <summary>
    ''' 小数点を付与する
    ''' </summary>
<<<<<<< HEAD
=======
    ''' <returns></returns>
>>>>>>> c370003cb60423c9c0b5950d172fa816fffdf5ec
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
    ''' 数・演算子（メンバ変数）をクリアする
    ''' </summary>
    Public Sub Clear()

<<<<<<< HEAD
        _OperandBufferText = String.Empty
        _OperandCurrentText = DefaultNumber
=======
        _OperandBufferText = DefaultNumber
        _OperandCurrentText = String.Empty
>>>>>>> c370003cb60423c9c0b5950d172fa816fffdf5ec
        _DisplayOperand = DisplayOperand.OperandCurrent
        _OperatorType = OperatorType.None
        _PreviousAction = ActionType.ClearAction

    End Sub

    ''' <summary>
    ''' TODO：numberAfterOperatorに数値が入っているときの動作が書けていない
    ''' numberAfterOperatorに数値が入っていれば、計算結果を返す
    ''' </summary>
    ''' <param name="inputOperatorType"></param>
    Public Sub OperatorAction(inputOperatorType As OperatorType)

<<<<<<< HEAD
        If _PreviousAction = ActionType.NumberAction Then
            If Not String.IsNullOrEmpty(_OperandBufferText) Then
                Calculate()
            End If
=======
        If Not String.IsNullOrEmpty(_OperandBufferText) Then
            Calculate()
>>>>>>> c370003cb60423c9c0b5950d172fa816fffdf5ec
        End If

        _PreviousAction = ActionType.OperatorAction
        _OperatorType = inputOperatorType

    End Sub

    ''' <summary>
    ''' 演算を実施後、変数・演算子の種類（メンバ変数）をリセット
    ''' </summary>
    Public Sub Equal()

        If Not String.IsNullOrEmpty(_OperandBufferText) Then
            Calculate()
        End If

        _PreviousAction = ActionType.EqualAction

    End Sub

    Private Sub Calculate()

        Dim op1 = ConvertToDecimal(_OperandBufferText)
        Dim op2 = ConvertToDecimal(_OperandCurrentText)

        Dim result = Calculate(_OperatorType, op1, op2)
<<<<<<< HEAD
        _OperandBufferText = CleanDisplayNumber(result.ToString())
=======
        _OperandBufferText = result.ToString()
>>>>>>> c370003cb60423c9c0b5950d172fa816fffdf5ec
        _DisplayOperand = DisplayOperand.OperandBuffer

    End Sub

<<<<<<< HEAD
    Private Shared Function CleanDisplayNumber(str As String) As String

        If str.Contains(".") Then
            ' 末尾の 0 を削除
            str = Regex.Replace(str, "0+$", "")
        End If
        ' 最後に小数点があれば削除
        str = Regex.Replace(str, "\.$", "")
        Return str

    End Function

=======
>>>>>>> c370003cb60423c9c0b5950d172fa816fffdf5ec
    Private Shared Function ConvertToDecimal(number As String)

        Dim value As Decimal
        Return If(Decimal.TryParse(number, value), value, 0)

    End Function

    ''' <summary>
    ''' numberBeforeOpetatorとnumberAfterOpetatorを演算子に応じて計算する。
    ''' </summary>
    ''' <returns></returns>
    Private Function Calculate(operatorType As OperatorType, op1 As Decimal, op2 As Decimal) As Decimal

        '演算子ボタンの種類に応じて計算する
        Select Case _OperatorType
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
