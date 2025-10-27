using Godot;
using System;
using System.Collections;

public partial class Animal : RigidBody2D
{
	public enum AnimalState { READY, DRAG, RELEASE }

	private static readonly Vector2 DRAG_LIM_MAX = new Vector2(0, 60);
	private static readonly Vector2 DRAG_LIM_MIN = new Vector2(-60, 0);
	[Export] private Sprite2D _arrow;
	[Export] private Label _debugLabel;
	[Export] private AudioStreamPlayer2D _stretchSound;
	[Export] private AudioStreamPlayer2D _launchSound;
	[Export] private AudioStreamPlayer2D _kickSound;
	[Export] private VisibleOnScreenNotifier2D _visibleOnScreenNotifier;

	private AnimalState _state = AnimalState.READY;
	private float _arrowScaleX = 0.0f;
	private Vector2 _start = Vector2.Zero;
	private Vector2 _dragStart = Vector2.Zero;
	private Vector2 _draggedVector = Vector2.Zero;
	private Vector2 _lastDraggedVector = Vector2.Zero;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ConnectSignals();
		InitializeVariables();
	}
	private void InitializeVariables()
	{
		_start = Position;
		_arrowScaleX = _arrow.Scale.X;
		_arrow.Hide();
	}

	private void ConnectSignals()
	{
		_visibleOnScreenNotifier.ScreenExited += OnScreenExited;
		SleepingStateChanged += ONSleepingStateChanged;
		InputEvent += OnInputEvent;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		UpdateState();
		UpdateDebugLabel();
	}
	private void UpdateDebugLabel()
	{
		_debugLabel.Text = $"ST: {_state} SL: {Sleeping}\n";
		_debugLabel.Text += $"_dragStart: {_dragStart.X:F1}, {_dragStart.Y:F1}\n";
		_debugLabel.Text += $"_draggedVector: {_draggedVector.X:F1}, {_draggedVector.Y:F1}";
	}
	private void StartDragging()
	{
		_dragStart = GetGlobalMousePosition();
		_arrow.Show();
	}

	private void StartRelease()
	{

	}

	private void ConstrainDragWithinLimits()
	{
		_lastDraggedVector = _draggedVector;
		_draggedVector = _draggedVector.Clamp(DRAG_LIM_MIN, DRAG_LIM_MAX);
		Position = _start + _draggedVector;
	}
	
	private void PlayStrechSound()
	{
		Vector2 diff = _draggedVector - _lastDraggedVector;
		if(diff.Length() > 0 && !_stretchSound.Playing)
        {
			_stretchSound.Play();
		}
		
	}
	private void UpdateDraggedVector()
	{
		_draggedVector = GetGlobalMousePosition() - _dragStart;
	
	}
	
	private void HandleDragging()
    {
		UpdateDraggedVector();
		PlayStrechSound();
		ConstrainDragWithinLimits();
    }

	
	private void UpdateState()
    {
		switch (_state)
		{
			case AnimalState.DRAG:
				HandleDragging();
				break;
			case AnimalState.RELEASE:
				break;
		}
	}

	private void ChangeState(AnimalState newState)
	{
		_state = newState;
		switch(_state)
        {
            case AnimalState.DRAG:
				StartDragging();
				break;
			case AnimalState.RELEASE:
				StartRelease();
				break;
        }
    }

	private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
	{
		if (_state == AnimalState.READY && @event.IsActionPressed("drag"))
		{
			GD.Print("Dragged");
			ChangeState(AnimalState.DRAG);
		}
		//GD.Print(@event);
	}
	
    private void ONSleepingStateChanged()
    {    
    }
	private void OnScreenExited()
	{
		GD.Print("Screen Exited");
	}
}
