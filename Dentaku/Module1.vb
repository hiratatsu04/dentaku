Public Module Module1

    '演算子の定義（列挙型）
    Public Enum OperatorType
        Plus = 0
        Minus = 1
        Times = 2
        Divide = 3
        None = 4
    End Enum

    'ボタンタイプの定義（列挙型）
    Public Enum OperationType
        NumberButton = 10
        OperatorButton = 11
        EqualButton = 12
        ClearButton = 13
    End Enum

End Module
