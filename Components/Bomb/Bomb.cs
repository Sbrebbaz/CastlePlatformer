using Godot;
using System;
using System.Diagnostics;
using System.Linq;

public partial class Bomb : CharacterBody2D
{
	private AnimatedSprite2D BombAnimation;
	private AnimatedSprite2D ExplosionAnimation;
	private Area2D ExplosionArea;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BombAnimation = GetNode<AnimatedSprite2D>("BombSprite");
		ExplosionAnimation = GetNode<AnimatedSprite2D>("ExplosionSprite");
		ExplosionArea = GetNode<Area2D>("ExplosionArea");
	}

	private void _on_bomb_sprite_animation_finished()
	{
		BombAnimation.Visible = false;
		ExplosionAnimation.Visible = true;
		ExplosionAnimation.Play();

		foreach (Node2D node2D in ExplosionArea.GetOverlappingBodies().ToList())
		{
			Debug.WriteLine(node2D);
			if (node2D is Killable)
			{
				((Killable)node2D).Kill();
			}
			else if (node2D is Breakable)
			{
				((Breakable)node2D).Break();
			}
			else
			{
				//Do nothing
			}
		}
	}

	private void _on_explosion_sprite_animation_finished()
	{
		ExplosionAnimation.Visible = false;
		QueueFree();
	}
}
