using System.Linq;
using Godot.Collections;

namespace Game.Player;

public partial class PlayerMovementController : Node, IStateMachine
{
    [ExportGroup("States")]
    [Export]
    private State _initialState;

    [Export]
    private Array<State> _states = [];

    public State CurrentState { get; private set; }

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
        initialState?.Enter();
        CurrentState = (State)initialState;
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }

    public void Update(double delta)
    {
        CurrentState?.Process(delta);
    }

    public void PhysicsUpdate(double delta)
    {
        CurrentState?.PhysicsProcess(delta);
    }

    /// <summary>
    /// Forcibly change state without calling transition
    /// </summary>
    public void ChangeState(IState newState)
    {
        Logger.LogDebug("Changing state", newState.ToString());
        newState.Enter();
        CurrentState = (State)newState;
    }

    public void ChangeState<T>()
        where T : State
    {
        var newState = _states.OfType<T>().FirstOrDefault() ?? null;
        if (newState is null)
        {
            Logger.LogError("Changing state with state type failed. State not found");
            return;
        }
        ChangeState(newState);
    }
}
