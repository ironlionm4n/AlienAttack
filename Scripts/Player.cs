using System;
using Godot;

namespace AlienAttack.Scripts;

public partial class Player : 
	CharacterBody2D
{
	[Export(PropertyHint.TypeString, "How fast the player moves")]
	private float moveSpeed;
	private Vector2 _desiredVelocity;
	private float _shipOffset = 33f;
	private PackedScene _rocketScene;
	private Node _rocketContainer;
	public static event Action<Player> TookDamage;
	
	public override void _Ready()
	{
		base._Ready();
		_rocketScene = (PackedScene) GD.Load("res://Scenes/Rocket.tscn");
		_rocketContainer = GetNode("RocketContainer");
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if (Input.IsActionJustPressed("Shoot"))
		{
			ShootRocket();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		_desiredVelocity = Vector2.Zero;
		
		InputCheck();

		Velocity = _desiredVelocity;
		MoveAndSlide();
		ConfinePlayerToScreen();
	}

	private void InputCheck()
	{
		if (Input.IsActionPressed("MoveRight"))
		{
			_desiredVelocity.X = moveSpeed;
		}

		if (Input.IsActionPressed("MoveLeft"))
		{
			_desiredVelocity.X = -moveSpeed;
		}

		if (Input.IsActionPressed("MoveUp"))
		{
			_desiredVelocity.Y = -moveSpeed;
		}

		if (Input.IsActionPressed("MoveDown"))
		{
			_desiredVelocity.Y = moveSpeed;
		}
		
		// Clamping velocity to avoid diagonal movement being faster than moving in a single direction
		_desiredVelocity.X = Mathf.Clamp(_desiredVelocity.X, -moveSpeed, moveSpeed);
		_desiredVelocity.Y = Mathf.Clamp(_desiredVelocity.Y, -moveSpeed, moveSpeed);
	}

	private void ConfinePlayerToScreen()
	{
		var viewportSize = GetViewportRect().Size;
		GlobalPosition = GlobalPosition.Clamp(Vector2.Zero, viewportSize);
	}
	
	private void ShootRocket()
	{
		var rocketInstance = (Area2D) _rocketScene.Instantiate();
		_rocketContainer.AddChild(rocketInstance);
		rocketInstance.GlobalPosition = GlobalPosition;
		rocketInstance.GlobalPosition =
			new Vector2(rocketInstance.GlobalPosition.X + 80f, rocketInstance.GlobalPosition.Y);
	}

	public void PlayerTakeDamage()
	{
		TookDamage?.Invoke(this);
	}
	
}