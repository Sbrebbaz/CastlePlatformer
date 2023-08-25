using Godot;
using System;
using System.Diagnostics;

public partial class GravityPotion : Area2D
{
	private bool PickedUp = false;

	private void _on_body_entered(Node2D body)
	{
		if (body is PlayableCharacter && !PickedUp)
		{
			PickedUp = true;

			//((PlayableCharacter)body).GravitySwapped = !((PlayableCharacter)body).GravitySwapped;
			BaseLevel.Gravity *= -1;

			Tween tweenPosition = GetTree().CreateTween();
			Tween tweenTransparency = GetTree().CreateTween();

			tweenPosition.TweenProperty(this, "position", Position - new Vector2(0, 25), 0.2f);
			tweenTransparency.TweenProperty(this, "modulate:a", 0, 0.2f);

			tweenPosition.Finished += TweenPosition_Finished;
		}
	}

	private void TweenPosition_Finished()
	{
		QueueFree();
	}
}
