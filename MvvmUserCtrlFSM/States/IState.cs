namespace MvvmUserCtrlFSM.States;

public interface IState
{
    void EnterState();
    void ExitState();
    void ProcessInput(string input);
}
