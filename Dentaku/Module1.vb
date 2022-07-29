Public Module Module1

    ''' <summary>
    ''' <para>演算子の種類の列挙型</para>
    ''' <para>・Plus</para>
    ''' <para>・Minus</para>
    ''' <para>・Times</para>
    ''' <para>・Divide</para>
    ''' <para>・None：演算子なし</para>
    ''' </summary>
    Public Enum OperatorType
        ''' <summary>
        ''' ＋
        ''' </summary>
        Plus
        ''' <summary>
        ''' ー
        ''' </summary>
        Minus
        ''' <summary>
        ''' ×
        ''' </summary>
        Times
        ''' <summary>
        ''' ÷
        ''' </summary>
        Divide
        None
    End Enum

    ''' <summary>
    ''' <para>計算機動作の列挙型</para>
    ''' <para>・NumberAction：数ボタン押下時の動作</para>
    ''' <para>・OperatorAction：演算子ボタン押下時の動作</para>
    ''' <para>・EqualAction：イコールボタン押下時の動作</para>
    ''' <para>・ClearAciton：クリアボタン押下時の動作</para>
    ''' </summary>
    Public Enum ActionType
        NumberAction
        OperatorAction
        EqualAction
        ClearAction
    End Enum

End Module
