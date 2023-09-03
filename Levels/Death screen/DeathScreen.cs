using Godot;
using System;

public partial class DeathScreen : Button
{
	private void _on_pressed()
	{
		GetTree().ChangeSceneToFile("res://Main menu/MainMenu.tscn");
	}
}


