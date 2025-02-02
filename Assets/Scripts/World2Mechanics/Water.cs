﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// Water
/// 
/// Script attached to water that gives player infinite jumps while in it.
/// While player is in water collider, player will repeatedly call OnTriggerStay2D.
/// Through player collider, give player infinite jumps.
/// 
/// Public functions allow water to be raised or lowered.
///
/// Depends on player's "BallController" component.
/// 
/// </summary>

public class Water : MonoBehaviour {
    private RhythmController rhythmController;

    // Tag the player object will have
    static string PLAYER_TAG = "Player";

    // Rate at which water will drain
    public float drainRate = 0.1f;

    // How far water will drain
    public float targetHeight = 0.0f;

    // When true, begins to drain water
    public bool drainingWater = false;

    // When true, gives the player infinite jumps
    public bool resetsJumps = true;

    // Start position of the water
    private Vector2 startPosition;
    // End position of the water
    private Vector2 endPosition;

    // Used by lerp to determine how far water has moved
    private float lerpFraction = 0.0f;

	// Use this for initialization
	void Start () {
        // Get the start position of the water
        startPosition = this.transform.position;

        // Calculate the ending position using target height
        endPosition = startPosition;
        endPosition.y = targetHeight;
        rhythmController = RhythmController.GetController();
	}
	
	// Update is called once per frame
	void Update () {
        if (drainingWater)
        {
            drainWater();
        }
	}

    // Called every frame collider stays in the water
    void OnTriggerEnter2D(Collider2D other)
    {
        // If the collider is player's, then give player infinite jump
        if (other.tag == PLAYER_TAG)
        {
			BallController ball = other.gameObject.GetComponent<BallController> ();
            rhythmController.SwitchToChannel(2);
            if (resetsJumps)
            {
                // Land player to reset jumps
				if (!ball.inWater)
					ball.Land();
				//Fixed jump reset in water for one instance of object
				//Remove edge collider to fix case for all water prefabs
            }
			ball.inWater = true;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == PLAYER_TAG)
        {
			BallController ball = other.gameObject.GetComponent<BallController> ();
            rhythmController.SwitchToChannel(1);
			ball.inWater = false;

        }
    }

    // Starts the water draining process
    public void beginDrainingWater()
    {
        drainingWater = true;
    }

    // Drain water using Lerp
    private void drainWater()
    {
        if(lerpFraction < 1.0)
        {
            lerpFraction += Time.deltaTime * drainRate;
            this.transform.position = Vector2.Lerp(startPosition, endPosition, lerpFraction);
        }
        else
        {
            // Done draining
            drainingWater = false;
        }
    }
}
