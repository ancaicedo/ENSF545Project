﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPuzzleController : MonoBehaviour {

	public GameObject[] Balls = {null, null};
	private Vector3[] ToyPosition;
	private Quaternion[] ToyRotation;
	private Rigidbody ball;
	public float gravityMultiplier = 2f;

	// Remember the original positions of the blocks.
	void Start () 
	{
		
		ToyPosition = new Vector3[Balls.Length];
		ToyRotation = new Quaternion[Balls.Length];
		for (int ii = 0; ii < Balls.Length; ii++)
		{
			ToyPosition [ii] = Balls [ii].transform.position;
			ToyRotation [ii] = Balls [ii].transform.rotation;
		}

		ball = Balls[0].GetComponent<Rigidbody>();
	}

	// Return the blocks to their original position.
	void ResetBlocks()
	{
		if (ToyPosition.Length != Balls.Length) return;

		for (int ii = 0; ii < Balls.Length; ii++)
		{
			Balls [ii].transform.SetPositionAndRotation(ToyPosition[ii], ToyRotation[ii]);
			Rigidbody RB = (Rigidbody)Balls[ii].GetComponent(typeof(Rigidbody));
			if (RB)	RB.velocity = Vector3.zero;
			if (RB) RB.angularVelocity = Vector3.zero;
		}
	}

	void IncreaseWeight()
	{
		Debug.Log("Increase the mass of the first ball");
		ball.mass += 1;
	}

	void DecreaseWeight()
	{
		Debug.Log("Decrease the mass of the first ball");
		ball.mass -= 1;
	}

	// Update is called once per frame
	void Update () 
	{

		// Return to starting position?
		if (Input.GetKeyDown("space"))
		{
			ResetBlocks();
			return;
		}
        else if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
	}

	void FixedUpdate()
	{
		ball.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);
	}
}
