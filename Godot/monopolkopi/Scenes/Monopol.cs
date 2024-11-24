using Godot;
using System;

public partial class Monopol : Node3D
{

	private Node3D Player;
	private int CurrentId = 1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetNode("Player") as Node3D;
	}

	private bool IsPressing = false;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!IsPressing && Input.IsActionPressed("ui_select")) {
			IsPressing = true;
			MoveToNextSpace();
		}

		if (Input.IsActionJustReleased("ui_select"))
		{
			IsPressing = false;
		}
	}

	private void MoveToNextSpace(int toMove = 1)
	{
		var next = CurrentId + (int) new Random().NextInt64(1, 6); // overflow
		Space nextNode;
		if (next == 1) {
			nextNode = GetNode<Space>("Board/Space");
		} else {
			nextNode = GetNode<Space>($"Board/Space{next}");
		}
		CurrentId = next;

		Player.GlobalPosition = new Vector3(nextNode.GlobalPosition.X, Player.GlobalPosition.Y, nextNode.GlobalPosition.Z);
	}
}
