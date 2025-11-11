using Godot;
using System;
using System.Collections.Generic;

public partial class ScoreManager : Node
{
	const int DEFAULT_SCORE = 1000;
	const string SCORE_FILE = "user://animal.save";

	public static ScoreManager Instance { get; private set; }
	
	private int _levelSelected;
	List<LevelScore> _levelScores = new List<LevelScore>();

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

	public static  LevelScore GetLevelScore(int levelNumber)
	{
		return Instance._levelScores.FirstOrDefault(ls => ls.LevelNumber == levelNumber);
	}
	
	public static int GetLevelBestScore(int levelNumber)
    {
		LevelScore LevelScore = GetLevelScore(levelNumber);
		if(LevelScore != null)
		{
			return LevelScore.BestScore;
		}
		return DEFAULT_SCORE;
    }

	public static void SetScoreForLevel(int levelNumber, int score)
	{
		LevelScore LevelScore = GetLevelScore(levelNumber);
		if(LevelScore != null)
		{
			if(score < LevelScore.BestScore)
			{
				LevelScore.BestScore = score;
				LevelScore.DateSet = DateTime.Now;
			}
		}
		else
		{
			Instance._levelScores.Add(new LevelScore(levelNumber, score));
		}
	}
}
