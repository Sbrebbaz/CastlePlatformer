using Godot;
using System;

public partial class Spike : StaticBody2D
{
	private void _on_area_2d_body_entered(Node2D body)
	{
		if (body is Killable)
		{
			((Killable)body).Kill();
		}
	}
}
