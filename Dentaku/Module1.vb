Public Module Module1

    '演算子の定義（列挙型）
    Public Enum OperatorType
        Plus
        Minus
        Times
        Divide
        None
    End Enum

    '動作の定義（列挙型）
    Public Enum ActionType
        NumberAction
        OperatorAction
        EqualAction
        ClearAction
    End Enum

End Module
