using Godot.Collections;

namespace Game.Player;

public partial class PlayerMovementController : Node, IStateMachine
{
    [ExportGroup("States")]
    [Export]
    private State _initialState;

    [Export]
    private Array<State> _states = [];

    private State _currentState;

    [ExportGroup("Components")]
    [Export]
    private Node2D _owner;

    [Export]
    private AnimatedSprite2D _sprite;

    [Export]
    private StatController _statController;

    public override void _Ready()
    {
        _sprite.Play();
        Start(_initialState);
    }

    public override void _Process(double delta)
    {
        Update(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        PhysicsUpdate(delta);
    }

    public void Start(IState initialState)
    {
        initialState.Enter();
        _currentState = (State)initialState;
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }

    public void Update(double delta)
    {
        _currentState?.Process(delta);
    }

    public void PhysicsUpdate(double delta)
    {
        _currentState?.PhysicsProcess(delta);
    }

    public void ChangeState(IState newState)
    {
        throw new NotImplementedException();
    }

    public void TransitionState(IState newState)
    {
        throw new NotImplementedException();
    }
}
