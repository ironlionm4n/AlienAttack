using Godot;

namespace AlienAttack.Scripts;

public partial class EnemySpawner : Node2D
{
    private PackedScene _enemy;
    private Node2D _spawnPositions;

    public override void _Ready()
    {
        _enemy = (PackedScene) GD.Load("res://Scenes/BasicEnemy.tscn");
        _spawnPositions = (Node2D) GetNode("SpawnPositions");
        base._Ready();
    }

	/// <summary>
	/// Spawn an enemy ship when timer reaches zero
	/// </summary>
	private void OnTimerTimeout() => SpawnEnemy();

	private void SpawnEnemy()
    {
        var spawnPositionsArray = _spawnPositions.GetChildren();
        var spawnPoint = (Node2D) spawnPositionsArray.PickRandom();
        
        var enemyInstance = (Area2D) _enemy.Instantiate();
        enemyInstance.GlobalPosition = spawnPoint.GlobalPosition;
        AddChild(enemyInstance);
    }
}