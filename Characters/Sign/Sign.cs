using Godot;
using System;

public partial class Sign : AnimatedSprite2D
{
	CanvasLayer TextLayer;
	Label SignLabel;

	public override void _Ready()
	{
		base._Ready();
		TextLayer = GetNode<CanvasLayer>("TextLayer");
		SignLabel = TextLayer.GetNode<ColorRect>("Sign").GetNode<Label>("SignLabel");

		SignLabel.Text = (string)GetMeta("Text");
	}


	private void _on_area_2d_body_entered(Node2D body)
	{
		if (body is PlayableCharacter)
		{
			TextLayer.Visible = true;
		}
	}


	private void _on_area_2d_body_exited(Node2D body)
	{
		if (body is PlayableCharacter)
		{
			TextLayer.Visible = false;
		}
	}
}


