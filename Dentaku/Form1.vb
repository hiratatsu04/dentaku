Public Class Form1

    Dim calculatorObject As New Calculator

    ''' <summary>
    ''' キー：OperatorType
    ''' 値　：演算子の文字列
    ''' </summary>
    Dim operatorTexts As New Dictionary(Of OperatorType, String)() From
        {
        {OperatorType.Plus, "＋"},
        {OperatorType.Minus, "－"},
        {OperatorType.Times, "×"},
        {OperatorType.Divide, "÷"}
        }

    Private Sub NumberButtonClick(sender As Object, e As EventArgs) Handles btn0.Click, btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click

        '押されたボタンの判別。ボタンのNAMEから「btn」を除き、数値に変換後、numTempに代入
        Dim buttonTextOfNumber = CType(sender, Button)
        Dim buttonText As String = buttonTextOfNumber.Name.Remove(0, 3)

        Dim inputNumber As Integer

        ' inputNumberに押されたボタンの数字を格納
        If Not Integer.TryParse(buttonText, inputNumber) Then
            MessageBox.Show("ボタンフォームに数値以外の値が入っています")
            Return
        End If

        Try
            calculatorObject.Number(inputNumber)
            txtShowResult.Text = calculatorObject.DisplayNumberText
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub OperatorButtonClick(sender As Object, e As EventArgs) Handles btnTimes.Click, btnDivide.Click, btnPlus.Click, btnMinus.Click

        Dim buttonTextOfOperator = CType(sender, Button)
        Dim operatorType As OperatorType

        Select Case buttonTextOfOperator.Name
            Case "btnPlus"
                operatorType = OperatorType.Plus
            Case "btnMinus"
                operatorType = OperatorType.Minus
            Case "btnTimes"
                operatorType = OperatorType.Times
            Case "btnDivide"
                operatorType = OperatorType.Divide
        End Select

        calculatorObject.OperatorAction(operatorType)
        txtShowOperator.Text = operatorTexts(operatorType)
        txtShowResult.Text = calculatorObject.DisplayNumberText

    End Sub

    Private Sub EqualButtonClick(sender As Object, e As EventArgs) Handles btnEqual.Click

        calculatorObject.Equal()

        txtShowResult.Text = calculatorObject.DisplayNumberText
        txtShowOperator.Text = "＝"

    End Sub

    Private Sub ClearButtonClick(sender As Object, e As EventArgs) Handles btnClear.Click

        calculatorObject.Clear()

        txtShowOperator.Text = ""
        txtShowResult.Text = calculatorObject.DisplayNumberText

    End Sub

    Private Sub btnPeriod_Click(sender As Object, e As EventArgs) Handles btnPoint.Click

        calculatorObject.Point()

        txtShowResult.Text = calculatorObject.DisplayNumberText

    End Sub

    ''' <summary>
    ''' フォームKeyDownイベント
    ''' </summary>
    Private Sub frmFunc_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'キーコードにより処理する
        Select Case e.KeyCode
            Case Keys.D0
                Me.btn0.Focus()              'フォーカスセット
                Me.btn0.PerformClick()       'ボタン１クリック実行
            Case Keys.D1
                Me.btn1.Focus()              'フォーカスセット
                Me.btn1.PerformClick()       'ボタン１クリック実行
            Case Keys.D2
                Me.btn2.Focus()              'フォーカスセット
                Me.btn2.PerformClick()       'ボタン１クリック実行
            Case Keys.D3
                Me.btn3.Focus()              'フォーカスセット
                Me.btn3.PerformClick()       'ボタン１クリック実行
            Case Keys.D4
                Me.btn4.Focus()              'フォーカスセット
                Me.btn4.PerformClick()       'ボタン１クリック実行
            Case Keys.D5
                Me.btn5.Focus()              'フォーカスセット
                Me.btn5.PerformClick()       'ボタン１クリック実行
            Case Keys.D6
                Me.btn6.Focus()              'フォーカスセット
                Me.btn6.PerformClick()       'ボタン１クリック実行
            Case Keys.D7
                Me.btn7.Focus()              'フォーカスセット
                Me.btn7.PerformClick()       'ボタン１クリック実行
            Case Keys.D8
                Me.btn8.Focus()              'フォーカスセット
                Me.btn8.PerformClick()       'ボタン１クリック実行
            Case Keys.D9
                Me.btn9.Focus()              'フォーカスセット
                Me.btn9.PerformClick()       'ボタン１クリック実行
            Case Keys.OemPeriod
                Me.btnPoint.Focus()              'フォーカスセット
                Me.btnPoint.PerformClick()       'ボタン１クリック実行
            Case Keys.Return
                Me.btnEqual.Focus()              'フォーカスセット
                Me.btnEqual.PerformClick()       'ボタン１クリック実行
        End Select
        e.Handled = True                    '他にKeyDownイベントを発生させない

        Debug.WriteLine(e.KeyCode)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

End Class
