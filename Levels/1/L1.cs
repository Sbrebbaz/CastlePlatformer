using Godot;
using System;

public partial class L1 : Node2D
{
	private void _on_end_level_level_end()
	{
		GetTree().ChangeSceneToFile("res://Levels/2/L2.tscn");
	}
}
