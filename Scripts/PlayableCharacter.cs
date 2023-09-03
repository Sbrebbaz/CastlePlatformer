using Godot;
using System;
using System.Diagnostics;

public partial class PlayableCharacter : CharacterBody2D, Killable
{
	public float WalkSpeed = 50f;
	public float BaseSpeed = 200f;
	public float RunSpeed = 400f;
	public float PushForce = 250f;

	public float JumpVelocityBase = -280.0f;

	public float JumpVelocity
	{
		get
		{
			return JumpVelocityBase * BaseLevel.GravityModifier;

		}
	}

	public float EnemyBounceVelocity = -200.0f;

	private bool _IsDead = false;

	public bool IsDead
	{
		get
		{
			return _IsDead;
		}
		set
		{
			_IsDead = value;
			AnimatedSprite2D.Play("Death");
		}
	}

	public AnimatedSprite2D AnimatedSprite2D;

	public override void _Ready()
	{
		base._Ready();
		AnimatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!IsDead)
		{
			Vector2 velocity = Velocity;
			float movementSpeed = BaseSpeed;

			switch (BaseLevel.GravityModifier)
			{
				case > 0:
					{
						RotationDegrees = 0;
						//AnimatedSprite2D.FlipV = false;
						UpDirection = new Vector2(0, -1);
						break;
					}
				case < 0:
					{
						RotationDegrees = 180;
						//AnimatedSprite2D.FlipV = true;
						UpDirection = new Vector2(0, 1);
						break;
					}
			}

			if (Input.IsActionPressed("ui_run"))
			{
				movementSpeed = RunSpeed;
			}
			else if (Input.IsActionPressed("ui_walk"))
			{
				movementSpeed = WalkSpeed;
			}

			// Add the gravity.
			if (!IsOnFloor())
			{
				velocity.Y += BaseLevel.Gravity * (float)delta;
				if (velocity.Y > 0)
				{
					AnimatedSprite2D.Play("Fall");
				}
			}

			// Handle Jump.
			if (
				(Input.IsActionJustPressed("ui_accept") || Input.IsActionJustPressed("ui_up"))
				&& IsOnFloor()
				)
			{
				velocity.Y += JumpVelocity;
				AnimatedSprite2D.Play("Jump");
			}

			// Get the input direction and handle the movement/deceleration.
			// As good practice, you should replace UI actions with custom gameplay actions.
			Vector2 direction = Input.GetVector("ui_left",
												"ui_right",
												"ui_left",
												"ui_right");

			switch (direction.X)
			{
				case > 0:
					{
						AnimatedSprite2D.FlipH = (BaseLevel.GravityModifier < 0);
						break;
					}
				case < 0:
					{
						AnimatedSprite2D.FlipH = (BaseLevel.GravityModifier > 0);
						break;
					}
			}

			if (direction != Vector2.Zero)
			{
				velocity.X = direction.X * movementSpeed;
				if (velocity.Y == 0)
				{
					AnimatedSprite2D.Play("Run");
				}
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, movementSpeed);
				if (velocity.Y == 0)
				{
					AnimatedSprite2D.Play("Idle");
				}
			}

			Velocity = velocity;

			if (MoveAndSlide())
			{
				for (int i = 0; i < GetSlideCollisionCount(); i++)
				{
					KinematicCollision2D collision = GetSlideCollision(i);
					if (collision.GetCollider() is Pushable)
					{
						((Pushable)collision.GetCollider()).ApplyForce(collision.GetNormal() * -PushForce);

					}

				}
			}

		}
	}

	private void _on_animated_sprite_2d_animation_finished()
	{
		if (IsDead)
		{
			QueueFree();
		}
	}

	private void _on_tree_exiting()
	{
		if (IsDead)
		{
			GetTree().ChangeSceneToFile("res://DeathScreen.tscn");
		}
	}

	public void Kill()
	{
		IsDead = true;
	}
}
