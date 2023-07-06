using Godot;
using System;
using AlienAttack.Scripts;

public partial class Game : Node2D
{
    private void OnDeathZoneEntered(Area2D area2D)
    {
        (area2D as Enemy)?.Die();
    }
}
