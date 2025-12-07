using Godot;
using System;

public partial class Player : RigidBody2D
{
	[Export]
	public Camera2D GameCamera;
	
	public bool Dead = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Freeze = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// if the disk is frozen, it's at the top and we can move it around
		if (Freeze)
		{
			// copy the current position into a new position
			Vector2 newPosition = Position;

			// set the X position to be the mouse position
			newPosition.X = GetViewport().GetMousePosition().X;

			// finally, set the position as the new position
			Position = newPosition;

			// drop_disk action is bound to the Left Click
			if (Input.IsActionJustPressed("drop_disk"))
			{
				Freeze = false;
			}
		}

		// snap camera Y position to my Y position ONLY if I'm not dead
		if (!Dead)
        {
            Vector2 newCamPosition = GameCamera.Position;

            // set the cam's y position to be equal to this player's y position
            newCamPosition.Y = Position.Y;

            GameCamera.Position = newCamPosition;
        }
	}
}
