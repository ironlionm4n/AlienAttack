using Godot;

namespace AlienAttack.Scripts;

public partial class Player : Node
{
	private CharacterBody2D _playerCharacterBody2D;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_playerCharacterBody2D = GetNode<CharacterBody2D>("root/Player");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GD.Print(_playerCharacterBody2D.Name);
	}
}