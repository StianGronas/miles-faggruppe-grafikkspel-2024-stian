using Godot;
using System;

public partial class Mario : CharacterBody3D
{
	[Export]
	Label3D Label { get; set; }
	int Score { get; set; } = 0;
	public const float Speed = 2.0f;
	public const float JumpVelocity = 3.5f;

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;

		var collision = MoveAndCollide(velocity * (float)delta);
		//if (collision != null)
		//{
		//	Velocity = Velocity.Slide(collision.GetNormal());
		//	var name = ((Node3D)collision.GetCollider()).Name;
		//	GD.Print("I collided with ", name);

		//	if (name.ToString().StartsWith("CoinBlock"))
		//	{
		//		Score += 1;
		//		Label.Text = Score.ToString();
		//	}
		//}

		MoveAndSlide();
	}
}
