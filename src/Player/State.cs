namespace Game;

public partial class State : Node, IState
{
    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void Transition(IState previousState) { }

    public virtual void Process(double delta) { }

    public void PhysicsProcess(double delta) { }
}
