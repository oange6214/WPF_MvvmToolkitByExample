using System.Diagnostics;

namespace MvvmUserCtrlFSM.States;
public class OperatorState : StateBase
{
    public OperatorState(StateBase state)
    {
        PreValue = state.PreValue;
        NowValue = state.NowValue;
        Initialize();
    }

    private void Initialize()
    {
        PreValue = 0;
        NowValue = 0;
        _operator = string.Empty;
        result = 0;
        historyOutput = string.Empty;
    }

    public override void ProcessInput(string input)
    {
        if (IsNumeric(input))
        {
            Debug.WriteLine($"Received Num1: {input}");

            StateMachine.TransitionTo(new Num2State(this));
        }
        else if (IsOperator(input))
        {
            Debug.WriteLine($"Received Operator: {input}");

        }
        else if (IsEqual(input))
        {
            Debug.WriteLine($"Received Equals: {input}");

            StateMachine.TransitionTo(new EqualsState(this));
        }
        else
        {
            Debug.WriteLine("Invalid input.");
        }
    }
}
