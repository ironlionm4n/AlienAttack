using Godot;

namespace AlienAttack.Scripts;

public partial class Enemy : Area2D
{
	[Export] private float moveSpeed;


	public override void _PhysicsProcess(double delta)
	{
		GlobalPosition += new Vector2(-moveSpeed * (float)delta, 0f);
	}

	private void OnBodyEntered(CharacterBody2D characterBody2D)
	{
		(characterBody2D as Player)?.PlayerTakeDamage();
		QueueFree();
	}

	public void Die() => QueueFree();
}