Public Class Form1

    Dim Num1 As Integer = 0     '演算子入力前の数値
    Dim Num2 As Integer = 0     '演算子入力後の数値

    '演算子の定義（列挙型）
    Private Enum Ope
        Plus = 0
        Minus = 1
        Times = 2
        Divide = 3
        None = 4
    End Enum

    Dim OpeValue As Integer = Ope.None     '演算子を格納する変数。上記の列挙型演算子を代入する

    'ボタンタイプの定義（列挙型）
    Private Enum BtnType
        NumBtn = 10
        OpeBtn = 11
        EqualBtn = 12
        ClearBtn = 13
    End Enum

    Dim PreBtn As Integer = BtnType.ClearBtn    '一つ前に押されたボタンを格納する。上記の列挙型演算子を代入する

    '数ボタンが押された時の動作
    Private Sub NumButtonClick(sender As Object, e As EventArgs) Handles btn0.Click, btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click

        Dim btnNumber = CType(sender, Button)   'senderをボタン型に変更
        Dim btnText As String = ""      'ボタンの[NAME]を格納する変数
        Dim numTemp As Integer      '押されたボタンを一時的に格納するローカル変数

        '押されたボタンの判別。ボタンのNAMEから「btn」を除いて、数値に変換して、numTempに代入
        btnText = btnNumber.Name.Remove(0, 3)
        numTemp = Integer.Parse(btnText)

        'Num1に数字が入っているか判別。入っていなければ、numTempを入れてプロシージャを抜ける
        If Num1 = 0 Then
            Num1 = numTemp
            txtShowResult.Text = Num1.ToString()
            Return
        End If

        '演算子が格納されているか判別。入っていなければNum1にnumTempを加えてプロシージャを抜ける
        If OpeValue = Ope.None Then
            Dim num1Text = Num1.ToString() & numTemp.ToString()
            Integer.TryParse(num1Text, Num1)    '文字列に変換後に足した後に数値に戻しているがこの動作不要？
            txtShowResult.Text = Num1.ToString()
            Return
        End If

        'Num2に数字が格納されているか判別。数字が入っておらず、ひとつ前に押されたボタンが演算子であれば、Num2にnumTempを入れる。演算子ボタン以外であればNum1にnumTempを加える。
        If Num2 = 0 Then
            If PreBtn = BtnType.OpeBtn Then
                Num2 = numTemp
                txtShowResult.Text = Num2.ToString()
                Return
            Else
                Dim nNum1Text = Num1.ToString() & numTemp.ToString()
                Integer.TryParse(nNum1Text, Num1)    '文字列に変換後に足した後に数値に戻しているがこの動作不要？
                txtShowResult.Text = Num1.ToString()
                Return
            End If
        End If

        Dim num2Text = Num2.ToString() & numTemp.ToString()
        Integer.TryParse(num2Text, Num2)    '文字列に変換後に足した後に数値に戻しているがこの動作不要？
        txtShowResult.Text = Num2.ToString()
        PreBtn = BtnType.NumBtn     'ボタンタイプに数ボタンをセット

    End Sub

    '演算子ボタンが押された時の動作
    Private Sub OpeButton_Click(sender As Object, e As EventArgs) Handles btnTimes.Click, btnDivide.Click, btnPlus.Click, btnMinus.Click

        Dim btnOpe = CType(sender, Button)  'senderをボタン型に変更
        Dim opeTemp As Integer      '演算子ボタンの種類を格納する変数
        Dim opeText As String = ""      '演算子テキストボックスに表示する文字

        Select Case btnOpe.Name
            Case "btnPlus"
                opeTemp = Ope.Plus
                opeText = "＋"
            Case "btnMinus"
                opeTemp = Ope.Minus
                opeText = "－"
            Case "btnTimes"
                opeTemp = Ope.Times
                opeText = "×"
            Case "btnDivide"
                opeTemp = Ope.Divide
                opeText = "÷"
        End Select

        OpeValue = opeTemp
        txtShowOperator.Text = opeText
        PreBtn = BtnType.OpeBtn 'ボタンタイプに演算子ボタンをセット

    End Sub

    'イコールボタンの動作
    Private Sub BtnEqual_Click(sender As Object, e As EventArgs) Handles btnEqual.Click

        Dim calResult As Double = 0

        '演算子ボタンの種類に応じて計算する
        Select Case OpeValue
            Case Ope.Plus
                calResult = Num1 + Num2
            Case Ope.Minus
                calResult = Num1 - Num2
            Case Ope.Times
                calResult = Num1 * Num2
            Case Ope.Divide
                calResult = Num1 / Num2
        End Select

        txtShowResult.Text = calResult.ToString()

        '全ての変数、演算子タイプをリセット
        Num1 = 0
        Num2 = 0
        OpeValue = Ope.None
        PreBtn = BtnType.EqualBtn   'ボタンタイプにイコールボタンをセット

    End Sub

    'クリアボタンの動作。全て表示、変数をリセット
    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtShowOperator.Text = ""
        txtShowResult.Text = ""
        Num1 = 0
        Num2 = 0
        OpeValue = Ope.None
        PreBtn = BtnType.ClearBtn   'ボタンタイプにイコールボタンをセット
    End Sub

    Private Function Calculate() As Double

        Dim calResult As Double = 0

        '演算子ボタンの種類に応じて計算する
        Select Case OpeValue
            Case Ope.Plus
                calResult = Num1 + Num2
            Case Ope.Minus
                calResult = Num1 - Num2
            Case Ope.Times
                calResult = Num1 * Num2
            Case Ope.Divide
                calResult = Num1 / Num2
        End Select

        Return calResult

    End Function

End Class
