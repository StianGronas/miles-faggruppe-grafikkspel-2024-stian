using Godot;
using System;

public partial class CoinBlock : Area3D
{
	private MeshInstance3D Mesh;
	private MeshInstance3D AltMesh;
	private bool Hit = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Mesh = GetNode<MeshInstance3D>("StaticBody3D/CoinBlock_Mesh");
		AltMesh = GetNode<MeshInstance3D>("StaticBody3D/CoinBlock_AltMesh");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnBodyEntered(Node3D body)
	{
		if (!Hit)
		{
			GD.Print("Block was hit, +1 coin");
			Hit = true;
			Mesh.Visible = false;
			AltMesh.Visible = true;
		}
	}
}
