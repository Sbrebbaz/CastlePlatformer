using Godot;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public partial class VanishWall : Node2D
{

	private void _on_area_2d_body_entered(Node2D body)
	{
		if (body is PlayableCharacter)
		{
			Tween tween = CreateTween();
			tween.Finished += TweenPosition_Finished;

			tween.TweenProperty(this, "modulate:a", 0, 1f);
		}
	}

	private void TweenPosition_Finished()
	{
		QueueFree();
	}


}
