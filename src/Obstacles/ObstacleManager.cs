using System.Collections.Generic;
using System.Linq;

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
    private Timer _spawnTimer = new();

    public override void _Ready()
    {
        PopulatePool();

        var time = GD.RandRange(_spawnTimeMin, _spawnTimeMax);
        _spawnTimer.Autostart = true;
        _spawnTimer.Start(time);
        _spawnTimer.Timeout += SpawnObstacle;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public void SpawnObstacle()
    {
        var obstacle = _obstacles[GD.RandRange(0, _obstacleTypes.Count - 1)];
        obstacle.Enter();
    }

    public void SpawnObstacle<T>()
        where T : IObstacle
    {
        var obstacle = _obstacles.OfType<T>().First();
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

                _obstacles.Add(obstacle);
                AddChild(obstacle);
            }
        }
    }
}
