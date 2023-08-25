using Godot;
using System;
using static CastlePlatformer.BaseLevelConstants;

public partial class BaseLevel : Node
{
	public static int CoinCounter { get; set; }
	public static int Gravity { get; set; } = DefaultGravity;
}
