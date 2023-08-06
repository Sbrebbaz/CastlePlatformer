using Godot;
using System;
using System.Diagnostics;

public partial class Consumable : Area2D
{
	private void _on_body_entered(Node2D body)
	{
		if (body.Name == "Player")
		{
			BaseLevel.CoinCounter += (int)GetMeta("Value");

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


