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

    private Timer _spawnTimer = new() { Autostart = true };

    public override void _Ready()
    {
        // Disable on player death
        GameWorld.Instance.OnPlayerDeath += () =>
        {
            _enabled = false;
            _spawnTimer.Stop();
        };

        PopulatePool();

        var time = GD.RandRange(_spawnTimeMin, _spawnTimeMax);
        AddChild(_spawnTimer);
        _spawnTimer.Start(time);
        _spawnTimer.Timeout += SpawnObstacle;
    }

    public override void _Process(double delta)
    {
        // if (Engine.GetProcessFrames() % 10 == 0)
        // {
        //     Logger.LogDebug($"On", string.Join(", ", _onObstacles));
        //     Logger.LogDebug($"Off", string.Join(", ", _obstacles));
        //     Logger.LogDebug(_spawnTimer.WaitTime);
        // }
    }

    // BUG: Performance issues. Shuffle is O(n) and RemoveAt is O(n) at worse case.
    public void SpawnObstacle()
    {
        if (!_enabled)
            return;
        if (_obstacles.Count == 0)
            return;

        var idx = GD.RandRange(0, _obstacles.Count - 1);
        var obstacle = _obstacles[idx];
        _obstacles.RemoveAt(GD.RandRange(0, _obstacles.Count - 1));
        _obstacles.Shuffle();

        _onObstacles.Enqueue(obstacle);

        obstacle.ProcessMode = ProcessModeEnum.Inherit;
        obstacle.Enter();

        _spawnTimer.WaitTime = GD.RandRange(_spawnTimeMin, _spawnTimeMax);
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
                    case ObstacleType.Floating:
                        obstacle.Position = new Vector2(
                            _groundSpawnPoint.Position.X,
                            GetViewport().GetVisibleRect().Size.Y / 2
                        );
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
                        case ObstacleType.Floating:
                            obstacle.Position = new Vector2(
                                _groundSpawnPoint.Position.X,
                                GetViewport().GetVisibleRect().Size.Y / 2
                            );
                            break;
                    }
                    _onObstacles.Dequeue();
                    _obstacles.Add(obstacle);
                };

                obstacle.Name = obstacle.Name + " " + i;
                Callable
                    .From(() =>
                    {
                        obstacle.ProcessMode = ProcessModeEnum.Disabled;
                    })
                    .CallDeferred();

                _obstacles.Add(obstacle);
                AddChild(obstacle);
            }
        }
    }
}
