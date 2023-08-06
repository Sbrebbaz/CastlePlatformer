using Godot;
using System;

public partial class Consumable : Area2D
{
	private void _on_body_entered(Node2D body)
	{
		if (body.Name == "Player")
		{

			BaseLevel.CoinCounter += 1;

			Tween tweenPosition = GetTree().CreateTween();
			Tween tweenTransparency = GetTree().CreateTween();

			tweenPosition.TweenProperty(this, "position", Position - new Vector2(0, 25), 0.15f);
			tweenTransparency.TweenProperty(this, "modulate:a", 0, 0.15f);

			tweenPosition.Finished += TweenPosition_Finished;
		}
	}

	private void TweenPosition_Finished()
	{
		QueueFree();
	}
}


