using Godot;
using System;

public partial class L7 : Node2D
{
	private void _on_end_level_level_end()
	{
		GetTree().ChangeSceneToFile("res://Levels/8/L8.tscn");
	}
}
