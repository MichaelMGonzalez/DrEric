﻿using UnityEngine;
using System.Collections;

public class ExplodingCollider : Chaser {

    public float explodingmultiplier = 1;

    //PIDController pid;
    // Use this for initialization
    void newStart ()
    {
        //pid = GetComponent<PIDController>();
        base.Start();
    }
	
	// Update is called once per frame
	void newUpdate () {
        base.Update();
	}

    void newOnCollisionEnter2D(Collision2D c)
    {
        Debug.Log(gameObject);
        //base.OnCollisionEnter2D(c);
        Transform otherTrans = c.gameObject.GetComponent<Transform>();
        if (otherTrans != base.pid.destinationTransform)
        {
            if (c.gameObject.name != Names.DRERIC || pid.destinationTransform.gameObject.name != Names.PLAYERHOLDER)
                return;
        }
        Vector3 otherPos = otherTrans.position;
        Vector3 drEricImpluse = restDistance * (otherPos - transform.position).normalized;
        c.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(drEricImpluse.x * explodingmultiplier, drEricImpluse.y * explodingmultiplier), ForceMode2D.Impulse);
        
        Destroy(this.gameObject);
    }
}
