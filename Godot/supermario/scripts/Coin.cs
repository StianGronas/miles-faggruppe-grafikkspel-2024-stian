using Godot;
using System;

public partial class Coin : Area3D
{
	private const float Speed = 1f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.RotateY(Speed * (float)delta);
	}

	private void OnBodyEntered(Node3D body)
	{
		GD.Print("+1 coin");
		QueueFree();
	}
}
