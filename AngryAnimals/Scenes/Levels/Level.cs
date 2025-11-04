using Godot;
using System;

public partial class Level : Node2D
{
	[Export] private Marker2D _animalSpawn;
	[Export] private PackedScene _animalScene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
		SignalManager.Instance.OnAnimalDied += OnAnimalDied;
		OnAnimalDied();
    }

    private void OnAnimalDied()
    {
		Animal newAnimal = _animalScene.Instantiate<Animal>();
		newAnimal.Position = _animalSpawn.Position;
		AddChild(newAnimal);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
    {
		if (Input.IsKeyPressed(Key.Q))
        {
			GetTree().ChangeSceneToFile("res://UI/Main/Main.tscn");
        }
    }

    public override void _ExitTree()
    {
		SignalManager.Instance.OnAnimalDied -= OnAnimalDied;
    }

}
