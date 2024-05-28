using Godot;
using System;

public partial class L2 : Node2D
{
	private void _on_end_level_level_end()
	{
		GetTree().ChangeSceneToFile("res://Levels/3/L3.tscn");
	}
}
