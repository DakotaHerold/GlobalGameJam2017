﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController3D : MonoBehaviour {

    public float speed = 1.0f;
    public float gravity = 9.8f;
    public int playerNumber = 0; 

    //Rigidbody rigidBody;
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private string horizontalMovementAxis;
    private string verticalMovementAxis;

    public Vector3 GetCharacterVelocity()
    {
        return controller.velocity; 
    }


    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        //rigidBody = GetComponent<Rigidbody>();

        horizontalMovementAxis = "Horizontal" + playerNumber;
        verticalMovementAxis = "Vertical" + playerNumber; 
 	}
	
	// Update is called once per frame
	void Update () {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis(horizontalMovementAxis), 0, Input.GetAxis(verticalMovementAxis));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
