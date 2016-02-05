﻿using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour {

	int hitCount = 0;
	public int hitsBeforeRetreat = 3;
	private enum State {IDLE, AGGRAVATED, RETREAT};
	private State state;
	public float maxTimeAggravated = 3f;
	public DialogBox dialog;
	
	int aggravateFrameCounter = 0;
	
	// Use this for initialization
	void Start () {
		state = State.IDLE;
	}
	
	void Update () {
		if (state == State.IDLE)
		{
			this.GetComponent<Animator>().SetInteger("Animation", 0);
		}
		else if (state == State.AGGRAVATED)
		{
			this.GetComponent<Animator>().SetInteger("Animation", 1);
			state = State.IDLE;
		}
		else if (state == State.RETREAT)
		{
			this.GetComponent<Animator>().SetInteger("Animation", 2);
			this.GetComponent<Platform>().enabled = true;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (state == State.IDLE) {
			hitCount++;
			switch (hitCount)
			{	
				case 2:
				dialog.SetText("Stop that! You better not hit me (1) more time(s)!");
				break;
				
				case 3:
				dialog.SetText("That's it! You've hit me a total of (3) times! I'm leaving!");
				break;
			}
			dialog.gameObject.SetActive(true);
			//Debug.Log ("I GOT HIT " + hitCount + " times");
			if (hitCount < hitsBeforeRetreat)
				state = State.AGGRAVATED;
			else
				state = State.RETREAT;
		}
	}
	
}