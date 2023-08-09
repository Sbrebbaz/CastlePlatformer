using Godot;
using System;
using System.Diagnostics;

public partial class BombSpawner : Node
{
	private PackedScene bomb = GD.Load<PackedScene>("res://Characters/Bomb/Bomb.tscn");

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_bomb"))
		{
			if (GetChildCount() == 0)
			{
				Bomb bombToSpawn = bomb.Instantiate<Bomb>();

				CharacterBody2D player = GetTree().Root.GetNode(GetParent().GetPath()).GetNode<CharacterBody2D>("Player");

				if (player != null)
				{
					bombToSpawn.Position = player.Position;
				}

				AddChild(bombToSpawn);
			}
		}
	}
}
