using Godot;
using System;

public partial class GameArea : MeshInstance3D
{
	[Export]
	PackedScene BlockScene;

	int columns = 7;
	int rows = 5;
	float xSpacing = 1.1f;
	float zSpacing = 0.4f;
	Color initialColor = new Color(1, 1, 1);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Console.WriteLine("running!");
		var firstBlock = GetNode<StaticBody3D>("./Block");
		float xPos = firstBlock.Position.X;
		float zPos = firstBlock.Position.Z;

		float nextXPos = xPos + xSpacing;
		float nextZPos = zPos;
		float r = 150;
		float g = 10;
		float b = 10;

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < columns; j++)
			{
				if (i == 0 && j == 0) continue;
				var newBlock = BlockScene.Instantiate<StaticBody3D>();
				newBlock.Position = new Vector3(nextXPos, firstBlock.Position.Y, nextZPos);
				//newBlock.Translate(new Vector3(nextXPos, nextYPos, 0));
                //newBlock.GlobalPosition = new Vector3(nextXPos, nextYPos, 0);
                newBlock.Name = "Block_" + (i + 1) + "_" + (j + 1);

				//var mesh = newBlock.GetNode<MeshInstance3D>("MeshInstance3D");
				//var newMat = new StandardMaterial3D();
				//newMat.AlbedoColor = new Color(r, g, b);
				//mesh.MaterialOverride = newMat;

				this.AddChild(newBlock);
				Console.WriteLine("Added new block: " + newBlock.Name);
				Console.WriteLine(newBlock.Position);
				r += 10;
				nextXPos += xSpacing;
			}
			nextXPos = xPos;
			nextZPos += zSpacing;
			r = 150;
			g += 50;
			b += 50;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
