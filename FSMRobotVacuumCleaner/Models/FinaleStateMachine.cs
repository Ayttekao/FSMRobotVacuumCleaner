namespace FSMRobotVacuumCleaner.Models;

public class FinaleStateMachine
{
    private Stack<Action> _stack;

    public FinaleStateMachine() => _stack = new Stack<Action>();

    public void Update() => GetCurrentState().Invoke();

    private Action GetCurrentState() => _stack.FirstOrDefault();

    public Action PopState() => _stack.Pop();

    public string GetStateName() => $"{GetCurrentState().Method.Name}";

    public void PushState(Action state)
    {
        if (GetCurrentState() != state)
            _stack.Push(state);
    }
}