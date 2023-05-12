namespace CommanLib;

public enum Operator
{
    None,
    Add,
    Substract,
    Multiply,
    Divide,
    Equal
}

public enum State
{
    Start,
    First,
    Operator,
    Second,
    End
}