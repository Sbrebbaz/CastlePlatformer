using Godot;
using System;

public partial class L6 : Node2D
{
	private void _on_end_level_level_end()
	{
		GetTree().ChangeSceneToFile("res://Levels/7/L7.tscn");
	}
}
