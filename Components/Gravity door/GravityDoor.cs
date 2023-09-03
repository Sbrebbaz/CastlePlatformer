using Godot;
using System;

public partial class GravityDoor : AnimatedSprite2D
{
	GravityDoor firstDoor;
	GravityDoor secondDoor;
	PlayableCharacter character;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		firstDoor = GetParent().GetChild<GravityDoor>(0);
		secondDoor = GetParent().GetChild<GravityDoor>(1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (character != null && Input.IsActionJustPressed("ui_door"))
		{
			if (character.GlobalPosition.DistanceTo(firstDoor.GlobalPosition) < character.GlobalPosition.DistanceTo(secondDoor.GlobalPosition))
			{
				character.GlobalPosition = secondDoor.GlobalPosition;
			}
			else
			{
				character.GlobalPosition = firstDoor.GlobalPosition;
			}
			BaseLevel.SwapPlayerGravity();
		}
	}

	private void _on_area_2d_body_entered(Node2D body)
	{
		if (body is PlayableCharacter)
		{
			character = (PlayableCharacter)body;
			Play("Open");
		}
	}


	private void _on_area_2d_body_exited(Node2D body)
	{
		if (body is PlayableCharacter)
		{
			character = null;
			Play("Close");
		}
	}
}


