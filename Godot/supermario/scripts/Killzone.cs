using Godot;
using System;

public partial class Killzone : Area3D
{
	[Export]
	Timer Timer { get; set; }
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
		GD.Print("You died!");
		Timer.Start();
	}

	private void OnTimerTimeout()
	{
		GetTree().ReloadCurrentScene();
	}
}
