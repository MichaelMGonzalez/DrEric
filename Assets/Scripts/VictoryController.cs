﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * Filename: VictoryController.cs \n
 * Author: Michael Gonzalez \n
 * Contributing Authors: N/A \n
 * Date Drafted: 8/2/2015 \n
 * Description: A very simple controller script that currently will only 
 *              instantiate a copy of the victory screen when another object
 *              collides with this one.
 */
public class VictoryController : MonoBehaviour
{
    /** The world where the level is in */
    public World world = World.Space;
    /** The level to load when the player hits this controller */
    public Level nextLevel = Level.One;
    /** The victory screen prefab */
    public GameObject victoryScreen;
    /** Spawns a victory screen when an object collides with this */

	//TESTING PERSISTENT DATA
	private int count;
	public Text countOut;

    bool hasWon = false;
	void Start() {
		count = PlayerPrefs.GetInt ("Count", 0);
	}
	//END TEST

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Instantiate(victoryScreen);
        //Invoke("LoadNextLevel", 2.0f);

        //TESTING PERSISTENT DATA

        if (countOut != null)
        {
            Debug.Log("This is temporarily fixed.");
            countOut.text = "" + count;
        }
        else
            Debug.LogError("AnhQuan, fix this NullReference exception please");
        saveCheckpoint();

		//END TEST
            if (collision.gameObject.tag == "Player"){
            if(!hasWon)
                Instantiate(victoryScreen);
            Invoke("LoadNextLevel", 2.0f);
            hasWon = true;
        }
    }

    void LoadNextLevel()
    {
        LevelLoader.LoadLevel(world, nextLevel);
    }

	void saveCheckpoint()
	{
		//TESTING PERSISTENT DATA

		PlayerPrefs.SetInt("Count", count + 1); 

		//This function will write to disk potentially causing a small hiccup,
		//therefore it is not recommended to call during actual gameplay.
		// - Unity API Ref
		PlayerPrefs.Save ();

    }

}
