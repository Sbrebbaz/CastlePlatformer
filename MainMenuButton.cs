using Godot;
using System;

public partial class MainMenuButton : Button
{
	private void _on_pressed()
	{
		GetTree().ChangeSceneToFile("res://MainMenu/MainMenu.tscn");
	}
}


