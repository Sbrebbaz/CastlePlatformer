using Godot;
using System;

public partial class Trampoline : StaticBody2D
{

	public float JumpVelocity = -500.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_area_2d_body_entered(Node2D body)
	{
		if (body.Name == "Player")
		{
			PlayableCharacter player = (PlayableCharacter)body;

			Vector2 velocity = player.Velocity;
			velocity.Y += JumpVelocity;
			player.Velocity = velocity;
			player.MoveAndSlide();
		}
	}
}


