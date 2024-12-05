using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Monopol : Node3D
{


	[Export]
	public Timer DiceTimer;
	[Export]
	public float DiceSpeed = 10f;
	[Export]
	public float MaxDiceSpeed = 50f;
	[Export]
	public float MinDiceSpeed = 10f;

	private bool IsPressing = false;
	private Vector3 DiceAxisAngle = (Vector3.Left + (Vector3.Forward * 0.5f)).Normalized();
	private Node3D Player;
	private int CurrentId = 1;
	private List<Space> Spaces;
	private int MaxId;
	private Node3D Dice;
	private bool DiceIsSpinning = true;
	private bool CannotRoll = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetNode("Player") as Node3D;
		this.Spaces = GetTree().GetNodesInGroup("Spaces")
			.Select((a) => a is Space ? a as Space : null)
			.Where((a) => a != null)
			.ToList();
		this.MaxId = this.Spaces.Last().Id;
		this.Dice = GetNode("Dice") as Node3D;
	}

	public void StartDiceSpinning()
	{
		CannotRoll = false;
		DiceIsSpinning = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!CannotRoll)
		{
			if (!IsPressing && Input.IsActionPressed("ui_select"))
			{
				IsPressing = true;
				MoveToNextSpace();
			}
		}

		if (Input.IsActionPressed("ui_up"))
		{
			DiceSpeed = Mathf.Min(MaxDiceSpeed, DiceSpeed + (10f * (float)delta));
		}

		if (Input.IsActionPressed("ui_down"))
		{
			DiceSpeed = Mathf.Max(MinDiceSpeed, DiceSpeed - (10f * (float)delta));
		}

		if (Input.IsActionJustReleased("ui_select"))
		{
			IsPressing = false;
		}

		if (DiceIsSpinning)
		{
			DiceAxisAngle = (DiceAxisAngle + new Vector3(0.2f, 0.2f, 0.2f) * (float)delta).Normalized();
			Dice.Rotate(DiceAxisAngle, DiceSpeed * (float)delta);
		}
	}

	private void MoveToNextSpace()
	{
		DiceIsSpinning = false;
		var diceRoll = (int)new Random().NextInt64(1, 6);
		var next = (CurrentId + diceRoll) % (MaxId + 1); // overflow
																										 // fix dice orientation after rolling
		switch (diceRoll)
		{
			case 1:
				Dice.RotationDegrees = new Vector3(0, 0, 0);
				break;
			case 2:
				Dice.RotationDegrees = new Vector3(0, 0, 90);
				break;
			case 3:
				Dice.RotationDegrees = new Vector3(90, 0, 0);
				break;
			case 4:
				Dice.RotationDegrees = new Vector3(270, 0, 0);
				break;
			case 5:
				Dice.RotationDegrees = new Vector3(0, 0, 270);
				break;
			case 6:
				Dice.RotationDegrees = new Vector3(180, 0, 0);
				break;
		}
		// rotation 0,0,0 = 1
		// rotation 90,0,0 = 3
		// rotation 180,0,0 = 6
		// rotation 270,0,0 = 4
		// rotation 0,0,90 = 2
		// rotation 0,0,270 = 5
		// start moving one by one (timer call to next?)

		Space nextNode;
		// Fix %
		if (next < CurrentId) next++;

		var nextSpace = next == 1 ? "" : next.ToString();
		nextNode = GetNode<Space>($"Board/Space{nextSpace}");
		CurrentId = next;

		Player.GlobalPosition = new Vector3(nextNode.GlobalPosition.X, Player.GlobalPosition.Y, nextNode.GlobalPosition.Z);

		this.CannotRoll = true;
		// start rolling after a delay
		DiceTimer.Start();
	}
}
