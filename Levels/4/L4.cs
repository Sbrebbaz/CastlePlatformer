using Godot;
using System;

public partial class L4 : Node2D
{
	private void _on_end_level_level_end()
	{
		GetTree().ChangeSceneToFile("res://Levels/5/L5.tscn");
	}
}
