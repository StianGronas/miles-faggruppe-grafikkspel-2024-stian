using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Monopol : Node3D
{

	private Node3D Player;
	private int CurrentId = 1;
	private List<Space> Spaces;
	private int MaxId;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetNode("Player") as Node3D;
		this.Spaces = GetTree().GetNodesInGroup("Spaces")
			.Select((a) => a is Space ? a as Space : null)
			.Where((a) => a != null)
			.ToList();
		this.MaxId = this.Spaces.Last().Id;
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
		var next = (CurrentId + (int) new Random().NextInt64(1, 6)) % (MaxId + 1); // overflow

		Space nextNode;
		// Fix %
		if (next < CurrentId) next++;

		var nextSpace = next == 1 ? "" : next.ToString();
		nextNode = GetNode<Space>($"Board/Space{nextSpace}");
		CurrentId = next;

		Player.GlobalPosition = new Vector3(nextNode.GlobalPosition.X, Player.GlobalPosition.Y, nextNode.GlobalPosition.Z);
	}
}
