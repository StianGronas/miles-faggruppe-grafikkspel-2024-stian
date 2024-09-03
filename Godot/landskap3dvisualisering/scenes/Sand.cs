using Godot;
using System;

public partial class Sand : Node3D
{
	[Export]
	float speed = 30.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var path3d = GetNode<Path3D>("Path3D");
		var pathFollow3d = path3d.GetNode<PathFollow3D>("PathFollow3D");

		pathFollow3d.Progress += (float)(delta * speed);
	}
}
