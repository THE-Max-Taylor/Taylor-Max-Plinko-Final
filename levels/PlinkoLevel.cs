using Godot;
using System;
using System.ComponentModel;

public partial class PlinkoLevel : Node2D
{
	// this is the value of the current round's score
	public int Score = 7000;

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

	[Export]
	public Label WinTime;

    //adding random difficulties
    //int[] plinkoDifficulto = [10000, 7000, 3000];

    // internal variables
    bool EnableRespawn = false;
	Vector2 InitialPosition;

	//checks to see if game is wom
	bool isWin = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		RespawnMessage.Hide();
		WinTime.Hide();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// write the score to the UI
		ScoreValue.Text = $"Ooohgg... careful.. you only have {Score} points left..";

		WinTime.Visible = isWin;

       

        if (Score > 0 && Score < 70001)
		{
            RespawnMessage.Visible = EnableRespawn;
            // spawn a new player
            if (Input.IsActionJustPressed("drop_disk") && EnableRespawn)
			{
				// spawning a new player here is OK because physics step hasn't run
				SpawnNewPlayer();

				EnableRespawn = false;
			}
		}

		if (Score <= 0)
		{
            ScoreValue.Text = "Sorry, chief. It's over. You're done. We're all done. It's over. It's over.";
            if (Input.IsActionPressed("drop_disk"))
            {
                ScoreValue.Text = "Didn't you hear? We're done. It's all over.";
            }

        }

		if (Score > 70001)
		{
            ScoreValue.Text = "Sorr- Yo, what? Wait what? YOU WIN!! NICE. NICE.";

			isWin = true;
            SpawnNewPlayer();
           

            if (Input.IsActionPressed("drop_disk"))
            {
                ScoreValue.Text = "TOO MANY YOUS!! IT'S GONNA BLOW!!";
		
            }
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
