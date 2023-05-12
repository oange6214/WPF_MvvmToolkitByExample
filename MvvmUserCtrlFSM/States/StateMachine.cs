namespace MvvmUserCtrlFSM.States;

public class StateMachine
{
    private static StateBase _currentState;

    public StateMachine()
    {
        _currentState = new StartState();
    }

    public void ProcessInput(string input)
    {
        _currentState.ProcessInput(input);
    }

    public static void TransitionTo(StateBase newState)
    {
        _currentState.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }
}