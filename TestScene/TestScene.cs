using CastlePlatformer;
using Godot;
using System;

public partial class TestScene : Node2D
{
	public override void _Ready()
	{
		//Reset gravity
		BaseLevel.Gravity = BaseLevelConstants.DefaultGravity;
		
		//Reset coin counter
		BaseLevel.CoinCounter = 0;
	}

}
