using Godot;

namespace AlienAttack.Scripts;

public partial class GameOver : Control
{
    public override void _Ready()
    {
        Game.PlayerDied += SetScore;
        base._Ready();
    }

    public override void _ExitTree()
    {
        Game.PlayerDied -= SetScore;
        base._ExitTree();
    }

    public void SetScore(int newScore)
    {
        var scoreLabel = (Label)GetNode("Panel/Score");
        scoreLabel.Text = "SCORE: " + newScore;
    }

    private void OnRetryPressed() => GetTree().ReloadCurrentScene();
}