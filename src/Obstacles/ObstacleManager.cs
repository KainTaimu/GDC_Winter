using System.Collections.Generic;

namespace Game.Obstacles;

public partial class ObstacleManager : Node
{
    [ExportCategory("Properties")]
    [Export]
    private bool _enabled = true;

    [ExportCategory("Obstacles")]
    [Export]
    private Godot.Collections.Dictionary<PackedScene, int> _obstacleTypes = []; // Value is amount of scene to preload

    [ExportCategory("Spawning")]
    [Export]
    private float _spawnTimeMin = 0;

    [Export]
    private float _spawnTimeMax = 1;

    [Export]
    private Node2D _groundSpawnPoint;

    [Export]
    private Node2D _roofSpawnPoint;

    private readonly List<Obstacle> _obstacles = [];
    private readonly Queue<Obstacle> _onObstacles = [];
    private readonly Queue<Obstacle> _offObstacles = [];

    private Timer _spawnTimer = new() { Autostart = true };

    public override void _Ready()
    {
        PopulatePool();

        var time = GD.RandRange(_spawnTimeMin, _spawnTimeMax);
        AddChild(_spawnTimer);
        _spawnTimer.Start(time);
        _spawnTimer.Timeout += SpawnObstacle;
    }

    public override void _Process(double delta)
    {
        if (Engine.GetProcessFrames() % 10 == 0)
        {
            Logger.LogDebug($"On", string.Join(", ", _onObstacles));
            Logger.LogDebug($"Off", string.Join(", ", _offObstacles));
        }
    }

    public void SpawnObstacle()
    {
        if (!_offObstacles.TryDequeue(out var obstacle))
            return;

        _onObstacles.Enqueue(obstacle);

        obstacle.ProcessMode = ProcessModeEnum.Inherit;
        obstacle.Enter();
    }

    private void PopulatePool()
    {
        foreach (var (scene, amount) in _obstacleTypes)
        {
            for (var i = 0; i < amount; i++)
            {
                var obstacle = scene.Instantiate<Obstacle>();
                switch (obstacle.Type)
                {
                    case ObstacleType.Ground:
                        obstacle.Position = _groundSpawnPoint.Position;
                        break;
                    case ObstacleType.Roof:
                        obstacle.Position = _roofSpawnPoint.Position;
                        break;
                }

                obstacle.OnExit += () =>
                {
                    // Can't disable without deferring
                    Callable
                        .From(() =>
                        {
                            obstacle.ProcessMode = ProcessModeEnum.Disabled;
                        })
                        .CallDeferred();
                    switch (obstacle.Type)
                    {
                        case ObstacleType.Ground:
                            obstacle.Position = _groundSpawnPoint.Position;
                            break;
                        case ObstacleType.Roof:
                            obstacle.Position = _roofSpawnPoint.Position;
                            break;
                    }
                    _onObstacles.Dequeue();
                    _offObstacles.Enqueue(obstacle);
                };

                obstacle.Name = obstacle.Name + " " + i;
                Callable
                    .From(() =>
                    {
                        obstacle.ProcessMode = ProcessModeEnum.Disabled;
                    })
                    .CallDeferred();

                _obstacles.Add(obstacle);
                _offObstacles.Enqueue(obstacle);
                AddChild(obstacle);
            }
        }
    }
}
