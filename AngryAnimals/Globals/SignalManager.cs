using Godot;
using System;

public partial class SignalManager : Node
{
	[Signal] public delegate void OnAnimalDiedEventHandler();

	public static SignalManager Instance { get; private set; }


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}

	public static void EmitOnAnimalDied()
    {
		Instance.EmitSignal(SignalName.OnAnimalDied);
    }


}
