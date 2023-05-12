using System;
using System.Diagnostics;

namespace MvvmUserCtrlFSM.States;

public abstract class StateBase : IState
{
    #region Fileds & Properties

    private decimal _preValue;
    private decimal _nowValue;
    protected string _operator;
    protected decimal result;
    protected string historyOutput;

    public decimal PreValue
    {
        get { return _preValue; }
        set { _preValue = value; }
    }
    public decimal NowValue
    {
        get { return _nowValue; }
        set { _nowValue = value; }
    }

    #endregion

    
    public abstract void ProcessInput(string input);


    #region Protected Method

    public void EnterState()
    {
        Debug.WriteLine("Enter Start State");
    }

    public void ExitState()
    {
        Console.WriteLine("Exit Start State");
    }

    protected bool IsNumeric(string input)
    {
        return int.TryParse(input, out _);
    }

    protected bool IsOperator(string input)
    {
        return input == "+" || input == "-" || input == "*" || input == "/";
    }

    protected bool IsEqual(string input)
    {
        return input == "=";
    }

    #endregion

}
