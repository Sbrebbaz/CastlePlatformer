using Godot;
using System;

public partial class DeathPlane : Area2D
{
	private void _on_body_entered(Node2D body)
	{
		if (body is PlayableCharacter)
		{
			((PlayableCharacter)body).IsDead = true;
			body.QueueFree();
		}
	}
}


