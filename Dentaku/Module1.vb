Public Module Module1

    '演算子の定義（列挙型）
    Public Enum OperatorType
        Plus = 0
        Minus = 1
        Times = 2
        Divide = 3
        None = 4
    End Enum

    'Dim operatorValue As OperatorType = OperatorType.None     '演算子を格納する変数。上記の列挙型演算子を代入する

    Public operatorTexts As New Dictionary(Of OperatorType, String)() From
        {
        {OperatorType.Plus, "＋"},
        {OperatorType.Minus, "－"},
        {OperatorType.Times, "×"},
        {OperatorType.Divide, "÷"}
        }

    'ボタンタイプの定義（列挙型）
    Public Enum OperationType
        NumberButton = 10
        OperatorButton = 11
        EqualButton = 12
        ClearButton = 13
    End Enum

    'Dim previousButton As ButtonType = ButtonType.ClearButton    '一つ前に押されたボタンを格納する。上記の列挙型演算子を代入する

End Module
