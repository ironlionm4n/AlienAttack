using Godot;
using System;
using AlienAttack.Scripts;

public partial class Game : Node2D
{
    private int _lives = 3;
    private Hud _hud;
    public override void _Ready()
    {
        _hud = (Hud) GetNode("UI/HUD");
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
        }
    }
}