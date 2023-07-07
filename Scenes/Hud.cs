using Godot;
using System;
using AlienAttack.Scripts;

public partial class Hud : Control
{
	private string _scoreText = "SCORE: ";
	private Label _scoreLabel;
	private int _currentScore;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_scoreLabel = (Label) GetNode("Score");
		Rocket.PlayerKilledEnemy += OnPlayerKilledEnemy;
	}

	public override void _ExitTree()
	{
		Rocket.PlayerKilledEnemy -= OnPlayerKilledEnemy;
		base._ExitTree();
	}

	private void OnPlayerKilledEnemy()
	{
		_currentScore++;
		SetScoreLabel(_currentScore);
	}

	public void SetScoreLabel(int newScore = 0)
	{
		_scoreLabel.Text = _scoreText + newScore;
	}

	public void SetLivesLeft(int lives)
	{
		for (int i = 0; i < 3; i++)
		{
			var hBoxContainer = (HBoxContainer) GetNode("HBoxContainer");
			var child = (Control) hBoxContainer.GetChild(i);
			if (i <= lives - 1)
			{
				child.Visible = true;	
			}
			else
			{
				child.Visible = false;
			}
		}
	}
}
