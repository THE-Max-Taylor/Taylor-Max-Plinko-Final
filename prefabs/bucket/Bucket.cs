using Godot;
using System;

public partial class Bucket : Area2D
{
	// these exports should be set on the LEVEL
	[Export]
	public int BucketScore = 0;

	[Export]
	public PlinkoLevel PlinkoLevelNode;


	private Label scoreLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// this is accessing by unique name, it's a very robust method for UI design
		scoreLabel = GetNode<Label>("%ScoreLabel");

		scoreLabel.Text = $"{BucketScore}";

        BodyEntered += Bucket_BodyEntered;
	}

    private void Bucket_BodyEntered(Node2D body)
    {
		// we ONLY want to increase the score when a player disk falls in the bucket
		if (body.IsInGroup("player"))
        {
			PlinkoLevelNode.IncreaseScore(BucketScore);

			Player playerDisk = (Player)body;
			playerDisk.Dead = true;
        }
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
