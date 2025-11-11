using Godot;
using System;

public partial class GameUi : Control
{
	[Export] private Label _attemptsLabel;
	[Export] private Label _levelLabel;
	[Export] private VBoxContainer _vb2;
	[Export] private AudioStreamPlayer _gameoverMusic;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_levelLabel.Text = $"Level{ScoreManager.GetLevelSelected()}";

		SignalManager.Instance.OnLevelComplete += OnlevelComplete;
		SignalManager.Instance.OnAttemptUpdated += OnAttemptUpdated;
	}

    public override void _ExitTree()
	{
		SignalManager.Instance.OnLevelComplete -= OnlevelComplete;
		SignalManager.Instance.OnAttemptUpdated -= OnAttemptUpdated;
	}

    private void OnAttemptUpdated(int num)
    {
		_attemptsLabel.Text = $"Attempts: {num}";
    }

	private void OnlevelComplete()
	{
		_vb2.Show();
		_gameoverMusic.Play();
	}

    public override void _Process(double delta)
    {
		if (_vb2.Visible && Input.IsKeyPressed(Key.Space))
        {
            GameManager.LoadMain();
		}
    }

}