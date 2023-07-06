using Godot;

namespace AlienAttack.Scripts;

public partial class Rocket : Area2D
{
    [Export] private float rocketSpeed;

    private VisibleOnScreenNotifier2D _notifier;

    public override void _Ready()
    {
        base._Ready();
        _notifier = GetNode<VisibleOnScreenNotifier2D>("VisibleNotifier");
        _notifier.Connect("screen_exited", new Callable(this, nameof(OnScreenExited)));
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        GlobalPosition += new Vector2(rocketSpeed * (float)delta, 0);
    }

    private void OnScreenExited()
    {
        QueueFree();
    }

    private void OnAreaEntered(Area2D area2D)
    {
        QueueFree();
        (area2D as Enemy)?.Die();
    }
}