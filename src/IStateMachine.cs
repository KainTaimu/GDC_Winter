public interface IStateMachine
{
    void Start(IState initialState);
    void Stop();
    void Update(double delta);
    void PhysicsUpdate(double delta);
    void ChangeState(IState newState);
    void TransitionState(IState newState);
}
