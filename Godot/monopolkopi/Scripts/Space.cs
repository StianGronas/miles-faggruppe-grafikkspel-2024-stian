using Godot;
using System;

[Tool]
public partial class Space : Node3D
{
	[Export]
	public int Id;
	[Export]
	public SpaceType SpaceType;
	[Export]
	public string Header;
	[Export]
	public Color SpaceColor;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var text = this.GetNode<MeshInstance3D>("Text");
		var textMesh = text.Mesh as TextMesh;
		textMesh.Text = Header ?? "Text - Please Set!";
		
		var spaceModel = this.GetNode<MeshInstance3D>("SpaceModel");
		var spaceMesh = spaceModel.Mesh as BoxMesh;
		var spaceMat = spaceMesh.Material as StandardMaterial3D;
		spaceMat.AlbedoColor = SpaceColor;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
