using CastlePlatformer;
using Godot;
using System;
using System.Diagnostics;

public partial class TestScene : Node2D
{
	public override void _Ready()
	{
		//Reset gravity
		BaseLevel.Gravity = BaseLevelConstants.DefaultGravity;
		
		//Reset coin counter
		BaseLevel.CoinCounter = 0;

		Debug.WriteLine("SCENE LOADED");
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
	}

}
