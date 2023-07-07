using System;
using Godot;

namespace AlienAttack.Scripts;

public partial class Game : Node2D
{
    private int _lives = 3;
    private AlienAttack.Scripts.Hud _hud;
    public static event Action<int> PlayerDied;

    public override void _Ready()
    {
        _hud = (AlienAttack.Scripts.Hud) GetNode("UI/HUD");
        _hud.SetScoreLabel();
        Player.TookDamage += OnTookDamage;
        base._Ready();
    }

    public override void _ExitTree()
    {
        Player.TookDamage -= OnTookDamage;
        base._ExitTree();
    }

    private void OnDeathZoneEntered(Area2D area2D)
    {
        (area2D as Enemy)?.Die();
    }

    private void OnTookDamage(Player player)
    {
        _lives--;
        _hud.SetLivesLeft(_lives);
        if (_lives <= 0)
        {
            player.QueueFree();
            var packedScene = (PackedScene)GD.Load("res://Scenes/GameOver.tscn");
            var gameOverInstance = packedScene.Instantiate();
            var uiCanvas = GetNode("UI");
            uiCanvas.AddChild(gameOverInstance);
            var gameOver = (GameOver)gameOverInstance;
            gameOver.SetScore(_hud.CurrentScore);
        }
    }
}