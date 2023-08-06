using Godot;
using System;
using System.Diagnostics;

public partial class Enemy : CharacterBody2D
{
	public bool IsDead = false;
	public bool IsAttacking = false;
	public const float Speed = 80f;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public AnimatedSprite2D AnimatedSprite2D;
	public PlayableCharacter Player;
	public Vector2 StartingPosition;

	public override void _Ready()
	{
		base._Ready();
		AnimatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		AnimatedSprite2D.Play("Idle");
		StartingPosition = new Vector2(Position.X, Position.Y);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!IsDead)
		{
			Vector2 velocity = Velocity;
			// Add the gravity.
			if (!IsOnFloor())
			{
				velocity.Y += gravity * (float)delta;
			}

			Vector2 direction = new Vector2();

			if (Player != null)
			{
				if (IsAttacking)
				{
					AnimatedSprite2D.Play("Attack");
				}
				else
				{
					AnimatedSprite2D.Play("Walk");
					direction = (Player.Position - this.Position).Normalized();
				}
			}
			else if (Math.Abs((StartingPosition - this.Position).Normalized().X) > 0.1f)
			{
				AnimatedSprite2D.Play("Walk");
				direction = (StartingPosition - this.Position).Normalized();
			}
			else
			{
				AnimatedSprite2D.Play("Idle");
				direction = Vector2.Zero;
			}

			switch (direction.X)
			{
				case > 0:
					{
						AnimatedSprite2D.FlipH = false;
						break;
					}
				case < 0:
					{
						AnimatedSprite2D.FlipH = true;
						break;
					}
			}

			velocity.X = direction.X * Speed;

			Velocity = velocity;
			MoveAndSlide();
		}
		else
		{
			AnimatedSprite2D.Play("Death");
		}
	}

	private void _on_player_detection_body_entered(Node2D body)
	{
		if (body.Name == "Player")
		{
			Player = (PlayableCharacter)body;
		}
	}

	private void _on_player_detection_body_exited(Node2D body)
	{
		if (body.Name == "Player")
		{
			Player = null;
		}
	}


	private void _on_kill_detection_body_entered(Node2D body)
	{
		if (body.Name == "Player" && !IsDead)
		{
			IsAttacking = false;
			IsDead = true;
			GetNode<CollisionShape2D>("Hitbox").SetDeferred("disabled", true);


			Vector2 velocity = Velocity;
			velocity.Y += Player.EnemyBounceVelocity;
			Player.Velocity = velocity;
		}
	}

	private void _on_animated_sprite_2d_animation_finished()
	{
		if (IsDead)
		{
			QueueFree();
		}
	}

	private void _on_attack_detection_body_entered(Node2D body)
	{
		if (body.Name == "Player")
		{
			IsAttacking = true;
		}
	}


	private void _on_attack_detection_body_exited(Node2D body)
	{
		if (body.Name == "Player")
		{
			IsAttacking = false;
		}
	}

	private void _on_animated_sprite_2d_frame_changed()
	{
		if (IsAttacking
			&& !IsDead
			&& Player != null
			&& AnimatedSprite2D.Animation == "Attack"
			&& AnimatedSprite2D.Frame == 3)
		{
			Player.SetDeferred("IsDead", true);
		}
	}

}
