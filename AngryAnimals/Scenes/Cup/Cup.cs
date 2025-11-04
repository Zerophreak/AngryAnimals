using Godot;
using System;

public partial class Cup : StaticBody2D
{
	[Export] private AnimationPlayer _animationPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
		_animationPlayer.AnimationFinished += OnAnimationFinished;
    }

    private void OnAnimationFinished(StringName animName)
    {
		SignalManager.EmitOnCupDestroyed();
		QueueFree();
    }

    public void DIe()
	{
		_animationPlayer.Play("Vanish");
	}

}
