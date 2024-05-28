using CastlePlatformer;
using Godot;
using System;
using static CastlePlatformer.BaseLevelConstants;

public partial class BaseLevel : Node
{
	public static int CoinCounter { get; set; }
	public static int Gravity { get; set; } = DefaultGravity;

	public static void SwapPlayerGravity()
	{
		Gravity *= -1;
	}

	public static int GravityModifier
	{
		get
		{
			return (Gravity > 0) ? 1 : -1;
		}
	}

	public static void InitializeBaseLevelValues()
    {
        //Reset gravity
        Gravity = DefaultGravity;

        //Reset coin counter
        CoinCounter = 0;
    }

}
