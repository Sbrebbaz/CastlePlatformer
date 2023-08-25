using Godot;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public partial class BreakableWall : Node2D, Breakable
{
	bool IsBroken = false;

	public void Break()
	{
		IsBroken = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (IsBroken)
		{
			Tween tween = CreateTween();
			tween.Finished += TweenPosition_Finished;

			tween.TweenProperty(this, "modulate:a", 0, 0.2f);

		}
	}

	private void TweenPosition_Finished()
	{
		QueueFree();
	}


}
