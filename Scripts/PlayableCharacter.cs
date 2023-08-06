using Godot;
using System;

public partial class PlayableCharacter : CharacterBody2D
{
	public float WalkSpeed = 50f;
	public float BaseSpeed = 200f;
	public float RunSpeed = 400f;
	public float JumpVelocity = -400.0f;
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

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

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
				velocity.Y += gravity * (float)delta;
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
						AnimatedSprite2D.FlipH = false;
						break;
					}
				case < 0:
					{
						AnimatedSprite2D.FlipH = true;
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
			MoveAndSlide();
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
		GetTree().ChangeSceneToFile("res://DeathScreen.tscn");
	}
}



