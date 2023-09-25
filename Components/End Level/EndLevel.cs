using Godot;
using System;
using System.Diagnostics;

public partial class EndLevel : AnimatedSprite2D
{
	public Node2D Player;

	public override void _Ready()
	{
		base._Ready();
		AnimationFinished += EndLevel_AnimationFinished;
	}

	private void EndLevel_AnimationFinished()
	{
		if (Player == null)
		{
			Play("Idle");
		}
	}

	[Signal]
	public delegate void LevelEndEventHandler();

	private void _on_sprite_2d_body_entered(Node2D body)
	{
		if (body is PlayableCharacter)
		{
			Player = (PlayableCharacter)body;
			Play("PlayerClose");
		}
	}

	private void _on_sprite_2d_body_exited(Node2D body)
	{
		if (body is PlayableCharacter)
		{
			Player = null;
			PlayBackwards("PlayerClose");
		}
	}

	public override void _Process(double delta)
	{
		if (Player != null && Input.IsActionJustPressed("ui_door"))
		{
			Debug.WriteLine("LEVEL END EVT");
			EmitSignal(SignalName.LevelEnd);
		}
		base._Process(delta);
	}

}
