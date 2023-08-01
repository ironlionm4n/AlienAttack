using System;
using Godot;

namespace AlienAttack.Scripts;

public partial class Rocket : Area2D
{
    [Export] private float rocketSpeed;

    private VisibleOnScreenNotifier2D _notifier;
    public static event Action PlayerKilledEnemy;
    public override void _Ready()
    {
        _notifier = GetNode<VisibleOnScreenNotifier2D>("VisibleNotifier");
        _notifier.Connect("screen_exited", new Callable(this, nameof(OnScreenExited)));
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += new Vector2(rocketSpeed * (float)delta, 0);
        base._PhysicsProcess(delta);
    }

	private void OnScreenExited() => QueueFree();

	private void OnAreaEntered(Area2D area2D)
    {
        var enemy = (Enemy)area2D;
        if (enemy != null)
        {
            PlayerKilledEnemy?.Invoke();
            enemy.Die();
        }
        QueueFree();
    }
}