using Godot;
using System;
using System.ComponentModel;

public partial class PlinkoLevel : Node2D
{
	// this is the value of the current round's score
	public int Score = 0;

	[Export]
	public Label ScoreValue;

	[Export]
	public Label RespawnMessage;

	[Export]
	public PackedScene PlayerScene;

	[Export]
	public Node2D PlayerDisks;

	[Export]
	public Camera2D GameCamera;

	[Export]
	public Player FirstPlayer;

    //adding random difficulties
    int[] plinkoDifficulto = [10000, 7000, 3000];

    // internal variables
    bool EnableRespawn = false;
	Vector2 InitialPosition;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{		
		Score = plinkoDifficulto[2];

		RespawnMessage.Hide();

		InitialPosition = FirstPlayer.Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// write the score to the UI
		ScoreValue.Text = $"Score: {Score}";
		RespawnMessage.Visible = EnableRespawn;

		// spawn a new player
        if (Input.IsActionJustPressed("drop_disk") && EnableRespawn)
        {
			// spawning a new player here is OK because physics step hasn't run
			SpawnNewPlayer();

			EnableRespawn = false;
        }

		//changes the difficulty depending on player input. this isn't an innuendo. leave me alone.
        if (Input.IsActionJustPressed("make_Hard"))
		{
            Score = plinkoDifficulto[1];
        }
    }

	void SpawnNewPlayer()
    {
		Player newPlayer = PlayerScene.Instantiate<Player>();
		newPlayer.Position = InitialPosition;

		// attach the player to the camera
		newPlayer.GameCamera = GameCamera;

        PlayerDisks.AddChild(newPlayer);
    }

	// remember: IncreaseScore() only gets called when the player body enters the "Bucket"
	public void IncreaseScore(int scoreIncrease)
	{
		// you can only increase the score if Enable Respawn is false
		if (!EnableRespawn)
        {
            Score += scoreIncrease;
            EnableRespawn = true;
        }

		// as exercise done in class, if we try to spawn the new player here
		// it creates problems. we MUST use CallDeferred()
		//SpawnNewPlayer();
		//CallDeferred("SpawnNewPlayer");
	}
}
