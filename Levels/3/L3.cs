using Godot;
using System;

public partial class L3 : Node2D
{
	private void _on_end_level_level_end()
	{
		GetTree().ChangeSceneToFile("res://Levels/4/L4.tscn");
	}
}
