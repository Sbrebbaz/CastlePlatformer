using Godot;
using System;

public partial class L5 : Node2D
{
	private void _on_end_level_level_end()
	{
		GetTree().ChangeSceneToFile("res://Levels/6/L6.tscn");
	}
}
