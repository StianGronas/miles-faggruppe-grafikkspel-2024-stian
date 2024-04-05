using Godot;
using System;

public partial class MainScene : Node3D
{
	public static int Counter { get; set; } = 0;
	
	static Label3D Label;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Label = GetNode<Label3D>("./ScoreLabel");
		Label.Text = "Score: " + Counter.ToString();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	
	}

	public static void IncreaseCounter()
	{
		Counter++;
		Label.Text = "Score: " + Counter.ToString();
	}
}
