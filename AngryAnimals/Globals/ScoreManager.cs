using Godot;
using System;

public partial class ScoreManager : Node
{
	const int DEFAULT_SCORE = 1000;

	public static ScoreManager Instance { get; private set; }

	private int _levelSelected;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}

	public static int GetLevelSelected()
	{
		return Instance._levelSelected;
	}

	public static int SetLevelSelected(int Level)
	{
		return Instance._levelSelected = Level;
	}
	
	public static int GetLevelBestScore()
    {
		return DEFAULT_SCORE;
    }
}
