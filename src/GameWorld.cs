namespace Game;

public partial class GameWorld : Node
{
    [Signal]
    public delegate void OnLevelChangeEventHandler(Level newLevel);

    [Signal]
    public delegate void OnPlayerDeathEventHandler();

    public static GameWorld Instance;

    public GameWorld()
    {
        if (Instance is not null)
        {
            Logger.LogError("Cannot have multiple instances of a singleton!");
            QueueFree();
            return;
        }

        Instance = this;
        ProcessMode = ProcessModeEnum.Always;
    }

    private Level _currentLevel;

    public Level CurrentLevel
    {
        get => _currentLevel;
        set
        {
            _currentLevel = value;
            EmitSignal(SignalName.OnLevelChange, value);
        }
    }
}
