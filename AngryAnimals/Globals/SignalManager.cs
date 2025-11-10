using Godot;
using System;

public partial class SignalManager : Node
{
	public static SignalManager Instance { get; private set; }

	[Signal] public delegate void OnPlaneDiedEventHandler();
	[Signal] public delegate void OnScoredEventHandler();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}

	public static void EmitOnPlaneDied()
	{
		Instance.EmitSignal(SignalName.OnPlaneDied);
	}

    public static void EmitOnScored()
    {
		Instance.EmitSignal(SignalName.OnScored);
	}
}
