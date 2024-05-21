using Godot;
using System;

public partial class Gate : Area3D
{
	[Export]
	Node3D MoveToPosition { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnBodyEntered(Node3D body)
	{
		GD.Print("Body entered!");
		GD.Print(body.Name);
		if (body is not null)
		{
			body.GlobalPosition = MoveToPosition.GlobalPosition;
		}
	}

}